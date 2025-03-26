using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这个脚本实现玩家碰撞树时，树干及树冠改变透明度的效果
public class TriggerItemFader : MonoBehaviour
{
    //触发器：当玩家碰撞时调用FadeOut()降低透明度
    private void OnTriggerEnter2D(Collider2D other)
    {
        //获取碰撞体子物体
        ItemFader[] faders =other.GetComponentsInChildren<ItemFader>();
        //保证子物体不为空
        if(faders.Length > 0)
        {
            //循环遍历调用子物体组件的FadeOut函数
            foreach(var item in faders)
            {
                item.FadeOut();
            }
        }
        //没有子物体的情况
        else
        {
            ItemFader fader = other.GetComponent<ItemFader>();
            if(fader != null)
            {
            fader.FadeOut();
            }
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        ItemFader[] faders = other.GetComponentsInChildren<ItemFader>();
        if (faders.Length > 0)
        {
            foreach (var item in faders)
            {
                item.FadeIn();
            }
        }
        //没有子物体的情况
        else
        {
            ItemFader fader = other.GetComponent<ItemFader>();
            if(fader != null)
            {
            fader.FadeIn();
            }
        }
    }
}
