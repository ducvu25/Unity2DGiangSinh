using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float smoothSpeed;
    [SerializeField] float deltaZoom;
    public bool zoom;

    Vector3 offset;

    float sizeMin;
    // Start is called before the first frame update
    void Start()
    {
        zoom = false;
        offset = transform.position - player.position;
        sizeMin = transform.GetComponent<Camera>().orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.fixedDeltaTime);
        transform.position = smoothedPosition;
    }
    public void ZoomOut()
    {
        if(transform.GetComponent<Camera>().orthographicSize < sizeMin*1.5f)
            transform.GetComponent<Camera>().orthographicSize += deltaZoom;
    }
    public void ZoomIn()
    {
        if (transform.GetComponent<Camera>().orthographicSize > sizeMin)
            transform.GetComponent<Camera>().orthographicSize -= deltaZoom;
    }
}
