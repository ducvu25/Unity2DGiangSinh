using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitMap : MonoBehaviour
{
    [SerializeField] float timeDelay = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(NextMap());
        }
    }
    IEnumerator NextMap()
    {
        yield return new WaitForSeconds(timeDelay);
        if(SceneManager.GetActiveScene().name == DataPlay.Name(NAME_MAP.lv3)){

        }
        else
        {
            PlayerController playerController = FindAnyObjectByType<PlayerController>();
            playerController.UpdateInfomationNextMap();
            PlayerPrefs.SetInt(DataPlay.NameKey(NAME_KEY_PREFABS.numberCoin), GameController.instance.Coin);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        //Debug.Log("Exit");
    }
}
