using UnityEngine;

[System.Serializable]
public class WebSocketMessageContent
{
	public float alpha;
	public float beta;
	public float gamma;

	public static WebSocketMessageContent Parse(string data)
	{
		return JsonUtility.FromJson<WebSocketMessageContent>(data);
	}
}