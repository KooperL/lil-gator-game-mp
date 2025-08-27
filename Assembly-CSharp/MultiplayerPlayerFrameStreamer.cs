using System;
using System.Collections;
using System.Text;
using UnityEngine;

public class MultiplayerPlayerFrameStreamer : MonoBehaviour
{
	private void Start()
	{
		Debug.Log("[LGG-MP] Initialising multiplayer coroutine");
		base.StartCoroutine(this.SendPositionLoop());
	}

	private IEnumerator SendPositionLoop()
	{
		for (;;)
		{
			this.GetPlayerAnimationData();
			this.GetCurrentHatID();
			this.GetCurrentLeftHandID();
			this.GetCurrentRightHandID();
			string displayName = this.displayName;
			string sessionKey = this.sessionKey;
			WorldState worldState = this.worldState;
			PlayerItemManager.EquippedState equippedState = this.equippedState;
			string hatID = this.hatItemID;
			string leftHandID = this.leftHandItemID;
			string rightHandID = this.rightHandItemID;
			bool attackTrigger = this.attackTrigger;
			bool ragdollTrigger = this.ragdollTrigger;
			Vector3 pos = this.pos;
			Vector3 forward = this.forward;
			float @float = this.speed;
			float float2 = this.verticalSpeed;
			float float3 = this.angle;
			bool @bool = this.grounded;
			bool bool2 = this.climbing;
			bool bool3 = this.swimming;
			bool bool4 = this.gliding;
			bool bool5 = this.sledding;
			string text = JsonUtility.ToJson(new MultiplayerPlayerFrameStreamer.MultiplayerPlayerStateData(pos, forward, displayName, sessionKey, worldState, @float, float2, float3, @bool, bool2, bool3, bool4, bool5, equippedState, attackTrigger, ragdollTrigger, hatID, leftHandID, rightHandID, ""));
			byte[] bytes = Encoding.UTF8.GetBytes(text);
			Debug.Log("[LGG-MP] Sending message");
			yield return MultiplayerCommunicationService.Instance.sendMessage(bytes);
		}
		yield break;
	}

	private void GetPlayerAnimationData()
	{
		this.displayName = MultiplayerConfigLoader.Instance.DisplayName;
		this.sessionKey = MultiplayerConfigLoader.Instance.SessionKey;
		this.worldState = Game.WorldState;
		this.pos = Player.RawPosition;
		this.forward = Player.Forward;
		this.equippedState = Player.itemManager.equippedState;
		this.ragdollTrigger = Player.actor.isRagdolling;
		this.attackTrigger = Player.itemManager.isWeaponAttacking;
		GameObject gameObject = Player.gameObject;
		if (gameObject == null)
		{
			return;
		}
		Transform transform = gameObject.transform.Find("Heroboy");
		if (transform == null)
		{
			return;
		}
		Animator component = transform.GetComponent<Animator>();
		if (!(component == null))
		{
			component.GetCurrentAnimatorStateInfo(0);
			this.speed = component.GetFloat("Speed");
			this.verticalSpeed = component.GetFloat("VerticalSpeed");
			this.angle = component.GetFloat("Angle");
			this.grounded = component.GetBool("Grounded");
			this.climbing = component.GetBool("Climbing");
			this.swimming = component.GetBool("Swimming");
			this.gliding = component.GetBool("Gliding");
			this.sledding = component.GetBool("Sledding");
		}
	}

	private void GetCurrentHatID()
	{
		if (ItemManager.i.HatIndex >= 0 && ItemManager.i.HatIndex < ItemManager.i.items.Length)
		{
			ItemObject hatItem = ItemManager.i.items[ItemManager.i.HatIndex];
			if (hatItem != null && hatItem.IsUnlocked)
			{
				this.hatItemID = hatItem.name;
			}
		}
		this.hatItemID = "";
	}

	private void GetCurrentLeftHandID()
	{
		string itemID = "";
		if (ItemManager.i.ItemIndex >= 0 && ItemManager.i.ItemIndex < ItemManager.i.items.Length)
		{
			ItemObject item = ItemManager.i.items[ItemManager.i.ItemIndex];
			if (item != null && item.IsUnlocked)
			{
				itemID = item.name;
			}
		}
		else if (ItemManager.i.PrimaryIndex >= 0 && ItemManager.i.PrimaryIndex < ItemManager.i.items.Length)
		{
			ItemObject primaryItem = ItemManager.i.items[ItemManager.i.PrimaryIndex];
			if (primaryItem != null && primaryItem.IsUnlocked)
			{
				itemID = primaryItem.name;
			}
		}
		this.leftHandItemID = itemID;
	}

	private void GetCurrentRightHandID()
	{
		string itemID = "";
		if (ItemManager.i.ItemIndex_R >= 0 && ItemManager.i.ItemIndex_R < ItemManager.i.items.Length)
		{
			ItemObject item = ItemManager.i.items[ItemManager.i.ItemIndex_R];
			if (item != null && item.IsUnlocked)
			{
				itemID = item.name;
			}
		}
		else if (ItemManager.i.SecondaryIndex >= 0 && ItemManager.i.SecondaryIndex < ItemManager.i.items.Length)
		{
			ItemObject secondaryItem = ItemManager.i.items[ItemManager.i.SecondaryIndex];
			if (secondaryItem != null && secondaryItem.IsUnlocked)
			{
				itemID = secondaryItem.name;
			}
		}
		this.rightHandItemID = itemID;
	}

	private WaitForSeconds wait = new WaitForSeconds(0.1f);

	private string displayName;

	private string sessionKey;

	private WorldState worldState;

	private PlayerItemManager.EquippedState equippedState;

	private float speed;

	private float verticalSpeed;

	private float angle;

	private bool grounded;

	private bool climbing;

	private bool swimming;

	private bool gliding;

	private bool sledding;

	private string hatItemID;

	private string leftHandItemID;

	private string rightHandItemID;

	private Vector3 pos;

	private Vector3 forward;

	private bool attackTrigger;

	private bool ragdollTrigger;

	[Serializable]
	public class MultiplayerPlayerFrameData
	{
		public MultiplayerPlayerFrameData(Vector3 pos, Vector3 dir, string displayName, string sessionKey, WorldState worldState, float speed, float verticalSpeed, float angle, bool grounded, bool climbing, bool swimming, bool gliding, bool sledding)
		{
			Debug.Log("OLD method used");
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

		public PlayerItemManager.EquippedState equippedState;

		public bool attackTrigger;

		public bool ragdollTrigger;

		public string hatItemID;

		public string leftHandItemID;

		public string rightHandItemID;
	}

	[Serializable]
	public class MultiplayerPlayerStateData
	{
		public MultiplayerPlayerStateData(Vector3 pos, Vector3 dir, string displayName, string sessionKey, WorldState worldState, float speed, float verticalSpeed, float angle, bool grounded, bool climbing, bool swimming, bool gliding, bool sledding, PlayerItemManager.EquippedState equippedState, bool attackTrigger, bool ragdollTrigger, string hatItemID, string leftHandItemID, string rightHandItemID, string animationHash)
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
			this.equippedState = equippedState;
			this.attackTrigger = attackTrigger;
			this.ragdollTrigger = ragdollTrigger;
			this.hatItemID = hatItemID;
			this.leftHandItemID = leftHandItemID;
			this.rightHandItemID = rightHandItemID;
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

		public PlayerItemManager.EquippedState equippedState;

		public bool attackTrigger;

		public bool ragdollTrigger;

		public string hatItemID;

		public string leftHandItemID;

		public string rightHandItemID;

		public string animationHash;
	}
}
