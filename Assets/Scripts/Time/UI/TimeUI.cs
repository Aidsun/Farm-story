using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    [Tooltip("��ҹ��תͼƬ")]
    public RectTransform dayNightImage;
    [Tooltip("��׼ʱ�䷽��(һ���������4Сʱ)")]
    public RectTransform clockParent;
    [Tooltip("����ͼƬ")]
    public Image seasonImage;
    [Tooltip("�����ı�")]
    public TextMeshProUGUI dateText;
    [Tooltip("ʱ���ı�")]
    public TextMeshProUGUI timeText;
    [Tooltip("����ͼƬ����")]
    public Sprite[] seasonSprites;

    //��׼ʱ�����Ӻ�������
    private List<GameObject> clockBlocks = new List<GameObject> ();

    private void Awake()
    {
        //ѭ����þ�׼ʱ�����Ӻ��ӣ�����ӵ�������
       for( int i = 0; i < clockParent.childCount; i++)
        {
            //��ÿ������������ӵ�������
            clockBlocks.Add(clockParent.GetChild(i).gameObject);
            //ÿ��ʱ���Ĭ�ϲ���ʾ
            clockParent.GetChild(i).gameObject.SetActive(false);
        }
    }
    //ע���¼�
    private void OnEnable()
    {
        EventHandler.GameMinuteEvent += onGameMinuteEvent;
        EventHandler.GameDateEvent += OnGameDateEvent;
    }
    private void OnDisable()
    {
        EventHandler.GameMinuteEvent -= onGameMinuteEvent;
        EventHandler.GameDateEvent -= OnGameDateEvent;
    }
    private void onGameMinuteEvent(int minute, int hour)
    {
        //����ʱ���ı�
        //timeText.text = hour.ToString("00") + ":" + minute.ToString("00");
        timeText.text = hour.ToString() + ":" + minute.ToString();

    }

    private void OnGameDateEvent(int hour, int day, int month, int year, Season season)
    {
        dateText.text = year.ToString() + "��" + month.ToString() + "��" + day.ToString() + "��";
        seasonImage.sprite = seasonSprites[(int)season];
        SwitchHourImage(hour);
        DayNightImageRotate(hour);
    }
/// <summary>
/// ������Ϸ��ǰʱ����ʾ��׼ʱ���
/// </summary>
/// <param name="hour"></param>
    private void SwitchHourImage(int hour)
    {
        int index = hour / 4;
        if(index == 0)
        {
            foreach(var item in clockBlocks)
            {
                item.SetActive(false);
            }
        }
        else
        {
            for(int i = 0;i < clockBlocks.Count; i++)
            {
                if(i < index+1)
                {
                    clockBlocks[i].SetActive(true);
                }
                else
                {
                    clockBlocks[i].SetActive(false);
                }
            }
        }
    }
    
    private void DayNightImageRotate(int hour)
    {
        var target = new Vector3(0, 0, hour * 15 - 90);
        dayNightImage.DORotate(target,1f,RotateMode.Fast);
    }
}
 