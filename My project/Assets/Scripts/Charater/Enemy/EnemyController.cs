using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] InformationSO informationSO;
    float timeIdle;
    float hp;
    bool facingRight;
    bool run;
    bool attack;
    [SerializeField] float delayAttack;
    [SerializeField] LayerMask lmPlayer;
    float _delayAttack;

    [SerializeField] HpUISetting hpSetting;
    Rigidbody2D rb;
    Animator ani;
    [SerializeField] List<Sprite> sprites;
    CircleCollider2D circleCollider;

    //Start is called before the first frame update
    void Start()
    {
        hp = informationSO.Hp;
        facingRight = true;
        run = false;
        attack = false;
        timeIdle = 0;
        _delayAttack = 0;

        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        circleCollider = GetComponent<CircleCollider2D>();

        Invoke("GetValueClass", 0.5f);
    }
    void GetValueClass()
    {
        hpSetting.Setting(hp / informationSO.Hp);
    }

    // Update is called once per frame
    void Update()
    {
        ani.SetBool("run", run);
        Run();
        if (_delayAttack > 0)
        {
            _delayAttack -= Time.deltaTime;
        }
        if (attack) return;
        if(timeIdle > 0)
        {
            timeIdle-= Time.deltaTime;
        }
        else
        {
            Flip();
            run = !run;
            timeIdle = Random.Range(2, 10);
        }
    }
    void Run()
    {
        rb.velocity = (run ? (facingRight ? Vector2.right : Vector2.left) : Vector2.zero) * informationSO.Speed;
    }
    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BulletPlayer"))
        {
            ani.SetTrigger("hit");
            BulletPlayer bullet = collision.gameObject.GetComponent<BulletPlayer>();
            int x = (int)Random.Range(0, 3);
            hp -= bullet.Dame*(x == 1 ? 1.5f : 1);
            UIController.instance.ShowHpAddDame(transform.position + Vector3.up*transform.GetComponent<Renderer>().bounds.size.y/2, -bullet.Dame * (x == 1 ? 1.5f : 1), x==1);
            GetValueClass();
            if (hp <= 0)
            {
                DestroyCharacter.instance.DestroyGameObject(transform.position, sprites);
                Destroy(gameObject);
            }
        }
        if (collision.CompareTag("Player"))
        {
            //ani.SetTrigger("hit");
            attack = true;
            ani.SetTrigger("find");
            run = true;
            if ((collision.gameObject.transform.position.x < transform.position.x && facingRight) || (collision.gameObject.transform.position.x > transform.position.x && !facingRight))
            {
                Flip();
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && circleCollider.IsTouchingLayers(lmPlayer) && _delayAttack <= 0)
        {
            //Debug.Log(informationSO.Damage);
            _delayAttack = delayAttack;
            collision.transform.GetComponent<PlayerController>().AddDame(informationSO.Damage);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            attack = false;
            ani.SetTrigger("notfind");
            run = false;
            Flip();
            timeIdle = Random.Range(2, 3);
            //Debug.Log("Exit");
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Flip();
        }
    }
}
