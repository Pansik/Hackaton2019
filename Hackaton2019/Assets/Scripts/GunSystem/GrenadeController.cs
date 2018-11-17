using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    public int grenadeAmount;
    public Transform grenadeStartTransform;
    public GameObject grenadePrefab;
    public float throwForce = 45f;
    public AudioClip throwSound;
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
        grenadeAmount--;
        PlayerSoundManager.instance.PlayClip(throwSound);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("GrenadeItem"))
        {
            grenadeAmount++;
            Destroy(other.gameObject);
        }
    }

}
