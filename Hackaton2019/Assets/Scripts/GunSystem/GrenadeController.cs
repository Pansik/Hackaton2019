using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    public float grenadeAmount;
    public GameObject grenadePrefab;

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            ThrowGrenade();
        }
    }

    private void ThrowGrenade()
    {
        GameObject newGrenade = Instantiate(grenadePrefab, transform.position, Quaternion.identity);
    }
}
