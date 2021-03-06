using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFourManager : MonoBehaviour
{
	private bool _isLoading = false;
	private Coroutine _loader = null;

	[SerializeField] private Fade fade;
	[SerializeField] private FadeLowPass fadeLowPass;
	[SerializeField] private CrossfadeMixer crossfadeMixer;

	void Awake()
	{
		GameManager.fourthScene();
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0) && !_isLoading)
		{
			LoadScene("Fifth Scene");
			fadeLowPass.Fade("avant_chute_cutoff", 0.35f);
		}
		if (GameManager.changedScene && !_isLoading)
		{
			GameManager.changedScene = false;
			LoadScene("Fifth Scene");
			fadeLowPass.Fade("avant_chute_cutoff", 0.35f);
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