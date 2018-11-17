using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public Image hpImage;
    public LowHpAnimation lowHpEffectReference;

    public Transform parentForAnimHP;
    public GameObject addHpAnim;

    private float maxHp = 100f;
    private float currentHp = 100f;

    [SerializeField]
    private Animator anim;
    public AudioClip collectBarSound;

    public bool Died = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
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
        if (Died)
            return;
        EnemySpawner.Instance.DeleteEnemies();
        anim.SetTrigger("Died");
        Debug.Log("RIP");
        Died = true;
    }

    private void DamageTaken(float damage)
    {
        Debug.Log("ouch! took " + damage + " damage");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bar"))
        {
            float bonusHp = Random.Range(20, 60);
            currentHp += bonusHp;
            PlayerSoundManager.instance.PlayClip(collectBarSound);
            Destroy(collision.gameObject);
            int bonusHpConverted = (int)bonusHp;
            addHpAnim.GetComponent<Text>().text = "+" + bonusHpConverted.ToString() + " HP";
            addHpAnim.transform.position = parentForAnimHP.position;
            addHpAnim.SetActive(true);
            StartCoroutine(DeactivateHpAnim());
        }
    }

    private IEnumerator DeactivateHpAnim()
    {
        yield return new WaitForSeconds(1);
        addHpAnim.SetActive(false);
    }

}
