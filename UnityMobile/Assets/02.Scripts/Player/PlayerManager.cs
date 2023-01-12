using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region ����
    //������Ʈ
    private Transform tr;
    private Rigidbody2D rigid;
    private Animator anim;

    //�̵�
    private float input_x;
    private float moveSpeed;
    private float isRight;

    //����
    private float input_jump;
    private bool isJump;
    private float jumpPower;

    //�ִϸ��̼�
    private bool isRun;

    //����
    private bool input_Attack;
    private int attackCount;
    #endregion

    void Awake()
    {
        //������Ʈ
        tr = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //�̵�
        moveSpeed = 10f;
        isRight = 1;
        isRun = false;

        //����
        isJump = false;
        jumpPower = 15;

        //����
        attackCount = 0;
    }

    void Update()
    {
        InputSystem();
        
    }

    void FixedUpdate()
    {
        Move();
        Jump();
        Attack();
    }

    #region �Լ�
    void InputSystem()
    {
        input_x = Input.GetAxis("Horizontal");
        input_jump = Input.GetAxis("Jump");
        input_Attack = Input.GetKey(KeyCode.F);
    }

    void Move()
    {
        rigid.velocity = new Vector2(input_x * moveSpeed, rigid.velocity.y);

        if((input_x > 0 && isRight < 0) || (input_x < 0 && isRight > 0))
        {
            FlipPlayer();
        }

        if (input_x != 0)
        {
            isRun = true;
        }
        else
        {
            isRun = false;
        }

        anim.SetBool("isRun", isRun);
    }

    void Jump()
    {
        if(input_jump != 0 && isJump == false)
        {
            rigid.velocity = Vector2.up * jumpPower;
            isJump = true;
            anim.SetTrigger("doJump");
        }
    }

    void FlipPlayer()
    {
        tr.eulerAngles = new Vector3(0, Mathf.Abs(tr.eulerAngles.y - 180), 0);
        isRight = isRight * -1;
    }

    void Attack()
    {
        if (input_Attack)
            attackCount++;
        else if (attackCount == 4)
            attackCount = 1;

        switch(attackCount)
        {
            case 1:
                anim.SetInteger("isAttack", 1);
                break;
            case 2:
                anim.SetInteger("isAttack", 2);
                break;
            case 3:
                anim.SetInteger("isAttack", 3);
                break;
        }


        
    }

    #endregion

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJump = false;
        }
    }
}
