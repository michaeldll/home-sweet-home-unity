using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InitSceneSeven : MonoBehaviour
{
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
		textToSpeechArr[1].text = GameManager.moonText[0];
		textToSpeechArr[2].text = GameManager.moonText[1];
		textToSpeechArr[3].text = GameManager.endText[0];
		textToSpeechArr[4].text = GameManager.sceneText;
		textToSpeechArr[5].text = GameManager.endText[1];
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
		yield return new WaitForSeconds(4.0f);

		toggleTyper(false);
		fade.FadeIn();
		//fade music
		crossfadeMixer.CrossfadeGroups("volPadHigh", "volPadLow", 2f);
		if (GameManager.isPhoneConnected) gRotate.enabled = true;
		textToSpeechArr[4].enabled = true;

		yield return new WaitUntil(() => GameManager.isVoiceLoaded == true);
		yield return new WaitForSeconds(6.2f);
		setObjective(6);
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
}
