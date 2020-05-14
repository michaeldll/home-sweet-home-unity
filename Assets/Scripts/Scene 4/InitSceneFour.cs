using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.UI;

public class InitSceneFour : MonoBehaviour
{
	[SerializeField] private TextToSpeech[] textToSpeechArr;
	[SerializeField] private TextTyperTalker textTyper = null;
	[SerializeField] private GameObject textCanvas = null;
	[SerializeField] private Fade fade = null;
	[SerializeField] private Image background = null;
	[SerializeField] private CrossfadeMixer crossfadeMixer = null;
	[SerializeField] private CinemachineVirtualCamera cam2 = null;
	void Start()
	{
		//black
		background.enabled = true;

		//set text
		textToSpeechArr[0].text = GameManager.introText;
		textToSpeechArr[1].text = GameManager.sceneText;
		textTyper.text = GameManager.introText;

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
		//fade music
		crossfadeMixer.CrossfadeGroups("volPadHigh", "volPadLow", 2f);

		yield return new WaitForSeconds(2.0f);
		textToSpeechArr[1].enabled = true;

		yield return new WaitForSeconds(1.0f);
		cam2.m_Priority = 2;

	}
}
