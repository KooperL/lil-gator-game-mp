using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

// Token: 0x020004CF RID: 1231
public class PlayerPositionStreamer : MonoBehaviour
{
	// Token: 0x06001EC2 RID: 7874
	private void Start()
	{
		base.StartCoroutine(this.SendPositionLoop());
	}

	// Token: 0x06001EC3 RID: 7875
	private IEnumerator SendPositionLoop()
	{
		for (;;)
		{
			string json = JsonUtility.ToJson(new PlayerPositionStreamer.PositionData(Player.RawPosition));
			byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
			UnityWebRequest request = new UnityWebRequest("http://192.168.8.167:8000", "POST");
			request.uploadHandler = new UploadHandlerRaw(bodyRaw);
			request.downloadHandler = new DownloadHandlerBuffer();
			request.SetRequestHeader("Content-Type", "application/json");
			yield return request.SendWebRequest();
			if (request.result != UnityWebRequest.Result.Success)
			{
				Debug.LogWarning("Failed to send position: " + request.error);
			}
			yield return this.wait;
			request = null;
		}
		yield break;
	}

	// Token: 0x04002056 RID: 8278
	private const string url = "http://192.168.8.167:8000";

	// Token: 0x04002057 RID: 8279
	private WaitForSeconds wait = new WaitForSeconds(0.1f);

	// Token: 0x020004D1 RID: 1233
	[Serializable]
	private class PositionData
	{
		// Token: 0x06001ECB RID: 7883
		public PositionData(Vector3 pos)
		{
			this.x = pos.x;
			this.y = pos.y;
			this.z = pos.z;
		}

		// Token: 0x0400205C RID: 8284
		public float x;

		// Token: 0x0400205D RID: 8285
		public float y;

		// Token: 0x0400205E RID: 8286
		public float z;
	}
}
