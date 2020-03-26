using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.UI;

public class InitSceneThree : MonoBehaviour
{
	[SerializeField] private TextToSpeech[] textToSpeech;
	[SerializeField] private TextTyperTalker textTyper;
	[SerializeField] private GameObject textCanvas;
	[SerializeField] private Fade fade;
	[SerializeField] private Image background;

	void Start()
	{
		//set text
		textToSpeech[0].text = GameManager.thirdScene.introText;
		textToSpeech[1].text = GameManager.thirdScene.sceneText;
		textTyper.text = GameManager.thirdScene.introText;

		//black
		background.enabled = true;

		//start animation
		StartCoroutine(Init());
	}

	void toggleTyper(bool toggle)
	{
		textTyper.enabled = toggle ? true : false;
		textCanvas.SetActive(toggle ? true : false);
	}

	IEnumerator Init()
	{
		yield return new WaitForSeconds(2.0f);
		textToSpeech[0].enabled = true;

		yield return new WaitUntil(() => GameManager.isVoiceLoaded == true);

		toggleTyper(true);
		yield return new WaitForSeconds(6.0f);

		toggleTyper(false);
		fade.FadeIn();
		yield return new WaitForSeconds(2.0f);

		textToSpeech[1].enabled = true;
	}
}
