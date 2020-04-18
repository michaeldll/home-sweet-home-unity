using UnityEngine;
using SimpleMsgPack;

[System.Serializable]
public class WebSocketMessage
{
	public string type;
	public string message;
	public string id;

	public static WebSocketMessage Parse(string data)
	{
		return JsonUtility.FromJson<WebSocketMessage>(data);
	}

	public byte[] ToByte()
	{
		var msgPack = new MsgPack();

		msgPack.ForcePathObject("p.type").AsString = type;
		msgPack.ForcePathObject("p.message").AsString = message;
		msgPack.ForcePathObject("p.id").AsString = id;

		return msgPack.Encode2Bytes();
	}
}