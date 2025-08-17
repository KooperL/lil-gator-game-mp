using System;
using System.Collections;
using System.Text;
using UnityEngine;

public class PlayerPositionStreamer : MonoBehaviour
{
	// Token: 0x06001E62 RID: 7778 RVA: 0x00017442 File Offset: 0x00015642
	private void Start()
	{
		Debug.Log("[LGG-MP] Initialising multiplayer coroutine");
		base.StartCoroutine(this.SendPositionLoop());
	}

	// Token: 0x06001E63 RID: 7779 RVA: 0x0001745B File Offset: 0x0001565B
	private IEnumerator SendPositionLoop()
	{
		for (;;)
		{
			string text = JsonUtility.ToJson(new PlayerPositionStreamer.PositionData(Player.RawPosition, Player.Forward));
			byte[] bytes = Encoding.UTF8.GetBytes(text);
			yield return MultiplayerCommunicationService.Instance.sendMessage(bytes);
		}
		yield break;
	}

	private WaitForSeconds wait = new WaitForSeconds(0.1f);

	[Serializable]
	public class PositionData
	{
		// Token: 0x06001E65 RID: 7781 RVA: 0x00077768 File Offset: 0x00075968
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

		public float x;

		public float y;

		public float z;

		public float fx;

		public float fy;

		public float fz;

		public string displayName;

		public string sessionKey;

		public WorldState worldState;
	}
}
