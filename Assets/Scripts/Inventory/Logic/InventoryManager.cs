using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aidusnFarm.Inventory
{

    //����������Ʒ�Ľű�����
    public class InventoryManager : Singleton<InventoryManager>
    {
        [Header("��Ʒ����")]
        public ItemDataList_SO itemDataList_SO;
        [Header("��������")]
        public InventoryBag_SO PlayerBag;

        private void Start()
        {
            EventHandler.CallUpdateInventoryUI(InventoryLocation.���, PlayerBag.itemList);
        }

        //����itemID�ҵ�item��Ϣ
        public ItemDetails GetItemDetails(int ID)
        {
            return itemDataList_SO.itemDetailsList.Find(item => item.itemID == ID);
        }

        //�����Ʒ
        public void AddItem(Item item,bool toDestory)
        {
            //�жϱ����Ƿ����и���Ʒ
            var index = GetItemIndexInBag(item.itemID);
            //�����Ʒ������
            AddItemAtIndex(item.itemID, index, 1);

            Debug.LogWarning(GetItemDetails(item.itemID).itemName+"("+GetItemDetails(item.itemID).itemID + ")"+ "ʰȡ�ɹ�");
            if (toDestory)
            {
                Destroy(item.gameObject);
            }
            //����UI
            EventHandler.CallUpdateInventoryUI(InventoryLocation.���, PlayerBag.itemList);
        }
/// <summary>
/// ��鱳���Ƿ��п�λ
/// </summary>
/// <returns></returns>
        //��鱳���Ƿ��п�λ
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
        /// �жϱ����Ƿ����и���Ʒ��
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
        /// ��ָ���������λ�������Ʒ
        /// </summary>
        /// <param name="ID">��ƷID</param>
        /// <param name="index">���</param>
        /// <param name="amount">����</param>
        private void AddItemAtIndex(int ID,int index,int amount)
        {
            //�����������û�������Ʒ��ͬʱ����������
            if(index == -1 && CheckBagCapacity())
            {
                //�����������ݴ���һ��������
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
            //�������������Ʒ
            else
            {
                int currentAmount = PlayerBag.itemList[index].itemAmount + amount;
                var item  = new InventoryItem { itemID = ID,itemAmount = currentAmount };
                PlayerBag.itemList[index] = item;
            }
        }
    }
} 
