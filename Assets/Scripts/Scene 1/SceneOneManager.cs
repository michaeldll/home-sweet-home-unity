using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneOneManager : MonoBehaviour
{
	private Coroutine _loader = null;
	private bool _isLoading = false;
	[SerializeField] private Fade fade;
	[SerializeField] private TextMeshProUGUI codeText;
	[SerializeField] private TextMeshProUGUI dotsText;
	[SerializeField] private GameObject codeTextBorder;
	[SerializeField] private TextMeshProUGUI subtitleText;
	[SerializeField] private TextMeshProUGUI connectionText;
	[SerializeField] private GameObject dotsGameObject;
	[SerializeField] private GameObject phoneLoadedGameObject;
	private string[] _dots = { ".", "..", "..." };
	private int _dotsIndex = 0;
	private int _countdownNumber = 10;
	private bool _isPreLoading;
	private WebSocketMessage _message;

	void Awake()
	{
		GameManager.firstScene();
		codeText.SetText($"{GameManager.name}");
		SetDotsText();
		InvokeRepeating("SendAccessKey", 0, 1);
		InvokeRepeating("AnimateDots", 0.75f, 0.75f);
	}

	void Update()
	{
		if (GameManager.isPhoneConnected && !_isPreLoading) SetPreLoadingScreen();

		if (Input.GetKey(KeyCode.RightArrow) && !_isPreLoading) SetPreLoadingScreen();
	}

	void SetPreLoadingScreen()
	{
		_isPreLoading = true;
		codeTextBorder.SetActive(false);
		dotsGameObject.SetActive(false);
		phoneLoadedGameObject.SetActive(true);
		subtitleText.SetText("The experience is starting");
		connectionText.SetText("Connected");
		codeText.SetText(_countdownNumber.ToString());
		InvokeRepeating("SetCountdown", 1, 1);
		Invoke("LoadSecondScene", 10f);
	}

	void LoadSecondScene()
	{
		LoadScene("Second Scene");
	}

	void SetCountdown()
	{
		if (_countdownNumber > 0) _countdownNumber = _countdownNumber - 1;
		codeText.SetText(_countdownNumber.ToString());
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
	void AnimateDots()
	{
		if (_dotsIndex < 2)
		{
			_dotsIndex += 1;
		}
		else
		{
			_dotsIndex = 0;
		}

		SetDotsText();
	}
	void SetDotsText()
	{
		dotsText.SetText(_dots[_dotsIndex]);
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
