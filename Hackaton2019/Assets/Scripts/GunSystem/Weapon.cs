using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject husk;
    public List<Transform> projectileStartTransforms;
    public float shootDelay;
    public float cameraShakeStrength;
    public Sprite icon;
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
        CameraShake.instance.StartShake(0.1f, cameraShakeStrength);
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
