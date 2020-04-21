using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTwoManager : MonoBehaviour
{
	private bool _isLoading = false;

	private Coroutine _loader = null;
	private WebSocketMessage _message;
	[SerializeField] private CrossfadeMixer crossfadeMixer;
	[SerializeField] private float crossfadeDuration = 4.0f;
	[SerializeField] private Fade fade;

	void Awake()
	{
		_message = new WebSocketMessage();
		GameManager.secondScene();
	}

	void Update()
	{
		if (GameManager.phoneCurrentScene > 0 && !_isLoading) { LoadScene("Third Scene"); }

		if (Input.GetKey(KeyCode.RightArrow) && !_isLoading) { LoadScene("Third Scene"); }
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