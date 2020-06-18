using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InitSceneThree : MonoBehaviour
{
	private WebSocketMessage _message;

	[SerializeField] private TextToSpeech[] textToSpeech;
	[SerializeField] private TextTyperTalker textTyper = null;
	[SerializeField] private GameObject textCanvas = null;
	[SerializeField] private Fade fade = null;
	[SerializeField] private Image background = null;
	[SerializeField] private CrossfadeMixer crossfadeMixer = null;
	[SerializeField] private TextMeshProUGUI objectiveText = null;
	[SerializeField] private Animator whitePhoneAnimator = null;
	[SerializeField] private GyroRotate gRotate = null;
	[SerializeField] private FadeLowPass fadeLowPass;

	void Start()
	{
		//set text
		textToSpeech[0].text = GameManager.introText;
		textToSpeech[1].text = GameManager.sceneText;
		textToSpeech[2].text = GameManager.secondSceneText;
		textTyper.text = GameManager.introText;

		//black
		background.enabled = true;

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
		yield return new WaitForSeconds(2.0f);
		textToSpeech[0].enabled = true;

		yield return new WaitUntil(() => GameManager.isVoiceLoaded == true);
		toggleTyper(true);

		yield return new WaitForSeconds(7.0f);
		toggleTyper(false);
		fade.FadeIn();

		//fade music
		fadeLowPass.Fade("avant_chute_cutoff", 2.0f, 4000, 451);
		//send readyForNextScene
		SendMessage("readyForNextScene", "{\"from\":\"0\", \"to\":\"1\"}");
		gRotate.enabled = true;

		yield return new WaitForSeconds(2.0f);
		textToSpeech[1].enabled = true;

		yield return new WaitUntil(() => GameManager.isVoiceLoaded == true);
		yield return new WaitForSeconds(4.3f);
		textToSpeech[2].enabled = true;

		yield return new WaitForSeconds(3.1f);
		setObjective(3);
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
