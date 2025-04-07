using aidusnFarm.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace aidusnFarm.inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [Header("��Ʒ��ʾ��")]
        public ItemToolTip itemTooltip; 
        [Header("��קͼƬ")]
        public Image DragItem;

        [Header("��ұ���")]
        [SerializeField] private GameObject bagUI;
        private bool bagOpened;

        [SerializeField] public SlotUI[] PlayerSlot;

        //E���л���������ʱ�ĳ�ʼ����
        //private int HightLightIndex = 0;

        private void OnEnable()
        {
            EventHandler.UpdateInventoryUI += OnUpdateInventoryUI;
        }
        private void OnDisable()
        {
            EventHandler.UpdateInventoryUI -= OnUpdateInventoryUI;
        }
        private void Start()
        {//��ÿһ������һ�����
            for (int i = 0; i < PlayerSlot.Length; i++)
            {
                PlayerSlot[i].slotIndex = i;
            }
            //��ȡ��ǰ�����Ĵ�״̬
            bagOpened = bagUI.activeInHierarchy;
        }
        private void Update()
        {
            //��ر����Ŀ���
            if (Input.GetKeyDown(KeyCode.Tab)) 
            {
                OpenBagUI();
            }

            //����E���л���������������
            //ChangeHightLightIndex();
        }
        private void OnUpdateInventoryUI(InventoryLocation location, List<InventoryItem> list)
        {
            switch (location)
            {
                case InventoryLocation.���:
                    for(int i = 0; i < PlayerSlot.Length; i++)
                    {
                        if (list[i].itemAmount > 0)
                        {
                            var item = InventoryManager.Instance.GetItemDetails(list[i].itemID);
                            PlayerSlot[i].UpdateSlot(item,list[i].itemAmount);
                        }
                        //����������ڻ�С��0�������Ϊ�ո���
                        else
                        {
                            PlayerSlot[i].UpdateEmptySlot();
                        }
                    }
                    break;
            }
        }/// <summary>
        /// �򿪱�������
        /// </summary>
        public void OpenBagUI()
        {
            bagOpened =! bagOpened;
            bagUI.SetActive(bagOpened);
        }
        /// <summary>
        /// ָ�����Ӹ�����ʾ
        /// </summary>
        /// <param name="index">������ʾ���ӵ�����</param>
        public void UpdateSlotHightLight(int index)
        {
            foreach(var slot in PlayerSlot)
            {
                if(slot.isSelected && slot.slotIndex == index)
                {
                    slot.slotHightlight.gameObject.SetActive(true);
                }
                else
                {
                    slot.isSelected = false;
                    slot.slotHightlight.gameObject.SetActive(false);
                }
            }
        }

        //private void ChangeHightLightIndex()
        //{
        //    //�������ֻ�������л��ײ�������
        //    //��Ϊ�и�����UI�������л�����Ҫ��һ
        //    if (Input.GetKeyDown(KeyCode.E))
        //    {
        //        Debug.LogWarning(HightLightIndex + "������E��");
        //        if (PlayerSlot[HightLightIndex].itemAmount > 0)
        //        {
        //            UpdateSlotHightLight(HightLightIndex);
        //            HightLightIndex = (HightLightIndex + 1) % 10;
        //        }

        //    }
        //    if (Input.GetKeyDown(KeyCode.Q)) 
        //    {
        //        Debug.LogWarning(HightLightIndex + "������Q��");
        //        if (PlayerSlot[HightLightIndex].itemAmount > 0)
        //        {
        //            UpdateSlotHightLight(HightLightIndex);
        //            HightLightIndex = (HightLightIndex - 1) % 10;
        //            if(HightLightIndex < 0)
        //            {
        //                HightLightIndex = 0;
        //            }
        //        }
        //    }
        //}
    }
}
