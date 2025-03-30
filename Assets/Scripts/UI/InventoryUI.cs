using aidusnFarm.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aidusnFarm.inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [Header("玩家背包")]
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
        {//给每一个格子一个序号
            for (int i = 0; i < PlayerSlot.Length; i++)
            {
                PlayerSlot[i].slotIndex = i;
            }
            //获取当前背包的打开状态
            bagOpened = bagUI.activeInHierarchy;
        }
        private void Update()
        {
            //监控背包的开关
            if (Input.GetKeyDown(KeyCode.Tab)) 
            {
                OpenBagUI();
            }
        }
        private void OnUpdateInventoryUI(InventoryLocation location, List<InventoryItem> list)
        {
            switch (location)
            {
                case InventoryLocation.玩家:
                    for(int i = 0; i < PlayerSlot.Length; i++)
                    {
                        if (list[i].itemAmount > 0)
                        {
                            var item = InventoryManager.Instance.GetItemDetails(list[i].itemID);
                            PlayerSlot[i].UpdateSlot(item,list[i].itemAmount);
                        }
                        //如果数量等于或小于0，则更新为空格子
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
