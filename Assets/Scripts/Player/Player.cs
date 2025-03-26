using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //获取玩家刚体
    private Rigidbody2D rb;

    [Tooltip("玩家的移动速度")]
    public float Speed = 5;
    [Tooltip("玩家斜方向移动的速度限制(建议0.6))")]
    public float restrictiveSpeed = 0.6f;
    [Tooltip("玩家能否斜方向上移动")]
    public bool canRateMove = true;
    //定义鼠标的输入
    private float inputX;
    private float inputY;
    //定义移动方向矢量
    private Vector2 movementInput;

    void Start()
    {
        //初始化获取刚体组件
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    //获取鼠标输入
    private void PlayerInput()
    {
        //可以斜方向上移动
        if (canRateMove)
        {
            inputX = Input.GetAxisRaw("Horizontal");
            inputY = Input.GetAxisRaw("Vertical");
        }
        //不能斜方向上移动
        else
        {
            if (inputY == 0)
            {
                inputX = Input.GetAxisRaw("Horizontal");
            }
            if (inputX == 0)
            {
                inputY = Input.GetAxisRaw("Vertical");
            }
        }
        //玩家斜方向移动的速度需要进行限制
        if (inputX != 0 && inputY != 0) 
        {
            inputX = inputX * restrictiveSpeed;
            inputY = inputY * restrictiveSpeed;
        }
        //计算移动矢量
        movementInput = new Vector2(inputX, inputY);
    }
    //移动玩家
    private void Movement()
    {
        //刚体的当前位置+移动矢量*移动速度*时间
        rb.MovePosition(rb.position + movementInput * Speed * Time.deltaTime);
    }
}
