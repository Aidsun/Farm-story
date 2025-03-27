using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEditor.Search;

public class ItemEditor : EditorWindow
{

    private ItemDataList_SO dataBase;
    private List<ItemDetails> itemList = new List<ItemDetails>();
    //��ȡ�б�ģ��
    private VisualTreeAsset itemRowTemplate;
    //��ȡ�Ҳ�ItemDetailsҳ��
    private ScrollView ItemDetailsSection;
    //��ǰ��Ʒ��Ϣ
    private ItemDetails activeItem;

    //Ĭ��Ԥ��ͼ��
    private Sprite defaultIcon;
    //ͼ��Ԥ����Ϣ
    private VisualElement iconPreview;
    //��ȡ���ItemListView�б�
    private ListView itemListView;

    [MenuItem("Aidsun/ItemEditor")]
    public static void ShowExample()
    {
        ItemEditor wnd = GetWindow<ItemEditor>();
        wnd.titleContent = new GUIContent("ItemEditor");
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;
        //����UXML����
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/UI Builder/ItemEditor.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);

        //�õ�ģ������
        itemRowTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/UI Builder/ItemRowTemplate.uxml");

        //�õ�Ĭ��ͼ��
        defaultIcon = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/M Studio/Art/Items/Icons/icon_M.png");

        //������ֵ
        itemListView = root.Q<VisualElement>("ItemList").Q<ListView>("ListView");
        ItemDetailsSection = root.Q<ScrollView>("ItemDetails");
        iconPreview = ItemDetailsSection.Q<VisualElement>("IconPreview");
        //��������
        LoadDataBase();
        //�����б�   
        GenerateListView();

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
        //�����������޷���������
        EditorUtility.SetDirty(dataBase);
        //Debug.LogWarning(itemList[0].itemID); 
    }
    //����item�б�
    private void GenerateListView()
    {
        Func<VisualElement> makeItem = () => itemRowTemplate.CloneTree();
        Action<VisualElement, int> bindItem = (e, i) =>
        {
            if (i < itemList.Count)
            {
                if (itemList[i].itemIcon != null)
                {
                    e.Q<VisualElement>("Icon");
                }
                e.Q<Label>("Name").text = itemList[i] == null ? "Name Is Null" : itemList[i].itemName;

            }
        };
        itemListView.fixedItemHeight = 60;
        itemListView.itemsSource = itemList;
        itemListView.makeItem = makeItem;
        itemListView.bindItem = bindItem;

        itemListView.onSelectionChange += OnListSelectionChange;
        ItemDetailsSection.visible = false;
    }

    private void OnListSelectionChange(IEnumerable<object> selectedItem)
    {
        activeItem = (ItemDetails)selectedItem.First();
        GetItemDetails();
        ItemDetailsSection.visible = true;
    }

    //����ItemDetails�ĸ������
    private void GetItemDetails()
    {
        ItemDetailsSection.MarkDirtyRepaint();

        //����ItemID
        ItemDetailsSection.Q<IntegerField>("ItemID").value = activeItem.itemID;
        ItemDetailsSection.Q<IntegerField>("ItemID").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemID = evt.newValue;
            itemListView.Rebuild();

        });
        //����ItemName
        ItemDetailsSection.Q<TextField>("ItemName").value = activeItem.itemName;
        ItemDetailsSection.Q<TextField>("ItemName").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemName = evt.newValue;
            itemListView.Rebuild();
        });
        //����ͼ��
        iconPreview.style.backgroundImage = activeItem.itemIcon == null ? defaultIcon.texture : activeItem.itemIcon.texture;
        var iconField = ItemDetailsSection.Q<ObjectField>("ItemIcon");
        {
            if (iconField == null)
            {
                var generalSection = ItemDetailsSection.Q<VisualElement>("General");
                iconField = generalSection?.Q<ObjectField>("ItemIcon");
            }
            else
            {
                ItemDetailsSection.Q<ObjectField>("ItemIcon").value = activeItem.itemIcon;
                ItemDetailsSection.Q<ObjectField>("ItemIcon").RegisterValueChangedCallback(evt =>
                {
                    Sprite newIcon = evt.newValue as Sprite;
                    activeItem.itemIcon = newIcon;
                    iconPreview.style.backgroundImage = newIcon == null ? defaultIcon.texture : newIcon.texture;
                    itemListView.Rebuild();
                }
                );
            }
        }
        //����ItemType
        //ItemDetailsSection.Q<EnumField>("ItemType").value = activeItem.itemType;
        //ItemDetailsSection.Q<EnumField>("ItemType").RegisterValueChangedCallback(evt =>
        //{
        //    activeItem.itemType = evt.newValue;
        //    itemListView.Rebuild();

        //});


        //��������
        ItemDetailsSection.Q<TextField>("Description").value = activeItem.itemDescription;
        ItemDetailsSection.Q<TextField>("Description").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemDescription = evt.newValue;
            itemListView.Rebuild();

        });
        //����ʹ�÷�Χ
        ItemDetailsSection.Q<IntegerField>("UseRadius").value = activeItem.itemUseRadius;
        ItemDetailsSection.Q<IntegerField>("UseRadius").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemUseRadius = evt.newValue;
            itemListView.Rebuild();
        });
        //�����ܷ�ʰ��
        ItemDetailsSection.Q<Toggle>("CanPickedUp").value = activeItem.canPickedup;
        ItemDetailsSection.Q<Toggle>("CanPickedUp").RegisterValueChangedCallback(evt =>
        {
            activeItem.canPickedup = evt.newValue;
            itemListView.Rebuild();
        });
        //�����ܷ񱻶���
        ItemDetailsSection.Q<Toggle>("CanDropped").value = activeItem.canDropped;
        ItemDetailsSection.Q<Toggle>("CanDropped").RegisterValueChangedCallback(evt =>
        {
            activeItem.canDropped = evt.newValue;
            itemListView.Rebuild();
        });
        //�����ܷ񱻾���
        ItemDetailsSection.Q<Toggle>("CanCarried").value = activeItem.canCarried;
        ItemDetailsSection.Q<Toggle>("CanCarried").RegisterValueChangedCallback(evt =>
        {
            activeItem.canCarried = evt.newValue;
            itemListView.Rebuild();
        });
        //������Ʒ�۸�
        ItemDetailsSection.Q<IntegerField>("ItemPrice").value = activeItem.itemPrice;
        ItemDetailsSection.Q<IntegerField>("ItemPrice").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemPrice = evt.newValue;
            itemListView.Rebuild();
        });
        //������Ʒ�����ۿ���
        ItemDetailsSection.Q<Slider>("SellPerecentage").value = activeItem.sellPercentage;
        ItemDetailsSection.Q<Slider>("SellPerecentage").RegisterValueChangedCallback(evt =>
        {
            activeItem.sellPercentage = evt.newValue;
            itemListView.Rebuild();
        });
    }
}