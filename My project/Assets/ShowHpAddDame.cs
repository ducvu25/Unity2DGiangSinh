using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowHpAddDame : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtDame;
    [SerializeField] Color32 color;
    public void Show(float dame, bool crits = false)
    {
        if(crits)
            txtDame.color = color;
        txtDame.text = dame.ToString();
    }
    void Destroy()
    {
        Destroy(gameObject);
    }
}
