using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    private Rigidbody rb;
    private GunController gunController;

    private void Awake()
    {
        gunController = FindObjectOfType<GunController>();
        rb = GetComponent<Rigidbody>();
        rb.velocity = gunController.gunTransform.forward * projectileSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //hurt that motherfucker
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }

    }

}
