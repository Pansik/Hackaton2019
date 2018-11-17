using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public Image hpImage;
    public LowHpAnimation lowHpEffectReference;
    private float maxHp = 100f;
    private float currentHp = 100f;

    [SerializeField]
    private Animator anim;


    bool died = false;

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
        hpImage.fillAmount = Mathf.Lerp(hpImage.fillAmount, currentHp / maxHp, 0.5f);
        if (currentHp < maxHp * 0.5f)
        {
            lowHpEffectReference.enabled = true;
        }
        else
        {
            lowHpEffectReference.enabled = false;
        }
    }

    public void GetHit(float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
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
        if (died)
            return;
        anim.SetTrigger("Died");
        Debug.Log("RIP");
        died = true;
    }

    private void DamageTaken(float damage)
    {
        Debug.Log("ouch! took " + damage + " damage");
    }
    
}
