using TMPro;
using UnityEngine.UI;
using UnityEngine;
namespace aidusnFarm.inventory
{
    public class SlotUI : MonoBehaviour
    {
        [Header("�����ȡ")]
        //������Ʒ��ͼ��
        [SerializeField] private Image slotImage;
        //������Ʒ������
        [SerializeField] private TextMeshProUGUI amountText;
        //������Ʒ�ĸ�����ʾ
        [SerializeField] private Image slotHightlight;
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
    }


}
