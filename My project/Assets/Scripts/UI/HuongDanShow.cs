using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuongDanShow : MonoBehaviour
{
    [SerializeField] string mes;
    [SerializeField] StateHuongDan type;
    [SerializeField] int typeHub = 0;
    [SerializeField] Sprite[] hubs;
    [SerializeField] bool facingRight = true;
    bool show;
    private void Start()
    {
        show = true;
        if(!facingRight)
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        GetComponent<SpriteRenderer>().sprite = hubs[typeHub];
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && show)
        {
            //Debug.Log("ok");
            HuongDanController.instance.ShowMes(mes, type);
            show = false;
        }
    }

}
