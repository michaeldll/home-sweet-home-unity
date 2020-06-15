using UnityEngine;

public class ToggleRagdoll : MonoBehaviour
{
	private Component[] _rigidbodies;
	[SerializeField] private bool mode = false;
	void Start()
	{
		toggle(mode);
	}
	public void toggle(bool mode)
	{
		_rigidbodies = GetComponentsInChildren<Rigidbody>();
		foreach (Rigidbody rb in _rigidbodies)
			rb.isKinematic = mode ? false : true;
	}
}