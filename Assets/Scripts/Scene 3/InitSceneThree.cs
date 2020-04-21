using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.UI;

public class InitSceneThree : MonoBehaviour
{
	[SerializeField] private TextToSpeech[] textToSpeech;
	[SerializeField] private TextTyperTalker textTyper = null;
	[SerializeField] private GameObject textCanvas = null;
	[SerializeField] private Fade fade = null;
	[SerializeField] private Image background = null;
	[SerializeField] private CrossfadeMixer crossfadeMixer = null;
	void Start()
	{
		//set text
		textToSpeech[0].text = GameManager.introText;
		textToSpeech[1].text = GameManager.sceneText;
		textTyper.text = GameManager.introText;

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
		yield return new WaitForSeconds(7.0f);

		toggleTyper(false);
		fade.FadeIn();
		//fade music
		crossfadeMixer.CrossfadeGroups("volPadHigh", "volPadLow", 2f);
		yield return new WaitForSeconds(2.0f);

		textToSpeech[1].enabled = true;
	}
}
