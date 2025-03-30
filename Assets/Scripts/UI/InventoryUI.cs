using aidusnFarm.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aidusnFarm.inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [Header("��ұ���")]
        [SerializeField] private GameObject bagUI;
        private bool bagOpened;

        [SerializeField] public SlotUI[] PlayerSlot;

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
        }
        public void OpenBagUI()
        {
            bagOpened =! bagOpened;
            bagUI.SetActive(bagOpened);
        }
    }
}
