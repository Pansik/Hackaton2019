using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimFlyHigh : MonoBehaviour
{
    private Text textRef;
    public float flySpeed;
    private float alpha = 255;
    private void Awake()
    {
        textRef = GetComponent<Text>();
    }

    void Update ()
    {
        transform.position += Vector3.up * flySpeed;
        alpha -= Time.deltaTime;
        textRef.color = new Color(textRef.color.r, textRef.color.g, textRef.color.b, alpha);
	}
}
