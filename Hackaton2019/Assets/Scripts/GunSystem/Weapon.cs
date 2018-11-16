using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public static event System.Action<float, float> EventOnShoot;
    public GameObject projectilePrefab;
    public List<Transform> projectileStartTransforms;
    public float shootDelay;
    public float cameraShakeStrength;
    public virtual void ShootProjectile()
    {
        foreach (Transform shootPosition in projectileStartTransforms)
        {
            GameObject newProjectile = Instantiate(projectilePrefab, shootPosition.position, shootPosition.rotation);
        }
        if(EventOnShoot != null)
        {
            EventOnShoot(0.1f, cameraShakeStrength);
        }
    }
}
