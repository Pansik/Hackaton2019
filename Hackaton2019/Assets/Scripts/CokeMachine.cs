using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CokeMachine : MonoBehaviour
{
    public GameObject barPrefab;
    public GameObject interactObject;
    public Transform barThrowTransform;
    public float throwForce;
    private bool canUse = true;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && canUse)
        {
            ThrowBar();
        }
    }

    private void ThrowBar()
    {
        GameObject newBar = Instantiate(barPrefab, barThrowTransform.position, barThrowTransform.rotation);
        newBar.GetComponent<Rigidbody>().AddForce(newBar.transform.forward * throwForce, ForceMode.Impulse);
        audioSource.Play();
        Destroy(interactObject);
        canUse = false;
    }
}
