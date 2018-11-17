using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAnimation : MonoBehaviour
{
    public float rotationSpeed;

	void Update ()
    {
        transform.Rotate(transform.up, 1 * rotationSpeed * Time.deltaTime);
	}
}
