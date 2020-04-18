using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MoveWithGyro : MonoBehaviour
{
	[SerializeField] private CinemachineVirtualCamera cam;
	[SerializeField] private float amplitude = 0.0001f;
	private Vector3 _currentEulerAngles;

	private float _x;
	private float _y;
	private float _z;

	private float _maxRotationX = 23.4f;
	private float _minRotationX = 22f;
	private float _minRotationY = 132.4f;
	private float _maxRotationY = 133f;

	void Start()
	{
		_x = 22.7f;
		_y = 132.7f;
		_z = 0f;
	}

	void Update()
	{
		// Debug.Log(GameManager.gyroAngleX);
		// Debug.Log(GameManager.gyroAngleY);

		// Debug.Log(_x + GameManager.gyroAngleX);
		// Debug.Log(_y + GameManager.gyroAngleY);

		_x = Mathf.Clamp(_x + GameManager.gyroAngleX * Mathf.Rad2Deg * amplitude, _minRotationX, _maxRotationX);
		_y = Mathf.Clamp(_y + GameManager.gyroAngleY * Mathf.Rad2Deg * amplitude, _minRotationY, _maxRotationY);

		_currentEulerAngles = new Vector3(_x, _y, _z);

		cam.transform.eulerAngles = _currentEulerAngles;
	}
}
