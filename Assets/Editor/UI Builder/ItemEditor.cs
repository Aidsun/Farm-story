using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System;

public class ItemEditor : EditorWindow
{

    private ItemDataList_SO dataBase;
    private List<ItemDetails> itemList = new List<ItemDetails>();
    //��ȡ�б�ģ��
    private VisualTreeAsset itemRowTemplate;

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

        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/UI Builder/ItemEditor.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);
        //�õ�ģ������
        itemRowTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/UI Builder/ItemRowTemplate.uxml");
        itemListView = root.Q<VisualElement>("ItemList").Q<ListView>("ListView");
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
    }
}