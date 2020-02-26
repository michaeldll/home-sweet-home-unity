using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//not used currently
public class RotateExample : MonoBehaviour
{
	//not used currently
	public float smooth = 5.0f;
	public float tiltAngle = 60.0f;

	void FixedUpdate()
	{
		// Smoothly tilts a transform towards a target rotation.
		float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
		float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;

		// Rotate the cube by converting the angles into a quaternion.
		Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);

		// Dampen towards the target rotation
		transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
	}
}
