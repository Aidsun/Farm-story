using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemEditor : EditorWindow
{


    //�������ݿ�
    private ItemDataList_SO dataBase;
    //������Ʒ��ϸ�б�
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
    //��ȡ������ݿ�Row1���Ҳ�ItemIcon
    private ObjectField Row1RightIcon;
    //����unity����ѡ�
    [MenuItem("Aidsun/ItemEditor")]
    //�����Ŀ�༭��
    public static void ShowExample()
    {
        ItemEditor wnd = GetWindow<ItemEditor>();
        wnd.titleContent = new GUIContent("ItemEditor");
    }
    //����GUI
    public void CreateGUI()
    {
        //������ڵ�
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
        //��ȡ���ItemList�б�
        itemListView = root.Q<VisualElement>("ItemList").Q<ListView>("ListView");
        //��ȡ�ұ�ItemDetails���ݿ�
        ItemDetailsSection = root.Q<ScrollView>("ItemDetails");
        //��ȡ�ұ����ݿ��ͼ��Ԥ��ͼ
        iconPreview = ItemDetailsSection.Q<VisualElement>("Row1").Q<VisualElement>("Container").Q<VisualElement>("IconPreview");
        //������ݿ�Row1���Ҳ�ItemIcon
        Row1RightIcon = ItemDetailsSection.Q<VisualElement>("Row1").Q<VisualElement>("Container").Q<VisualElement>("General").Q<ObjectField>("ItemIcon");

        //��ȡ��Ӱ�ť
        root.Q<Button>("AddButton").clicked += OnAddItemClicked;
        root.Q<Button>("DeleteButton").clicked += OnDeleteItemClicked;

        //��������
        LoadDataBase();
        //�����б�   
        GenerateListView();

    }

    //ɾ���¼�
    private void OnDeleteItemClicked()
    {
        itemList.Remove(activeItem);
        itemListView.Rebuild();
        ItemDetailsSection.visible = false;
    }
    //�����¼�
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
        //�����������޷���������
        EditorUtility.SetDirty(dataBase);
    }
    //����item�б�
    private void GenerateListView()
    {
       //���itemListView������������
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
        //�̶�itemListView�ĸ߶�
        itemListView.fixedItemHeight = 60;
        //����itemListView������Դ
        itemListView.itemsSource = itemList;
        //����itemListView��������
        itemListView.makeItem = makeItem;
        //itemListView�����ݰ�
        itemListView.bindItem = bindItem;
        //��ѡ�е�Listִ��OnListSelectionChange����
        itemListView.selectionChanged += OnListSelectionChange;
        //δ��ѡ�е�itemList��itemDetails���ɼ�
        ItemDetailsSection.visible = false;
    }

    private void OnListSelectionChange(IEnumerable<object> selectedItem)
    {
        //���ûitemΪItemList�ĵ�һ��
        activeItem = (ItemDetails)selectedItem.First();
        //��ȡItemDetails
        GetItemDetails();
        //����ѡ�е�itemList��Ӧ��itemDetails�ɼ�
        ItemDetailsSection.visible = true;
    }

    //����ItemDetails�ĸ������
    private void GetItemDetails()
    {
        //��¼����
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
        //Row1����ͼ��
        iconPreview.style.backgroundImage = activeItem.itemIcon == null ? defaultIcon.texture : activeItem.itemIcon.texture;
        //Row1�Ҳ��Spriteѡ���
        Row1RightIcon.value = activeItem.itemIcon;
        //Row1�Ҳ��Spriteѡ���Ļص�����
        Row1RightIcon.RegisterValueChangedCallback(evt => {
        
            Sprite newIcon  = evt.newValue as Sprite;
            activeItem.itemIcon = newIcon;

            iconPreview.style.backgroundImage = newIcon == null ? defaultIcon.texture : newIcon.texture;
            itemListView.Rebuild();
        });

        //��������ͼ��
        ItemDetailsSection.Q<ObjectField>("ItemSprite").value = activeItem.itemOnWorldSprite;
        ItemDetailsSection.Q<ObjectField>("ItemSprite").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemOnWorldSprite = (Sprite)evt.newValue;
        });

        //������Ʒ����
        ItemDetailsSection.Q<EnumField>("ItemType").Init(activeItem.itemType);
        ItemDetailsSection.Q<EnumField>("ItemType").RegisterValueChangedCallback(evt =>
        {
            activeItem.itemType = (ItemType)evt.newValue;
        });

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