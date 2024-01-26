using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        GetComponent<Button>().onClick.AddListener(Click);
    }
    void Click()
    {
        animator.SetTrigger("Selected");
    }
/*
    // Update is called once per frame
    void Update()
    {
        
    }*/
}
