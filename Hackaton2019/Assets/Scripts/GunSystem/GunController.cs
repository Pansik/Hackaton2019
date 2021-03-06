﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    public Weapon currentWeapon;
    public Transform gunTransform;
    public AudioClip pickupSound;
    private float shootDelay;
    private float shootDelayTimer;
    private bool canShoot = false;
    private MovementController movementController;
    public Image uiWeaponIcon;

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
        Destroy(currentWeapon.gameObject);
        newWeapon.gameObject.transform.position = gunTransform.position;
        newWeapon.gameObject.transform.parent = gunTransform.parent;
        newWeapon.gameObject.transform.rotation = gunTransform.rotation;
        currentWeapon = newWeapon;
        PlayerSoundManager.instance.PlayClip(pickupSound);
        SetupShootDelay();
        uiWeaponIcon.sprite = newWeapon.icon;
    }

    private void SetupShootDelay()
    {
        shootDelay = currentWeapon.shootDelay;
        shootDelayTimer = shootDelay;
    }

}
