using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] Vector2 range;
    Transform pointTxtCoin;
    [SerializeField] float smoothSpeed;
    [SerializeField] float delta;

    int value;
    bool check;
    private void Start()
    {
        value = (int) Random.Range(range.x, range.y);
        check = true;
        pointTxtCoin = GameObject.FindGameObjectWithTag("PointCoin").transform;
    }
    private void Update()
    {
        if (!check)
        {
            if(Vector2.Distance(transform.position, pointTxtCoin.position) > 0.5f)
            {
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, pointTxtCoin.position, smoothSpeed * Time.fixedDeltaTime);
                transform.position = smoothedPosition;
                smoothSpeed += delta;
            }
            else
            {
                GameController.instance.AddCoin(value);
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && check)
        {
            check = false;
            transform.GetComponent<Rigidbody2D>().gravityScale = 0;
            transform.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.GetComponent<Animator>().SetTrigger("add");
        }
    }
}
