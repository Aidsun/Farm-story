using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
//����ű�ʵ��������߽��Ч��
public class SwitchBounds : MonoBehaviour
{
    //�л���������ĵ���
    private void Start()
    {
        SwitchConfinerShape();
    }
    private void SwitchConfinerShape()
    {
        PolygonCollider2D confinershape = GameObject.FindGameObjectWithTag("BoundsConfiner").GetComponent<PolygonCollider2D>();
        CinemachineConfiner confiner = GetComponent<CinemachineConfiner>();
        confiner.m_BoundingShape2D = confinershape;
        confiner.InvalidatePathCache();
    } 
}
 