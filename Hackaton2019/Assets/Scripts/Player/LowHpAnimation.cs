using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LowHpAnimation : MonoBehaviour
{
    public Color targetColor;
    private Image myImage;
    private Color startColor;

	void Start ()
    {
        myImage = GetComponent<Image>();
        startColor = myImage.color;
	}

    void Update()
    {
        myImage.color = Color.Lerp(startColor,targetColor, Mathf.PingPong(Time.time, 0.5f));
    }

}
