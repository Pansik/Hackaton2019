using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayDestroy : MonoBehaviour
{
    public float destroyDelay;
    private void Start()
    {
        Destroy(gameObject, destroyDelay);
    }
}
