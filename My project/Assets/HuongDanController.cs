using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum StateHuongDan
{
    s_default,
    s_happy,
    s_funny,
    s_hungry
}
[System.Serializable]
public class MES
{
    public string mes;
    public StateHuongDan value;
    public MES(string mes, StateHuongDan value)
    {
        this.mes = mes;
        this.value = value;
    }
}
public class HuongDanController : MonoBehaviour
{
    public static HuongDanController instance;
    Follow follow;
    [SerializeField] GameObject goTxt;
    [SerializeField] GameObject goIcon;
    [SerializeField] TextMeshProUGUI txtMes;

    Animator animator;

    List<MES> mess;

    private void Awake()
    {
        int n = FindObjectsOfType<HuongDanController>().Length;
        //Debug.Log(n);
        if(n > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mess = new List<MES>();
        animator = goIcon.transform.GetComponent<Animator>();
        follow = transform.GetComponent<Follow>();
        goTxt.SetActive(false);
    }
    public void ShowMes(string s, StateHuongDan type)
    {
        //Debug.Log(s);
        mess.Add(new MES(s, type));
        if(!goTxt.activeSelf) { ShowMes(); }
    }
    void ShowMes()
    {
        if (mess.Count > 0)
        {
            //Debug.Log("2. " + mess[0].mes);
            goTxt.SetActive(true);
            animator.SetInteger("state", (int)mess[0].value);
            StartCoroutine(Show(mess[0].mes, 0, 1));
            mess.Remove(mess[0]);
        }
    }
    IEnumerator Show(string s, int i, float delay)
    {
        //Debug.Log(s);
        string mes = s.Substring(0, i);
        txtMes.text = mes;

        yield return new WaitForSeconds(0.05f);        
        if (i < s.Length)
        {
            StartCoroutine(Show(s, i + 1, delay));
        }
        else
        {
            Invoke("Next", 1);
        }
        //Debug.Log(mes.Length + " " + s.Length + " " + txtMes.text.Length);

    }
    public void Next()
    {
        animator.SetInteger("state", 0);
        goTxt.SetActive(false);
        ShowMes();
    }
    public void Flip()
    {
        goTxt.transform.GetChild(0).localScale = new Vector3(-goTxt.transform.GetChild(0).localScale.x, goTxt.transform.GetChild(0).localScale.y, goTxt.transform.GetChild(0).localScale.z);
    }
}
