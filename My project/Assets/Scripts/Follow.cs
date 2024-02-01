using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] float smoothSpeed;
    [SerializeField] float amplitude;
    [SerializeField] float frequency;

    bool facingRight;
    float t;
    // Start is called before the first frame update
    void Start()
    {
        facingRight = true;
        t = 0;
    }
    Vector3 targetPosition;
    // Update is called once per frame
    void FixedUpdate()
    {
        float t2 = Time.time;
        targetPosition = transform.position + new Vector3(0, Mathf.Sin(t2 * frequency) * amplitude - Mathf.Sin(t * frequency) * amplitude, 0);
        t = t2;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
/*    public void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }*/
}
