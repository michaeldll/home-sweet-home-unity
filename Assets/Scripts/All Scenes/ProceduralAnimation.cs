using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralAnimation : MonoBehaviour
{
	[SerializeField] private Transform LookTarget = null;
	[SerializeField] private Transform HandTarget = null;
	[SerializeField] private Transform HandPole = null;
	[SerializeField] private Transform Attraction = null;

	void LateUpdate()
	{
		//hand and look
		var normDist = Mathf.Clamp((Vector3.Distance(LookTarget.position, Attraction.position) - 0.3f) / 1f, 0, 1);
		HandTarget.rotation = Quaternion.Lerp(Quaternion.Euler(90, 0, 0), HandTarget.rotation, normDist);
		HandTarget.position = Vector3.Lerp(Attraction.position, HandTarget.position, normDist);
		HandPole.position = Vector3.Lerp(HandTarget.position + Vector3.down * 2, HandTarget.position + Vector3.forward * 2f, normDist);
		LookTarget.position = Vector3.Lerp(Attraction.position, LookTarget.position, normDist);
	}
}
