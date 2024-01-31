using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
class TextShow
{
    public TextMeshProUGUI txtShow;
    [Space(30)]
    public string[] value = { "1", "1" };
}
public class SettingLanguage : MonoBehaviour
{
    [SerializeField] List<TextShow> list;
    void Start()
    {
        UpdateLanguage();
    }
    public void UpdateLanguage()
    {
        int type = DataPlay.TypeLanguage;
        for (int i = 0; i < list.Count; i++)
        {
            list[i].txtShow.text = list[i].value[type];
        }
    }
}
