using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class SceneSixManager : MonoBehaviour
{
	private bool _isLoading = false;
	private bool _lerpFirstColor = false;
	private bool _lerpSecondColor = false;
	private bool _hasTappedOnce = false;
	private bool _hasTappedTwice = false;
	private Coroutine _loader = null;
	private WebSocketMessage _message;
	private AudioSource[] _musicSources = new AudioSource[5];

	[SerializeField] private Fade fade;
	[SerializeField] private FadeLowPass fadeLowPass;
	[SerializeField] private CrossfadeMixer crossfadeMixer;
	[SerializeField] private Animator fallingAnimator;
	[SerializeField] private Animator dissolveGroundAnimator;
	[SerializeField] private Animator[] dissolveEnvironmentAnimators;
	[SerializeField] private Camera mainCamera;
	[SerializeField] private CinemachineVirtualCamera cam2;
	[SerializeField] private GameObject phoneSpotlight;
	[SerializeField] private Color[] cameraBackgroundColors;
	[SerializeField] [Range(0f, 1f)] private float lerpTime;
	[SerializeField] private TextMeshProUGUI objectiveText = null;
	[SerializeField] private Animator whitePhoneAnimator = null;

	void Awake()
	{
		GameManager.sixthScene();
		GetMusic();
	}

	void GetMusic()
	{
		_musicSources[0] = GameObject.FindWithTag("music_intro").GetComponent<AudioSource>();
		_musicSources[1] = GameObject.FindWithTag("music_avant_chute").GetComponent<AudioSource>();
		_musicSources[2] = GameObject.FindWithTag("music_chute_portable").GetComponent<AudioSource>();
		_musicSources[3] = GameObject.FindWithTag("music_apres_chute").GetComponent<AudioSource>();
		_musicSources[4] = GameObject.FindWithTag("music_fin").GetComponent<AudioSource>();
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0) && !_isLoading)
		{
			GameManager.currentInteractionIndex += 1;
			switch (GameManager.currentInteractionIndex)
			{
				case 1:
					setObjective(5);
					break;
				case 2:
					setObjective(-1);
					GameManager.startFalling = true;
					_musicSources[2].Play();
					break;
				default:
					break;
			}
		}

		switch (GameManager.sheepTapped)
		{
			case 1:
				if (!_hasTappedOnce)
				{
					_hasTappedOnce = true;
					setObjective(5);
				}
				break;
			case 2:
				if (!_hasTappedTwice)
				{
					_hasTappedTwice = true;
					setObjective(-1);
					GameManager.startFalling = true;
				}
				break;
			default:
				break;
		}

		if (_lerpFirstColor)
		{
			fadeCameraBackgroundColor(true);
		}

		if (_lerpSecondColor)
		{
			fadeCameraBackgroundColor(false);
		}

		if (GameManager.startFalling)
		{
			GameManager.startFalling = false;
			HandleFall();
		}

		if (GameManager.changedScene && !_isLoading) { GameManager.changedScene = false; LoadScene("Seventh Scene"); }
	}
	void HandleFall()
	{
		//start anim and zoom
		fallingAnimator.SetBool("startFalling", true);
		StartCoroutine(ToggleDelayCamera(true, 0.4f));

		//start shader, turn off phone and send websocket message
		StartCoroutine(FadeFallingSoundIn(0.5f));
		StartCoroutine(DelayShaderFadeIn(0.5f));
		StartCoroutine(ToggleDelayPhoneSpotlight(false, 1.3f));
		StartCoroutine(SendDropPhone(1.3f));

		//turn on phone, send websocket message, zoom out, and reset shaders
		StartCoroutine(ToggleDelayPhoneSpotlight(true, 8.5f));
		StartCoroutine(AtPhoneOn(8.5f));
		StartCoroutine(ToggleDelayCamera(false, 11f));
		StartCoroutine(ResetGroundDissolve(9.25f));
		StartCoroutine(ResetEnvironmentDissolves(10f));
		StartCoroutine(ToggleDelayCamera(false, 11f));

		//load next scene
		StartCoroutine(LoadNextScene(15f));
	}
	public IEnumerator FadeFallingSoundIn(float delay)
	{
		yield return new WaitForSeconds(delay);
		crossfadeMixer.Crossfade("avant_chute", 2f, "chute_portable");
	}

	public IEnumerator LoadNextScene(float delay)
	{
		yield return new WaitForSeconds(delay);
		_musicSources[1].Stop();
		_musicSources[2].Stop();
		fadeLowPass.Fade("apres_chute_cutoff", 0.35f, 600);
		GameManager.currentInteractionIndex = 0;
		setObjective(-1);
		LoadScene("Seventh Scene");
	}

	public IEnumerator SendDropPhone(float delay)
	{
		yield return new WaitForSeconds(delay);
		SendMessage("dropPhone", "{\"from\":\"0\", \"to\":\"0\"}");
	}

	public IEnumerator AtPhoneOn(float delay)
	{
		yield return new WaitForSeconds(delay);
		SendMessage("liftPhone", "{\"from\":\"0\", \"to\":\"0\"}");

		yield return new WaitForSeconds(2f);
		_musicSources[3].Play();
		crossfadeMixer.Crossfade("chute_portable", 3f, "apres_chute");
	}

	public IEnumerator ToggleDelayCamera(bool mode, float delay)
	{
		yield return new WaitForSeconds(delay);
		if (mode)
		{
			cam2.m_Priority = 2;
			_lerpFirstColor = true; _lerpSecondColor = false;
		}
		else if (!mode)
		{
			cam2.m_Priority = 0;
			_lerpFirstColor = false; _lerpSecondColor = true;
		}
	}

	public IEnumerator ToggleDelayPhoneSpotlight(bool mode, float delay)
	{
		yield return new WaitForSeconds(delay);
		phoneSpotlight.SetActive(mode);
	}

	public IEnumerator DelayShaderFadeIn(float delay)
	{
		yield return new WaitForSeconds(delay);
		dissolveGroundAnimator.SetBool("startFading", true);
		foreach (var animator in dissolveEnvironmentAnimators)
		{
			animator.SetBool("startDissolving", true);
		}
	}

	public IEnumerator ResetGroundDissolve(float delay)
	{
		yield return new WaitForSeconds(delay);
		dissolveGroundAnimator.SetBool("startFading", false);
		dissolveGroundAnimator.Play("ResetFadeGround");
	}

	public IEnumerator ResetEnvironmentDissolves(float delay)
	{
		yield return new WaitForSeconds(delay);
		foreach (var animator in dissolveEnvironmentAnimators)
		{
			animator.SetBool("startDissolving", false);
			animator.Play("ResetDissolve");
		}
	}

	void fadeCameraBackgroundColor(bool toggle)
	{
		if (toggle)
		{
			mainCamera.backgroundColor = Color.Lerp(mainCamera.backgroundColor, cameraBackgroundColors[0], lerpTime);
		}
		else if (!toggle)
		{
			mainCamera.backgroundColor = Color.Lerp(mainCamera.backgroundColor, cameraBackgroundColors[1], lerpTime);
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

	public IEnumerator AsyncLoader(string name)
	{
		fade.FadeOut();
		yield return new WaitForSeconds(2f);

		// The Application loads the Scene in the background as the current Scene runs.
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);

		// Wait until the asynchronous scene fully loads
		while (!asyncLoad.isDone)
		{
			yield return null;
		}
	}

	void LoadScene(string name)
	{
		//fade music
		// crossfadeMixer.CrossfadeGroups("volPadLow", "volPadHigh", 2f);

		if (_loader != null)
		{
			//Debug.LogWarning("Scene load already in progress. Will not load.");
			return;
		}

		_isLoading = true;
		_loader = StartCoroutine(AsyncLoader(name));
	}
}