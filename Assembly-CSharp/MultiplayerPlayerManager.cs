using System;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerPlayerManager : MonoBehaviour
{
	// Token: 0x06001E63 RID: 7779 RVA: 0x00077A30 File Offset: 0x00075C30
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

	// Token: 0x06001E64 RID: 7780 RVA: 0x00077C68 File Offset: 0x00075E68
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

	// Token: 0x06001E65 RID: 7781 RVA: 0x0001744C File Offset: 0x0001564C
	private void Start()
	{
		this.FindPlayerTemplate();
	}

	// Token: 0x06001E66 RID: 7782 RVA: 0x00077D28 File Offset: 0x00075F28
	private void FindPlayerTemplate()
	{
		foreach (GameObject gameObject in global::UnityEngine.Object.FindObjectsOfType<GameObject>())
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

	// Token: 0x06001E67 RID: 7783 RVA: 0x00077DA8 File Offset: 0x00075FA8
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

	// Token: 0x06001E68 RID: 7784 RVA: 0x00077DE0 File Offset: 0x00075FE0
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

	// Token: 0x06001E69 RID: 7785 RVA: 0x00077F7C File Offset: 0x0007617C
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

	// Token: 0x06001E6A RID: 7786 RVA: 0x00078014 File Offset: 0x00076214
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

	// Token: 0x06001E6B RID: 7787 RVA: 0x000780A8 File Offset: 0x000762A8
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
			GameObject gameObject = new GameObject("+" + netId);
			gameObject.transform.position = initialPos;
			gameObject.transform.rotation = initialRot;
			Transform transform = this.FindChildByName(this._playerTemplate.transform, "Heroboy");
			if (transform != null)
			{
				GameObject gameObject2 = global::UnityEngine.Object.Instantiate<GameObject>(transform.gameObject);
				gameObject2.transform.SetParent(gameObject.transform);
				gameObject2.transform.localPosition = Vector3.zero;
				gameObject2.transform.localRotation = Quaternion.identity;
				Debug.Log("[LGG-MP] REMOVED FOR TESTING -- this.CleanupRemotePlayerComponents(remoteHeroboy);");
				string text = ((!string.IsNullOrEmpty(playerName)) ? playerName : ("+" + netId));
				this.CreateNameTag(gameObject, text);
				this._players[netId] = gameObject;
				Debug.Log(string.Format("[LGG-MP] Created remote player: {0} ({1}) at {2}", netId, text, initialPos));
				Shader shader = Shader.Find("Sprites/Default");
				if (shader != null)
				{
					Renderer[] componentsInChildren = gameObject2.GetComponentsInChildren<Renderer>();
					if (componentsInChildren.Length != 0)
					{
						foreach (Renderer renderer in componentsInChildren)
						{
							if (renderer != null)
							{
								Material[] materials = renderer.materials;
								Material[] array2 = new Material[materials.Length + 1];
								for (int j = 0; j < materials.Length; j++)
								{
									array2[j] = materials[j];
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
					else
					{
						Debug.Log("[LGG-MP] No renderers found on Heroboy object");
					}
				}
				else
				{
					Debug.Log("[LGG-MP] Shader is null");
				}
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

	// Token: 0x06001E6C RID: 7788 RVA: 0x00078338 File Offset: 0x00076538
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

	// Token: 0x06001E6D RID: 7789 RVA: 0x0007843C File Offset: 0x0007663C
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

	// Token: 0x06001E6E RID: 7790 RVA: 0x000784B8 File Offset: 0x000766B8
	public void OnStateWithAnimation(string netId, Vector3 pos, Quaternion rot, Vector3 vel, double remoteTime, int animStateHash, float animNormalizedTime, float animSpeed, float animVerticalSpeed, float animAngle, bool animGrounded, bool animClimbing, bool animSwimming, bool animGliding, bool animSledding)
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
			Transform child = gameObject.transform.GetChild(0);
			if (child != null)
			{
				Animator component = child.GetComponent<Animator>();
				if (component != null)
				{
					try
					{
						component.SetFloat("Speed", animSpeed);
						component.SetFloat("VerticalSpeed", animVerticalSpeed);
						component.SetFloat("Angle", animAngle);
						component.SetBool("Grounded", animGrounded);
						component.SetBool("Climbing", animClimbing);
						component.SetBool("Swimming", animSwimming);
						component.SetBool("Gliding", animGliding);
						component.SetBool("Sledding", animSledding);
						component.SetBool("Throwing", false);
						component.SetBool("Aiming", false);
						component.Play(animStateHash, 0, animNormalizedTime);
						Debug.Log(string.Format("[LGG-MP] Player {0} - Synced to state {1}, Speed: {2:F2}", netId, animStateHash, animSpeed));
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

	// Token: 0x06001E6F RID: 7791 RVA: 0x00078698 File Offset: 0x00076898
	public void OnStateSimpleAnimation(string netId, Vector3 pos, Quaternion rot, Vector3 vel, double remoteTime, int animStateHash, float animNormalizedTime, float animSpeed)
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
			Transform child = gameObject.transform.GetChild(0);
			if (child != null)
			{
				Animator component = child.GetComponent<Animator>();
				if (component != null)
				{
					try
					{
						component.SetFloat("Speed", animSpeed);
						component.SetBool("Grounded", true);
						component.Play(animStateHash, 0, animNormalizedTime);
						Debug.Log(string.Format("[LGG-MP] Player {0} - Simple sync to state {1}, Speed: {2:F2}", netId, animStateHash, animSpeed));
					}
					catch (Exception ex)
					{
						Debug.LogError("[LGG-MP] Simple animation sync error: " + ex.Message);
					}
				}
			}
		}
		catch (Exception ex2)
		{
			Debug.LogError("[LGG-MP] Failed to simple sync player " + netId + " state: " + ex2.Message);
		}
	}

	// Token: 0x06001E71 RID: 7793 RVA: 0x000787F4 File Offset: 0x000769F4
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

	private readonly Dictionary<string, GameObject> _players = new Dictionary<string, GameObject>();

	private GameObject _playerTemplate;

	private readonly Dictionary<string, string> _playerNames = new Dictionary<string, string>();

	private readonly Dictionary<string, Vector3> _lastPositions = new Dictionary<string, Vector3>();

	private readonly Dictionary<string, double> _lastUpdateTimes = new Dictionary<string, double>();
}
