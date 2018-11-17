using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrenadeController : MonoBehaviour
{
    public int grenadeAmount;
    public Transform grenadeStartTransform;
    public GameObject grenadePrefab;
    public float throwForce = 45f;
    public AudioClip throwSound;
    public List<Image> grenadeIcons;
    public Sprite fullGrenadeSprite;
    public Sprite emptyGrenadeSprite;
    public AudioClip pickupSound;

    private void Update()
    {
        if(Input.GetMouseButtonDown(1) && grenadeAmount > 0)
        {
            ThrowGrenade();
        }
    }

    private void ThrowGrenade()
    {
        GameObject newGrenade = Instantiate(grenadePrefab, grenadeStartTransform.position, Quaternion.identity);
        Rigidbody rb = newGrenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce);
        RemoveGrenade();
        PlayerSoundManager.instance.PlayClip(throwSound);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("GrenadeItem"))
        {
            if (grenadeAmount < 3)
            {
                AddGrenade();
                Destroy(other.gameObject);
            }
        }
    }

    private void AddGrenade()
    {
        grenadeIcons[grenadeAmount].sprite = fullGrenadeSprite;
        grenadeAmount++;
        PlayerSoundManager.instance.PlayClip(pickupSound);
    }

    private void RemoveGrenade()
    {
        grenadeAmount--;
        grenadeIcons[grenadeAmount].sprite = emptyGrenadeSprite;
    }


}
