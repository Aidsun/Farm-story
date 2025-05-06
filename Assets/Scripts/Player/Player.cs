using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    //��ȡ��Ҹ���
    private Rigidbody2D rb;

    [Tooltip("��ҵ��ƶ��ٶ�")]
    public float Speed = 5;
    [Tooltip("���б�����ƶ����ٶ�����(����0.6))")]
    public float restrictiveSpeed = 0.6f;
    [Tooltip("����ܷ�б�������ƶ�")]
    public bool canRateMove = true;
    //������������
    private float inputX;
    private float inputY;
    //�����ƶ�����ʸ��
    private Vector2 movementInput;

    //��ȡ��������
    private Animator[] animators;

    private bool isMoving;
    //����Ƿ���Կ��ƽ�ɫ�ƶ�
    private bool inputDisable;
    //����λ�÷�����ƫ��
    private Vector3 offetPosition =new Vector3 ((float)-2.3,(float) -0.8,(float) -0.14);
    private void Awake()
    {
        //��ʼ����ȡ�������
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

    //��ȡ�������
    private void PlayerInput()
    {
        //����б�������ƶ�
        if (canRateMove)
        {
            inputX = Input.GetAxisRaw("Horizontal");
            inputY = Input.GetAxisRaw("Vertical");
        }
        //����б�������ƶ�
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
        //���б�����ƶ����ٶ���Ҫ��������
        if (inputX != 0 && inputY != 0) 
        {
            inputX = inputX * restrictiveSpeed;
            inputY = inputY * restrictiveSpeed;
        }
        //��ס���shift���м���
        if (Input.GetKey(KeyCode.LeftShift))
        {
            inputX = inputX * 1.2f;
            inputY = inputY * 1.2f;
        }
        //�����ƶ�ʸ��
        movementInput = new Vector2(inputX, inputY);

        isMoving = movementInput != Vector2.zero;
    }
    //�ƶ����
    private void Movement()
    {
        //����ĵ�ǰλ��+�ƶ�ʸ��*�ƶ��ٶ�*ʱ��
        rb.MovePosition(rb.position + movementInput * Speed * Time.deltaTime);
    }

    //�����л���������״̬
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
