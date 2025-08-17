using System;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerPlayerManager : MonoBehaviour
{
	// Token: 0x06001EB0 RID: 7856 RVA: 0x00078BD0 File Offset: 0x00076DD0
	public void OnState(string netId, Vector3 pos, Quaternion rot, Vector3 vel, double remoteTime)
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
			Vector3 vector = vel;
			if (vel.magnitude == 0f && this._lastPositions.ContainsKey(netId) && this._lastUpdateTimes.ContainsKey(netId))
			{
				Vector3 vector2 = this._lastPositions[netId];
				double num = this._lastUpdateTimes[netId];
				double num2 = remoteTime - num;
				if (num2 > 0.0)
				{
					vector = (pos - vector2) / (float)num2;
					Debug.Log(string.Format("[LGG-MP] Player {0} - Calculated velocity: {1:F2} from position change", netId, vector.magnitude));
				}
			}
			this._lastPositions[netId] = pos;
			this._lastUpdateTimes[netId] = remoteTime;
			gameObject.transform.position = pos;
			gameObject.transform.rotation = rot;
			this.UpdateNameTagRotation(gameObject);
			Transform child = gameObject.transform.GetChild(0);
			if (child != null)
			{
				Animator component = child.GetComponent<Animator>();
				if (component != null)
				{
					try
					{
						float magnitude = vector.magnitude;
						if (magnitude > 0.1f)
						{
							Debug.Log(string.Format("[LGG-MP] Player {0} - MOVING: Speed={1:F2}, Velocity={2}", netId, magnitude, vector));
						}
						component.SetFloat("Speed", magnitude);
						component.SetFloat("VerticalSpeed", 0f);
						component.SetBool("Grounded", true);
						if (magnitude > 0.1f)
						{
							component.SetFloat("Angle", 0f);
						}
					}
					catch (Exception ex)
					{
						Debug.LogError("[LGG-MP] Animation update error: " + ex.Message);
					}
				}
			}
		}
		catch (Exception ex2)
		{
			Debug.LogError("[LGG-MP] Failed to update player " + netId + " state: " + ex2.Message);
		}
	}

	// Token: 0x06001EB1 RID: 7857 RVA: 0x00078E08 File Offset: 0x00077008
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
				global::UnityEngine.Object.Destroy(gameObject);
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

	// Token: 0x06001EB2 RID: 7858 RVA: 0x000176A3 File Offset: 0x000158A3
	private void Start()
	{
		this.FindPlayerTemplate();
	}

	// Token: 0x06001EB3 RID: 7859 RVA: 0x00078EC8 File Offset: 0x000770C8
	private void FindPlayerTemplate()
	{
		foreach (GameObject gameObject in global::UnityEngine.Object.FindObjectsOfType<GameObject>())
		{
			if (gameObject.name == "Player (Baby)")
			{
				this._playerTemplate = gameObject;
				Debug.Log("[LGG-MP] Found player template: Player (Baby)");
				break;
			}
		}
		if (this._playerTemplate == null)
		{
			Debug.LogError("[LGG-MP] Could not find Player (Baby) template!");
		}
	}

	// Token: 0x06001EB4 RID: 7860 RVA: 0x00078F2C File Offset: 0x0007712C
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

	// Token: 0x06001EB5 RID: 7861 RVA: 0x00078F64 File Offset: 0x00077164
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
					global::UnityEngine.Object.DestroyImmediate(component);
				}
				catch (Exception ex)
				{
					Debug.LogWarning("Could not remove component " + component.GetType().Name + ": " + ex.Message);
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
				Debug.Log("Gently reset animator movement parameters");
			}
			catch (Exception ex2)
			{
				Debug.LogWarning("Could not reset animator: " + ex2.Message);
			}
		}
		Debug.Log("Cleaned up remote player components");
	}

	// Token: 0x06001EB6 RID: 7862 RVA: 0x00079100 File Offset: 0x00077300
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
					case 1:
						animator.SetFloat(paramName, Convert.ToSingle(value));
						goto IL_0075;
					case 2:
						goto IL_0075;
					case 3:
						animator.SetInteger(paramName, Convert.ToInt32(value));
						goto IL_0075;
					case 4:
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

	// Token: 0x06001EB7 RID: 7863 RVA: 0x00079198 File Offset: 0x00077398
	private void OnDestroy()
	{
		foreach (KeyValuePair<string, GameObject> keyValuePair in this._players)
		{
			if (keyValuePair.Value != null)
			{
				global::UnityEngine.Object.Destroy(keyValuePair.Value);
			}
		}
		this._players.Clear();
		this._playerNames.Clear();
		this._lastPositions.Clear();
		this._lastUpdateTimes.Clear();
	}

	// Token: 0x06001EB8 RID: 7864 RVA: 0x0007922C File Offset: 0x0007742C
	public void EnsurePlayer(string netId, Vector3 initialPos, Quaternion initialRot, string playerName = null)
	{
		if (this._players.ContainsKey(netId))
		{
			Debug.Log("[LGG-MP] Player " + netId + " already exists");
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
			GameObject gameObject = new GameObject("RemotePlayer_" + netId);
			gameObject.transform.position = initialPos;
			gameObject.transform.rotation = initialRot;
			Transform transform = this.FindChildByName(this._playerTemplate.transform, "Heroboy");
			if (transform != null)
			{
				GameObject gameObject2 = global::UnityEngine.Object.Instantiate<GameObject>(transform.gameObject);
				gameObject2.transform.SetParent(gameObject.transform);
				gameObject2.transform.localPosition = Vector3.zero;
				gameObject2.transform.localRotation = Quaternion.identity;
				this.CleanupRemotePlayerComponents(gameObject2);
				string text = ((!string.IsNullOrEmpty(playerName)) ? playerName : ("Gator " + netId));
				this.CreateNameTag(gameObject, text);
				this._players[netId] = gameObject;
				Debug.Log(string.Format("[LGG-MP] Created remote player: {0} ({1}) at {2}", netId, text, initialPos));
			}
			else
			{
				Debug.LogError("[LGG-MP] Could not find Heroboy in player template!");
				global::UnityEngine.Object.Destroy(gameObject);
			}
		}
		catch (Exception ex)
		{
			Debug.LogError("[LGG-MP] Failed to create remote player " + netId + ": " + ex.Message);
		}
	}

	// Token: 0x06001EB9 RID: 7865 RVA: 0x000793B4 File Offset: 0x000775B4
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
			textMesh.anchor = 4;
			textMesh.alignment = 1;
			gameObject2.transform.localScale = Vector3.one * 0.1f;
			Debug.Log("[LGG-MP] Successfully created name tag for: " + playerName);
		}
		catch (Exception ex)
		{
			Debug.LogError("[LGG-MP] Failed to create name tag: " + ex.Message);
		}
	}

	// Token: 0x06001EBA RID: 7866 RVA: 0x000794B8 File Offset: 0x000776B8
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
					camera = global::UnityEngine.Object.FindObjectOfType<Camera>();
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

	private readonly Dictionary<string, GameObject> _players = new Dictionary<string, GameObject>();

	private GameObject _playerTemplate;

	private readonly Dictionary<string, string> _playerNames = new Dictionary<string, string>();

	private readonly Dictionary<string, Vector3> _lastPositions = new Dictionary<string, Vector3>();

	private readonly Dictionary<string, double> _lastUpdateTimes = new Dictionary<string, double>();
}
