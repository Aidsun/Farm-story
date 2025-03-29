using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//生成的Item信息     
namespace aidusnFarm.Inventory
{
    public class Item : MonoBehaviour
    {
        //生成物品的ID
        public int itemID;
        //生成物品的渲染图形
        private SpriteRenderer spriteRenderer;
        //生成物品的详细信息
        public ItemDetails itemDetails;
        //生成物品的碰撞体
        private BoxCollider2D coll;

        private void Awake()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            coll = GetComponent<BoxCollider2D>();
        }
        private void Start()
        {
            if (itemID != 0)
            {
                Init(itemID);
            }
        }
        //物品初始化
        public void Init(int ID)
        {
            itemID = ID;
            itemDetails = InventoryManager.Instance.GetItemDetails(itemID);

            if (itemDetails != null)
            {//确保有图片显示出来，如果有世界图标就显示世界图标，如果没有就显示itemIcon；
                spriteRenderer.sprite = itemDetails.itemOnWorldSprite != null ? itemDetails.itemOnWorldSprite : itemDetails.itemIcon;
                //修改碰撞体尺寸
                Vector2 newSize = new Vector2(spriteRenderer.sprite.bounds.size.x, spriteRenderer.sprite.bounds.size.y);
                coll.size = newSize;
                coll.offset = new Vector2(0, spriteRenderer.sprite.bounds.center.y);
            }
        }
    }
}
