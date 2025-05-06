using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
//����ű�ʵ��������߽��Ч��
public class SwitchBounds : MonoBehaviour
{
    //�л���������ĵ���
    private void OnEnable()
    {
        EventHandler.AfterSceneUnloadEvent += SwitchConfinerShape;
    }
    private void OnDisable()
    {
        EventHandler.AfterSceneUnloadEvent -= SwitchConfinerShape;
    }
    private void SwitchConfinerShape()
    {
        //�л�����֮��ͨ�����ұ�ǩ��Ѱ���������Ե
        PolygonCollider2D confinershape = GameObject.FindGameObjectWithTag("BoundsConfiner").GetComponent<PolygonCollider2D>();
        //��ȡ���������Confiner���
        CinemachineConfiner confiner = GetComponent<CinemachineConfiner>();
        //���ҵ����������Ե��ֵ��Confiner
        confiner.m_BoundingShape2D = confinershape;
        //����߽����ݻ���
        confiner.InvalidatePathCache();
    } 
}
 