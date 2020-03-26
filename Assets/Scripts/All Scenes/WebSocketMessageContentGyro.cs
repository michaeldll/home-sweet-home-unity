using UnityEngine;

[System.Serializable]
public class WebSocketMessageContentGyro
{
	public float _x;
	public float _y;
	public float _z;
	public string _order;

	public static WebSocketMessageContentGyro Parse(string data)
	{
		return JsonUtility.FromJson<WebSocketMessageContentGyro>(data);
	}
}