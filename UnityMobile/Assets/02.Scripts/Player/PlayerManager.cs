using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region 변수
    //컴포넌트
    private Transform tr;
    private Rigidbody2D rigid;
    private Animator anim;

    //이동
    private float input_x;
    private float moveSpeed;
    private float isRight;

    //점프
    private float input_jump;
    private bool isJump;
    private float jumpPower;

    //애니매이션
    private bool isRun;

    //공격
    private bool input_attack;
    private int attackCount;

    #endregion

    void Awake()
    {
        //컴포넌트
        tr = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //이동
        moveSpeed = 10f;
        isRight = 1;
        isRun = false;

        //점프
        isJump = false;
        jumpPower = 15;

        //공격
        input_attack = false;
        attackCount = 0;
    }

    void Update()
    {
        InputSystem();
        Attack();
    }

    void FixedUpdate()
    {
        Move();
        Jump();
        
    }

    #region 함수
    void InputSystem()
    {
        input_x = Input.GetAxis("Horizontal");
        input_jump = Input.GetAxis("Jump");
        input_attack = Input.GetKeyDown(KeyCode.F);
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
        if (Input.GetKeyDown(KeyCode.F))
        {
            attackCount++;
            StartCoroutine(AttackAnim());
        }
            
    }

    IEnumerator AttackAnim()
    {
        switch (attackCount)
        {
            case 1:
                anim.SetInteger("attackCount", 1);
                break;
            case 2:
                anim.SetInteger("attackCount", 2);
                break;
            case 3:
                anim.SetInteger("attackCount", 3);
                break;
        }

        yield return null;
        anim.SetInteger("attackCount", 0);

        yield return new WaitForSeconds(2f);
        attackCount = 0;
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
