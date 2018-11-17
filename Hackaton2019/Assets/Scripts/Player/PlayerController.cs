using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {


    public static PlayerController Instance;
    [SerializeField]
    private Slider hpBar;
    [SerializeField]
    private Slider secondaryHPBar;

    private float maxHp = 100f;
    private float hp = 100f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


    private void Update()
    {
        
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10, Color.black);
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
