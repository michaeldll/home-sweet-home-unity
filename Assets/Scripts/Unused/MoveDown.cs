using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
	[SerializeField] private float speed = 0.1f;
	[SerializeField] private float limit = 0.05f;
	void FixedUpdate()
	{
		// transform.position.y = transform.position.y - speed * Time.deltaTime;
		if (transform.position.y >= limit)
			transform.Translate(Vector3.down * Time.deltaTime * speed);
	}
}
