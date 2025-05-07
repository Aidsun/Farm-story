using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CursorManager : MonoBehaviour
{
    //���������ߣ����ӣ���Ʒ�����ͼƬ
    public Sprite normal, tool, seed,item;
    //��ǰ����ͼƬ
    private Sprite currentSprite;
    //���ͼƬ
    private Image cursorImage;
    //ͼƬ���
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
    /// �������ͼƬ
    /// </summary>
    /// <param name="sprite">ͼƬSprite</param>
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
            ItemType.���� => seed,
            ItemType.�������� => tool,
            ItemType.��Ʒ => item,
            _ => normal
        };

        }

    }
    /// <summary>
    /// �Ƿ���UI����
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
