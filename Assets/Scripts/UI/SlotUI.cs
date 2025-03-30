using TMPro;
using UnityEngine.UI;
using UnityEngine;
namespace aidusnFarm.inventory
{
    public class SlotUI : MonoBehaviour
    {
        [Header("组件获取")]
        //单个物品的图标
        [SerializeField] private Image slotImage;
        //单个物品的数量
        [SerializeField] private TextMeshProUGUI amountText;
        //单个物品的高亮显示
        [SerializeField] private Image slotHightlight;
        //单个物品为空时不能被选择
        [SerializeField] private Button button;
        [Header("格子类型")]
        public SlotType slotType;
        [Header("判断是否被选中")]
        public bool isSelected;
        //物品信息
        public ItemDetails itemDetails;
        //物品数量
        public int itemAmount;
        //格子序号
        public int slotIndex;
        private void Start()
        {
            isSelected = false;
            if (itemDetails.itemID == 0)
            {
                UpdateEmptySlot();
            }
        }



        /// <summary>
        /// 更新格子UI和信息
        /// </summary>
        /// <param name="item">物品信息</param>
        /// <param name="amount">持有数量</param>
        public void UpdateSlot(ItemDetails item, int amount)
        {
            itemDetails = item;
            slotImage.sprite = item.itemIcon;
            slotImage.enabled = true;
            itemAmount = amount;
            amountText.text = amount.ToString();
            button.interactable = true;
        }

        /// <summary>
        /// 讲Slot格子更新为空
        /// </summary>
        public void UpdateEmptySlot()
        {
            if (isSelected)
            {
                isSelected = false;
            }
            slotImage.enabled = false;
            amountText.text = string.Empty;
            button.interactable = false;
        }
    }


}
