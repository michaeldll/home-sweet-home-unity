using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFourManager : MonoBehaviour
{
	private bool _isLoading = false;

	private Coroutine _loader = null;

	[SerializeField] private Fade fade;

	void Awake()
	{
		if (GameManager.name == "") GameManager.secondScene();
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.RightArrow) && !_isLoading) LoadScene("Fifth Scene"); ;
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