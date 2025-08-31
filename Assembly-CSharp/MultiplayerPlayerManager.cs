using System;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerPlayerManager : MonoBehaviour
{
	public void Despawn(string netId)
	{
		if (!this._players.ContainsKey(netId))
		{
			Debug.LogWarning("[LGG-MP] Tried to despawn unknown player: " + netId);
			return;
		}
		try
		{
			GameObject gameObject = this._players[netId];
			if (gameObject != null)
			{
				Object.Destroy(gameObject);
				Debug.Log("[LGG-MP] Despawned remote player: " + netId);
			}
			this._players.Remove(netId);
			this._playerNames.Remove(netId);
			this._lastPositions.Remove(netId);
			this._lastUpdateTimes.Remove(netId);
		}
		catch (Exception ex)
		{
			Debug.LogError("[LGG-MP] Failed to despawn player " + netId + ": " + ex.Message);
		}
	}

	private void Start()
	{
		this.FindPlayerTemplate();
	}

	private void FindPlayerTemplate()
	{
		foreach (GameObject gameObject in Object.FindObjectsOfType<GameObject>())
		{
			if (gameObject.name == "Player" || gameObject.name == "Player (Baby)")
			{
				this._playerTemplate = gameObject;
				Debug.Log("[LGG-MP] Found player template: " + gameObject.name);
				break;
			}
		}
		if (this._playerTemplate == null)
		{
			Debug.LogError("[LGG-MP] Could not find template!");
		}
	}

	private Transform FindChildByName(Transform parent, string name)
	{
		for (int i = 0; i < parent.childCount; i++)
		{
			Transform child = parent.GetChild(i);
			if (child.name == name)
			{
				return child;
			}
		}
		return null;
	}

	private void CleanupRemotePlayerComponents(GameObject remoteHeroboy)
	{
		foreach (Component component in new Component[]
		{
			remoteHeroboy.GetComponent<PlayerActorMover>(),
			remoteHeroboy.GetComponent<FootIKSmooth>(),
			remoteHeroboy.GetComponent<HandIK>(),
			remoteHeroboy.GetComponent<Footsteps>(),
			remoteHeroboy.GetComponent<PlayerEffects>(),
			remoteHeroboy.GetComponent<AnimatorHeadTracking>(),
			remoteHeroboy.GetComponent<PlayerActorStates>(),
			remoteHeroboy.GetComponent<PlayerOverrideAnimations>(),
			remoteHeroboy.GetComponent<SyncAnimationToMusic>(),
			remoteHeroboy.GetComponent<DialogueActor>(),
			remoteHeroboy.GetComponent<DialogueActorEnable>()
		})
		{
			if (component != null)
			{
				try
				{
					Object.DestroyImmediate(component);
				}
				catch (Exception ex)
				{
					Debug.LogWarning("[LGG-MP] Could not remove component " + component.GetType().Name + ": " + ex.Message);
				}
			}
		}
		Animator component2 = remoteHeroboy.GetComponent<Animator>();
		if (component2 != null)
		{
			try
			{
				component2.SetFloat("Speed", 0f);
				component2.SetFloat("VerticalSpeed", 0f);
				component2.SetBool("Grounded", true);
				component2.SetBool("Climbing", false);
				component2.SetBool("Swimming", false);
				component2.SetBool("Gliding", false);
				component2.SetBool("Sledding", false);
				component2.SetBool("Throwing", false);
				component2.SetBool("Aiming", false);
				Debug.Log("[LGG-MP] Gently reset animator movement parameters");
			}
			catch (Exception ex2)
			{
				Debug.LogWarning("[LGG-MP] Could not reset animator: " + ex2.Message);
			}
		}
		Debug.Log("[LGG-MP] Cleaned up remote player components");
	}

	private void SetAnimatorParam(Animator animator, string paramName, object value)
	{
		try
		{
			AnimatorControllerParameter[] parameters = animator.parameters;
			int i = 0;
			while (i < parameters.Length)
			{
				AnimatorControllerParameter animatorControllerParameter = parameters[i];
				if (animatorControllerParameter.name == paramName)
				{
					switch (animatorControllerParameter.type)
					{
					case AnimatorControllerParameterType.Float:
						animator.SetFloat(paramName, Convert.ToSingle(value));
						goto IL_0075;
					case (AnimatorControllerParameterType)2:
						goto IL_0075;
					case AnimatorControllerParameterType.Int:
						animator.SetInteger(paramName, Convert.ToInt32(value));
						goto IL_0075;
					case AnimatorControllerParameterType.Bool:
						animator.SetBool(paramName, Convert.ToBoolean(value));
						goto IL_0075;
					default:
						goto IL_0075;
					}
				}
				else
				{
					i++;
				}
			}
			IL_0075:;
		}
		catch (Exception)
		{
		}
	}

	private void OnDestroy()
	{
		foreach (KeyValuePair<string, GameObject> keyValuePair in this._players)
		{
			if (keyValuePair.Value != null)
			{
				Object.Destroy(keyValuePair.Value);
			}
		}
		this._players.Clear();
		this._playerNames.Clear();
		this._lastPositions.Clear();
		this._lastUpdateTimes.Clear();
		this._lastItemStates.Clear();
	}

	public void EnsurePlayer(string netId, Vector3 initialPos, Quaternion initialRot, string playerName = null)
	{
		if (this._players.ContainsKey(netId))
		{
			if (MultiplayerConfigLoader.Instance.logTemp.Contains("B"))
			{
				Debug.Log("[LGG-MP] Player " + netId + " already exists");
			}
			return;
		}
		if (this._playerTemplate == null)
		{
			Debug.LogError("[LGG-MP] No player template available!");
			return;
		}
		try
		{
			if (!string.IsNullOrEmpty(playerName))
			{
				this._playerNames[netId] = playerName;
			}
			GameObject gameObject = new GameObject("+" + netId);
			gameObject.transform.position = initialPos;
			gameObject.transform.rotation = initialRot;
			Transform transform = this.FindChildByName(this._playerTemplate.transform, "Heroboy");
			if (transform != null)
			{
				GameObject gameObject2 = Object.Instantiate<GameObject>(transform.gameObject);
				gameObject2.transform.SetParent(gameObject.transform);
				gameObject2.transform.localPosition = Vector3.zero;
				gameObject2.transform.localRotation = Quaternion.identity;
				DialogueActor dialogueActor = gameObject2.transform.GetComponent<DialogueActor>();
				if (dialogueActor)
				{
					dialogueActor.isPlayer = false;
				}
				this.CleanupRemotePlayerComponents(gameObject2);
				string text = ((!string.IsNullOrEmpty(playerName)) ? playerName : ("+" + netId));
				this.CreateNameTag(gameObject, text);
				this._players[netId] = gameObject;
				Debug.Log(string.Format("[LGG-MP] Created remote player: {0} ({1}) at {2}", netId, text, initialPos));
				this.ApplyRemotePlayerVisualEffects(gameObject2);
			}
			else
			{
				Debug.LogError("[LGG-MP] Could not find Heroboy in player template!");
				Object.Destroy(gameObject);
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("[LGG-MP] Failed to create remote player " + netId + ": " + ex.Message);
		}
	}

	private void CreateNameTag(GameObject player, string playerName)
	{
		try
		{
			Debug.Log("[LGG-MP] Creating 3D text name tag for: " + playerName);
			GameObject gameObject = new GameObject("NameTag");
			gameObject.transform.SetParent(player.transform);
			gameObject.transform.localPosition = new Vector3(0f, 2.5f, 0f);
			GameObject gameObject2 = new GameObject("NameText");
			gameObject2.transform.SetParent(gameObject.transform);
			gameObject2.transform.localPosition = Vector3.zero;
			TextMesh textMesh = gameObject2.AddComponent<TextMesh>();
			textMesh.text = playerName;
			textMesh.fontSize = 20;
			textMesh.color = Color.white;
			textMesh.anchor = TextAnchor.MiddleCenter;
			textMesh.alignment = TextAlignment.Center;
			gameObject2.transform.localScale = Vector3.one * 0.1f;
			Debug.Log("[LGG-MP] Successfully created name tag for: " + playerName);
		}
		catch (Exception ex)
		{
			Debug.LogError("[LGG-MP] Failed to create name tag: " + ex.Message);
		}
	}

	private void UpdateNameTagRotation(GameObject player)
	{
		try
		{
			Transform transform = player.transform.Find("NameTag");
			if (transform != null)
			{
				Camera camera = Camera.main;
				if (camera == null)
				{
					camera = Object.FindObjectOfType<Camera>();
				}
				if (camera != null)
				{
					transform.LookAt(camera.transform);
					transform.Rotate(0f, 180f, 0f);
				}
			}
		}
		catch (Exception)
		{
		}
	}

	public void PurgeAllMultiplayerActivity()
	{
		foreach (string text in new List<string>(this._players.Keys))
		{
			this.Despawn(text);
		}
		this._players.Clear();
		this._playerNames.Clear();
		this._lastPositions.Clear();
		this._lastUpdateTimes.Clear();
		Debug.Log("[LGG-MP] All multiplayer activity has been completely purged.");
	}

	public void OnStateUpdate(string netId, Vector3 pos, Quaternion rot, Vector3 vel, double remoteTime, int animStateHash, float animNormalizedTime, float animSpeed, float animVerticalSpeed, float animAngle, bool animGrounded, bool animClimbing, bool animSwimming, bool animGliding, bool animSledding, PlayerItemManager.EquippedState equippedState, bool attackTrigger, bool ragdollTrigger, string hatItemID, string leftHandItemID, string rightHandItemID)
	{
		if (!this._players.ContainsKey(netId))
		{
			Debug.LogWarning("[LGG-MP] Received state for unknown player: " + netId);
			return;
		}
		GameObject gameObject = this._players[netId];
		if (gameObject == null)
		{
			Debug.LogWarning("[LGG-MP] Player object is null for: " + netId);
			this._players.Remove(netId);
			return;
		}
		try
		{
			this._lastPositions[netId] = pos;
			this._lastUpdateTimes[netId] = remoteTime;
			gameObject.transform.position = pos;
			gameObject.transform.rotation = rot;
			this.UpdateNameTagRotation(gameObject);
			MultiplayerPlayerManager.PlayerAnimationState playerAnimationState;
			if (animGliding)
			{
				playerAnimationState = MultiplayerPlayerManager.PlayerAnimationState.Gliding;
			}
			else if (animSwimming)
			{
				playerAnimationState = MultiplayerPlayerManager.PlayerAnimationState.Swimming;
			}
			else if (animSledding)
			{
				playerAnimationState = MultiplayerPlayerManager.PlayerAnimationState.Sledding;
			}
			else if (animClimbing)
			{
				playerAnimationState = MultiplayerPlayerManager.PlayerAnimationState.Climbing;
			}
			else
			{
				playerAnimationState = MultiplayerPlayerManager.PlayerAnimationState.Ragdoll;
			}
			this.UpdateRemotePlayerItemsSimple(gameObject, hatItemID, leftHandItemID, rightHandItemID, netId, playerAnimationState);
			Transform child = gameObject.transform.GetChild(0);
			if (child != null)
			{
				Animator animator = child.GetComponent<Animator>();
				if (animator != null)
				{
					try
					{
						animator.SetFloat("Speed", animSpeed);
						animator.SetFloat("VerticalSpeed", animVerticalSpeed);
						animator.SetFloat("Angle", animAngle);
						animator.SetBool("Grounded", animGrounded);
						animator.SetBool("Climbing", animClimbing);
						animator.SetBool("Swimming", animSwimming);
						animator.SetBool("Gliding", animGliding);
						animator.SetBool("Sledding", animSledding);
						animator.SetBool("Throwing", false);
						animator.SetBool("Aiming", false);
						if (attackTrigger)
						{
							try
							{
								animator.SetTrigger("Attack");
							}
							catch
							{
								Debug.Log("[LGG-MP] Attack trigger not found on remote player animator");
							}
						}
						if (animStateHash != 0)
						{
							animator.Play(animStateHash, 0, animNormalizedTime);
						}
					}
					catch (Exception ex)
					{
						Debug.LogError("[LGG-MP] Animation sync error: " + ex.Message);
					}
				}
			}
		}
		catch (Exception ex2)
		{
			Debug.LogError("[LGG-MP] Failed to sync player " + netId + " state: " + ex2.Message);
		}
	}

	private void SpawnItemOnRemotePlayer(string itemID, Transform anchor, string itemName)
	{
		if (anchor == null)
		{
			Debug.LogWarning("[LGG-MP] Anchor is null for item: " + itemID);
			return;
		}
		try
		{
			ItemObject item = ItemManager.i.itemDic[itemID];
			if (item != null && item.prefab != null)
			{
				Transform existingItem = anchor.Find(itemName);
				if (existingItem != null)
				{
					Object.Destroy(existingItem.gameObject);
				}
				GameObject gameObject = Object.Instantiate<GameObject>(item.prefab, anchor);
				gameObject.name = itemName;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localRotation = Quaternion.identity;
				IItemBehaviour behaviour = gameObject.GetComponent<IItemBehaviour>();
				if (behaviour != null)
				{
					Object.DestroyImmediate((Component)behaviour);
				}
				if (MultiplayerConfigLoader.Instance.logTemp.Contains("B"))
				{
					Debug.Log("[LGG-MP] Spawned item: " + itemID + " at anchor: " + anchor.name);
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("[LGG-MP] Failed to spawn item " + itemID + ": " + ex.Message);
		}
	}

	private Transform FindTransformRecursive(Transform parent, string name)
	{
		if (parent.name.Contains(name))
		{
			return parent;
		}
		for (int i = 0; i < parent.childCount; i++)
		{
			Transform result = this.FindTransformRecursive(parent.GetChild(i), name);
			if (result != null)
			{
				return result;
			}
		}
		return null;
	}

	private void ApplyRemotePlayerVisualEffects(GameObject playerObject)
	{
		Shader shader = Shader.Find("Sprites/Default");
		if (shader != null)
		{
			Renderer[] componentsInChildren = playerObject.GetComponentsInChildren<Renderer>();
			if (componentsInChildren.Length != 0)
			{
				foreach (Renderer renderer in componentsInChildren)
				{
					if (renderer != null)
					{
						Material[] materials = renderer.materials;
						Material[] array2 = new Material[materials.Length + 1];
						for (int i = 0; i < materials.Length; i++)
						{
							array2[i] = materials[i];
						}
						Material material = new Material(shader);
						material.color = new Color(1f, 1f, 1f, 0.3f);
						material.renderQueue = 3001;
						array2[array2.Length - 1] = material;
						renderer.materials = array2;
						Debug.Log("[LGG-MP] Applied white overlay to renderer: " + renderer.name);
					}
				}
			}
		}
	}

	private void SetupRemotePlayerItems(GameObject remotePlayer, string leftHandItemID, string rightHandItemID, string hatItemID, MultiplayerPlayerManager.PlayerAnimationState playerAnimationState)
	{
		if (ItemManager.i == null)
		{
			Debug.LogWarning("[LGG-MP] ItemManager.i is null, cannot setup remote player items");
			return;
		}
		try
		{
			Transform heroboy = remotePlayer.transform.GetChild(0);
			if (heroboy == null)
			{
				Debug.LogWarning("[LGG-MP] Could not find Heroboy child");
			}
			else
			{
				if (playerAnimationState == MultiplayerPlayerManager.PlayerAnimationState.Sledding)
				{
					this.FindTransformRecursive(heroboy, "ShieldSledAnchor");
				}
				else if (leftHandItemID.ToLower().Contains("shield"))
				{
					this.FindTransformRecursive(heroboy, "ShieldArmAnchor");
				}
				else
				{
					this.FindTransformRecursive(heroboy, "HandAnchor.L");
				}
				Transform leftHandAnchor = this.FindTransformRecursive(heroboy, "HandAnchor.L");
				if (leftHandAnchor == null)
				{
					Debug.LogWarning("[LGG-MP] Left hand anchor is null");
				}
				Transform rightHandAnchor = this.FindTransformRecursive(heroboy, "HandAnchor.R");
				if (rightHandAnchor == null)
				{
					Debug.LogWarning("[LGG-MP] Right hand anchor is null");
				}
				Transform hatAnchor = this.FindTransformRecursive(heroboy, "Head");
				if (hatAnchor == null)
				{
					Debug.LogWarning("[LGG-MP] Hat anchor is null");
				}
				if (!string.IsNullOrEmpty(leftHandItemID) && ItemManager.i.itemDic.ContainsKey(leftHandItemID))
				{
					if (MultiplayerConfigLoader.Instance.logTemp.Contains("B"))
					{
						Debug.Log("[LGG-MP] Spawning " + leftHandItemID + " on " + leftHandAnchor.name);
					}
					this.SpawnItemOnRemotePlayer(leftHandItemID, leftHandAnchor, "LeftHandItem");
				}
				if (!string.IsNullOrEmpty(rightHandItemID) && ItemManager.i.itemDic.ContainsKey(rightHandItemID))
				{
					if (MultiplayerConfigLoader.Instance.logTemp.Contains("B"))
					{
						Debug.Log("[LGG-MP] Spawning " + rightHandItemID + " on " + rightHandAnchor.name);
					}
					this.SpawnItemOnRemotePlayer(rightHandItemID, rightHandAnchor, "RightHandItem");
				}
				if (!string.IsNullOrEmpty(hatItemID) && ItemManager.i.itemDic.ContainsKey(hatItemID))
				{
					if (MultiplayerConfigLoader.Instance.logTemp.Contains("B"))
					{
						Debug.Log("[LGG-MP] Spawning " + hatItemID + " on " + hatAnchor.name);
					}
					this.SpawnItemOnRemotePlayer(hatItemID, hatAnchor, "HatItem");
				}
				Debug.Log("[LGG-MP] Successfully setup remote player items");
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("[LGG-MP] Failed to setup remote player items: " + ex.Message);
		}
	}

	private void UpdateRemotePlayerItemsSimple(GameObject remotePlayer, string hatItemID, string leftHandItemID, string rightHandItemID, string netId, MultiplayerPlayerManager.PlayerAnimationState playerAnimationState)
	{
		if (MultiplayerConfigLoader.Instance.logTemp.Contains("B"))
		{
			Debug.Log("[LGG-MP] Updating items for " + netId);
		}
		if (!this._lastItemStates.ContainsKey(netId))
		{
			this._lastItemStates[netId] = new MultiplayerPlayerManager.RemotePlayerItemState();
		}
		MultiplayerPlayerManager.RemotePlayerItemState lastState = this._lastItemStates[netId];
		if (!(lastState.hatItemID != hatItemID) && !(lastState.leftHandItemID != leftHandItemID) && !(lastState.rightHandItemID != rightHandItemID))
		{
			if (MultiplayerConfigLoader.Instance.logTemp.Contains("B"))
			{
				Debug.Log("[LGG-MP] Early return for " + netId + " when updating items");
			}
			return;
		}
		try
		{
			this.SetupRemotePlayerItems(remotePlayer, leftHandItemID, rightHandItemID, hatItemID, playerAnimationState);
			lastState.hatItemID = hatItemID;
			lastState.leftHandItemID = leftHandItemID;
			lastState.rightHandItemID = rightHandItemID;
			Debug.Log(string.Format("[LGG-MP] Updated items for player {0}: Hat={1}, Left={2}, Right={3}", new object[] { netId, hatItemID, leftHandItemID, rightHandItemID }));
		}
		catch (Exception ex)
		{
			Debug.LogError("[LGG-MP] Failed to update remote player items: " + ex.Message);
		}
	}

	private readonly Dictionary<string, GameObject> _players = new Dictionary<string, GameObject>();

	private GameObject _playerTemplate;

	public readonly Dictionary<string, string> _playerNames = new Dictionary<string, string>();

	private readonly Dictionary<string, Vector3> _lastPositions = new Dictionary<string, Vector3>();

	private readonly Dictionary<string, double> _lastUpdateTimes = new Dictionary<string, double>();

	private readonly Dictionary<string, MultiplayerPlayerManager.RemotePlayerItemState> _lastItemStates = new Dictionary<string, MultiplayerPlayerManager.RemotePlayerItemState>();

	private class RemotePlayerItemState
	{
		public string hatItemID = "";

		public string leftHandItemID = "";

		public string rightHandItemID = "";
	}

	public enum PlayerAnimationState
	{
		Gliding,
		Swimming,
		Sledding,
		Climbing,
		Ragdoll
	}
}
