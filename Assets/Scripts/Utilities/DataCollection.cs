using UnityEngine;

//这个脚本为了显示整个系统中的物品详情
[System.Serializable]
public class ItemDetails
{
    //物品ID
    public int itemID;
    //物品名称
    public string itemName;
    //物品类型
    public ItemType itemType; 
    //物品图标
    public Sprite itemIcon;
    //真实物品图标
    public Sprite itemOnWorldSprite;
    //物品描述
    public string itemDescription;
    //物品使用范围
    public int itemUseRadius;
    //物品能否被拿起
    public bool canPickedup;
    //物品能否被丢下
    public bool canDropped;
    //物品能否被举着
    public bool canCarried;
    //物品的价格
    public int itemPrice;
    //物品买卖的价格百分比
    [Range(0, 1)]
    public float sellPercentage;
}
[System.Serializable]
//背包数据结构
public struct InventoryItem
{
    public int itemID;
    public int itemAmount;
}
[System.Serializable]
public class AnimatorType
{
    public PartType partType;
    public PartName partName;
    public AnimatorOverrideController overrideController;
}