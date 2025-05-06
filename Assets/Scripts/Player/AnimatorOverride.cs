using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorOverride : MonoBehaviour
{
    private Animator[] animators;
    public SpriteRenderer holdItem;
    [Header("�����ֶ����б�")]
    public List<AnimatorType> animatorTypes;
    //�������λ��Ӧ�Ķ���
    private Dictionary<string, Animator> animatorNameDict = new Dictionary<string, Animator>();

    private void Awake()
    {
        animators = GetComponentsInChildren<Animator>();
        //���������λ��ӵ��ֵ���
        foreach(var anim in animators)
        {
            animatorNameDict.Add(anim.name, anim);
        }
    }
    //ע���¼�
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
        SwitchAnimator(PartType.��);
    }

    private void OnItemSelectedEvent(ItemDetails itemDetails, bool isSelected)
    {
        //WrokFlow:��ͬ�Ĺ��߷��ز�ͬ�Ķ�������Ҫ�����ﲹȫ
        PartType currentType = itemDetails.itemType switch
        {
            ItemType.���� => PartType.����,
            ItemType.��Ʒ => PartType.����, 
            _ => PartType.��
        }; 
        //�л�ѡ��״̬
        if(isSelected == false)
        {
            currentType = PartType.��;
            holdItem.enabled = false;
        }
        else
        {
            if(currentType == PartType.����)
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
 