using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTwoManager : MonoBehaviour
{
	private int _nbOfClicks = 0;
	private bool _isLoading = false;

	private Coroutine _loader = null;
	private WebSocketMessage _message;

	void Awake()
	{
		_message = new WebSocketMessage();
	}
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Debug.Log("click");

			_nbOfClicks += 1;

			if (WebSocketClient.Instance != null)
			{
				_message.type = "notif";
				_message.message = "{\"soundName\":\"look at me now\"}";

				WebSocketClient.Instance.Send(_message);
			}
		}

		if (_nbOfClicks > 2 && !_isLoading) LoadScene("Third Scene"); ;

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
		// The Application loads the Scene in the background as the current Scene runs.
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);

		// Wait until the asynchronous scene fully loads
		while (!asyncLoad.isDone)
		{
			yield return null;
		}
	}
}