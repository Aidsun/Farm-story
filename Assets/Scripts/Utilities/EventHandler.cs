using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public static class EventHandler
{
    public static event Action<InventoryLocation, List<InventoryItem>> UpdateInventoryUI;
    public static void CallUpdateInventoryUI(InventoryLocation location, List<InventoryItem> list)
    {
        UpdateInventoryUI?.Invoke(location, list);
    }

    public static event Action<int, Vector3> InstantiateItemInScene;
    public static void CallInstantiateItemInScene(int ID, Vector3 pos)
    {
        InstantiateItemInScene?.Invoke(ID, pos);
    }
    public static event Action<ItemDetails, bool> ItemSelectedEvent;
    public static void CallItemSelectedEvent(ItemDetails itemDetails, bool isSelected)
    {
        ItemSelectedEvent?.Invoke(itemDetails, isSelected);
    }
    ///summary
    ///更新分钟和小时
    ///summary
    public static event Action<int, int> GameMinuteEvent;
    public static void CallGameMinuteEvent(int minute, int hour)
    {
        GameMinuteEvent?.Invoke(minute, hour);
    }
    //更新小时，日，月，年，季节
    public static event Action<int, int, int, int, Season> GameDateEvent;
    public static void CallGameDateEvent(int hour,int day, int month,int year,Season season)
    {
        GameDateEvent?.Invoke(hour,day,month,year,season);
    }
    //切换场景并且传送到目标位置
    public static event Action<string, Vector3> TransitionEvent;
    public static void CallTransitionEvent(string sceneName,Vector3 position)
    {
        TransitionEvent?.Invoke(sceneName,position);
    }

}
