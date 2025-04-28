using UnityEngine;

//����ű�Ϊ����ʾ����ϵͳ�е���Ʒ����
[System.Serializable]
public class ItemDetails
{
    //��ƷID
    public int itemID;
    //��Ʒ����
    public string itemName;
    //��Ʒ����
    public ItemType itemType; 
    //��Ʒͼ��
    public Sprite itemIcon;
    //��ʵ��Ʒͼ��
    public Sprite itemOnWorldSprite;
    //��Ʒ����
    public string itemDescription;
    //��Ʒʹ�÷�Χ
    public int itemUseRadius;
    //��Ʒ�ܷ�����
    public bool canPickedup;
    //��Ʒ�ܷ񱻶���
    public bool canDropped;
    //��Ʒ�ܷ񱻾���
    public bool canCarried;
    //��Ʒ�ļ۸�
    public int itemPrice;
    //��Ʒ�����ļ۸�ٷֱ�
    [Range(0, 1)]
    public float sellPercentage;
}
[System.Serializable]
//�������ݽṹ
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