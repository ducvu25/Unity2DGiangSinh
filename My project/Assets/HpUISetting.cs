using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpUISetting : MonoBehaviour
{

    Image image1, image2;
    private void Start()
    {
        image1 = transform.GetChild(0).GetComponent<Image>();
        image2 = transform.GetChild(1).GetComponent<Image>();
    }
    public void Setting(float value)
    {
        image2.fillAmount = value;
        image1.fillAmount = 1 - value;
    }
}
