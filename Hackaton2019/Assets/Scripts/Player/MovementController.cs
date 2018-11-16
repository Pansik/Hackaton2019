﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    public event System.Action<Weapon> EventOnPickupWeapon;
    private Vector3 velocity;
    private Rigidbody myRigidbody;

    [SerializeField]
    private Camera viewCamera;


    public float moveSpeed = 5;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void LookAt(Vector3 lookPoint)
    {
        Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(heightCorrectedPoint);
    }

    public void FixedUpdate()
    {
        myRigidbody.MovePosition(myRigidbody.position + velocity * Time.fixedDeltaTime);

    }

    void Update()
    {
        //var x = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f;
        //var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        //Vector3 moveInput = new Vector3(x, 0, z);
        //Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        //if (Input.GetKey(KeyCode.LeftArrow) == false && Input.GetKey(KeyCode.RightArrow) == false)
        //    moveVelocity = Vector3.zero;
        //Debug.Log(x + "     horizontal    " + z);
        //Move(moveVelocity);
        //transform.Translate(x, 0, z);

        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        Move(moveVelocity);
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, point, Color.red);
            //Debug.DrawRay(ray.origin,ray.direction * 100,Color.red);
            LookAt(point);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Weapon"))
        {
            if (EventOnPickupWeapon != null)
                EventOnPickupWeapon(other.GetComponent<Weapon>());
        }

    }

}