using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	public Collider MainCollider = null;
	public Collider[] AllColliders = null;
	public bool initRagdoll = false;

	// Use this for initialization
	void Awake()
	{
		MainCollider = GetComponent<Collider>();
		AllColliders = GetComponentsInChildren<Collider>(true);
		DoRagdoll(initRagdoll);
	}

	public void DoRagdoll(bool isRagdoll)
	{
		foreach (var col in AllColliders)
			col.enabled = isRagdoll;
		MainCollider.enabled = !isRagdoll;
		// GetComponent<Rigidbody>().useGravity = !isRagdoll;
		GetComponent<Animator>().enabled = !isRagdoll;
	}
}

