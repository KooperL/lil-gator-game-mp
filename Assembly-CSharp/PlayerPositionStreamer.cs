using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

// Token: 0x020004CF RID: 1231
public class PlayerPositionStreamer : MonoBehaviour
{
	// Token: 0x06001E62 RID: 7778
	private void Start()
	{
		Debug.Log("Initialising multiplayer coroutine");
		base.StartCoroutine(this.SendPositionLoop());
	}

	// Token: 0x06001E63 RID: 7779
	private IEnumerator SendPositionLoop()
	{
		for (;;)
		{
			string json = JsonUtility.ToJson(new PlayerPositionStreamer.PositionData(Player.RawPosition, Player.Forward));
			byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
			UnityWebRequest request = new UnityWebRequest(MultiplayerConfigLoader.Instance.ServerHost, "POST");
			request.uploadHandler = new UploadHandlerRaw(bodyRaw);
			request.downloadHandler = new DownloadHandlerBuffer();
			request.SetRequestHeader("Content-Type", "application/json");
			yield return request.SendWebRequest();
			if (request.result != UnityWebRequest.Result.Success)
			{
				Debug.LogWarning("Failed to send position: " + request.error);
			}
			else
			{
				PlayerPositionStreamer.PositionData data = JsonUtility.FromJson<PlayerPositionStreamer.PositionData>(request.downloadHandler.text);
				Vector3 position = new Vector3(data.x, data.y, data.z);
				Quaternion rotation = Quaternion.LookRotation(new Vector3(data.fx, data.fy, data.fz));
				GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
				gameObject.transform.position = position;
				gameObject.transform.rotation = rotation;
				gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
				gameObject.name = "RemotePlayer_" + data.displayName;
				gameObject.GetComponent<Renderer>().material.color = Color.green;
			}
			yield return this.wait;
			request = null;
			request = null;
			request = null;
			request = null;
			request = null;
			request = null;
			request = null;
			request = null;
			request = null;
			request = null;
			request = null;
		}
		yield break;
	}

	// Token: 0x04001FE7 RID: 8167
	private const string url = "http://192.168.8.167:8000";

	// Token: 0x04001FE8 RID: 8168
	private WaitForSeconds wait = new WaitForSeconds(0.1f);

	// Token: 0x020004D1 RID: 1233
	[Serializable]
	private class PositionData
	{
		// Token: 0x06001E6B RID: 7787
		public PositionData(Vector3 pos, Vector3 dir)
		{
			this.x = pos.x;
			this.y = pos.y;
			this.z = pos.z;
			this.fx = dir.x;
			this.fy = dir.y;
			this.fz = dir.z;
			this.displayName = MultiplayerConfigLoader.Instance.DisplayName;
			this.sessionKey = MultiplayerConfigLoader.Instance.SessionKey;
			this.worldState = Game.WorldState;
		}

		// Token: 0x04001FED RID: 8173
		public float x;

		// Token: 0x04001FEE RID: 8174
		public float y;

		// Token: 0x04001FEF RID: 8175
		public float z;

		// Token: 0x04001FF0 RID: 8176
		public float fx;

		// Token: 0x04001FF1 RID: 8177
		public float fy;

		// Token: 0x04001FF2 RID: 8178
		public float fz;

		// Token: 0x040020A7 RID: 8359
		public string displayName;

		// Token: 0x040020A8 RID: 8360
		public string sessionKey;

		// Token: 0x04002106 RID: 8454
		public WorldState worldState;
	}
}
