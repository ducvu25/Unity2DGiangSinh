using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySprite : MonoBehaviour
{
    [SerializeField] float angle, speed;
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(new Vector2(Random.Range(-angle, angle), Random.Range(0, speed)), ForceMode2D.Impulse);
    }
}
