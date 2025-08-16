using System;
using System.Collections;
using System.Text;
using UnityEngine;

public class PlayerPositionStreamer : MonoBehaviour
{
	private void Start()
	{
		Debug.Log("Initialising multiplayer coroutine");
		base.StartCoroutine(this.SendPositionLoop());
	}

	private IEnumerator SendPositionLoop()
	{
		for (;;)
		{
			string json = JsonUtility.ToJson(new PlayerPositionStreamer.PositionData(Player.RawPosition, Player.Forward));
			byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
			yield return MultiplayerCommunicationService.Instance.sendMessage(bodyRaw);
		}
		yield break;
	}

	private const string url = "http://192.168.8.167:8000";

	private WaitForSeconds wait = new WaitForSeconds(0.1f);

	[Serializable]
	public class PositionData
	{
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
