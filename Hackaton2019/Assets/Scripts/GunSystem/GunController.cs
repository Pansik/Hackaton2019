using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Weapon currentWeapon;
    private float shootDelay;
    private float shootDelayTimer;
    private bool canShoot = false;

    private void Start()
    {
        SetupShootDelay();
    }

    public void Update()
    {
        shootDelayTimer -= Time.deltaTime;
        if (shootDelayTimer <= 0)
            canShoot = true;

        if(Input.GetMouseButtonDown(0) && canShoot)
        {
            shootDelayTimer = shootDelay;
            canShoot = false;
            currentWeapon.ShootProjectile();
        }
    }

    private void OnWeaponPickup()
    {
        SetupShootDelay();
    }

    private void SetupShootDelay()
    {
        shootDelay = currentWeapon.shootDelay;
        shootDelayTimer = shootDelay;
    }

}
