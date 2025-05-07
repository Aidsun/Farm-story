using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CursorManager : MonoBehaviour
{
    //正常，工具，种子，商品的鼠标图片
    public Sprite normal, tool, seed,item;
    //当前鼠标的图片
    private Sprite currentSprite;
    //鼠标图片
    private Image cursorImage;
    //图片组件
    private RectTransform cursorCanvas;

    private void Start()
    {
        cursorCanvas = GameObject.FindGameObjectWithTag("CursorCanvas").GetComponent<RectTransform>();
        cursorImage = cursorCanvas.GetChild(0).GetComponent<Image>();
        currentSprite = normal;
        SetCursorImage(seed);
    }
    private void OnEnable()
    {
        EventHandler.ItemSelectedEvent += OnItemSelectedEvent;
    }
    private void OnDisable()
    {
        EventHandler.ItemSelectedEvent -= OnItemSelectedEvent;
    }


    private void Update()
    {
        if (cursorCanvas == null) return;
        cursorImage.transform.position = Input.mousePosition;
        if(!InteractWithUI())
        { 
            SetCursorImage(currentSprite);
        }
        else
        {
            SetCursorImage(normal);
        }
    }
    /// <summary>
    /// 设置鼠标图片
    /// </summary>
    /// <param name="sprite">图片Sprite</param>
    private void SetCursorImage(Sprite sprite)
    {
        cursorImage.sprite = sprite;
        cursorImage.color = new Color(1, 1, 1, 1);
    }
    private void OnItemSelectedEvent(ItemDetails itemDetails, bool isSelected)
    {
        if (!isSelected)
        {
            currentSprite = normal;
        }
        else
        {
        currentSprite = itemDetails.itemType switch
        {
            ItemType.种子 => seed,
            ItemType.砍树工具 => tool,
            ItemType.商品 => item,
            _ => normal
        };

        }

    }
    /// <summary>
    /// 是否与UI互动
    /// </summary>
    /// <returns></returns>
    private bool InteractWithUI()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        return false;
    }
}
