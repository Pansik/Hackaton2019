using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private const int GRENADE_DAMAGE = 3;
    private const int PLAYER_GRANADE_DAMAGE = 15;
    public GameObject explosionEffect;
    public float explosionForce = 700f;
    public float timeToDetonate = 3;
    public float radius;
    public AudioClip explosionSound;

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
        PlayerSoundManager.instance.PlayClip(explosionSound);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            EnemyController enemyController = nearbyObject.GetComponent<EnemyController>();
            if(enemyController != null)
            {
                enemyController.TakeDamage(GRENADE_DAMAGE);
            }
            else
            {
                PlayerController playerController = nearbyObject.GetComponent<PlayerController>();
                if(playerController != null)
                    playerController.GetHit(PLAYER_GRANADE_DAMAGE);
            }
            if(rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, radius);
            }
        }
        CameraShake.instance.StartShake(0.2f, 0.3f);
        Destroy(gameObject);
    }

}
