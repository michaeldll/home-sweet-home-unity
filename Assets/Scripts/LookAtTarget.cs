using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
	[SerializeField] private Transform target = null;
	[SerializeField] private float speed = 5.0f;
	private Rigidbody _rb;
	private Quaternion qTo;
	private Quaternion qSlerp;
	void Awake()
	{
		_rb = gameObject.GetComponent<Rigidbody>();
	}
	void FixedUpdate()
	{
		if (GameManager.hasFirstIntroEnded)
		{
			qTo = Quaternion.LookRotation(target.position - transform.position);
			qSlerp = Quaternion.Slerp(transform.rotation, qTo, speed * Time.deltaTime);
			_rb.MoveRotation(qSlerp);
		}
	}
}
