using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject explosionEffect;
    public float explosionForce = 700f;
    public float timeToDetonate = 3;
    public float radius;

    private void Update()
    {
        timeToDetonate -= Time.deltaTime;
        if(timeToDetonate <= 0)
        {
            Explode();
        }
    }

    private void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, radius);
            }
        }
        CameraShake.instance.StartShake(0.2f, 0.3f);
        Destroy(gameObject);
    }

}
