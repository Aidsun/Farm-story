using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

    //获取动画数组
    private Animator[] animators;

    private bool isMoving;
    //玩家是否可以控制角色移动
    private bool inputDisable;
    //传送位置发生的偏移
    private Vector3 offetPosition =new Vector3 ((float)-2.3,(float) -0.8,(float) -0.14);
    private void Awake()
    {
        //初始化获取刚体组件
        rb = GetComponent<Rigidbody2D>();
        animators = GetComponentsInChildren<Animator>();
    }
    private void OnEnable()
    {
        EventHandler.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneUnloadEvent += OnAfterSceneUnloadEvent;
        EventHandler.MoveToPosition += OnMoveToPosition;
    }

    private void OnDisable()
    {
        EventHandler.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneUnloadEvent -= OnAfterSceneUnloadEvent;
        EventHandler.MoveToPosition -= OnMoveToPosition;
    }
    private void OnMoveToPosition(Vector3 targetPosition)
    {
        transform.position = targetPosition + offetPosition;
    }

    private void OnAfterSceneUnloadEvent()
    {
        inputDisable = false;
    }

    private void OnBeforeSceneUnloadEvent()
    {
        inputDisable = true;
    }

    void Update()
    {
        if (inputDisable == false)
            PlayerInput();
        else
            isMoving = false;
        SwitchAnimation();
    }


    private void FixedUpdate()
    {
        if(!inputDisable)
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
        //按住左边shift进行加速
        if (Input.GetKey(KeyCode.LeftShift))
        {
            inputX = inputX * 1.2f;
            inputY = inputY * 1.2f;
        }
        //计算移动矢量
        movementInput = new Vector2(inputX, inputY);

        isMoving = movementInput != Vector2.zero;
    }
    //移动玩家
    private void Movement()
    {
        //刚体的当前位置+移动矢量*移动速度*时间
        rb.MovePosition(rb.position + movementInput * Speed * Time.deltaTime);
    }

    //设置切换动画播放状态
    private void SwitchAnimation()
    {
        foreach(var anim in animators)
        {
            anim.SetBool("IsMoving", isMoving);
            if (isMoving)
            {
                anim.SetFloat("InputX", inputX);
                anim.SetFloat("InputY", inputY);
            }
        }
    }
}
