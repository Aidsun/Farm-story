using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorOverride : MonoBehaviour
{
    private Animator[] animators;
    public SpriteRenderer holdItem;
    [Header("各部分动画列表")]
    public List<AnimatorType> animatorTypes;
    //身体各部位对应的动画
    private Dictionary<string, Animator> animatorNameDict = new Dictionary<string, Animator>();

    private void Awake()
    {
        animators = GetComponentsInChildren<Animator>();
        //把身体各部位添加到字典里
        foreach(var anim in animators)
        {
            animatorNameDict.Add(anim.name, anim);
        }
    }
    //注册事件
    private void OnEnable()
    {
        EventHandler.ItemSelectedEvent += OnItemSelectedEvent;
        EventHandler.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
    }
    private void OnDisable()
    {
        EventHandler.ItemSelectedEvent -= OnItemSelectedEvent;
        EventHandler.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent;
    }

    private void OnBeforeSceneUnloadEvent()
    {
        holdItem.enabled = false;
        SwitchAnimator(PartType.空);
    }

    private void OnItemSelectedEvent(ItemDetails itemDetails, bool isSelected)
    {
        //WrokFlow:不同的工具返回不同的动画，需要在这里补全
        PartType currentType = itemDetails.itemType switch
        {
            ItemType.种子 => PartType.举起,
            ItemType.商品 => PartType.举起, 
            _ => PartType.空
        }; 
        //切换选择状态
        if(isSelected == false)
        {
            currentType = PartType.空;
            holdItem.enabled = false;
        }
        else
        {
            if(currentType == PartType.举起)
            {
                holdItem.sprite = itemDetails.itemOnWorldSprite;
                holdItem.enabled = true;
            }
        }
        SwitchAnimator(currentType);
    }
    private void SwitchAnimator(PartType partType)
    {
         foreach(var item in animatorTypes)
        {
            if(item.partType == partType)
            {
                animatorNameDict[item.partName.ToString()].runtimeAnimatorController = item.overrideController;
            }
        }
    }
}
 