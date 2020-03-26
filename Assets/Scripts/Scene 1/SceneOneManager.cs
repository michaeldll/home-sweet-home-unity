using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneOneManager : MonoBehaviour
{
	private Coroutine loader = null;
	private bool isLoading = false;
	void Update()
	{
		if (GameManager.isPhoneConnected && !isLoading) LoadScene("Second Scene");
	}

	void LoadScene(string name)
	{

		if (loader != null)
		{
			//Debug.LogWarning("Scene load already in progress. Will not load.");
			return;
		}

		isLoading = true;
		loader = StartCoroutine(AsyncLoader(name));
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
