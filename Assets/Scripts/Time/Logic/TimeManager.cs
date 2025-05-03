using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    //定义时间的具体数字
    private int gameSecond, gameMinute, gameHour, gameDay, gameMonth, gameYear;
    //定义季节
    private Season gameSeason = Season.春天;
   //定义每个季节拥有的月数
    private int monthInSeason = 3;
    //定义时间是否暂停
    private bool gameClockPause;
    //计时器
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
/// 初始化游戏时间
/// </summary>
    private void NewGameTime()
    {
        gameSecond = 0;
        gameMinute = 0;
        gameHour = 7;
        gameDay = 1;
        gameMonth = 1;
        gameYear = 2025;
        gameSeason = Season.春天;

    }
    /// <summary>
    /// 更新游戏时间逻辑
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
        //Debug.LogWarning(gameYear+"年"+gameMonth+"月"+gameDay+"日\t"+gameHour+":"+gameMinute+":"+ gameSecond  );
    }

}
 