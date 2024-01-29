using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuongDanShow : MonoBehaviour
{
    [SerializeField] string mes;
    [SerializeField] StateHuongDan type;
    [SerializeField] int typeHub = 0;
    [SerializeField] Sprite[] hubs;

    bool show;
    private void Start()
    {
        show = true;
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
