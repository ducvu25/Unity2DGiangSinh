using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Information", menuName = "Character/Information")]
public class InformationSO : ScriptableObject
{
    [SerializeField] float hp = 0; 
    [SerializeField] float mp = 0;
    [SerializeField] float speed = 0;
    [SerializeField] float damage = 0;

    // Constructor
    public InformationSO(float hp, float mp, float spd, float dmg)
    {
        hp = hp;
        mp = mp;
        speed = spd;
        damage = dmg;
    }

    // Getter and Setter
    public float Hp
    {
        get { return hp; }
        set { hp = value; }
    }

    public float Mp
    {
        get { return mp; }
        set { mp = value; }
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }
}
