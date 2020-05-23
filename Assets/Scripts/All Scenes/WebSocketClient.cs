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

	// public delegate void MessageAction(WebSocketMessage webSocketMessage);
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
		if (showLog) { Debug.Log("WebSocketClient - OnError : " + e.Message); }
	}

	private void OnMessage(object sender, MessageEventArgs e)
	{
		// if (showLog) Debug.Log(e.Data);

		if (e.Data != "Null" && e.Data != "" && e.Data != null)
		{
			var webSocketMessage = ProcessMessage(e.Data);
			if (showLog)
			{
				/*
					{
						"type":"orientation",
						"message":"{
							\"_x\":-0.6071305695921945,
							\"_y\":-3.003360119444884,
							\"_z\":-0.016295066848099844,
							\"_order\":\"YXZ\
						"}
					"}*/
				// Debug.Log("WebSocketClient - OnMessage : " + e.Data);
				// Debug.Log(e.Data);
				// Debug.Log(webSocketMessage.message);
				// Debug.Log(webSocketMessage.id);
				// Debug.Log(GameManager.name);
				// Debug.Log(WebSocketMessageContentGyro._x);
				// Debug.Log(WebSocke tMessageContentGyro._y * Mathf.Rad2Deg);
				// Debug.Log(WebSocketMessageContentGyro._y);
				// Debug.Log(WebSocketMessageContentGyro._z);
			}


			if (webSocketMessage.type == "orientation")
			{
				GameManager.isPhoneConnected = true;
				var WebSocketMessageContentGyro = ProcessMessageContent(webSocketMessage.message);

				GameManager.gyroAngleX = WebSocketMessageContentGyro._x * Mathf.Rad2Deg;
				GameManager.gyroAngleY = WebSocketMessageContentGyro._y * Mathf.Rad2Deg;
				GameManager.gyroAngleZ = WebSocketMessageContentGyro._z * Mathf.Rad2Deg;
			}

			else if (webSocketMessage.type == "changeScene")
			{
				GameManager.isPhoneConnected = true;
				GameManager.phoneCurrentScene += 1;
			}
		}

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
		if (showLog) Debug.Log($"sending {message.id} {message.type} {message.message}");
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
