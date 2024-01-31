using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject goSetting;
    [SerializeField] GameObject goAbout;

    SettingLanguage settingLanguage;
    // Start is called before the first frame update
    void Start()
    {
        settingLanguage = GetComponent<SettingLanguage>();
        goSetting.SetActive(false);
        goAbout.SetActive(false);
    }
    public void NewGame()
    {
        SceneManager.LoadScene(DataPlay.Name(NAME_MAP.lv1));
    }
    public void Setting()
    {
        goAbout.SetActive(false);
        goSetting.SetActive(!goSetting.activeSelf);
    }
    public void About()
    {
        goSetting.SetActive(false);
        goAbout.SetActive(!goAbout.activeSelf);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void UpdateLanguage(int type)
    {
        DataPlay.TypeLanguage = type;
        settingLanguage.UpdateLanguage();
    }
}
