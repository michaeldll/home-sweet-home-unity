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
		if (Input.GetMouseButtonDown(0))
		{
			crossfadeMixer.CrossfadeGroups(crossfadeDuration);
			if (WebSocketClient.Instance != null)
			{
				_message.type = "sound";
				_message.message = "{\"soundName\":\"notif\"}";

				WebSocketClient.Instance.Send(_message);
			}

		}

		if (GameManager.phoneCurrentScene > 0 && !_isLoading) { Debug.Log(GameManager.phoneCurrentScene); LoadScene("Third Scene"); }

		if (Input.GetKey(KeyCode.RightArrow) && !_isLoading) LoadScene("Third Scene"); ;
	}

	void LoadScene(string name)
	{

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