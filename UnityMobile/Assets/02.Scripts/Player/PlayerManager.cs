using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region ����
    //�̵�
    private float Playerspeed;
    private float h;
    
    //����
    private bool isJump = false;
    private float Jumppower;

    //������Ʈ
    private Transform tr;
    Rigidbody2D rigid;

    #endregion

    private void Awake()
    {
        //��
        Playerspeed = 100f;
        Jumppower = 10f;

        //������Ʈ
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

    #region �Լ�
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
