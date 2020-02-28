using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroRotate : MonoBehaviour
{
	[SerializeField] private Transform phone = null;
	[SerializeField] private float speed = 1.0f;
	[SerializeField] private float x = GameManager.gyroAngleX;
	private float _lowerLimit = 79, _upperLimit = 115;

	void Update()
	{
		if (GameManager.hasSecondIntroEnded)
		{
			x = GameManager.gyroAngleX;

			// Flip and clamp
			float clampedX = Mathf.Clamp(flipX(x), 75, 135);

			// Rotate the phone by converting Eulor into Quaternion
			Quaternion target = Quaternion.Euler(clampedX, -90.0f, 0);

			// Dampen towards the target rotation
			phone.rotation = Quaternion.Slerp(phone.rotation, target, Time.deltaTime * speed);
		}
	}

	float flipX(float x)
	{
		return -x + 180.0f;
	}
}
