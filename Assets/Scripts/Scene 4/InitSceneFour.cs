using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.UI;

public class InitSceneFour : MonoBehaviour
{
	[SerializeField] private TextToSpeech[] textToSpeechArr;
	[SerializeField] private TextTyperTalker textTyper;
	[SerializeField] private GameObject textCanvas;
	[SerializeField] private Fade fade;
	[SerializeField] private Image background;
	void Start()
	{
		//black
		background.enabled = true;

		//set text
		textToSpeechArr[0].text = GameManager.fourthScene.introText;
		textToSpeechArr[1].text = GameManager.fourthScene.sceneText;
		textTyper.text = GameManager.fourthScene.introText;

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
		yield return new WaitForSeconds(2f);
		textToSpeechArr[0].enabled = true;

		yield return new WaitUntil(() => GameManager.isVoiceLoaded == true);

		toggleTyper(true);
		yield return new WaitForSeconds(12.0f);

		toggleTyper(false);
		fade.FadeIn();

		yield return new WaitForSeconds(2.0f);
		textToSpeechArr[1].enabled = true;

	}
}
