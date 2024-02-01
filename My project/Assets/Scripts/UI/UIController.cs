using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum MESS
{
    hetMP,
    slow,
    skill,
    hoiMP,
}

public class UIController : MonoBehaviour
{
    public static UIController instance;
    [SerializeField] GameObject goShowHp;

    [SerializeField] GameObject goSetting;

    [SerializeField] TextMeshProUGUI txtCoin;
    [SerializeField] GameObject goShowMes;
    [SerializeField] float timeAliveMess = 2f;
    [SerializeField] float delayMes = 0.5f;
    float _delayMes;

    [Header("\n--------End---------")]
    [SerializeField] GameObject goEnd;
    [SerializeField] TextMeshProUGUI txtCoinEnd;
    [SerializeField] float delayShowCoin = 0.2f;


    SettingLanguage settingLanguage;

    string[,] messValue = { { "Năng lượng không đủ", "Làm chậm", "Đang hồi", "Hồi năng lượng" }, { "Insufficient energy", "Slow", "Regenerating", "Mana regeneration" } };


    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        settingLanguage = GetComponent<SettingLanguage>();
        settingLanguage.UpdateLanguage();
        goSetting.SetActive(false);
        goEnd.SetActive(false);
    }
    private void Update()
    {
        if (_delayMes > 0)
        {
            _delayMes -= Time.deltaTime;
        }
    }

    public void ShowSettingOn()
    {
        //Debug.Log("On");
        GameController.instance.PauseGame = true;
        goSetting.SetActive(true);
        goSetting.transform.GetComponent<Animator>().SetTrigger("On");
    }
    public void ShowSettingOff()
    {
        //Debug.Log("Off");
        GameController.instance.PauseGame = false;
        goSetting.transform.GetComponent<Animator>().SetTrigger("Off");
        StartCoroutine(Off(1.5f));
    }
    IEnumerator Off(float time)
    {
        yield return new WaitForSeconds(time);
        goSetting.SetActive(false);
    }
    public void EndGame()
    {
        StartCoroutine(EndGame(2));
    }
    IEnumerator EndGame(float time)
    {
        yield return new WaitForSeconds(time);
        goEnd.SetActive(true);
        StartCoroutine(UpdateCoinEnd(0, GameController.instance.Coin, delayShowCoin/(GameController.instance.Coin + 1)));
    }
    IEnumerator UpdateCoinEnd(int i, int n, float t)
    {
        yield return new WaitForSeconds(t);
        txtCoinEnd.text = i.ToString();
        if (i < n)
        {
            StartCoroutine(UpdateCoinEnd(i+1, n, t));
        }
    }
    public void ShowHpAddDame(Vector3 pos, float dame, bool crits)
    {
        GameObject go = Instantiate(goShowHp, pos, Quaternion.identity);
        go.GetComponent<ShowHpAddDame>().Show(dame, crits);
    }
    public void ShowMess(Vector3 pos, int value)
    {
        if (_delayMes > 0) return;
        _delayMes = delayMes;
        GameObject go = Instantiate(goShowMes, pos, Quaternion.identity);
        go.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = messValue[DataPlay.TypeLanguage, value];
        Destroy(go, timeAliveMess);
    }

    public void UpdateCoin(int value)
    {
        if(txtCoin != null)
            txtCoin.text = value.ToString();
    }
}
