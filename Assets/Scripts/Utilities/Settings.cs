using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������Ϸ��ϸ΢����
public class Settings
{
    [Tooltip("������ɫ�仯����ʱ��")]
    public const float fadeDuration = 0.35f;
    [Tooltip("������ɫ�仯���")]
    public const float targetAlpha = 0.6f;

    //ʱ�����
    [Tooltip("ʱ����ֵ������ԽСʱ��Խ��")]
    public const float secondThreshold = 0.1f;
    [Tooltip("��������ֵ")]
    public const int secondHold = 59;
    [Tooltip("���ӵ���ֵ")]
    public const int mintueHold = 59;
    [Tooltip("Сʱ����ֵ")]
    public const int hourHold = 23;
    [Tooltip("��������ֵ")]
    public const int dayHold = 30;
    [Tooltip("���ڵ���ֵ")]
    public const int seasonHold = 3;
}
