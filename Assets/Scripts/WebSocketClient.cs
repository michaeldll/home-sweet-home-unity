using System;
using System.Collections;
using UnityEngine;
using WebSocketSharp;
using TMPro;

public class WebSocketClient : MonoBehaviour
{
	[SerializeField] private string url = "ws://localhost:8080";
	[SerializeField] private bool showLog = false;
	[SerializeField] private bool isStartScreen = false;
	[SerializeField] private TMP_Text beginScreenText = null;

	private int _id;
	private WebSocket _webSocket;

	public static WebSocketClient Instance;

	public delegate void MessageAction(WebSocketMessage webSocketMessage);
	public static event MessageAction OnMessageReceived;
	public GameObject toGameObject;
	private float _rotateAngleX = 0.0f;
	private bool _isConnected = false;

	private void Awake()
	{
		if (Instance == null)
			Instance = this;
		else
			Destroy(this);

		_id = gameObject.GetInstanceID();

		_webSocket = new WebSocket(url);
		ConnectClient();
	}

	private void ConnectClient()
	{
		_webSocket.OnOpen += OnOpen;
		_webSocket.OnClose += OnClose;
		_webSocket.OnError += OnError;
		_webSocket.OnMessage += OnMessage;

		_webSocket.Connect();
	}

	/*
     * Events
     */

	private void OnOpen(object sender, EventArgs e)
	{
		if (showLog) Debug.Log("WebSocketClient - OnOpen");
	}

	private void OnClose(object sender, CloseEventArgs e)
	{
		if (showLog) Debug.Log("WebSocketClient - OnClose");

		_webSocket.OnOpen -= OnOpen;
		_webSocket.OnClose -= OnClose;
		_webSocket.OnError -= OnError;
		_webSocket.OnMessage -= OnMessage;
	}

	private void OnError(object sender, ErrorEventArgs e)
	{
		if (showLog) Debug.Log("WebSocketClient - OnError : " + e.Message);
	}

	private void OnMessage(object sender, MessageEventArgs e)
	{
		var webSocketMessage = ProcessMessage(e.Data);

		if (showLog)
		{
			Debug.Log("WebSocketClient - OnMessage : " + e.Data);
			Debug.Log(webSocketMessage.type);
			Debug.Log(webSocketMessage.message);
		}
		_isConnected = true;
		_rotateAngleX = webSocketMessage.message;
	}

	void Update()
	{
		if (_isConnected)
			if (!isStartScreen)
			{
				toGameObject.transform.Rotate(_rotateAngleX / 10, 0.0f, 0.0f, Space.Self);
			}
			else StartCoroutine(BeginGame());
	}

	/*
     * Methods
     */
	private WebSocketMessage ProcessMessage(string data)
	{
		return WebSocketMessage.Parse(data);
	}

	private IEnumerator BeginGame()
	{
		beginScreenText.text = "Connected !";
		yield return new WaitForSeconds(1.0f);
		SceneTransitioner.Instance.LoadScene(1);
	}
}
