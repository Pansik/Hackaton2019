using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    private MovementController movementController;
    public Weapon currentWeapon;
    private float shootDelay;
    private float shootDelayTimer;
    private bool canShoot = false;

    private void Start()
    {
        movementController = GetComponent<MovementController>();
        movementController.EventOnPickupWeapon += OnWeaponPickup;
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

    private void OnWeaponPickup(Weapon newWeapon)
    {
        currentWeapon = newWeapon;
        Debug.Log(newWeapon.gameObject.name);
        SetupShootDelay();
    }

    private void SetupShootDelay()
    {
        shootDelay = currentWeapon.shootDelay;
        shootDelayTimer = shootDelay;
    }

}
