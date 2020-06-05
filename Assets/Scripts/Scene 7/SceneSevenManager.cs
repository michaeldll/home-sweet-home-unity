using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using TMPro;

public class SceneSevenManager : MonoBehaviour
{
	private bool _isLoading = false;
	private int _clicks = 0;
	[SerializeField] private TextToSpeech[] textToSpeechArr;
	[SerializeField] private Fade fade;
	[SerializeField] private CinemachineVirtualCamera[] cams;
	[SerializeField] private GameObject phoneSpotlight;
	[SerializeField] private GameObject phoneSpotlight2;
	[SerializeField] private GameObject perso;
	[SerializeField] private GameObject persoRagdoll;
	[SerializeField] private GameObject phone;
	[SerializeField] private CrossfadeMixer crossfadeMixer;
	[SerializeField] private TextMeshProUGUI[] usernameFields;
	[SerializeField] private GameObject endingGameObject;
	[SerializeField] private CanvasRenderer[] endingCanvasRenderers;
	[SerializeField] private TextMeshProUGUI objectiveText = null;
	[SerializeField] private Animator whitePhoneAnimator = null;
	[SerializeField] private CanvasGroup whiteUICanvasGroup = null;
	[SerializeField] private CanvasGroup greyUICanvasGroup = null;
	private bool hasEnded = false;

	void Awake()
	{
		GameManager.seventhScene();
		foreach (var field in usernameFields)
		{
			field.SetText(GameManager.name);
		}
		greyUICanvasGroup.alpha = 0;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			_clicks += 1;
		}

		switch (_clicks)
		{
			case 1:
				setObjective(7);
				textToSpeechArr[1].enabled = true;
				break;
			case 2:
				setObjective(8);
				textToSpeechArr[2].enabled = true;
				break;
			case 3:
				setObjective(-1);
				StartCoroutine(Die());
				break;
			default:
				break;
		}

		if (hasEnded)
		{
			foreach (var renderer in endingCanvasRenderers)
			{
				renderer.SetAlpha(renderer.GetAlpha() + 0.006f);
			}
			whiteUICanvasGroup.alpha -= 0.012f;
			greyUICanvasGroup.alpha += 0.006f;
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
		textToSpeechArr[3].enabled = true;

		yield return new WaitUntil(() => GameManager.isVoiceLoaded == true);
		yield return new WaitForSeconds(6.0f);
		perso.SetActive(false);
		persoRagdoll.SetActive(true);
		phone.SetActive(false);

		yield return new WaitForSeconds(2f);
		textToSpeechArr[5].enabled = true;

		yield return new WaitUntil(() => GameManager.isVoiceLoaded == true);
		yield return new WaitForSeconds(5.0f);
		cams[1].m_Priority = 2;

		yield return new WaitForSeconds(4.0f);
		// phone.SetActive(true);
		// phoneSpotlight.SetActive(true);
		// cams[2].m_Priority = 3;
		if (hasEnded == false)
		{
			foreach (var renderer in endingCanvasRenderers)
			{
				renderer.SetAlpha(0f);
			}
			endingGameObject.SetActive(true);
			hasEnded = true;
		}
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