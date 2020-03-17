using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.UI;

public class InitSceneTwo : MonoBehaviour
{
	[SerializeField] private GameObject phone = null;
	[SerializeField] private Transform init = null;
	[SerializeField] private Transform end = null;
	[SerializeField] private CinemachineVirtualCamera cam2 = null;
	[SerializeField] private GameObject appt = null;
	[SerializeField] private GameObject beam = null;
	[SerializeField] private Image[] images;
	public static InitSceneTwo Instance { get; private set; } // static singleton

	void Start()
	{
		phone.transform.position = init.position;
		StartCoroutine(WaitAndMove());
	}

	IEnumerator WaitAndMove()
	{
		yield return new WaitForSeconds(1.0f);
		phone.transform.DOMove(end.position, 2.0f);
		yield return new WaitForSeconds(0.3f);
		GameManager.hasFirstIntroEnded = true;
		yield return new WaitForSeconds(2.3f);
		foreach (var image in images)
		{
			image.enabled = true;
		}
		cam2.m_Priority = 2;
		appt.SetActive(true);
		beam.SetActive(true);
		yield return new WaitForSeconds(2.0f);
		foreach (var image in images)
		{
			image.enabled = false;
		}
		GameManager.hasSecondIntroEnded = true;
	}
}
