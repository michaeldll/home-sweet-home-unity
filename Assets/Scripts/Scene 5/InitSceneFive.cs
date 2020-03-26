using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.UI;

public class InitSceneFive : MonoBehaviour
{
	[SerializeField] private TextToSpeech[] textToSpeechArr;
	[SerializeField] private TextTyperTalker textTyper;
	[SerializeField] private GameObject textCanvas;
	[SerializeField] private Fade fade;
	[SerializeField] private Image background;
	[SerializeField] private GyroRotate gRotate;

	void Start()
	{
		//black
		background.enabled = true;

		//set text
		textToSpeechArr[0].text = GameManager.fifthScene.introText;
		textTyper.text = GameManager.fifthScene.introText;

		gRotate.enabled = false;

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
		yield return new WaitForSeconds(8.5f);

		toggleTyper(false);
		fade.FadeIn();

		yield return new WaitForSeconds(2.0f);
		if (GameManager.isPhoneConnected) gRotate.enabled = true;
	}
}
