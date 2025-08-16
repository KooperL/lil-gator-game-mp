using System;
using System.Collections;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class MultiplayerCommunicationService
{
	public async void initConnection()
	{
		Uri uri = new Uri("ws://192.168.8.167:8000");
		using (this._webSocket = new ClientWebSocket())
		{
			await this._webSocket.ConnectAsync(uri, default(CancellationToken));
			Debug.Log("WebSocket connected.");
			while (this._webSocket.State == WebSocketState.Open)
			{
				byte[] bytes = new byte[1024];
				ArraySegment<byte> segment = new ArraySegment<byte>(bytes);
				WebSocketReceiveResult result = await this._webSocket.ReceiveAsync(segment, default(CancellationToken));
				string message = Encoding.UTF8.GetString(bytes, 0, result.Count);
				Debug.Log("Received: " + message);
				this.receiveMessage(message);
				bytes = null;
				bytes = null;
				bytes = null;
				bytes = null;
				bytes = null;
				bytes = null;
			}
			await this._webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client closed", default(CancellationToken));
		}
		ClientWebSocket clientWebSocket = null;
	}

	// (get) Token: 0x06001EB4 RID: 7860
	public static MultiplayerCommunicationService Instance
	{
		get
		{
			if (MultiplayerCommunicationService._instance == null)
			{
				MultiplayerCommunicationService._instance = new MultiplayerCommunicationService();
			}
			return MultiplayerCommunicationService._instance;
		}
	}

	public void receiveMessage(string message)
	{
		PlayerPositionStreamer.PositionData positionData = JsonUtility.FromJson<PlayerPositionStreamer.PositionData>(message);
		Vector3 vector = new Vector3(positionData.x, positionData.y, positionData.z);
		Quaternion quaternion = Quaternion.LookRotation(new Vector3(positionData.fx, positionData.fy, positionData.fz));
		GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
		gameObject.transform.position = vector;
		gameObject.transform.rotation = quaternion;
		gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		gameObject.name = "RemotePlayer_" + positionData.displayName;
		gameObject.GetComponent<Renderer>().material.color = Color.green;
	}

	public IEnumerator sendMessage(byte[] bytes)
	{
		if (this._webSocket == null)
		{
			Debug.LogWarning("WebSocket not instantiated.");
			yield break;
		}
		if (this._webSocket.State != WebSocketState.Open)
		{
			Debug.LogWarning("WebSocket not open.");
			yield break;
		}
		ArraySegment<byte> buffer = new ArraySegment<byte>(bytes);
		Task sendTask = this._webSocket.SendAsync(buffer, WebSocketMessageType.Binary, true, CancellationToken.None);
		while (!sendTask.IsCompleted)
		{
			yield return null;
		}
		if (sendTask.IsFaulted)
		{
			string text = "WebSocket send failed: ";
			AggregateException exception = sendTask.Exception;
			Debug.LogError(text + ((exception != null) ? exception.Message : null));
		}
		else
		{
			Debug.Log("Message sent via WebSocket.");
		}
		yield break;
	}

	private static MultiplayerCommunicationService _instance;

	private ClientWebSocket _webSocket;
}
