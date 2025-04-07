using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor.Profiling;
using aidusnFarm.Inventory;
namespace aidusnFarm.inventory
{
    public class SlotUI : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Header("组件获取")]
        //单个物品的图标
        [SerializeField] private Image slotImage;
        //单个物品的数量
        [SerializeField] private TextMeshProUGUI amountText;
        //单个物品的高亮显示
        [SerializeField] public Image slotHightlight;
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
        //父物体
        private InventoryUI inventoryUI => GetComponentInParent<InventoryUI>();
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
        /// <summary>
        /// 格子高亮点击事件
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerClick(PointerEventData eventData)
        {
            if (itemAmount == 0) return;
            isSelected = !isSelected;
            slotHightlight.gameObject.SetActive(isSelected);
            inventoryUI.UpdateSlotHightLight(slotIndex);
        }

        //开始拖拽
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (itemAmount != 0)
            {
                inventoryUI.DragItem.enabled = true;
                inventoryUI.DragItem.sprite = slotImage.sprite;
                inventoryUI.DragItem.SetNativeSize();
                isSelected = true;
                inventoryUI.UpdateSlotHightLight(slotIndex);
            }
        }
        //拖拽中
        public void OnDrag(PointerEventData eventData)
        {
            inventoryUI.DragItem.transform.position = Input.mousePosition;
        }
        //结束拖拽
        public void OnEndDrag(PointerEventData eventData)
        {
            inventoryUI.DragItem.enabled = false;
            //射线检测
            if (eventData.pointerCurrentRaycast.gameObject != null)
            {
                //如果对象不是slot就返回空
                if (eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotUI>() == null)
                    return;

                //定义目标slot
                var targetSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotUI>();
                int targetIndex = targetSlot.slotIndex;

                if (slotType == SlotType.背包 && targetSlot.slotType == SlotType.背包)
                {
                    InventoryManager.Instance.SwapItem(slotIndex, targetIndex);
                }
                //清空所有高亮显示 
                inventoryUI.UpdateSlotHightLight(-1);

            }
            //把物品仍在地上
            //else
            //{
            //    //鼠标对应地图上的坐标
            //    if (itemDetails.canDropped)
            //    {
            //        var pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            //        EventHandler.CallInstantiateItemInScene(itemDetails.itemID, pos);
            //    }
            //}
        }
    }


}
