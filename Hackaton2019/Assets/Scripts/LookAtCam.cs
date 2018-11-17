using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCam : MonoBehaviour {

    public Transform objToLookAt;
	void Update ()
    {
        transform.LookAt(objToLookAt);
	}
}
