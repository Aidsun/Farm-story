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

        if(itemDetails.itemType == ItemType.种子 || itemDetails.itemType == ItemType.商品|| itemDetails.itemType == ItemType.家具)
        {
            bottomPart.SetActive(true);
            var price = itemDetails.itemPrice;
            if(slotType == SlotType.背包)
            {
                price = (int)(price * itemDetails.sellPercentage);
            }
            valueText.text = price.ToString();
        }
        else
        {
            bottomPart.SetActive(false);
        }
        //强制刷新物品提示栏
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }
}
