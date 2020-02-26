using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeCameras : MonoBehaviour
{
	[SerializeField] private CinemachineVirtualCamera cam;
	void Start()
	{
		StartCoroutine(Wait());
	}

	private IEnumerator Wait()
	{
		yield return new WaitForSeconds(1f);
		cam.m_Priority = 2;
	}
}
