using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCharacter : MonoBehaviour
{
    [SerializeField] GameObject preSprite;
    [SerializeField] float timeOfExistence = 1f;
    [SerializeField] GameObject preCoin;
    public static DestroyCharacter instance;
    [SerializeField] float angle, speed;
    private void Awake()
    {
        instance = this;
    }
    public void DestroyGameObject(Vector3 pos, List<Sprite> sprites, bool initCoin = true)
    {
        for(int i = 0; i < sprites.Count; i++)
        {
            GameObject go = Instantiate(preSprite, pos, Quaternion.Euler(Random.Range(0, 180), 0, Random.Range(0, 180)));
            go.transform.GetComponent<SpriteRenderer>().sprite = sprites[i];
            go.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-angle, angle), Random.Range(0, speed)), ForceMode2D.Impulse);
            Destroy(go, timeOfExistence);
        }
        if(!initCoin) { return; }
        int num = (int)Random.Range(0, 5);
        for(int i = 0; i < num; i++)
        {
            GameObject go = Instantiate(preCoin, pos, Quaternion.Euler(Random.Range(0, 180), 0, 0));
            go.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-angle, angle), Random.Range(0, speed)), ForceMode2D.Impulse);
            Destroy(go, timeOfExistence*3);
        }
    }
}
