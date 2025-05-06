using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
//这个脚本实现摄像机边界的效果
public class SwitchBounds : MonoBehaviour
{
    //切换场景后更改调用
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
        //切换场景之后，通过查找标签来寻找摄像机边缘
        PolygonCollider2D confinershape = GameObject.FindGameObjectWithTag("BoundsConfiner").GetComponent<PolygonCollider2D>();
        //获取虚拟相机的Confiner组件
        CinemachineConfiner confiner = GetComponent<CinemachineConfiner>();
        //将找到的摄像机边缘赋值给Confiner
        confiner.m_BoundingShape2D = confinershape;
        //清除边界数据缓存
        confiner.InvalidatePathCache();
    } 
}
 