using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    [SerializeField] float timeDestroy = 2f;
    [SerializeField] float force = 100f;
    [SerializeField] float dame = 10f;
    public bool facingRight;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeDestroy);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = (facingRight ? Vector2.right : Vector2.left)*force;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy (gameObject);
    }

    public float Dame
    {
        get { return dame; }
        set { dame = value; }
    }
}
