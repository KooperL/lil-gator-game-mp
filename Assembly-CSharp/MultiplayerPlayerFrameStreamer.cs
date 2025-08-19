using System;
using System.Collections;
using System.Text;
using UnityEngine;

public class MultiplayerPlayerFrameStreamer : MonoBehaviour
{
	// Token: 0x06001E9C RID: 7836 RVA: 0x00017609 File Offset: 0x00015809
	private void Start()
	{
		Debug.Log("[LGG-MP] Initialising multiplayer coroutine");
		base.StartCoroutine(this.SendPositionLoop());
	}

	private IEnumerator SendPositionLoop()
	{
		for (;;)
		{
			string displayName = MultiplayerConfigLoader.Instance.DisplayName;
			string sessionKey = MultiplayerConfigLoader.Instance.SessionKey;
			WorldState worldState = Game.WorldState;
			GameObject gameObject = Player.gameObject;
			if (gameObject == null)
			{
				break;
			}
			Transform transform = gameObject.transform.Find("Heroboy");
			if (transform == null)
			{
				goto IL_016A;
			}
			Animator component = transform.GetComponent<Animator>();
			if (!(component == null))
			{
				component.GetCurrentAnimatorStateInfo(0);
				float @float = component.GetFloat("Speed");
				float float2 = component.GetFloat("VerticalSpeed");
				float float3 = component.GetFloat("Angle");
				bool @bool = component.GetBool("Grounded");
				bool bool2 = component.GetBool("Climbing");
				bool bool3 = component.GetBool("Swimming");
				bool bool4 = component.GetBool("Gliding");
				bool bool5 = component.GetBool("Sledding");
				string text = JsonUtility.ToJson(new MultiplayerPlayerFrameStreamer.MultiplayerPlayerFrameData(Player.RawPosition, Player.Forward, displayName, sessionKey, worldState, @float, float2, float3, @bool, bool2, bool3, bool4, bool5));
				byte[] bytes = Encoding.UTF8.GetBytes(text);
				Debug.Log("[LGG-MP] Sending message");
				yield return MultiplayerCommunicationService.Instance.sendMessage(bytes);
			}
		}
		Debug.LogWarning("[LGG-MP] Could not find Player (baby)");
		yield break;
		IL_016A:
		Debug.LogWarning("[LGG-MP] Could not find Heroboy");
		yield break;
	}

	private WaitForSeconds wait = new WaitForSeconds(0.1f);

	[Serializable]
	public class MultiplayerPlayerFrameData
	{
		// Token: 0x06001E9F RID: 7839 RVA: 0x00079464 File Offset: 0x00077664
		public MultiplayerPlayerFrameData(Vector3 pos, Vector3 dir, string displayName, string sessionKey, WorldState worldState, float speed, float verticalSpeed, float angle, bool grounded, bool climbing, bool swimming, bool gliding, bool sledding)
		{
			this.x = pos.x;
			this.y = pos.y;
			this.z = pos.z;
			this.fx = dir.x;
			this.fy = dir.y;
			this.fz = dir.z;
			this.displayName = displayName;
			this.sessionKey = sessionKey;
			this.worldState = worldState;
			this.speed = speed;
			this.verticalSpeed = verticalSpeed;
			this.angle = angle;
			this.grounded = grounded;
			this.climbing = climbing;
			this.swimming = swimming;
			this.gliding = gliding;
			this.sledding = sledding;
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

		public float speed;

		public float verticalSpeed;

		public float angle;

		public bool grounded;

		public bool climbing;

		public bool swimming;

		public bool gliding;

		public bool sledding;
	}
}
