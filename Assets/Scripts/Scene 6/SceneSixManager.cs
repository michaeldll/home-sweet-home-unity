using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class SceneSixManager : MonoBehaviour
{
	private bool _isLoading = false;

	private Coroutine _loader = null;
	private bool _fadeBackgroundToBlack = false;
	private bool _fadeBackgroundToYellow = false;

	[SerializeField] private Fade fade;
	[SerializeField] private CrossfadeMixer crossfadeMixer;
	[SerializeField] private Animator fallingAnimator;
	[SerializeField] private Animator dissolveGroundAnimator;
	[SerializeField] private Animator[] dissolveEnvironmentAnimators;
	[SerializeField] private Camera mainCamera;
	[SerializeField] private CinemachineVirtualCamera cam2;
	[SerializeField] private GameObject phoneSpotlight;
	[SerializeField] private Color[] cameraBackgroundColors;
	[SerializeField] [Range(0f, 1f)] private float lerpTime;
	private bool _lerpFirstColor = false;
	private bool _lerpSecondColor = false;

	void Awake()
	{
		GameManager.sixthScene();
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.DownArrow) && !_isLoading) GameManager.startFalling = true;

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
			HandleDissolve();
		}

		if (Input.GetKey(KeyCode.RightArrow) && !_isLoading) LoadScene("Seventh Scene"); ;
	}
	void HandleDissolve()
	{
		fallingAnimator.SetBool("startFalling", true);
		StartCoroutine(ToggleDelayCamera(true, 0.4f));

		StartCoroutine(DelayShaderFadeIn(0.5f));
		StartCoroutine(ToggleDelayPhoneSpotlight(false, 1.3f));

		StartCoroutine(ToggleDelayPhoneSpotlight(true, 8.5f));
		StartCoroutine(ResetGroundDissolve(9.25f));
		StartCoroutine(ResetEnvironmentDissolves(10f));
		StartCoroutine(ToggleDelayCamera(false, 11f));
	}

	void LoadScene(string name)
	{
		//fade music
		crossfadeMixer.CrossfadeGroups("volPadLow", "volPadHigh", 2f);

		if (_loader != null)
		{
			//Debug.LogWarning("Scene load already in progress. Will not load.");
			return;
		}

		_isLoading = true;
		_loader = StartCoroutine(AsyncLoader(name));
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
}