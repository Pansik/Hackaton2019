using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public static event System.Action<Weapon> EventOnShoot;
    public GameObject projectilePrefab;
    public GameObject husk;
    public List<Transform> projectileStartTransforms;
    public float shootDelay;
    public float cameraShakeStrength;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public virtual void ShootProjectile()
    {
        foreach (Transform shootPosition in projectileStartTransforms)
        {
            GameObject newProjectile = Instantiate(projectilePrefab, shootPosition.position, shootPosition.rotation);
            PlayShootSound();
            DropHusk(shootPosition.position);
        }

        if(EventOnShoot != null)
        {
            EventOnShoot(this);
        }
    }

    private void DropHusk(Vector3 dropPosition)
    {
        GameObject newHusk = Instantiate(husk, dropPosition, Quaternion.identity);
    }

    private void PlayShootSound()
    {
        audioSource.Play();
    }
}
