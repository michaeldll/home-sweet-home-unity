using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneOneManager : MonoBehaviour
{
	private Coroutine loader = null;
	private bool _isLoading = false;
	[SerializeField] private Fade fade;
	void Update()
	{
		if (GameManager.isPhoneConnected && !_isLoading) LoadScene("Second Scene");

		if (Input.GetKey(KeyCode.RightArrow) && !_isLoading) LoadScene("Second Scene");
	}

	void LoadScene(string name)
	{

		if (loader != null)
		{
			//Debug.LogWarning("Scene load already in progress. Will not load.");
			return;
		}

		_isLoading = true;
		loader = StartCoroutine(AsyncLoader(name));
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
