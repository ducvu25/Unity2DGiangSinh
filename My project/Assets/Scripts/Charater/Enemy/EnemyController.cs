using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] InformationSO informationSO;
    float hp;
    bool facingRight;
    bool attack;
    bool run;

    [SerializeField] HpUISetting hpSetting;
    Rigidbody2D rb;
    Animator ani;
    [SerializeField] List<Sprite> sprites;

    //Start is called before the first frame update
    void Start()
    {
        hp = informationSO.Hp;
        facingRight = true;
        attack = false;
        run = false;

        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();

        Invoke("GetValueClass", 0.5f);
    }
    void GetValueClass()
    {
        hpSetting.Setting(hp / informationSO.Hp);
    }

    // Update is called once per frame
    void Update()
    {
        if(attack)
        {

        }
        else
        {
            ani.SetBool("run", run);
            Run(); 
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
        if (collision.CompareTag("Player"))
        {
            //ani.SetTrigger("hit");
            run = true;
            if((collision.gameObject.transform.position.x < transform.position.x && facingRight) || (collision.gameObject.transform.position.x > transform.position.x && !facingRight))
                Flip();
            //Debug.Log("Enter");
        }
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
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Stay");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            run = false;
            Debug.Log("Exit");
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
