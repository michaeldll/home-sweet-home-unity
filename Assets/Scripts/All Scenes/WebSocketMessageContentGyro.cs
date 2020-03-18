using UnityEngine;

[System.Serializable]
public class WebSocketMessageContentGyro
{
	public float alpha;
	public float beta;
	public float gamma;

	public static WebSocketMessageContentGyro Parse(string data)
	{
		return JsonUtility.FromJson<WebSocketMessageContentGyro>(data);
	}
}