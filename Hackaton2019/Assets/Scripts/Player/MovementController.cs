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
    public Animator anim;
    [SerializeField]
    private Camera viewCamera;


    Vector3 oldTransform;

    [SerializeField]
    private Camera mainCamera;
    private Vector3 forward, right;

    public float moveSpeed = 5;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        forward = mainCamera.transform.forward;
        forward.y = 0f;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0) )* forward;
    }

    //public void Move(Vector3 _velocity)
    //{
    //    velocity = _velocity;
    //}

    public void LookAt(Vector3 lookPoint)
    {
        Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(heightCorrectedPoint);
    }

    //public void FixedUpdate()
    //{
    //    myRigidbody.MovePosition(myRigidbody.position + velocity * Time.fixedDeltaTime);

    //}

    void Update()
    {
        //Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        //Vector3 moveVelocity = moveInput.normalized * moveSpeed;

        Vector3 direction = new Vector3(Input.GetAxisRaw("HorizontalKey"), 0, Input.GetAxisRaw("VerticalKey"));
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("HorizontalKey");
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("VerticalKey");
        
        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);
        transform.forward = heading;
        transform.position += rightMovement;
        transform.position += upMovement;


        var temp = heading + upMovement;


        var tempVector = oldTransform - transform.position;

        anim.SetBool("Walk", false);
        anim.SetBool("WalkLeft", false);
        anim.SetBool("WalkRight", false);
        anim.SetBool("WalkBackwards", false);
        
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
        //Move(moveVelocity);
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Dash(temp));
        }

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            crosshair.transform.position = new Vector3(point.x, point.y+0.1f, point.z);
            LookAt(point);
        }

        oldTransform = transform.position;
    }

    private IEnumerator Dash(Vector3 velocity)
    {
        myRigidbody.velocity = velocity * dashSpeed;
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
