using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region 변수
    //이동
    private float Playerspeed;
    private float h;
    
    //점프
    private bool isJump = false;
    private float Jumppower;

    //컴포넌트
    private Transform tr;
    Rigidbody2D rigid;

    #endregion

    private void Awake()
    {
        //값
        Playerspeed = 100f;
        Jumppower = 10f;

        //컴포넌트
        tr = this.GetComponent<Transform>();
        rigid = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    void FixedUpdate()
    {
        
    }

    #region 함수
    void Move()
    {
        h = Input.GetAxis("Horizontal");

        rigid.AddForce(Vector2.right * h, ForceMode2D.Force);
    }

    void Jump()
    {
        isJump = Input.GetButtonDown("Jump");

        if(isJump == true)
        {
            rigid.AddForce(Vector2.up * Jumppower, ForceMode2D.Impulse);
        }
    }

    #endregion
}
