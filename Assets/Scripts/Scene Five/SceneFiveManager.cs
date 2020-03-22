using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFiveManager : MonoBehaviour
{
	private int _nbOfClicks = 0;
	private bool _isLoading = false;

	private Coroutine _loader = null;

	void Awake(){
		GameManager.lookAt = true;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			// Debug.Log("click");
			_nbOfClicks += 1;
		}

		if (_nbOfClicks > 2 && !_isLoading) LoadScene("Sixth Scene"); ;
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