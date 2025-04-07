using aidusnFarm.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace aidusnFarm.inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [Header("物品提示框")]
        public ItemToolTip itemTooltip; 
        [Header("拖拽图片")]
        public Image DragItem;

        [Header("玩家背包")]
        [SerializeField] private GameObject bagUI;
        private bool bagOpened;

        [SerializeField] public SlotUI[] PlayerSlot;

        //E键切换高亮格子时的初始索引
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

            //监听E键切换背包索引并高亮
            //ChangeHightLightIndex();
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
        }/// <summary>
        /// 打开背包函数
        /// </summary>
        public void OpenBagUI()
        {
            bagOpened =! bagOpened;
            bagUI.SetActive(bagOpened);
        }
        /// <summary>
        /// 指定格子高亮显示
        /// </summary>
        /// <param name="index">高亮显示格子的索引</param>
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
        //    //这个函数只能用来切换底部背包栏
        //    //因为有个背包UI，所以切换索引要加一
        //    if (Input.GetKeyDown(KeyCode.E))
        //    {
        //        Debug.LogWarning(HightLightIndex + "按下了E键");
        //        if (PlayerSlot[HightLightIndex].itemAmount > 0)
        //        {
        //            UpdateSlotHightLight(HightLightIndex);
        //            HightLightIndex = (HightLightIndex + 1) % 10;
        //        }

        //    }
        //    if (Input.GetKeyDown(KeyCode.Q)) 
        //    {
        //        Debug.LogWarning(HightLightIndex + "按下了Q键");
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
