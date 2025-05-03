using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    [Tooltip("日夜轮转图片")]
    public RectTransform dayNightImage;
    [Tooltip("精准时间方块(一个方块等于4小时)")]
    public RectTransform clockParent;
    [Tooltip("季节图片")]
    public Image seasonImage;
    [Tooltip("日期文本")]
    public TextMeshProUGUI dateText;
    [Tooltip("时间文本")]
    public TextMeshProUGUI timeText;
    [Tooltip("季节图片数组")]
    public Sprite[] seasonSprites;

    //精准时间块的子孩子数组
    private List<GameObject> clockBlocks = new List<GameObject> ();

    private void Awake()
    {
        //循环获得精准时间块的子孩子，并添加到数组中
       for( int i = 0; i < clockParent.childCount; i++)
        {
            //将每个孩子依次添加到数组中
            clockBlocks.Add(clockParent.GetChild(i).gameObject);
            //每个时间块默认不显示
            clockParent.GetChild(i).gameObject.SetActive(false);
        }
    }
    //注册事件
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
        //更新时间文本
        //timeText.text = hour.ToString("00") + ":" + minute.ToString("00");
        timeText.text = hour.ToString() + ":" + minute.ToString();

    }

    private void OnGameDateEvent(int hour, int day, int month, int year, Season season)
    {
        dateText.text = year.ToString() + "年" + month.ToString() + "月" + day.ToString() + "日";
        seasonImage.sprite = seasonSprites[(int)season];
        SwitchHourImage(hour);
        DayNightImageRotate(hour);
    }
/// <summary>
/// 根据游戏当前时间显示精准时间块
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
 