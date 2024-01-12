using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("\n--------Thong tin-------")]
    [SerializeField] float speed;
    [SerializeField] float jumpFoce;
    [Range(1, 5)]
    [SerializeField] int numberJump;
    int _numberJump;

    bool facingRight;

    Rigidbody2D rigidbody2D;
    Animator animator;
    Vector2 move;
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        facingRight = true;
        _numberJump = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        Run();
    }
    void CheckInput()
    {
        move = Vector2.zero;
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            move.x = -1;
            if (facingRight)
                Flip();
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            move.x = 1;
            if (!facingRight)
                Flip();
        }

        if( Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
    }
    void Run()
    {
        rigidbody2D.velocity = new Vector2(move.x*speed, rigidbody2D.velocity.y);
    }
    void Jump()
    {
        if(_numberJump < numberJump)
        {
            _numberJump++;
            rigidbody2D.AddForce(jumpFoce * Vector2.up/_numberJump);
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            _numberJump = 0;
        }
    }
}
