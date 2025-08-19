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
		Uri uri = new Uri("ws://" + MultiplayerConfigLoader.Instance.ServerHost + "?sessionKey=" + MultiplayerConfigLoader.Instance.SessionKey);
		using (this._webSocket = new ClientWebSocket())
		{
			await this._webSocket.ConnectAsync(uri, default(CancellationToken));
			Debug.Log("[LGG-MP] WebSocket connected");
			while (this._webSocket.State == WebSocketState.Open)
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

	// (get) Token: 0x06001E88 RID: 7816 RVA: 0x00017591 File Offset: 0x00015791
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

	// Token: 0x06001E89 RID: 7817 RVA: 0x00078AE0 File Offset: 0x00076CE0
	public void receiveMessage(string message)
	{
		try
		{
			if (!(MultiplayerNetworkBootstrap.manager == null))
			{
				MultiplayerPlayerFrameStreamer.MultiplayerPlayerFrameData multiplayerPlayerFrameData = JsonUtility.FromJson<MultiplayerPlayerFrameStreamer.MultiplayerPlayerFrameData>(message);
				if (!(multiplayerPlayerFrameData.displayName == MultiplayerConfigLoader.Instance.DisplayName) || !(MultiplayerConfigLoader.Instance.DisplayName != "debug"))
				{
					if (multiplayerPlayerFrameData.worldState != Game.WorldState)
					{
						MultiplayerNetworkBootstrap.manager.Despawn(multiplayerPlayerFrameData.displayName);
					}
					else
					{
						Vector3 vector = new Vector3(multiplayerPlayerFrameData.x, multiplayerPlayerFrameData.y, multiplayerPlayerFrameData.z);
						Quaternion quaternion = Quaternion.LookRotation(new Vector3(multiplayerPlayerFrameData.fx, multiplayerPlayerFrameData.fy, multiplayerPlayerFrameData.fz));
						Vector3 zero = Vector3.zero;
						double unscaledTimeAsDouble = Time.unscaledTimeAsDouble;
						MultiplayerNetworkBootstrap.manager.EnsurePlayer(multiplayerPlayerFrameData.displayName, vector, quaternion, null);
						MultiplayerNetworkBootstrap.manager.OnStateWithAnimation(multiplayerPlayerFrameData.displayName, vector, quaternion, zero, unscaledTimeAsDouble, -1597646531, 0f, multiplayerPlayerFrameData.speed, multiplayerPlayerFrameData.verticalSpeed, multiplayerPlayerFrameData.angle, multiplayerPlayerFrameData.grounded, multiplayerPlayerFrameData.climbing, multiplayerPlayerFrameData.swimming, multiplayerPlayerFrameData.gliding, multiplayerPlayerFrameData.sledding);
					}
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("[LGG-MP] Failed to process message: " + ex.Message);
		}
	}

	// Token: 0x06001E8A RID: 7818 RVA: 0x000175A9 File Offset: 0x000157A9
	public IEnumerator sendMessage(byte[] bytes)
	{
		if (this._webSocket == null)
		{
			Debug.LogWarning("[LGG-MP] Attempted to send message, WebSocket not instantiated");
			yield break;
		}
		if (this._webSocket.State != WebSocketState.Open)
		{
			Debug.LogWarning("[LGG-MP] Attempted to send message, WebSocket was not open");
			yield break;
		}
		ArraySegment<byte> arraySegment = new ArraySegment<byte>(bytes);
		Task sendTask = this._webSocket.SendAsync(arraySegment, WebSocketMessageType.Binary, true, CancellationToken.None);
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
