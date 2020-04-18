using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RbMoveWithGyro : MonoBehaviour
{
	[SerializeField] private float thrust = 1.0f;
	[SerializeField] private bool isRigidbody = true;
	private Rigidbody _rb;

	void Awake()
	{
		_rb = gameObject.GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		if (isRigidbody)
		{
			_rb.AddForce(transform.right * thrust * Input.GetAxis("Horizontal"));
			_rb.AddForce(transform.up * thrust * Input.GetAxis("Vertical"));
		}
		else
		{
			if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1) transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * thrust);
			if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.1) transform.Translate(Vector3.up * Input.GetAxis("Vertical") * Time.deltaTime * thrust);
		}
	}
}
