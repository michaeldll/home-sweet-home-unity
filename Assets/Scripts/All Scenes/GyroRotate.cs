using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroRotate : MonoBehaviour
{
	[SerializeField] private Transform phone = null;
	[SerializeField] private float speed = 1.0f;
	private float x = GameManager.gyroAngleX;
	private float y = GameManager.gyroAngleY;
	[SerializeField] private int _yLowerLimit = -60, _yUpperLimit = 10;
	[SerializeField] private int _zLowerLimit = -10, _zUpperLimit = 40;
	// [SerializeField] private Animator turnBackController = null;

	void Update()
	{
		x = GameManager.gyroAngleX;
		y = GameManager.gyroAngleY;

		if (x < _zLowerLimit || x > _zUpperLimit)
		{
			// Debug.Log("isover");
		}

		// if (Input.GetMouseButtonDown(0)) turnBackController.SetBool("turnBack", true);

		// Handle and clamp gyro coordinates
		float clampedY = Mathf.Clamp(handleGyroY(y), _yLowerLimit, _yUpperLimit);
		float clampedZ = Mathf.Clamp(handleGyroX(x), _zLowerLimit, _zUpperLimit);

		// Rotate the phone by converting Euler into Quaternion
		// Don't need to change Y
		Quaternion target = Quaternion.Euler(0, clampedY, clampedZ);

		// Dampen towards the target rotation
		phone.rotation = Quaternion.Slerp(phone.rotation, target, Time.deltaTime * speed);
		// }
	}

	float handleGyroX(float x)
	{
		return -x;
	}

	float handleGyroY(float y)
	{
		return -(y + 90);
	}
}