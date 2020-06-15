using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	public Color lerpedColor = Color.white;
	public Camera cam;

	// Update is called once per frame
	void Update()
	{
		lerpedColor = Color.Lerp(Color.white, Color.black, Mathf.InverseLerp(0, 1, Time.time));
		cam.backgroundColor = lerpedColor;
	}
}
