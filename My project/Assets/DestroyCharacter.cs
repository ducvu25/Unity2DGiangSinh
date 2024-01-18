using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCharacter : MonoBehaviour
{
    [SerializeField] GameObject preSprite;
    [SerializeField] float timeOfExistence = 1f;
    public static DestroyCharacter instance;
    private void Awake()
    {
        instance = this;
    }
    public void DestroyGameObject(Vector3 pos, List<Sprite> sprites)
    {
        for(int i = 0; i < sprites.Count; i++)
        {
            GameObject go = Instantiate(preSprite, pos, Quaternion.Euler(Random.Range(0, 180), Random.Range(0, 180), 0));
            go.transform.GetComponent<SpriteRenderer>().sprite = sprites[i];
            Destroy(go, timeOfExistence);
        }
    }
}
