using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MoveWithCursor : MonoBehaviour
{
	[SerializeField] private CinemachineVirtualCamera cam;
	[SerializeField] private float amplitude = 0.0001f;
	private Vector3 _currentEulerAngles;

	private float _x;
	private float _y;
	private float _z;

	private float _maxRotationX = 23.4f;
	private float _minRotationX = 22.0f;
	private float _minRotationY = 130f;
	private float _maxRotationY = 133f;

	void Start()
	{
		_x = 22.7f;
		_y = 132.7f;
		_z = 0f;
	}

	void Update()
	{
		var xFromMouse = (Input.mousePosition.x - Screen.height / 2) * amplitude;
		var yFromMouse = (Input.mousePosition.y - Screen.width / 2) * amplitude;

		Debug.Log(yFromMouse);

		_x = Mathf.Clamp(_x + xFromMouse, _minRotationX, _maxRotationX);
		_y = Mathf.Clamp(_y + yFromMouse, _minRotationY, _maxRotationY);

		_currentEulerAngles = new Vector3(_x, _y, _z);

		//apply the change to the gameObject
		cam.transform.eulerAngles = _currentEulerAngles;
	}
}
