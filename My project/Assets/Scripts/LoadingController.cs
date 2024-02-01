using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtLoad;
    [SerializeField] float delayRan = 0.2f;
    int start;
    // Start is called before the first frame update
    void Start()
    {
        start = 0;
        txtLoad.text ="Đang tải dữ liệu: " + start.ToString() + "%";
        StartCoroutine(load(delayRan));
    }
    IEnumerator load(float delay)
    {
        yield return new WaitForSeconds(delay);
        int x = (int)Random.Range(0, 3);
        if(x == 1)
        {
            start += (int)Random.Range(0, 15);
            if(start > 100) start = 100;
            txtLoad.text = "Đang tải dữ liệu: " + start.ToString() + "%";
            if (start == 100)
            {
                txtLoad.text = "Tải hoàn tất!";
                Invoke(nameof(Load), 0.5f);
            }
        }
        if(start < 100)
            StartCoroutine(load(delay));
    }
    void Load()
    {
        SceneManager.LoadScene(DataPlay.Name(NAME_MAP.menu));
    }
}
