using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemEditor : EditorWindow
{


    //定义数据库
    private ItemDataList_SO dataBase;
    //定义物品详细列表
    private List<ItemDetails> itemList = new List<ItemDetails>();
    //获取列表模板
    private VisualTreeAsset itemRowTemplate;
    //获取右侧ItemDetails页面
    private ScrollView ItemDetailsSection;
    //当前物品信息
    private ItemDetails activeItem;

    //默认预览图标
    private Sprite defaultIcon;
    //图标预览信息
    private VisualElement iconPreview;
    //获取左侧ItemListView列表
    private ListView itemListView;
    //获取左侧内容框Row1的右侧ItemIcon
    private ObjectField Row1RightIcon;
    //设置unity工具选项卡
    [MenuItem("Aidsun/ItemEditor")]
    //添加项目编辑器
    public static void ShowExample()
    {
        ItemEditor wnd = GetWindow<ItemEditor>();
        wnd.titleContent = new GUIContent("ItemEditor");
    }
    //绘制GUI
    public void CreateGUI()
    {
        //定义根节点
        VisualElement root = rootVisualElement;

        //导入UXML数据
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/UI Builder/ItemEditor.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);

        //拿到模板数据
        itemRowTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/UI Builder/ItemRowTemplate.uxml");

        //拿到默认图标
        defaultIcon = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/M Studio/Art/Items/Icons/icon_M.png");

        //变量赋值
        //获取左边ItemList列表
        itemListView = root.Q<VisualElement>("ItemList").Q<ListView>("ListView");
        //获取右边ItemDetails内容框
        ItemDetailsSection = root.Q<ScrollView>("ItemDetails");
        //获取右边内容框的图标预览图
        iconPreview = ItemDetailsSection.Q<VisualElement>("Row1").Q<VisualElement>("Container").Q<VisualElement>("IconPreview");
        //左侧内容框Row1的右侧ItemIcon
        Row1RightIcon = ItemDetailsSection.Q<VisualElement>("Row1").Q<VisualElement>("Container").Q<VisualElement>("General").Q<ObjectField>("ItemIcon");

        //获取添加按钮
        root.Q<Button>("AddButton").clicked += OnAddItemClicked;
        root.Q<Button>("DeleteButton").clicked += OnDeleteItemClicked;

        //加载数据
        LoadDataBase();
        //生成列表   
        GenerateListView();

    }

    //删除事件
    private void OnDeleteItemClicked()
    {
        itemList.Remove(activeItem);
        itemListView.Rebuild();
        ItemDetailsSection.visible = false;
    }
    //增加事件
    private void OnAddItemClicked()
    {
        ItemDetails newItem = new ItemDetails();
        newItem.itemName = "New Name";
        newItem.itemID = 1001 + itemList.Count;
        itemList.Add(newItem);
        itemListView.Rebuild();
    }

    private void LoadDataBase()
    {
        var dataArray = AssetDatabase.FindAssets("ItemDataList_SO");
        if (dataArray.Length > 1)
        {
            var path = AssetDatabase.GUIDToAssetPath(dataArray[0]);
            dataBase = AssetDatabase.LoadAssetAtPath(path, typeof(ItemDataList_SO)) as ItemDataList_SO;
        }
        itemList = dataBase.itemDetailsList;
        //如果不标记则无法保存数据
        EditorUtility.SetDirty(dataBase);
    }
    //生成item列表
    private void GenerateListView()
    {
       //清楚itemListView的数据树缓存
        Func<VisualElement> makeItem = () => itemRowTemplate.CloneTree();
        Action<VisualElement, int> bindItem = (e, i) =>
        {
            if (i < itemList.Count)
            {
                if (itemList[i].itemIcon != null)
                {
                    e.Q<VisualElement>("Icon").style.backgroundImage = itemList[i].itemIcon.texture ;
                }
                else
                {
                    e.Q<VisualElement>("Icon").style.backgroundImage = defaultIcon.texture;
                }
                e.Q<Label>("Name").text = itemList[i] == null ? "Name Is Null" : itemList[i].itemName;
            }
        };
        //固定itemListView的高度
        itemListView.fixedItemHeight = 60;
        //定义itemListView的数据源
        itemListView.itemsSource = itemList;
        //生成itemListView的数据树
        itemListView.makeItem = makeItem;
        //itemListView的数据绑定
        itemListView.bindItem = bindItem;
        //被选中的List执行OnListSelectionChange函数
        itemListView.selectionChanged += OnListSelectionChange;
        //未被选中的itemList的itemDetails不可见
        ItemDetailsSection.visible = false;
    }

    private void OnListSelectionChange(IEnumerable<object> selectedItem)
    {
        //设置活动item为ItemList的第一个
        activeItem = (ItemDetails)selectedItem.First();
        //获取ItemDetails
        GetItemDetails();
        //设置选中的itemList对应的itemDetails可见
        ItemDetailsSection.visible = true;
    }

    //设置ItemDetails的各项参数
    private void GetItemDetails()
    {
        //记录数据
        ItemDetailsSection.MarkDirtyRepaint();

        //连接ItemID
        ItemDetailsSection.Q<IntegerField>("ItemID").value = activeItem.itemID;
        ItemDetailsSection.Q<IntegerField>("ItemID").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemID = evt.newValue;
            itemListView.Rebuild();

        });
        //连接ItemName
        ItemDetailsSection.Q<TextField>("ItemName").value = activeItem.itemName;
        ItemDetailsSection.Q<TextField>("ItemName").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemName = evt.newValue;
            itemListView.Rebuild();
        });

        //连接图标
        //Row1左侧的图标
        iconPreview.style.backgroundImage = activeItem.itemIcon == null ? defaultIcon.texture : activeItem.itemIcon.texture;
        //Row1右侧的Sprite选择框
        Row1RightIcon.value = activeItem.itemIcon;
        //Row1右侧的Sprite选择框的回调函数
        Row1RightIcon.RegisterValueChangedCallback(evt => {
        
            Sprite newIcon  = evt.newValue as Sprite;
            activeItem.itemIcon = newIcon;

            iconPreview.style.backgroundImage = newIcon == null ? defaultIcon.texture : newIcon.texture;
            itemListView.Rebuild();
        });

        //连接世界图标
        ItemDetailsSection.Q<ObjectField>("ItemSprite").value = activeItem.itemOnWorldSprite;
        ItemDetailsSection.Q<ObjectField>("ItemSprite").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemOnWorldSprite = (Sprite)evt.newValue;
        });

        //连接物品类型
        ItemDetailsSection.Q<EnumField>("ItemType").Init(activeItem.itemType);
        ItemDetailsSection.Q<EnumField>("ItemType").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemType = (ItemType)evt.newValue;
        });

        //连接描述
        ItemDetailsSection.Q<TextField>("Description").value = activeItem.itemDescription;
        ItemDetailsSection.Q<TextField>("Description").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemDescription = evt.newValue;
            itemListView.Rebuild();

        });
        //连接使用范围
        ItemDetailsSection.Q<IntegerField>("UseRadius").value = activeItem.itemUseRadius;
        ItemDetailsSection.Q<IntegerField>("UseRadius").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemUseRadius = evt.newValue;
            itemListView.Rebuild();
        });
        //连接能否被拾起
        ItemDetailsSection.Q<Toggle>("CanPickedUp").value = activeItem.canPickedup;
        ItemDetailsSection.Q<Toggle>("CanPickedUp").RegisterValueChangedCallback(evt =>
        {
            activeItem.canPickedup = evt.newValue;
            itemListView.Rebuild();
        });
        //连接能否被丢弃
        ItemDetailsSection.Q<Toggle>("CanDropped").value = activeItem.canDropped;
        ItemDetailsSection.Q<Toggle>("CanDropped").RegisterValueChangedCallback(evt =>
        {
            activeItem.canDropped = evt.newValue;
            itemListView.Rebuild();
        });
        //连接能否被举起
        ItemDetailsSection.Q<Toggle>("CanCarried").value = activeItem.canCarried;
        ItemDetailsSection.Q<Toggle>("CanCarried").RegisterValueChangedCallback(evt =>
        {
            activeItem.canCarried = evt.newValue;
            itemListView.Rebuild();
        });
        //连接物品价格
        ItemDetailsSection.Q<IntegerField>("ItemPrice").value = activeItem.itemPrice;
        ItemDetailsSection.Q<IntegerField>("ItemPrice").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemPrice = evt.newValue;
            itemListView.Rebuild();
        });
        //连接物品售卖折扣率
        ItemDetailsSection.Q<Slider>("SellPerecentage").value = activeItem.sellPercentage;
        ItemDetailsSection.Q<Slider>("SellPerecentage").RegisterValueChangedCallback(evt =>
        {
            activeItem.sellPercentage = evt.newValue;
            itemListView.Rebuild();
        });
    }
}