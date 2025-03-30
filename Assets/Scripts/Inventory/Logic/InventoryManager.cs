using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aidusnFarm.Inventory
{

    //管理所有物品的脚本代码
    public class InventoryManager : Singleton<InventoryManager>
    {
        [Header("物品数据")]
        public ItemDataList_SO itemDataList_SO;
        [Header("背包数据")]
        public InventoryBag_SO PlayerBag;

        private void Start()
        {
            EventHandler.CallUpdateInventoryUI(InventoryLocation.玩家, PlayerBag.itemList);
        }

        //利用itemID找到item信息
        public ItemDetails GetItemDetails(int ID)
        {
            return itemDataList_SO.itemDetailsList.Find(item => item.itemID == ID);
        }

        //添加物品
        public void AddItem(Item item,bool toDestory)
        {
            //判断背包是否已有该物品
            var index = GetItemIndexInBag(item.itemID);
            //添加物品到背包
            AddItemAtIndex(item.itemID, index, 1);

            Debug.LogWarning(GetItemDetails(item.itemID).itemName+"("+GetItemDetails(item.itemID).itemID + ")"+ "拾取成功");
            if (toDestory)
            {
                Destroy(item.gameObject);
            }
            //更新UI
            EventHandler.CallUpdateInventoryUI(InventoryLocation.玩家, PlayerBag.itemList);
        }
/// <summary>
/// 检查背包是否有空位
/// </summary>
/// <returns></returns>
        //检查背包是否有空位
        private bool CheckBagCapacity()
        {
            for (int i = 0; i < PlayerBag.itemList.Count; i++)
            {
                if (PlayerBag.itemList[i].itemID == 0)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 判断背包是否已有该物品，
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private int GetItemIndexInBag(int ID)
        {
            for (int i = 0; i < PlayerBag.itemList.Count; i++)
            {
                if (PlayerBag.itemList[i].itemID == ID)
                {
                    return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// 在指定背包序号位置添加物品
        /// </summary>
        /// <param name="ID">物品ID</param>
        /// <param name="index">序号</param>
        /// <param name="amount">数量</param>
        private void AddItemAtIndex(int ID,int index,int amount)
        {
            //如果背包里面没有这个物品，同时背包有容量
            if(index == -1 && CheckBagCapacity())
            {
                //根据已有数据创建一个新物体
                var item = new InventoryItem { itemID = ID,itemAmount = amount };
                for(int i = 0; i < PlayerBag.itemList.Count; i++)
                {
                    if (PlayerBag.itemList[i].itemID == 0)
                    {
                        PlayerBag.itemList[i] = item;
                        break;
                    }
                }
            }
            //背包里有这个物品
            else
            {
                int currentAmount = PlayerBag.itemList[index].itemAmount + amount;
                var item  = new InventoryItem { itemID = ID,itemAmount = currentAmount };
                PlayerBag.itemList[index] = item;
            }
        }
    }
} 
