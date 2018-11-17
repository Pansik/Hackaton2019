using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Husk : MonoBehaviour
{
    public float maxImpulseForce;
    public float minImpulseForce;
    private Rigidbody rb;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        //rb.AddForce(transform.up * Random.Range(minImpulseForce/100 ,maxImpulseForce/100), ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        audioSource.Play();
    }

}
