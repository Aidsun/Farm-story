using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����ű�ʵ�������ײ��ʱ�����ɼ����ڸı�͸���ȵ�Ч��
public class TriggerItemFader : MonoBehaviour
{
    //���������������ײʱ����FadeOut()����͸����
    private void OnTriggerEnter2D(Collider2D other)
    {
        //��ȡ��ײ��������
        ItemFader[] faders =other.GetComponentsInChildren<ItemFader>();
        //��֤�����岻Ϊ��
        if(faders.Length > 0)
        {
            //ѭ���������������������FadeOut����
            foreach(var item in faders)
            {
                item.FadeOut();
            }
        }
        //û������������
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
        //û������������
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
