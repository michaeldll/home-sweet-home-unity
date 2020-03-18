using System;
using System.Collections;
using UnityEngine;
using WebSocketSharp;

public class WebSocketClient : MonoBehaviour
{
	[SerializeField] private string url = "ws://localhost:8080";
	[SerializeField] private bool showLog = false;

	private int _id;
	private WebSocket _webSocket;

	public static WebSocketClient Instance;

	public delegate void MessageAction(WebSocketMessage webSocketMessage);
	// public static event MessageAction OnMessageReceived;

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

	/*
     * Events
     */

	private void OnOpen(object sender, System.EventArgs e)
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

		GameManager.isPhoneConnected = false;
	}

	private void OnError(object sender, ErrorEventArgs e)
	{
		if (showLog) Debug.Log("WebSocketClient - OnError : " + e.Message);
	}

	private void OnMessage(object sender, MessageEventArgs e)
	{
		var webSocketMessage = ProcessMessage(e.Data);
		var WebSocketMessageContentGyro = ProcessMessageContent(webSocketMessage.message);

		if (showLog)
		{
			// Debug.Log("WebSocketClient - OnMessage : " + e.Data);
			// Debug.Log(webSocketMessage.type);
			// Debug.Log(WebSocketMessageContentGyro.alpha);
			// Debug.Log(WebSocketMessageContentGyro.beta);
			Debug.Log(WebSocketMessageContentGyro.gamma);
		}

		GameManager.isPhoneConnected = true;

		GameManager.gyroAngleX = WebSocketMessageContentGyro.beta;
		GameManager.gyroAngleY = WebSocketMessageContentGyro.gamma;
		GameManager.gyroAngleZ = WebSocketMessageContentGyro.alpha;
	}

	/*
     * Methods
     */
	private WebSocketMessage ProcessMessage(string data)
	{
		return WebSocketMessage.Parse(data);
	}

	private WebSocketMessageContentGyro ProcessMessageContent(string data)
	{
		return WebSocketMessageContentGyro.Parse(data);
	}

	public void Send(WebSocketMessage message)
	{
		if (!_webSocket.IsAlive)
		{
			ConnectClient();
			return;
		}
		Debug.Log("sending");
		_webSocket.Send(CreateMessage(message));
	}

	private byte[] CreateMessage(WebSocketMessage message)
	{
		return message.ToByte();
	}

	private void ConnectClient()
	{
		_webSocket.OnOpen += OnOpen;
		_webSocket.OnClose += OnClose;
		_webSocket.OnError += OnError;
		_webSocket.OnMessage += OnMessage;

		_webSocket.Connect();
	}
}
