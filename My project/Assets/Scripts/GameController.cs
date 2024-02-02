
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    int coin;
    bool pauseGame;

    public static GameController instance;
    UIController uiController;

    private void Awake()
    {
        /*int n = FindObjectsOfType<GameController>().Length;
        if(n > 1)
            Destroy(gameObject);*/
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (DataPlay.StartGame)
        {
            coin = 0;
        }
        else
        {
            coin = PlayerPrefs.GetInt(DataPlay.NameKey(NAME_KEY_PREFABS.numberCoin), 0);
        }
        uiController = GetComponent<UIController>();   
        uiController.UpdateCoin(coin);
    }
    public void AddCoin(int value)
    {
        coin += value;
        uiController.UpdateCoin(coin);
    }
    public bool PauseGame
    {
        get { return pauseGame; }
        set { pauseGame = value; }
    }
    public void Pause()
    {
        pauseGame = !pauseGame;
    }
    public int Coin
    {
        get { return coin; }
        set { coin = value; }
    }
    public void Reset()
    {
        DataPlay.StartGame = true;
        SceneManager.LoadScene(DataPlay.Name(NAME_MAP.lv1));
    }
    public void Menu()
    {
        DataPlay.StartGame = true;
        SceneManager.LoadScene(DataPlay.Name(NAME_MAP.menu));
    }
    public void Exit()
    {
        Application.Quit();
    }
}
