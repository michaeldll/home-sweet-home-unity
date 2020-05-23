using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneOneManager : MonoBehaviour
{
	private Coroutine _loader = null;
	private bool _isLoading = false;
	[SerializeField] private Fade fade;
	[SerializeField] private TextMeshProUGUI codeText;
	private WebSocketMessage _message;

	void Awake()
	{
		GameManager.firstScene();
		codeText.SetText($"Access Key: {GameManager.name}");
	}
	
	void Update()
	{
		InvokeRepeating("SendAccessKey", 0, 1);

		if (GameManager.isPhoneConnected && !_isLoading) LoadScene("Second Scene");

		if (Input.GetKey(KeyCode.RightArrow) && !_isLoading) LoadScene("Second Scene");
	}

	void SendAccessKey()
	{
		if (WebSocketClient.Instance != null)
		{
			_message = new WebSocketMessage();
			_message.id = GameManager.name;
			_message.type = "sendAccessKey";
			_message.message = "{\"accessKey\":\"" + GameManager.name + "\"}";

			WebSocketClient.Instance.Send(_message);
		}
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
