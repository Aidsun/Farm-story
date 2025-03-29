using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//���ɵ�Item��Ϣ     
namespace aidusnFarm.Inventory
{
    public class Item : MonoBehaviour
    {
        //������Ʒ��ID
        public int itemID;
        //������Ʒ����Ⱦͼ��
        private SpriteRenderer spriteRenderer;
        //������Ʒ����ϸ��Ϣ
        public ItemDetails itemDetails;
        //������Ʒ����ײ��
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
        //��Ʒ��ʼ��
        public void Init(int ID)
        {
            itemID = ID;
            itemDetails = InventoryManager.Instance.GetItemDetails(itemID);

            if (itemDetails != null)
            {//ȷ����ͼƬ��ʾ���������������ͼ�����ʾ����ͼ�꣬���û�о���ʾitemIcon��
                spriteRenderer.sprite = itemDetails.itemOnWorldSprite != null ? itemDetails.itemOnWorldSprite : itemDetails.itemIcon;
                //�޸���ײ��ߴ�
                Vector2 newSize = new Vector2(spriteRenderer.sprite.bounds.size.x, spriteRenderer.sprite.bounds.size.y);
                coll.size = newSize;
                coll.offset = new Vector2(0, spriteRenderer.sprite.bounds.center.y);
            }
        }
    }
}
