using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;

public class InitSceneFour : MonoBehaviour
{
	private WebSocketMessage _message;

	[SerializeField] private TextToSpeech[] textToSpeechArr;
	[SerializeField] private TextTyperTalker textTyper = null;
	[SerializeField] private GameObject textCanvas = null;
	[SerializeField] private Fade fade = null;
	[SerializeField] private FadeLowPass fadeLowPass;
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
		textToSpeechArr[2].text = GameManager.secondSceneText;
		textToSpeechArr[3].text = GameManager.thirdSceneText;
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
		fadeLowPass.Fade("avant_chute_cutoff", 2.0f, 4000, 451);
		//send readyForNextScene
		SendMessage("readyForNextScene", "{\"from\":\"0\", \"to\":\"1\"}");

		yield return new WaitForSeconds(2.0f);
		textToSpeechArr[1].enabled = true;

		yield return new WaitForSeconds(0.7f);
		cam2.m_Priority = 2;

		yield return new WaitUntil(() => GameManager.isVoiceLoaded == true);

		yield return new WaitForSeconds(6.5f);
		textToSpeechArr[2].enabled = true;

		yield return new WaitUntil(() => GameManager.isVoiceLoaded == true);

		yield return new WaitForSeconds(2.8f);
		textToSpeechArr[3].enabled = true;
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
