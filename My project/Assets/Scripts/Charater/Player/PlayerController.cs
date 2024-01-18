using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum State
{
    idle,
    run,
    jump1,
    jump2,
    climb
}
public class PlayerController : MonoBehaviour
{
    [Header("\n--------Thong tin-------")]
    [SerializeField] InformationSO information;
    [SerializeField] float jumpFoce;
    [Range(1, 5)]
    [SerializeField] int numberJump;
    int _numberJump;
    [SerializeField] float delayJump;
    float _delayJump;
    float hp, mp;
    [SerializeField] HpUISetting hpSetting;
    [SerializeField] HpUISetting mpSetting;
    [SerializeField] float recuperateMp = 0.5f;
    [Header("\n---------Attack-------")]
    [SerializeField] float timeAttack;
    float _timeAttack;
    [SerializeField] float consumptionMp = 20f;
    [SerializeField] GameObject preBullet;
    [SerializeField] Transform pointSpawnBullet;

    [Header("\n---------Roll-------")]
    [SerializeField] float delayRoll;
    float _delayRoll;

    [Header("\n---------Bong-------")]
    [SerializeField] GameObject goGhost;
    [SerializeField] float delayGhost;
    float _delayGhost;

    bool facingRight;

    Rigidbody2D rigidbody2D;
    Animator animator;
    Vector2 move;

    // ----- Climb
    [Header("\n-----Climb-----")]
    [SerializeField] LayerMask lmClimd;
    [SerializeField] float distanceClimd = 0.5f;
    bool checkClimb;
    float gravity;

    [Header("\n-----Water-----")]
    [SerializeField] LayerMask lmWater;
    [SerializeField] float distanceWater = 0.5f;
    [SerializeField] float archimedesThrust;
    bool checkWater;

    [Header("\n--------Zoom cam---------")]
    [SerializeField] float delayZoom;
    float _delayZoom;
    CameraFollow cameraFollow;
     
    
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cameraFollow = FindAnyObjectByType<CameraFollow>();
        //capsuleCollider = GetComponent<CapsuleCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        facingRight = true;
        _numberJump = 0;
        _delayGhost = 0;
        _delayRoll = 0;
        _delayJump = 0;
        _delayZoom = 0;
        _timeAttack = 0;
        checkClimb = false;
        checkWater = false;
        hp = information.Hp;
        mp = information.Mp;
        gravity = rigidbody2D.gravityScale;

        Invoke("SetValueClass", 0.5f);
    }
    void SetValueClass()
    {
        hpSetting.Setting(hp/information.Hp);
        mpSetting.Setting(mp/information.Mp);
    }

    // Update is called once per frame
    void Update()
    {
       // CheckLayer();
        CheckInput();
        UpdateAnimation();
    }
    /*
    void CheckLayer()
    {
        if(Physics2D.OverlapCircle(pointCheckLayer.position, distanceClimd, lmClimd))
        {
            if (!checkClimb)
            {
                animator.Play("Climb");
                checkClimb = true;
                rigidbody2D.gravityScale = 0;
                _numberJump = 0;
            }
        }
        else
        {
            checkClimb = false;
        }
        // Physics2D.OverlapCircle(pointCheckLayer.position, distanceWater, lmWater)
        /*if (Physics2D.Raycast(pointCheckLayer.position, Vector2.down, distanceWater, lmWater)){
            if(!checkWater)
            {
                checkWater = true;
                rigidbody2D.gravityScale = -archimedesThrust;
            }
            Debug.Log("In");
        }*/
        /*if (pointCheckLayer.GetComponent<CapsuleCollider2D>().IsTouchingLayers(lmWater))
        {
            if (!checkWater)
            {
                checkWater = true;
                rigidbody2D.gravityScale = -archimedesThrust;
            }
            Debug.Log("In");
        }
        else *//*if (!checkClimb && rigidbody2D.gravityScale != gravity)*//*
        {
            if (checkWater)
            {
                checkWater = false;
                Debug.Log("Out");
                rigidbody2D.gravityScale = gravity;
            }
            //Debug.Log("Out");
        }
    }
*/
    void CheckInput()
    {
        move = Vector2.zero;
        if (_delayRoll > 0)
            _delayRoll -= Time.deltaTime;
        if(_delayJump > 0)
            _delayJump -= Time.deltaTime;
        if(_timeAttack > 0)
            _timeAttack -= Time.deltaTime;
        float roll = 1;
        if (Input.GetKeyDown(KeyCode.R))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (_delayRoll <= 0 || !facingRight)
            {
                _delayRoll = delayRoll;
            }else if(_delayRoll > 0)
            {
                animator.SetTrigger("Roll");
                roll = 3;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (_delayRoll <= 0 || facingRight)
            {
                _delayRoll = delayRoll;
            }
            else if (_delayRoll > 0)
            {
                animator.SetTrigger("Roll");
                roll = 3;
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            move.x = -1*roll;
            if (facingRight)
                Flip();
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            move.x = 1*roll;
            if (!facingRight)
                Flip();
        }
        //Debug.Log(isDomestic);
        if (checkClimb)
        {
            if (Input.GetKey(KeyCode.DownArrow))
                move.y = -1;
            else if (Input.GetKey(KeyCode.UpArrow))
                move.y = 1;
            else
                move.y = 0;
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, move.y * information.Speed);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (_delayJump <= 0)
                {
                    _delayJump = delayJump;
                    Jump();
                }
            }
        }
        Run();

    }
    void Attack()
    {
        if (_timeAttack <= 0)
        {
            if (mp >= consumptionMp)
            {
                mp -= consumptionMp;
                mpSetting.Setting(mp / information.Mp);
                _timeAttack = timeAttack;
                animator.SetTrigger("Attack");
            }
            else
            {
                // thông báo hết mp
            }
        }
        else
        {
            // thông báo chưa hồi chiêu
        }
    }
    void Run()
    {
        if (checkWater)
            move.x /= 2;
        rigidbody2D.velocity = new Vector2(move.x*information.Speed, rigidbody2D.velocity.y);
    }
    void Jump()
    {
        if(_numberJump < numberJump)
        {
            _numberJump++;
            if(!checkWater)
                rigidbody2D.AddForce(jumpFoce * Vector2.up/_numberJump);
            else
                rigidbody2D.AddForce(jumpFoce/1.5f * Vector2.up / _numberJump);
            //Debug.Log(rigidbody2D.velocity.y + " " + (jumpFoce));
            if (rigidbody2D.velocity.y > jumpFoce/55)
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    void SpawnBullet()
    {
        GameObject go = Instantiate(preBullet, pointSpawnBullet.position, Quaternion.Euler(0, 0, facingRight ? -90 : 90));
        go.transform.GetComponent<BulletPlayer>().Dame = information.Damage;
        go.transform.GetComponent<BulletPlayer>().facingRight = facingRight;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            _numberJump = 0;
        }
    }
    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            rigidbody2D.gravityScale = -archimedesThrust;
            Debug.Log("INN");
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.gravityScale = archimedesThrust;
            _numberJump = 0;
            checkWater = true;
            //Debug.Log("INN2");
        }
        if (collision.CompareTag("Lader"))
        {
            animator.Play("Climb");
            checkClimb = true;
            rigidbody2D.gravityScale = 0;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
       if (collision.CompareTag("Lader"))
        {
            checkClimb = false;
            if(!checkWater)
                rigidbody2D.gravityScale = gravity;
        }
        if (collision.CompareTag("Water"))// && !checkClimb)
        {
            checkWater = false;
            if(!checkClimb)
                rigidbody2D.gravityScale = gravity;
            //Debug.Log("Out");
        }
    }
    void UpdateAnimation()
    {
        State state = State.idle;
        if(checkClimb)
        {
            state = State.climb;
            //Debug.Log(rigidbody2D.velocity.y);
            if (Math.Abs(rigidbody2D.velocity.y) < 0.1f && animator.GetInteger("State") == (int)State.climb)
            {
                //animator.StartPlayback();
                animator.enabled = false;
            }
            else
            {
                animator.enabled = true;
               // animator.StopPlayback();
            }
            animator.SetInteger("State", (int)state);
        }
        else
        {
            if (!animator.enabled)
                animator.enabled = true;
            if (move.x != 0)
                state = State.run;
            if (rigidbody2D.velocity.y > 0.1f)
                state = State.jump1;
            else if (rigidbody2D.velocity.y < -0.1f)
                state = State.jump2;
            if (_delayGhost > 0)
                _delayGhost -= Time.deltaTime;
            if (state >= State.jump1 && _delayGhost <= 0)
            {
                _delayGhost = delayGhost;
                GameObject go = Instantiate(goGhost, transform.position, Quaternion.identity);
                go.GetComponent<GhostController>().hub = transform.GetComponent<SpriteRenderer>().sprite;
                go.transform.localScale = transform.localScale;
            }
            animator.SetInteger("State", (int)state);
        }
        if(state != State.idle)
        {
            if (!cameraFollow.zoom)
            {
                _delayZoom = delayZoom;
                cameraFollow.zoom = true;
            }
            if(_delayZoom > 0)
                _delayZoom -= Time.deltaTime;
            else
                cameraFollow.ZoomOut();
        }
        else
        {
            if (cameraFollow.zoom)
            {
                _delayZoom = delayZoom;
                cameraFollow.zoom = false;
            }
            if (_delayZoom > 0)
                _delayZoom -= Time.deltaTime;
            else
            {
                cameraFollow.ZoomIn();
                if(mp < information.Mp)
                {
                    mp += recuperateMp;
                    if(mp > information.Mp)
                    {
                        mp = information.Mp;
                    }
                    mpSetting.Setting(mp / information.Mp);
                }
            }
        }
    }
    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(pointCheckLayer.position, distanceClimd);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(pointCheckLayer.position, pointCheckLayer.position - new Vector3(0, distanceWater, 0));
        //Gizmos.DrawWireSphere(pointCheckLayer.position, distanceWater);
    }*/
}
