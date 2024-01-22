using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushRoom : MonoBehaviour
{
    [SerializeField] int numberJump;
    int _numberJump;
    [SerializeField] float delayJump;
    float _delayJump;
    Animator animator;
    [SerializeField] Transform pointCheck;
    [SerializeField] float force;

    // Start is called before the first frame update
    void Start()
    {
        _numberJump = 0;
        _delayJump = 0;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_delayJump > 0)
        {
            _delayJump -= Time.deltaTime;
        }
        else if (_numberJump == numberJump)
            _numberJump = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player") && _numberJump < numberJump && collision.transform.position.y >= pointCheck.position.y)
        {
            Debug.Log(_numberJump);
            _numberJump++;
            if(_numberJump == numberJump)
                _delayJump = delayJump;
            animator.SetTrigger("Jump");
            collision.transform.GetComponent<PlayerController>().Rigidbody2D.AddForce(new Vector2(0, force));
        }
    }
}
