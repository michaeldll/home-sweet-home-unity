using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.SceneManagement;
using Cinemachine;
using TMPro;

public class SceneSevenManagerNew : MonoBehaviour
{
	private int _clicks = 0;
	private bool _hasEnded = false;
	private bool _showEndingFeed = false;
	[SerializeField] private TextToSpeechMultiple textToSpeech;
	[SerializeField] private CinemachineVirtualCamera[] cams;
	[SerializeField] private GameObject perso;
	// [SerializeField] private GameObject persoRagdoll;
	[SerializeField] private Animator deathPersoAnimator;
	[SerializeField] private Animator deathPhoneAnimator;
	[SerializeField] private GameObject phone;
	[SerializeField] private TextMeshProUGUI[] usernameFields;
	[SerializeField] private GameObject endingGameObject;
	[SerializeField] private CanvasRenderer[] endingCanvasRenderers;
	[SerializeField] private CanvasRenderer endingBackground;
	[SerializeField] private TextMeshProUGUI whiteObjectiveText = null;
	[SerializeField] private Animator whitePhoneAnimator = null;
	[SerializeField] private CanvasGroup whiteUICanvasGroup = null;
	[SerializeField] private CanvasGroup greyUICanvasGroup = null;

	void Awake()
	{
		GameManager.seventhScene();
		foreach (var field in usernameFields)
		{
			field.SetText(GameManager.name);
			field.alpha = 0;
		}
		endingBackground.SetAlpha(0);
		greyUICanvasGroup.alpha = 0;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			_clicks += 1;

			switch (_clicks)
			{
				case 1:
					setObjective(7);
					textToSpeech.text = GameManager.moonTexts[0];
					textToSpeech.TrySpeak();
					break;
				case 2:
					setObjective(8);
					textToSpeech.text = GameManager.moonTexts[1];
					textToSpeech.TrySpeak();	
					break;
				case 3:
					setObjective(-1);
					textToSpeech.text = GameManager.moonTexts[2];
					textToSpeech.TrySpeak();
					StartCoroutine(Die());
					break;
				default:
					break;
			}
		}

		if (_hasEnded)
		{
			whiteUICanvasGroup.alpha -= 0.006f;
			greyUICanvasGroup.alpha += 0.006f;
			endingBackground.SetAlpha(endingBackground.GetAlpha() + 0.006f);
		}

		if (_showEndingFeed)
		{
			foreach (var renderer in endingCanvasRenderers)
			{
				renderer.SetAlpha(renderer.GetAlpha() + 0.006f);
			}
			foreach (var field in usernameFields)
			{
				field.alpha += 0.006f;
			}
		}

		// if (Input.GetKey(KeyCode.RightArrow) && !_isLoading)
		// {
		// 	_isLoading = true;
		// 	fade.FadeOut();
		// 	crossfadeMixer.CrossfadeGroups("volPadLow", "volPadHigh", 5f);
		// }
	}

	IEnumerator Die()
	{
		yield return new WaitUntil(() => GameManager.isVoiceLoaded == true);
		yield return new WaitForSeconds(4.8f);
		// perso.SetActive(false);
		// persoRagdoll.SetActive(true);
		deathPersoAnimator.SetBool("isDead", true);
		deathPhoneAnimator.SetBool("isDead", true);
		phone.SetActive(false);

		yield return new WaitForSeconds(1.5f);
		textToSpeech.text = GameManager.endTexts[0];
		textToSpeech.TrySpeak();
		cams[1].m_Priority = 2;

		yield return new WaitUntil(() => GameManager.isVoiceLoaded == true);
		yield return new WaitForSeconds(2.0f);
		textToSpeech.text = GameManager.endTexts[1];
		textToSpeech.TrySpeak();

		yield return new WaitUntil(() => GameManager.isVoiceLoaded == true);
		yield return new WaitForSeconds(1.5f);
		textToSpeech.text = GameManager.endTexts[2];
		textToSpeech.TrySpeak();
		
		yield return new WaitUntil(() => GameManager.isVoiceLoaded == true);
		yield return new WaitForSeconds(1.5f);
		textToSpeech.text = GameManager.endTexts[3];
		textToSpeech.TrySpeak();

		yield return new WaitUntil(() => GameManager.isVoiceLoaded == true);
		yield return new WaitForSeconds(5f);
		textToSpeech.text = GameManager.endTexts[4];
		textToSpeech.TrySpeak(); 

		yield return new WaitUntil(() => GameManager.isVoiceLoaded == true);
		yield return new WaitForSeconds(3f);
		if (_hasEnded == false)
		{
			foreach (var renderer in endingCanvasRenderers)
			{
				renderer.SetAlpha(0f);
			}
			endingGameObject.SetActive(true);
			_hasEnded = true;

			yield return new WaitForSeconds(4.0f);
			_showEndingFeed = true;
		}
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
}