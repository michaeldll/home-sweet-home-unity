using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.SceneManagement;
using Cinemachine;
using TMPro;

public class SceneSevenManagerNew : MonoBehaviour
{
	private WebSocketMessage _message;
	private int _clicks = 0;
	private bool _hasEnded = false;
	private bool _showEndingFeed = false;
	private bool _hasJumpedOnce = false;
	private bool _hasJumpedTwice = false;
	private bool _hasJumpedThrice = false;
	private AudioSource[] _musicSources = new AudioSource[5];

	[SerializeField] private TextToSpeechMultiple textToSpeech;
	[SerializeField] private CinemachineVirtualCamera[] cams;
	[SerializeField] private GameObject perso;
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
	[SerializeField] private CrossfadeMixer crossfadeMixer = null;

	void Awake()
	{
		GameManager.seventhScene();
		GetMusic();
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
		// if (Input.GetMouseButtonDown(0))
		// {
		// 	_clicks += 1;

		// 	switch (_clicks)
		// 	{
		// 		case 1:
		// 			setObjective(7);
		// 			textToSpeech.text = GameManager.moonTexts[0];
		// 			textToSpeech.TrySpeak();
		// 			break;
		// 		case 2:
		// 			setObjective(8);
		// 			textToSpeech.text = GameManager.moonTexts[1];
		// 			textToSpeech.TrySpeak();
		// 			break;
		// 		case 3:
		// 			setObjective(-1);
		// 			textToSpeech.text = GameManager.moonTexts[2];
		// 			textToSpeech.TrySpeak();
		// 			StartCoroutine(Die());
		// 			break;
		// 		default:
		// 			break;
		// 	}
		// }

		switch (GameManager.jumps)
		{
			case 1:
				if (!_hasJumpedOnce)
				{
					_hasJumpedOnce = true;
					setObjective(7);
					textToSpeech.text = GameManager.moonTexts[0];
					textToSpeech.TrySpeak();
				}
				break;
			case 2:
				if (!_hasJumpedTwice)
				{
					_hasJumpedTwice = true;
					setObjective(8);
					textToSpeech.text = GameManager.moonTexts[1];
					textToSpeech.TrySpeak();
				}
				break;
			case 3:
				if (!_hasJumpedThrice)
				{
					_hasJumpedThrice = true;
					setObjective(-1);
					textToSpeech.text = GameManager.moonTexts[2];
					textToSpeech.TrySpeak();
					StartCoroutine(Die());
				}
				break;
			default:
				break;
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
	}

	IEnumerator Die()
	{
		yield return new WaitUntil(() => GameManager.isVoiceLoaded == true);
		yield return new WaitForSeconds(4.8f);
		// perso.SetActive(false);
		// persoRagdoll.SetActive(true);
		deathPersoAnimator.SetBool("isDead", true);
		deathPhoneAnimator.SetBool("isDead", true);
		SendMessage("isDead", "{\"yep\":\"he ded\"}");
		phone.SetActive(false);

		yield return new WaitForSeconds(1.5f);
		textToSpeech.text = GameManager.endTexts[0];
		textToSpeech.TrySpeak();
		cams[1].m_Priority = 2;
		_musicSources[4].Play();
		crossfadeMixer.Crossfade("apres_chute", 1.5f, "fin");

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
		_musicSources[3].Stop();
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
			SendMessage("showCredits", "{\"yep\":\"he credit\"}");
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

	void GetMusic()
	{
		_musicSources[0] = GameObject.FindWithTag("music_intro").GetComponent<AudioSource>();
		_musicSources[1] = GameObject.FindWithTag("music_avant_chute").GetComponent<AudioSource>();
		_musicSources[2] = GameObject.FindWithTag("music_chute_portable").GetComponent<AudioSource>();
		_musicSources[3] = GameObject.FindWithTag("music_apres_chute").GetComponent<AudioSource>();
		_musicSources[4] = GameObject.FindWithTag("music_fin").GetComponent<AudioSource>();
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