using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInfomation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Show(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Show(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Show(false);
        }
    }
    void Show(bool show)
    {
        for(int i=0; i<transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(show);
        }
    }
}
