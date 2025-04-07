using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemToolTip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI typeText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private GameObject bottomPart;

    public void SetupTooltip(ItemDetails itemDetails,SlotType slotType)
    {
        nameText.text = itemDetails.itemName;
        typeText.text = itemDetails.itemType.ToString();
        descriptionText.text = itemDetails.itemDescription;

        if(itemDetails.itemType == ItemType.���� || itemDetails.itemType == ItemType.��Ʒ|| itemDetails.itemType == ItemType.�Ҿ�)
        {
            bottomPart.SetActive(true);
            var price = itemDetails.itemPrice;
            if(slotType == SlotType.����)
            {
                price = (int)(price * itemDetails.sellPercentage);
            }
            valueText.text = price.ToString();
        }
        else
        {
            bottomPart.SetActive(false);
        }
        //ǿ��ˢ����Ʒ��ʾ��
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }
}
