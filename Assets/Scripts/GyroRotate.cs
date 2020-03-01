using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroRotate : MonoBehaviour
{
	[SerializeField] private Transform phone = null;
	[SerializeField] private float speed = 1.0f;
	private float x = GameManager.gyroAngleX;
	private float y = GameManager.gyroAngleY;
	private int _xLowerLimit = 79, _xUpperLimit = 115;
	private int _yLowerLimit = -20, _yUpperLimit = 45;

	void Update()
	{
		if (GameManager.hasSecondIntroEnded)
		{
			x = GameManager.gyroAngleX;
			y = GameManager.gyroAngleY;

			// Handle and clamp gyro coordinates
			float clampedX = Mathf.Clamp(handleGyroX(x), _xLowerLimit, _xUpperLimit);
			float clampedZ = Mathf.Clamp(y, _yLowerLimit, _yUpperLimit);

			// Rotate the phone by converting Euler into Quaternion
			// Don't need to change Y
			Quaternion target = Quaternion.Euler(clampedX, -90.0f, clampedZ);

			// Dampen towards the target rotation
			phone.rotation = Quaternion.Slerp(phone.rotation, target, Time.deltaTime * speed);
		}
	}

	float handleGyroX(float x)
	{
		return -x + 180.0f;
	}

	float handleGyroY(float y)
	{
		return y - 90.0f;
	}
}
