using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InitSceneSix : MonoBehaviour
{
	private WebSocketMessage _message;

	[SerializeField] private TextToSpeech[] textToSpeechArr;
	[SerializeField] private TextTyperTalker textTyper = null;
	[SerializeField] private GameObject textCanvas = null;
	[SerializeField] private Fade fade = null;
	[SerializeField] private Image background = null;
	[SerializeField] private GyroRotate gRotate = null;
	[SerializeField] private CrossfadeMixer crossfadeMixer = null;
	[SerializeField] private TextMeshProUGUI objectiveText = null;
	[SerializeField] private Animator whitePhoneAnimator = null;
	void Start()
	{
		//black
		background.enabled = true;

		//set text
		textToSpeechArr[0].text = GameManager.introText;
		textToSpeechArr[1].text = GameManager.sceneText;
		textToSpeechArr[2].text = GameManager.secondSceneText;
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

		yield return new WaitForSeconds(8.5f);
		toggleTyper(false);
		fade.FadeIn();

		//fade music
		// crossfadeMixer.CrossfadeGroups("volPadHigh", "volPadLow", 2f);
		gRotate.enabled = true;

		//send readyForNextScene
		SendMessage("readyForNextScene", "{\"from\":\"0\", \"to\":\"1\"}");

		yield return new WaitForSeconds(1.0f);
		textToSpeechArr[1].enabled = true;

		yield return new WaitUntil(() => GameManager.isVoiceLoaded == true);
		yield return new WaitForSeconds(4.5f);
		textToSpeechArr[2].enabled = true;

		yield return new WaitUntil(() => GameManager.isVoiceLoaded == true);
		yield return new WaitForSeconds(5.3f);
		setObjective(4);
	}

	void setObjective(int objectiveIndex)
	{
		if (objectiveIndex > -1)
		{
			objectiveText.SetText(GameManager.objectiveTexts[objectiveIndex]);
			whitePhoneAnimator.SetBool("isInactive", false);
			whitePhoneAnimator.SetBool("isActive", true);
		}
		else
		{
			objectiveText.SetText("");
			whitePhoneAnimator.SetBool("isActive", false);
			whitePhoneAnimator.SetBool("isInactive", true);
		}
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
