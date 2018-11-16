using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {


    public static PlayerController Instance;
    [SerializeField]
    private Slider hpBar;

    private float maxHp = 100f;
    private float hp = 100f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void GetHit(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Death();
        }
        else
        {
            DamageTaken(damage);
        }
    }

    

    private void Death()
    {
        Debug.Log("RIP");
    }

    private void DamageTaken(float damage)
    {
        Debug.Log("ouch! took " + damage + " damage");
        hpBar.value = hp;
    }
    
}
