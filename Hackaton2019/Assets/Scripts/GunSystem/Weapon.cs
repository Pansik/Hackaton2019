using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform projectileStartTransform;
    public float shootDelay;

    public void ShootProjectile()
    {
        GameObject newProjectile = Instantiate(projectilePrefab, projectileStartTransform.position, projectileStartTransform.rotation);
    }
}
