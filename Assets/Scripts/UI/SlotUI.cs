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
        [Header("�����ȡ")]
        //������Ʒ��ͼ��
        [SerializeField] private Image slotImage;
        //������Ʒ������
        [SerializeField] private TextMeshProUGUI amountText;
        //������Ʒ�ĸ�����ʾ
        [SerializeField] public Image slotHightlight;
        //������ƷΪ��ʱ���ܱ�ѡ��
        [SerializeField] private Button button;
        [Header("��������")]
        public SlotType slotType;
        [Header("�ж��Ƿ�ѡ��")]
        public bool isSelected;
        //��Ʒ��Ϣ
        public ItemDetails itemDetails;
        //��Ʒ����
        public int itemAmount;
        //�������
        public int slotIndex;
        //������
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
        /// ���¸���UI����Ϣ
        /// </summary>
        /// <param name="item">��Ʒ��Ϣ</param>
        /// <param name="amount">��������</param>
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
        /// ��Slot���Ӹ���Ϊ��
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
        /// ���Ӹ�������¼�
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerClick(PointerEventData eventData)
        {
            if (itemAmount == 0) return;
            isSelected = !isSelected;
            slotHightlight.gameObject.SetActive(isSelected);
            inventoryUI.UpdateSlotHightLight(slotIndex);
        }

        //��ʼ��ק
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
        //��ק��
        public void OnDrag(PointerEventData eventData)
        {
            inventoryUI.DragItem.transform.position = Input.mousePosition;
        }
        //������ק
        public void OnEndDrag(PointerEventData eventData)
        {
            inventoryUI.DragItem.enabled = false;
            //���߼��
            if (eventData.pointerCurrentRaycast.gameObject != null)
            {
                //���������slot�ͷ��ؿ�
                if (eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotUI>() == null)
                    return;

                //����Ŀ��slot
                var targetSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotUI>();
                int targetIndex = targetSlot.slotIndex;

                if (slotType == SlotType.���� && targetSlot.slotType == SlotType.����)
                {
                    InventoryManager.Instance.SwapItem(slotIndex, targetIndex);
                }
                //������и�����ʾ 
                inventoryUI.UpdateSlotHightLight(-1);

            }
            //����Ʒ���ڵ���
            //else
            //{
            //    //����Ӧ��ͼ�ϵ�����
            //    if (itemDetails.canDropped)
            //    {
            //        var pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            //        EventHandler.CallInstantiateItemInScene(itemDetails.itemID, pos);
            //    }
            //}
        }
    }


}
