using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;

    private void Update()
    {
        transform.position += transform.forward * projectileSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            //hurt that motherfucker
            Destroy(gameObject);
        }

        if(other.CompareTag("Untagged"))
        {
            Destroy(gameObject);
        }
    }

}
