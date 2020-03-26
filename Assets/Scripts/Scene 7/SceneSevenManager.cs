using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class SceneSevenManager : MonoBehaviour
{
	private bool _isLoading = false;
	private int _clicks = 0;
	[SerializeField] private TextToSpeech[] textToSpeechArr;
	[SerializeField] private Fade fade;
	[SerializeField] private CinemachineVirtualCamera[] cams;
	[SerializeField] private ToggleRagdoll tRagdoll;
	[SerializeField] private GameObject phoneSpotlight;
	[SerializeField] private GameObject phoneSpotlight2;
	private bool hasEnded = false;

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			_clicks += 1;
		}

		switch (_clicks)
		{
			case 1:
				textToSpeechArr[1].enabled = true;
				break;
			case 2:
				textToSpeechArr[2].enabled = true;
				break;
			case 3:
				StartCoroutine(Die());
				break;
			case 4:
				StartCoroutine(Epilogue());



				break;
			default:
				break;
		}

		if (Input.GetKey(KeyCode.RightArrow) && !_isLoading)
		{
			_isLoading = true;
			fade.FadeOut();
		}
	}

	IEnumerator Die()
	{
		textToSpeechArr[3].enabled = true;
		yield return new WaitUntil(() => GameManager.isVoiceLoaded == true);
		yield return new WaitForSeconds(6.0f);

		tRagdoll.toggle(true);
		yield return new WaitForSeconds(1.0f);

		cams[1].m_Priority = 2;
		yield return new WaitForSeconds(4.0f);

		phoneSpotlight.SetActive(false);
	}

	IEnumerator Epilogue()
	{
		cams[2].m_Priority = 3;
		yield return new WaitForSeconds(4.0f);

		if (hasEnded == false)
		{
			phoneSpotlight2.SetActive(true);
			hasEnded = true;
		}
	}
}