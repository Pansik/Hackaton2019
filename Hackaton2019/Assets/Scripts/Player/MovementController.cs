using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    public event System.Action<Weapon> EventOnPickupWeapon;
    public GameObject crosshair;
    [SerializeField]
    private float dashTime = 0.3f;
    [SerializeField]
    private float dashSpeed = 5;
    private Vector3 velocity;
    private Rigidbody myRigidbody;

    [SerializeField]
    private Animator anim;
    [SerializeField]
    private Camera viewCamera;


    Vector3 oldTransform;


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

        var tempVector = oldTransform - transform.position;

        anim.SetBool("Walk", false);
        anim.SetBool("WalkLeft", false);
        anim.SetBool("WalkRight", false);
        anim.SetBool("WalkBackwards", false);

        Debug.Log(tempVector);
        if (tempVector.z >= 0.1)
        {
            anim.SetBool("WalkBackwards", true);
        }else if(tempVector.z <= -0.1)
        {
            anim.SetBool("Walk", true);
        }else if(tempVector.x >= 0.1)
        {
            anim.SetBool("WalkLeft", true);
        }else if(tempVector.x <= -0.1)
        {
            anim.SetBool("WalkRight", true);
        }


//;       if(moveVelocity != Vector3.zero)
//        {
//            anim.SetBool("Walk", true);
//        }
//        else
//        {
//            anim.SetBool("Walk", false);
//        }
        Move(moveVelocity);
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space");
            //myRigidbody.velocity = velocity * dashSpeed;
            //myRigidbody.MovePosition(myRigidbody.position + velocity * Time.fixedDeltaTime * 10);
            StartCoroutine(Dash(velocity));
        }

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, point, Color.red);
            crosshair.transform.position = new Vector3(point.x, point.y+0.1f, point.z);
            //Debug.DrawRay(ray.origin,ray.direction * 100,Color.red);
            LookAt(point);
        }

        oldTransform = transform.position;
    }

    private IEnumerator Dash(Vector3 velocity)
    {
        myRigidbody.velocity = velocity * dashSpeed;
        //for (float i = 0f; i < 0.3f; i += Time.deltaTime)
        //{

        //    myRigidbody.MovePosition(myRigidbody.position + velocity * Time.fixedDeltaTime * 2.2f);
        //    yield return null;
        //}
        yield return new WaitForSeconds(dashTime);
        myRigidbody.velocity = Vector3.zero;
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
