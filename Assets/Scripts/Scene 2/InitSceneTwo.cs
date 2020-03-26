using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.UI;

public class InitSceneTwo : MonoBehaviour
{
	[SerializeField] private Transform[] phoneTransforms; //Inital, Top, Bottom
	[SerializeField] private Transform[] characterTransforms; //Top, Bottom, Head Reset Transform
	[SerializeField] private CinemachineVirtualCamera[] cams; //Start cam, Scene cam
	[SerializeField] private GameObject[] assets; //appt, phone, phone beam, character,
	[SerializeField] private GameObject[] lights; //spotlight and beam, point light 1, directional light
	[SerializeField] private GameObject titleAnimation;
	[SerializeField] private TextToSpeech[] textToSpeechArr;
	[SerializeField] private TextTyperTalker textTyper;
	[SerializeField] private GameObject textCanvas;
	[SerializeField] private Camera mainCamera;
	[SerializeField] private GyroRotate gRotate;
	[SerializeField] private LookAtTarget lookAt;
	[SerializeField] private GameObject head;

	void Start()
	{
		//black
		mainCamera.backgroundColor = new Color(0f, 0f, 0f, 1f);

		//hide everything
		toggleGameObjects(assets, false);
		toggleGameObjects(lights, false);
		titleAnimation.SetActive(false);

		//reset text
		textToSpeechArr[0].text = GameManager.secondScene.introText;
		textToSpeechArr[1].text = GameManager.secondScene.preSceneText;
		textToSpeechArr[2].text = GameManager.secondScene.sceneText;
		textTyper.text = GameManager.secondScene.introText;

		//reset positions to beginning
		assets[1].transform.position = phoneTransforms[0].position;
		assets[3].transform.position = characterTransforms[0].position;

		//disable scripts
		gRotate.enabled = false;
		lookAt.enabled = false;

		//start animation
		StartCoroutine(Init());
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

	void toggleTyper(bool toggle)
	{
		textTyper.enabled = toggle ? true : false;
		textCanvas.SetActive(toggle ? true : false);
	}

	IEnumerator Init()
	{
		yield return new WaitForSeconds(2f);
		textToSpeechArr[0].enabled = true;

		yield return new WaitUntil(() => GameManager.isVoiceLoaded == true);
		toggleTyper(true);

		yield return new WaitForSeconds(12.0f);
		toggleTyper(false);

		//show phone and character
		assets[1].SetActive(true);
		assets[3].SetActive(true);
		toggleGameObjects(lights, true);

		yield return new WaitForSeconds(1f);
		textToSpeechArr[1].enabled = true;

		yield return new WaitUntil(() => GameManager.isVoiceLoaded == true);
		yield return new WaitForSeconds(3f);
		//start moving phone to end position
		assets[1].transform.DOMove(phoneTransforms[1].position, 2.0f);

		yield return new WaitForSeconds(0.6f);
		//turn on phone
		assets[2].SetActive(true);
		//start looking at phone
		lookAt.enabled = true;

		yield return new WaitForSeconds(4.8f);
		//reset head to avoid long neck bug
		lookAt.enabled = false;
		head.transform.position = characterTransforms[3].transform.position;
		// head.transform.rotation = characterTransforms[3].transform.rotation;

		//move dude and phone
		assets[1].transform.DOMove(phoneTransforms[2].position, 2.0f);
		assets[3].transform.DOMove(characterTransforms[1].position, 2.0f);
		head.transform.DOMove(characterTransforms[2].position, 2.0f);

		yield return new WaitForSeconds(1.2f);
		//show intro animation
		titleAnimation.SetActive(true);

		yield return new WaitForSeconds(7.5f);
		//enable look
		lookAt.enabled = true;
		//reset positions
		assets[1].transform.position = phoneTransforms[1].position;
		assets[3].transform.position = characterTransforms[0].position;
		//disable anim
		titleAnimation.SetActive(false);
		//switch cams
		cams[1].m_Priority = 3;
		//show appartment
		assets[0].SetActive(true);
		//show lights
		toggleGameObjects(lights, true);
		//bg yellow
		mainCamera.backgroundColor = new Color(0.82f, 0.6979567f, 0.51168f, 1f);
		//turn on gyro
		if (GameManager.isPhoneConnected) gRotate.enabled = true;
		//talk
		textToSpeechArr[2].enabled = true;
	}
}
