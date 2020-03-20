using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.UI;

public class InitSceneTwo : MonoBehaviour
{
	[SerializeField] private Transform[] phoneTransforms; //beginning, end
	[SerializeField] private CinemachineVirtualCamera cam2 = null;
	[SerializeField] private GameObject[] assets; //appt, phone, phone beam, character,
	[SerializeField] private GameObject[] lights; //spotlight and beam, point light 1, directional light
	[SerializeField] private Image[] images;

	void Start()
	{
		//hide everything
		toggleGameObjects(assets, false);
		toggleGameObjects(lights, false);
		toggleIntroAnim(images, false);

		//start animation
		StartCoroutine(InitScene());
	}

	void toggleGameObjects(GameObject[] gObjects, bool toggle)
	{
		foreach (var gObject in gObjects)
		{
			gObject.SetActive(toggle ? true : false);
		}
	}

	void toggleIntroAnim(Image[] imgs, bool toggle)
	{
		foreach (var img in imgs)
		{
			img.enabled = toggle ? true : false;
		}

	}

	IEnumerator InitScene()
	{
		//show phone and character
		assets[1].SetActive(true);
		assets[3].SetActive(true);
		//reset phone position to beginning position
		assets[1].transform.position = phoneTransforms[0].position;
		yield return new WaitForSeconds(1.0f);


		//start moving phone to end position
		assets[1].transform.DOMove(phoneTransforms[1].position, 2.0f);
		yield return new WaitForSeconds(0.3f);

		//turn on phone
		assets[2].SetActive(true);
		//start looking at phone
		GameManager.lookAt = true;
		yield return new WaitForSeconds(2.3f);

		//show intro animation
		toggleIntroAnim(images, true);
		//switch cams
		cam2.m_Priority = 2;
		//show appartment
		assets[0].SetActive(true);
		//show lights
		toggleGameObjects(lights, true);
		yield return new WaitForSeconds(2.0f);

		//end intro
		toggleIntroAnim(images, false);
		GameManager.hasSecondIntroEnded = true;
	}
}
