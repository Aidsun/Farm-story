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

    void Start()
    {
        //��ʼ����ȡ�������
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
        //�����ƶ�ʸ��
        movementInput = new Vector2(inputX, inputY);
    }
    //�ƶ����
    private void Movement()
    {
        //����ĵ�ǰλ��+�ƶ�ʸ��*�ƶ��ٶ�*ʱ��
        rb.MovePosition(rb.position + movementInput * Speed * Time.deltaTime);
    }
}
