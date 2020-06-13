using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.UI;

public class InitSceneFive : MonoBehaviour
{
	private WebSocketMessage _message;
	[SerializeField] private TextToSpeech[] textToSpeechArr;
	[SerializeField] private TextTyperTalker textTyper = null;
	[SerializeField] private GameObject textCanvas = null;
	[SerializeField] private Fade fade = null;
	[SerializeField] private Image background = null;
	[SerializeField] private GyroRotate gRotate = null;
	[SerializeField] private CrossfadeMixer crossfadeMixer = null;

	void Start()
	{
		//black
		background.enabled = true;

		//set text
		textToSpeechArr[0].text = GameManager.introText;
		textToSpeechArr[1].text = GameManager.sceneText;
		textToSpeechArr[2].text = GameManager.secondSceneText;
		textToSpeechArr[3].text = GameManager.thirdSceneText;
		textTyper.text = GameManager.introText;

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
		yield return new WaitForSeconds(8f);

		toggleTyper(false);
		fade.FadeIn();
		//fade music
		crossfadeMixer.CrossfadeGroups("volPadHigh", "volPadLow", 2f);
		if (GameManager.isPhoneConnected) gRotate.enabled = true;
		//send readyForNextScene
		SendMessage("readyForNextScene", "{\"from\":\"0\", \"to\":\"1\"}");

		textToSpeechArr[1].enabled = true;
		yield return new WaitUntil(() => GameManager.isVoiceLoaded == true);
		yield return new WaitForSeconds(4.5f);

		textToSpeechArr[2].enabled = true;
		yield return new WaitUntil(() => GameManager.isVoiceLoaded == true);
		yield return new WaitForSeconds(3.8f);

		textToSpeechArr[3].enabled = true;
		yield return new WaitUntil(() => GameManager.isVoiceLoaded == true);
		yield return new WaitForSeconds(3.5f);
	}

	void SendMessage(string type, string message)
	{
		if (WebSocketClient.Instance != null)
		{
			_message = new WebSocketMessage();
			_message.id = GameManager.name;
			_message.type = type; //"readyForNextScene"
			_message.message = message; //"{\"from\":\"0\", \"to\":\"0\"}"

			WebSocketClient.Instance.Send(_message);
		}
	}
}
