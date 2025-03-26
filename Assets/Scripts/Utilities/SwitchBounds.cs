using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
//这个脚本实现摄像机边界的效果
public class SwitchBounds : MonoBehaviour
{
    //切换场景后更改调用
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
 