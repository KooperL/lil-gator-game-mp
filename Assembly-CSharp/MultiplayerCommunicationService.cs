using System;
using System.Collections;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class MultiplayerCommunicationService
{
	// Token: 0x06001E8E RID: 7822 RVA: 0x00078108 File Offset: 0x00076308
	public async void initConnection()
	{
		Uri uri = new Uri("ws://" + MultiplayerConfigLoader.Instance.ServerHost);
		using (this._webSocket = new ClientWebSocket())
		{
			await this._webSocket.ConnectAsync(uri, default(CancellationToken));
			Debug.Log("[LGG-MP] WebSocket connected");
			while (this._webSocket.State == 2)
			{
				byte[] bytes = new byte[1024];
				ArraySegment<byte> arraySegment = new ArraySegment<byte>(bytes);
				WebSocketReceiveResult webSocketReceiveResult = await this._webSocket.ReceiveAsync(arraySegment, default(CancellationToken));
				string @string = Encoding.UTF8.GetString(bytes, 0, webSocketReceiveResult.Count);
				Debug.Log("[LGG-MP] Received: " + @string);
				this.receiveMessage(@string);
				bytes = null;
				bytes = null;
				bytes = null;
				bytes = null;
				bytes = null;
				bytes = null;
				bytes = null;
				bytes = null;
				bytes = null;
				bytes = null;
				bytes = null;
				bytes = null;
				bytes = null;
			}
			await this._webSocket.CloseAsync(1000, "Client closed", default(CancellationToken));
		}
		ClientWebSocket clientWebSocket = null;
	}

	// (get) Token: 0x06001E90 RID: 7824 RVA: 0x000175C9 File Offset: 0x000157C9
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

	// Token: 0x06001E91 RID: 7825 RVA: 0x00078140 File Offset: 0x00076340
	public void receiveMessage(string message)
	{
		try
		{
			if (!(MultiplayerNetworkBootstrap.manager == null))
			{
				PlayerPositionStreamer.PositionData positionData = JsonUtility.FromJson<PlayerPositionStreamer.PositionData>(message);
				if (positionData.worldState != Game.WorldState)
				{
					MultiplayerNetworkBootstrap.manager.Despawn(positionData.displayName);
				}
				else
				{
					Vector3 vector = new Vector3(positionData.x, positionData.y, positionData.z);
					Quaternion quaternion = Quaternion.LookRotation(new Vector3(positionData.fx, positionData.fy, positionData.fz));
					Vector3 zero = Vector3.zero;
					double unscaledTimeAsDouble = Time.unscaledTimeAsDouble;
					MultiplayerNetworkBootstrap.manager.EnsurePlayer(positionData.displayName, vector, quaternion, null);
					MultiplayerNetworkBootstrap.manager.OnState(positionData.displayName, vector, quaternion, zero, unscaledTimeAsDouble);
				}
			}
		}
		catch (Exception)
		{
			Debug.LogError("[LGG-MP] Failed to process message");
		}
	}

	// Token: 0x06001E92 RID: 7826 RVA: 0x000175E1 File Offset: 0x000157E1
	public IEnumerator sendMessage(byte[] bytes)
	{
		if (this._webSocket == null)
		{
			Debug.LogWarning("[LGG-MP] WebSocket not instantiated");
			yield break;
		}
		if (this._webSocket.State != 2)
		{
			Debug.LogWarning("[LGG-MP] WebSocket not open");
			yield break;
		}
		ArraySegment<byte> arraySegment = new ArraySegment<byte>(bytes);
		Task sendTask = this._webSocket.SendAsync(arraySegment, 1, true, CancellationToken.None);
		while (!sendTask.IsCompleted)
		{
			yield return null;
		}
		if (sendTask.IsFaulted)
		{
			string text = "[LGG-MP] WebSocket send failed: ";
			AggregateException exception = sendTask.Exception;
			Debug.LogError(text + ((exception != null) ? exception.Message : null));
		}
		else
		{
			Debug.Log("[LGG-MP] Message sent via WebSocket: " + Encoding.UTF8.GetString(bytes));
		}
		yield break;
	}

	private static MultiplayerCommunicationService _instance;

	private ClientWebSocket _webSocket;
}
