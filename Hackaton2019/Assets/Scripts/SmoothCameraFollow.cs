using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour {

    [SerializeField]
    private Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        //var temp = new Vector3(smoothedPosition.x, transform.position.y, smoothedPosition.z);
        transform.position = smoothedPosition;
        
    }
}
