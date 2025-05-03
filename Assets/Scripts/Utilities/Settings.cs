using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//整体游戏的细微设置
public class Settings
{
    [Tooltip("树冠颜色变化过渡时间")]
    public const float fadeDuration = 0.35f;
    [Tooltip("树冠颜色变化深度")]
    public const float targetAlpha = 0.6f;

    //时间相关
    [Tooltip("时间阈值，数字越小时间越快")]
    public const float secondThreshold = 0.1f;
    [Tooltip("秒数的阈值")]
    public const int secondHold = 59;
    [Tooltip("分钟的阈值")]
    public const int mintueHold = 59;
    [Tooltip("小时的阈值")]
    public const int hourHold = 23;
    [Tooltip("天数的阈值")]
    public const int dayHold = 30;
    [Tooltip("季节的阈值")]
    public const int seasonHold = 3;
}
