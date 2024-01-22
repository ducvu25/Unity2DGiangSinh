using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    [SerializeField] GameObject goShowHp;

    [SerializeField] GameObject goSetting;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
       
    }

    public void ShowSettingOn()
    {
        Debug.Log("On");
        goSetting.transform.GetComponent<Animator>().SetTrigger("On");
    }
    public void ShowSettingOff()
    {
        Debug.Log("Off");
        goSetting.transform.GetComponent<Animator>().SetTrigger("Off");
    }

    public void ShowHpAddDame(Vector3 pos, float dame, bool crits)
    {
        GameObject go = Instantiate(goShowHp, pos, Quaternion.identity);
        go.GetComponent<ShowHpAddDame>().Show(dame, crits);
    }
}
