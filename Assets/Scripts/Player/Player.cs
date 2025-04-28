using System.Collections;
using System.Collections.Generic;
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
    void Start()
    {
        //��ʼ����ȡ�������
        rb = GetComponent<Rigidbody2D>();
        animators = GetComponentsInChildren<Animator>();
    }

    void Update()
    {
        PlayerInput();
        SwitchAnimation();
    }

    private void FixedUpdate()
    {
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
