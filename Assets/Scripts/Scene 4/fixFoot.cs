using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fixFoot : MonoBehaviour
{
	[SerializeField] private Transform foot = null;
	private float _x = 0;
	private float _y = 0;
	private float _z = 0;
	void Awake()
	{
		_x = foot.rotation.eulerAngles.x;
		_y = foot.rotation.eulerAngles.y;
		_z = foot.rotation.eulerAngles.z;
		// Debug.Log(foot.rotation.eulerAngles.x);
	}
	void LateUpdate()
	{
		// Debug.Log(foot.rotation.eulerAngles);
		foot.rotation = Quaternion.Euler(_x, _y, _z);
	}
}
