using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InitSceneSevenNew : MonoBehaviour
{
	private WebSocketMessage _message;
	[SerializeField] private TextToSpeechMultiple textToSpeech;
	[SerializeField] private TextTyperTalker textTyper = null;
	[SerializeField] private GameObject textCanvas = null;
	[SerializeField] private Fade fade = null;
	[SerializeField] private Image background = null;
	[SerializeField] private GyroRotate gRotate = null;
	[SerializeField] private CrossfadeMixer crossfadeMixer = null;
	[SerializeField] private TextMeshProUGUI whiteObjectiveText = null;
	[SerializeField] private Animator whitePhoneAnimator = null;
	void Awake()
	{
		toggleTyper(false);
	}

	void Start()
	{
		Debug.developerConsoleVisible = true;
		//black
		background.enabled = true;

		//set text
		textTyper.text = GameManager.introText;
		
		gRotate.enabled = false;

		Debug.LogError("0");
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
		Debug.LogError("1");
		textToSpeech.text = GameManager.introText;
		textToSpeech.TrySpeak();
		
		yield return new WaitUntil(() => GameManager.isVoiceLoaded == true);
		Debug.LogError("2");
		toggleTyper(true);

		yield return new WaitForSeconds(4.0f);
		Debug.LogError("3");
		SendMessage("readyForNextScene", "{\"from\":\"0\", \"to\":\"1\"}");
		toggleTyper(false);
		fade.FadeIn();
		crossfadeMixer.CrossfadeGroups("volPadHigh", "volPadLow", 2f); //fade music
		if (GameManager.isPhoneConnected) gRotate.enabled = true;
		textToSpeech.text = GameManager.sceneText;
		textToSpeech.TrySpeak();

		yield return new WaitUntil(() => GameManager.isVoiceLoaded == true);
		yield return new WaitForSeconds(6.2f);
		Debug.LogError("4");
		setObjective(6);
	}

	void setObjective(int objectiveIndex)
	{
		if (objectiveIndex > -1)
		{
			whiteObjectiveText.SetText(GameManager.objectiveTexts[objectiveIndex]);
			whitePhoneAnimator.SetBool("isInactive", false);
			whitePhoneAnimator.SetBool("isActive", true);
		}
		else
		{
			whiteObjectiveText.SetText("");
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
