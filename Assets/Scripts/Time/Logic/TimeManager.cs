using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    //����ʱ��ľ�������
    private int gameSecond, gameMinute, gameHour, gameDay, gameMonth, gameYear;
    //���弾��
    private Season gameSeason = Season.����;
   //����ÿ������ӵ�е�����
    private int monthInSeason = 3;
    //����ʱ���Ƿ���ͣ
    private bool gameClockPause;
    //��ʱ��
    private float tikTime;

    private void Awake()
    {
        NewGameTime();
    }
    private void Update()
    {
        if (!gameClockPause)
        {
            tikTime += Time.deltaTime;
            if(tikTime >= Settings.secondThreshold)
            {
                tikTime -= Settings.secondThreshold;
                UpdateGameTime();
            }
        }
    }
/// <summary>
/// ��ʼ����Ϸʱ��
/// </summary>
    private void NewGameTime()
    {
        gameSecond = 0;
        gameMinute = 0;
        gameHour = 7;
        gameDay = 1;
        gameMonth = 1;
        gameYear = 2025;
        gameSeason = Season.����;

    }
    /// <summary>
    /// ������Ϸʱ���߼�
    /// </summary>
    private void UpdateGameTime()
    {
        gameSecond++;
        if(gameSecond > Settings.secondHold)
        {
            gameMinute++;
            gameSecond = 0;
            if(gameMinute > Settings.mintueHold)
            {
                gameHour++;
                gameMinute = 0;
                if (gameHour > Settings.hourHold)
                {
                    gameDay++;
                    gameHour = 0;
                    if (gameDay > Settings.dayHold)
                    {
                        gameMonth++;
                        gameDay = 1;
                        if(gameMonth > 12)
                        {
                            gameMonth = 1;
                        }
                        monthInSeason--;
                        if(monthInSeason == 0)
                        {
                            monthInSeason = 3;
                            int seasonNumber = (int)gameSeason;
                            seasonNumber++;
                            if(seasonNumber > Settings.seasonHold)
                            {
                                seasonNumber = 0;
                                gameYear++;
                            }
                            gameSeason = (Season)seasonNumber;
                        }
                    }
                }
            }
        }
        //Debug.LogWarning(gameYear+"��"+gameMonth+"��"+gameDay+"��\t"+gameHour+":"+gameMinute+":"+ gameSecond  );
    }

}
 