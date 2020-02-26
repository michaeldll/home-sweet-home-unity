using UnityEngine;

[System.Serializable]
public class WebSocketMessage
{
	public string type;
	public float message;

	public static WebSocketMessage Parse(string data)
	{
		return JsonUtility.FromJson<WebSocketMessage>(data);
	}
}