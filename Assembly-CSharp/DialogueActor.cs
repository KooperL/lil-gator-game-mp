using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000D9 RID: 217
public class DialogueActor : MonoBehaviour
{
	// Token: 0x06000397 RID: 919 RVA: 0x00026460 File Offset: 0x00024660
	private static string PositionString(ActorPosition position)
	{
		switch (position)
		{
		case ActorPosition.P_Standing:
			return "";
		case ActorPosition.P_Sitting:
			return "Sitting|";
		case ActorPosition.P_Seated:
			return "Seated|";
		case ActorPosition.P_Laying:
			return "Laying|";
		case ActorPosition.P_Prone:
			return "Prone|";
		case ActorPosition.P_InSwing:
			return "InSwing|";
		default:
			return "";
		}
	}

	// Token: 0x06000398 RID: 920 RVA: 0x000264B8 File Offset: 0x000246B8
	private static string StateString(ActorState state)
	{
		switch (state)
		{
		case ActorState.S_Neutral:
			return "Neutral";
		case ActorState.S_Happy:
			return "Happy";
		case ActorState.S_Sad:
			return "Sad";
		case ActorState.S_Scared:
			return "Scared";
		case ActorState.S_Cool:
			return "Cool";
		case ActorState.S_Excited:
			return "Excited";
		case ActorState.S_Busy:
			return "Busy";
		case ActorState.S_Tense:
			return "Tense";
		case ActorState.S_Annoyed:
			return "Annoyed";
		case ActorState.S_Nervous:
			return "Nervous";
		case ActorState.S_Antsy:
			return "Antsy";
		default:
			return "";
		}
	}

	// Token: 0x06000399 RID: 921 RVA: 0x00026540 File Offset: 0x00024740
	private static int GetPositionStateHash(int position, int state, Animator animator, int stateLayer)
	{
		if (DialogueActor.positionStateHashes[position, state] != 0)
		{
			return DialogueActor.positionStateHashes[position, state];
		}
		int num = Animator.StringToHash(DialogueActor.PositionString((ActorPosition)position) + DialogueActor.StateString((ActorState)state));
		if (!animator.HasState(stateLayer, num))
		{
			num = Animator.StringToHash(DialogueActor.PositionString((ActorPosition)position) + DialogueActor.StateString(ActorState.S_Neutral));
		}
		DialogueActor.positionStateHashes[position, state] = num;
		return num;
	}

	// Token: 0x0600039A RID: 922 RVA: 0x000265B0 File Offset: 0x000247B0
	private static string GetTransitionName(int oldPosition, int newPosition)
	{
		if (oldPosition == 0 && newPosition == 1)
		{
			return "Sit Down";
		}
		if (oldPosition == 1 && newPosition == 0)
		{
			return "Stand Up";
		}
		if (oldPosition == 0 && newPosition == 2)
		{
			return "SeatHopIn";
		}
		if (oldPosition == 2 && newPosition == 0)
		{
			return "SeatHopOut";
		}
		if (oldPosition == 0 && newPosition == 5)
		{
			return "SeatHopIn";
		}
		if (oldPosition == 5 && newPosition == 0)
		{
			return "SeatHopOut";
		}
		if (oldPosition == 0 && newPosition == 3)
		{
			return "E_Pratfall";
		}
		return "";
	}

	// Token: 0x0600039B RID: 923 RVA: 0x00026624 File Offset: 0x00024824
	private static int GetTransitionHash(int oldPosition, int newPosition)
	{
		if (DialogueActor.transitionHashes[oldPosition, newPosition] == 0)
		{
			string transitionName = DialogueActor.GetTransitionName(oldPosition, newPosition);
			if (string.IsNullOrEmpty(transitionName))
			{
				DialogueActor.transitionHashes[oldPosition, newPosition] = -1;
			}
			else
			{
				DialogueActor.transitionHashes[oldPosition, newPosition] = Animator.StringToHash(transitionName);
			}
		}
		return DialogueActor.transitionHashes[oldPosition, newPosition];
	}

	// Token: 0x0600039C RID: 924 RVA: 0x00004BE9 File Offset: 0x00002DE9
	private static GameObject GetStateEffect(ActorState state)
	{
		if (state == ActorState.S_Nervous)
		{
			return EffectsManager.e.sweatPrefab;
		}
		return null;
	}

	// Token: 0x1700003C RID: 60
	// (get) Token: 0x0600039D RID: 925 RVA: 0x00004BFC File Offset: 0x00002DFC
	public Transform DialogueAnchor
	{
		get
		{
			if (this.customAnchorPoint)
			{
				return this.customAnchorTransform;
			}
			if (this.head != null)
			{
				return this.head;
			}
			return base.transform;
		}
	}

	// Token: 0x1700003D RID: 61
	// (get) Token: 0x0600039E RID: 926 RVA: 0x00004C28 File Offset: 0x00002E28
	public Vector3 FocusPosition
	{
		get
		{
			return base.transform.TransformPoint(this.localFocusPosition);
		}
	}

	// Token: 0x1700003E RID: 62
	// (get) Token: 0x0600039F RID: 927 RVA: 0x00004C3B File Offset: 0x00002E3B
	// (set) Token: 0x060003A0 RID: 928 RVA: 0x00004C43 File Offset: 0x00002E43
	public bool LookAt
	{
		get
		{
			return this.lookAt;
		}
		set
		{
			this.lookAt = value;
			if (value)
			{
				base.enabled = true;
			}
		}
	}

	// Token: 0x1700003F RID: 63
	// (get) Token: 0x060003A1 RID: 929 RVA: 0x00004C56 File Offset: 0x00002E56
	// (set) Token: 0x060003A2 RID: 930 RVA: 0x00004C5E File Offset: 0x00002E5E
	public bool LookAtDialogue
	{
		get
		{
			return this.lookAtDialogue;
		}
		set
		{
			this.lookAtDialogue = value;
			if (value)
			{
				base.enabled = true;
				return;
			}
			if (this.delayedSetLookCoroutine != null)
			{
				base.StopCoroutine(this.delayedSetLookCoroutine);
			}
		}
	}

	// Token: 0x060003A3 RID: 931 RVA: 0x00004C86 File Offset: 0x00002E86
	public void ProximityTrigger()
	{
		this.lastProximityTime = Time.time;
	}

	// Token: 0x17000040 RID: 64
	// (get) Token: 0x060003A4 RID: 932 RVA: 0x00004C93 File Offset: 0x00002E93
	public bool IsInDialogue
	{
		get
		{
			return this.IsInStandardDialogue || this.IsInBubbleDialogue;
		}
	}

	// Token: 0x17000041 RID: 65
	// (get) Token: 0x060003A5 RID: 933 RVA: 0x00004CA5 File Offset: 0x00002EA5
	// (set) Token: 0x060003A6 RID: 934 RVA: 0x00004CAD File Offset: 0x00002EAD
	public bool IsInStandardDialogue
	{
		get
		{
			return this.isInStandardDialogue;
		}
		set
		{
			if (value == this.isInStandardDialogue)
			{
				return;
			}
			if (value && !this.IsInDialogue)
			{
				this.onEnterDialogue.Invoke();
			}
			if (!value && this.IsInDialogue)
			{
				this.onExitDialogue.Invoke();
			}
			this.isInStandardDialogue = value;
		}
	}

	// Token: 0x17000042 RID: 66
	// (get) Token: 0x060003A7 RID: 935 RVA: 0x00004CEC File Offset: 0x00002EEC
	// (set) Token: 0x060003A8 RID: 936 RVA: 0x00004CF4 File Offset: 0x00002EF4
	public bool IsInBubbleDialogue
	{
		get
		{
			return this.isInBubbleDialogue;
		}
		set
		{
			if (value == this.isInBubbleDialogue)
			{
				return;
			}
			if (value && !this.IsInDialogue)
			{
				this.onEnterDialogue.Invoke();
			}
			if (!value && this.IsInDialogue)
			{
				this.onExitDialogue.Invoke();
			}
			this.isInBubbleDialogue = value;
		}
	}

	// Token: 0x060003A9 RID: 937 RVA: 0x0002667C File Offset: 0x0002487C
	[ContextMenu("Snap To Floor")]
	public void SnapToFloor()
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position + Vector3.up, Vector3.down, ref raycastHit, 3f, LayerMask.GetMask(new string[] { "Terrain", "Default" })))
		{
			base.transform.position = raycastHit.point;
		}
	}

	// Token: 0x060003AA RID: 938 RVA: 0x00002229 File Offset: 0x00000429
	[ContextMenu("Reset Bones")]
	public void ResetBones()
	{
	}

	// Token: 0x060003AB RID: 939 RVA: 0x000266E0 File Offset: 0x000248E0
	private void Awake()
	{
		this.awakeTime = Time.time;
		if (this.isPlayer)
		{
			DialogueActor.playerActor = this;
		}
		if (this.leftEye != null)
		{
			this.eyeSprite = this.leftEye.sprite;
		}
		if (this.customFocus)
		{
			this.localFocusPosition = this.customFocusPosition;
		}
		else if (this.head != null)
		{
			this.localFocusPosition = base.transform.InverseTransformPoint(this.head.position);
		}
		else
		{
			this.localFocusPosition = Vector3.up;
		}
		this.waitForEndOfFrame = new WaitForEndOfFrame();
		if (this.head != null)
		{
			this.initialHeadRotation = this.head.localRotation;
		}
		if (this.jaw != null)
		{
			this.initialJawRotation = this.jaw.localRotation;
		}
		if (this.chest != null)
		{
			this.initialChestRotation = this.chest.localRotation;
		}
		if (this.lowerSpine != null)
		{
			this.initialLowerSpineRotation = this.lowerSpine.localRotation;
		}
		base.gameObject.AddComponent<DialogueActorEnable>().dialogueActor = this;
	}

	// Token: 0x060003AC RID: 940 RVA: 0x0002680C File Offset: 0x00024A0C
	private void OnEnable()
	{
		if (this.animatorIKHook != null)
		{
			this.animatorIKHook.enabled = true;
		}
		if (this.isPlayer)
		{
			DialogueActor.playerActor = this;
		}
		this.GetAnimatorLayers();
		this.UpdateActorState(0.25f);
		if (this.animator != null)
		{
			this.isAnimatorEnabled = this.animator.enabled;
		}
		if (this.onEnable != null)
		{
			this.onEnable.Invoke();
		}
		if (!this.isPlayer && !this.ignoreUnlock && this.profile != null && !this.profile.IsUnlocked && !DialogueActor.questNPCs.Contains(this) && this.showNpcMarker && this.profile.id != "")
		{
			DialogueActor.questNPCs.Add(this);
			if (this.showNpcMarker && this.npcMarker == null)
			{
				this.npcMarker = Object.Instantiate<GameObject>(Prefabs.p.npcMarker, this.DialogueAnchor);
				this.npcMarker.GetComponent<QuestMarker>().attachedActor = this;
				this.npcMarker.transform.localScale = 1f / base.transform.localScale.x * Vector3.one;
				this.profile.OnChange += this.OnProfileUnlockedChanged;
			}
		}
	}

	// Token: 0x060003AD RID: 941 RVA: 0x00004D33 File Offset: 0x00002F33
	private void OnProfileUnlockedChanged(object sender, bool isUnlocked)
	{
		if (isUnlocked && this.npcMarker != null)
		{
			Object.Destroy(this.npcMarker);
		}
	}

	// Token: 0x060003AE RID: 942 RVA: 0x00004D51 File Offset: 0x00002F51
	private void OnDisable()
	{
		if (this.animatorIKHook != null)
		{
			this.animatorIKHook.enabled = false;
		}
	}

	// Token: 0x060003AF RID: 943 RVA: 0x00004D6D File Offset: 0x00002F6D
	private void OnDestroy()
	{
		if (DialogueActor.questNPCs.Contains(this))
		{
			DialogueActor.questNPCs.Remove(this);
		}
		if (this.npcMarker != null)
		{
			Object.Destroy(this.npcMarker);
		}
	}

	// Token: 0x060003B0 RID: 944 RVA: 0x00026988 File Offset: 0x00024B88
	private void GetAnimatorLayers()
	{
		if (this.animator != null)
		{
			this.upperBodyLayer = this.animator.GetLayerIndex("Upper Body Emotes");
			this.fullBodyLayer = this.animator.GetLayerIndex("Full Body Emotes");
			this.stateLayer = this.animator.GetLayerIndex("Actor States");
		}
	}

	// Token: 0x17000043 RID: 67
	// (get) Token: 0x060003B1 RID: 945 RVA: 0x000269E8 File Offset: 0x00024BE8
	private bool IsEmoting
	{
		get
		{
			if (Time.time - this.lastEmoteStart <= 0.25f)
			{
				return true;
			}
			int num = (this.isUpperBodyEmote ? this.upperBodyLayer : this.fullBodyLayer);
			return num != -1 && this.animator.GetCurrentAnimatorStateInfo(num).shortNameHash != DialogueActor.defaultEmote;
		}
	}

	// Token: 0x17000044 RID: 68
	// (get) Token: 0x060003B2 RID: 946 RVA: 0x00026A48 File Offset: 0x00024C48
	public bool IsCurrentlyEmoting
	{
		get
		{
			if (Time.time - this.lastEmoteStart <= 0.3f)
			{
				this.isCurrentlyEmoting = true;
				return true;
			}
			if (!this.isCurrentlyEmoting)
			{
				return false;
			}
			int num = (this.isUpperBodyEmote ? this.upperBodyLayer : this.fullBodyLayer);
			this.isCurrentlyEmoting = num != -1 && this.animator.GetCurrentAnimatorStateInfo(num).shortNameHash != DialogueActor.defaultEmote;
			return this.isCurrentlyEmoting;
		}
	}

	// Token: 0x17000045 RID: 69
	// (get) Token: 0x060003B3 RID: 947 RVA: 0x00026AC4 File Offset: 0x00024CC4
	public int CurrentEmoteHash
	{
		get
		{
			int num = (this.isUpperBodyEmote ? this.upperBodyLayer : this.fullBodyLayer);
			if (num == -1)
			{
				return -1;
			}
			if (Time.time - this.lastEmoteStart <= 0.25f)
			{
				return 0;
			}
			return this.animator.GetCurrentAnimatorStateInfo(num).shortNameHash;
		}
	}

	// Token: 0x060003B4 RID: 948 RVA: 0x00026B18 File Offset: 0x00024D18
	public void SetEmote(int emoteHash, bool skipTransition = false)
	{
		if (this.onChangeStateOrEmote != null)
		{
			this.onChangeStateOrEmote.Invoke();
		}
		if (this.upperBodyLayer == -1)
		{
			this.GetAnimatorLayers();
		}
		this.holdEmote = false;
		if (emoteHash == 0)
		{
			this.ClearEmote(false, false);
			return;
		}
		if (emoteHash == -1)
		{
			this.ClearEmote(true, false);
			return;
		}
		if (this.animator == null)
		{
			return;
		}
		bool flag = this.IsEmoting;
		int num = this.currentEmoteHash;
		bool flag2 = true;
		if (this.upperBodyLayer != -1 && this.animator.HasState(this.upperBodyLayer, emoteHash))
		{
			this.currentEmoteHash = emoteHash;
			this.lastEmoteStart = Time.time;
			flag2 = true;
			this.animator.CrossFadeInFixedTime(emoteHash, skipTransition ? 0.01f : 0.25f, this.upperBodyLayer);
		}
		else if (this.fullBodyLayer != -1 && this.animator.HasState(this.fullBodyLayer, emoteHash))
		{
			this.currentEmoteHash = emoteHash;
			this.lastEmoteStart = Time.time;
			flag2 = false;
			this.animator.CrossFadeInFixedTime(emoteHash, skipTransition ? 0.01f : 0.25f, this.fullBodyLayer);
		}
		if (flag2 != this.isUpperBodyEmote && flag)
		{
			int num2 = (this.isUpperBodyEmote ? this.upperBodyLayer : this.fullBodyLayer);
			if (num2 != -1)
			{
				this.animator.CrossFadeInFixedTime(DialogueActor.defaultEmote, skipTransition ? 0.01f : 0.25f, num2);
			}
		}
		this.isEmoting = true;
		this.isCurrentlyEmoting = true;
		this.isUpperBodyEmote = flag2;
	}

	// Token: 0x060003B5 RID: 949 RVA: 0x00004DA1 File Offset: 0x00002FA1
	public void HoldEmote()
	{
		this.holdEmote = true;
	}

	// Token: 0x060003B6 RID: 950 RVA: 0x00026C8C File Offset: 0x00024E8C
	public void ClearEmote(bool overrideHoldEmote = false, bool ignoreHoldEmote = false)
	{
		if (this.animator == null || !this.IsEmoting || (this.holdEmote && !overrideHoldEmote && !ignoreHoldEmote))
		{
			return;
		}
		if (this.onChangeStateOrEmote != null)
		{
			this.onChangeStateOrEmote.Invoke();
		}
		this.isEmoting = false;
		this.isCurrentlyEmoting = false;
		if (!ignoreHoldEmote)
		{
			this.holdEmote = false;
		}
		int num = (this.isUpperBodyEmote ? this.upperBodyLayer : this.fullBodyLayer);
		if (num != -1)
		{
			this.animator.CrossFadeInFixedTime(DialogueActor.defaultEmote, 0.25f, num);
		}
		this.lastEmoteStart = -10f;
		this.currentEmoteHash = 0;
	}

	// Token: 0x17000046 RID: 70
	// (get) Token: 0x060003B7 RID: 951 RVA: 0x00004DAA File Offset: 0x00002FAA
	// (set) Token: 0x060003B8 RID: 952 RVA: 0x00004DB2 File Offset: 0x00002FB2
	public int State
	{
		get
		{
			return this.currentState;
		}
		set
		{
			this.SetStateAndPosition(value, -1, false, false);
		}
	}

	// Token: 0x17000047 RID: 71
	// (get) Token: 0x060003B9 RID: 953 RVA: 0x00004DBE File Offset: 0x00002FBE
	// (set) Token: 0x060003BA RID: 954 RVA: 0x00004DC6 File Offset: 0x00002FC6
	public int Position
	{
		get
		{
			return this.currentPosition;
		}
		set
		{
			this.SetStateAndPosition(-1, value, false, false);
		}
	}

	// Token: 0x060003BB RID: 955 RVA: 0x00026D2C File Offset: 0x00024F2C
	public void SetStateString(string stateName)
	{
		ActorState actorState;
		if (Enum.TryParse<ActorState>(stateName, out actorState))
		{
			this.SetStateAndPosition((int)actorState, -1, false, false);
		}
	}

	// Token: 0x060003BC RID: 956 RVA: 0x00026D50 File Offset: 0x00024F50
	public void SetStateAndPosition(int newState, int newPosition, bool skipTransition = false, bool immediate = false)
	{
		if (this.animator == null)
		{
			return;
		}
		if (this.onChangeStateOrEmote != null)
		{
			this.onChangeStateOrEmote.Invoke();
		}
		if (Time.timeSinceLevelLoad < 1f || Time.time - this.startTime < 1f || this.startTime < 0f)
		{
			skipTransition = true;
		}
		bool flag = false;
		bool flag2 = false;
		if (newState != -1 && newState != this.currentState)
		{
			this.currentState = newState;
			flag2 = true;
			if (this.stateEffect != null)
			{
				Object.Destroy(this.stateEffect);
			}
			GameObject gameObject = DialogueActor.GetStateEffect((ActorState)newState);
			if (gameObject != null)
			{
				this.stateEffect = Object.Instantiate<GameObject>(gameObject, this.head);
				this.stateEffect.transform.localScale = Vector3.one;
			}
		}
		if (newPosition != -1 && newPosition != this.currentPosition)
		{
			if (!skipTransition && this.DoPositionTransitions(this.currentPosition, newPosition))
			{
				this.holdEmote = true;
				flag = true;
			}
			this.currentPosition = newPosition;
			flag2 = true;
		}
		if (flag2)
		{
			if (flag)
			{
				CoroutineUtil.Start(this.UpdateActorStateDelayed());
				return;
			}
			if (immediate)
			{
				this.UpdateActorState(0f);
				return;
			}
			this.UpdateActorState(0.25f);
		}
	}

	// Token: 0x060003BD RID: 957 RVA: 0x00026E78 File Offset: 0x00025078
	private bool DoPositionTransitions(int from, int to)
	{
		if (Time.time - this.lastEmoteStart <= 0.25f)
		{
			return true;
		}
		int transitionHash = DialogueActor.GetTransitionHash(from, to);
		if (transitionHash != -1)
		{
			this.SetEmote(transitionHash, false);
			return true;
		}
		return false;
	}

	// Token: 0x060003BE RID: 958 RVA: 0x00004DD2 File Offset: 0x00002FD2
	private IEnumerator UpdateActorStateDelayed()
	{
		if (DialogueActor.waitForEmoteDelay == null)
		{
			DialogueActor.waitForEmoteDelay = new WaitForSeconds(0.125f);
		}
		yield return DialogueActor.waitForEmoteDelay;
		this.UpdateActorState(0.25f);
		yield break;
	}

	// Token: 0x060003BF RID: 959 RVA: 0x00026EB4 File Offset: 0x000250B4
	private void UpdateActorState(float transitionTime = 0.25f)
	{
		if (this.animator == null || this.stateLayer == -1)
		{
			return;
		}
		int positionStateHash = DialogueActor.GetPositionStateHash(this.currentPosition, this.currentState, this.animator, this.stateLayer);
		bool flag = true;
		if (this.animator.IsInTransition(this.stateLayer))
		{
			if (this.animator.GetNextAnimatorStateInfo(this.stateLayer).shortNameHash != positionStateHash)
			{
				flag = false;
			}
		}
		else if (this.animator.GetCurrentAnimatorStateInfo(this.stateLayer).shortNameHash != positionStateHash)
		{
			flag = false;
		}
		if (!flag)
		{
			this.animator.CrossFadeInFixedTime(positionStateHash, transitionTime, this.stateLayer);
		}
		if (this.doBlinks && this.blinkSprite != null)
		{
			this.nextBlinkTime = Time.time;
			return;
		}
		this.UpdateEyeState();
	}

	// Token: 0x060003C0 RID: 960 RVA: 0x00026F88 File Offset: 0x00025188
	public void SetActorLookTarget(DialogueActor lookTarget, bool immediate = false)
	{
		if (!base.gameObject.activeInHierarchy)
		{
			if (lookTarget == null || lookTarget == this)
			{
				this.LookAtDialogue = false;
				return;
			}
			this.LookAtDialogue = true;
			this.lookAtDialogueTarget = lookTarget.FocusPosition;
			this.lookAtActor = lookTarget;
			return;
		}
		else
		{
			if (lookTarget == null || lookTarget == this)
			{
				if (this.delayedSetLookCoroutine != null)
				{
					base.StopCoroutine(this.delayedSetLookCoroutine);
				}
				this.LookAtDialogue = false;
				return;
			}
			if (this.delayedSetLookCoroutine != null)
			{
				base.StopCoroutine(this.delayedSetLookCoroutine);
				this.FinishSetActorLookTargetDelayed();
			}
			if (immediate)
			{
				this.LookAtDialogue = true;
				this.lookAtDialogueTarget = lookTarget.FocusPosition;
				this.lookAtActor = lookTarget;
				return;
			}
			this.delayedSetLookTarget = lookTarget;
			this.delayedSetLookCoroutine = this.SetActorLookTargetDelayed();
			base.StartCoroutine(this.delayedSetLookCoroutine);
			return;
		}
	}

	// Token: 0x060003C1 RID: 961 RVA: 0x00004DE1 File Offset: 0x00002FE1
	public IEnumerator SetActorLookTargetDelayed()
	{
		yield return new WaitForSeconds(Random.Range(1f, 0.2f));
		this.FinishSetActorLookTargetDelayed();
		this.delayedSetLookCoroutine = null;
		yield break;
	}

	// Token: 0x060003C2 RID: 962 RVA: 0x00027060 File Offset: 0x00025260
	private void FinishSetActorLookTargetDelayed()
	{
		if (this.delayedSetLookTarget == null || this.delayedSetLookTarget == this)
		{
			this.LookAtDialogue = false;
		}
		else
		{
			this.LookAtDialogue = true;
			this.lookAtDialogueTarget = this.delayedSetLookTarget.FocusPosition;
			this.lookAtActor = this.delayedSetLookTarget;
		}
		this.delayedSetLookTarget = null;
	}

	// Token: 0x060003C3 RID: 963 RVA: 0x000270C0 File Offset: 0x000252C0
	protected virtual void Start()
	{
		if (this.animator != null && this.animator.gameObject != base.gameObject)
		{
			this.animatorIKHook = this.animator.gameObject.AddComponent<OnAnimatorIKHook>();
			this.animatorIKHook.actor = this;
		}
		this.startTime = Time.time;
		if (this.onLoad != null && this.profile != null)
		{
			this.onLoad.Invoke(!this.profile.IsUnlocked);
		}
	}

	// Token: 0x060003C4 RID: 964 RVA: 0x00004DF0 File Offset: 0x00002FF0
	[ContextMenu("Generate Eyes")]
	public void GenerateEyes()
	{
		base.StartCoroutine(this.GenerateEyesAsync());
	}

	// Token: 0x060003C5 RID: 965 RVA: 0x00004DFF File Offset: 0x00002FFF
	public IEnumerator GenerateEyesAsync()
	{
		yield return null;
		yield break;
	}

	// Token: 0x060003C6 RID: 966 RVA: 0x00002229 File Offset: 0x00000429
	[ContextMenu("Copy Right Eye To Left")]
	public void CopyRightEyeToLeft()
	{
	}

	// Token: 0x060003C7 RID: 967 RVA: 0x00027150 File Offset: 0x00025350
	[ContextMenu("Auto Populate Fields")]
	public void AutoPopulateFields()
	{
		if (this.animator == null)
		{
			this.animator = base.GetComponent<Animator>();
		}
		if (this.animator == null)
		{
			this.animator = base.GetComponentInChildren<Animator>();
		}
		if (this.head == null)
		{
			this.head = base.transform.FindDeepChild("Head");
		}
		if (this.jaw == null)
		{
			this.jaw = base.transform.FindDeepChild("Jaw");
		}
		if (this.lowerSpine == null)
		{
			this.lowerSpine = base.transform.FindDeepChild("LowerSpine");
		}
		if (this.chest == null)
		{
			this.chest = base.transform.FindDeepChild("Chest");
		}
		if (this.leftHand == null)
		{
			this.leftHand = base.transform.FindDeepChild("Hand.L");
		}
		if (this.rightHand == null)
		{
			this.rightHand = base.transform.FindDeepChild("Hand.R");
		}
	}

	// Token: 0x060003C8 RID: 968 RVA: 0x0002726C File Offset: 0x0002546C
	[ContextMenu("Populate LOD Group")]
	public void PopulateLODGroup()
	{
		LODGroup component = base.GetComponent<LODGroup>();
		LOD[] lods = component.GetLODs();
		List<Renderer> list = new List<Renderer>(lods[0].renderers);
		if (this.leftEye != null && !list.Contains(this.leftEye))
		{
			list.Add(this.leftEye);
		}
		if (this.rightEye != null && !list.Contains(this.rightEye))
		{
			list.Add(this.rightEye);
		}
		lods[0].renderers = list.ToArray();
		component.SetLODs(lods);
	}

	// Token: 0x060003C9 RID: 969 RVA: 0x00027300 File Offset: 0x00025500
	private void UpdateLookAtRotation(bool includeGlobal = false, bool readAnimatorValues = true)
	{
		if (this.isRagdolling)
		{
			this.globalLookAtWeight = (this.shoulderLookAtWeight = (this.headLookAtWeight = 0f));
			return;
		}
		if (this.LookAt || this.LookAtDialogue)
		{
			if (this.lookAtActor != null)
			{
				this.lookAtDialogueTarget = this.lookAtActor.FocusPosition;
			}
			Vector3 vector = (this.LookAtDialogue ? this.lookAtDialogueTarget : this.lookAtTarget) - this.head.position;
			vector.y *= 0.25f;
			Quaternion quaternion = Quaternion.RotateTowards(base.transform.rotation, Quaternion.LookRotation(vector), 55f);
			if (this.lookAtRotation == Quaternion.identity)
			{
				this.lookAtRotation = base.transform.rotation;
			}
			this.lookAtRotation = Quaternion.Lerp(this.lookAtRotation, quaternion, 5f * Time.deltaTime);
		}
		float num = this.lookAtLerp;
		float num2 = this.shoulderLookAmount;
		float num3 = this.lookAtAmount;
		if (readAnimatorValues)
		{
			num2 *= this.animator.GetFloat(DialogueActor.shoulderTrackingID);
			num3 *= this.animator.GetFloat(DialogueActor.headTrackingID);
		}
		if (includeGlobal)
		{
			num2 *= num;
			num3 *= num;
		}
		this.globalLookAtWeight = Mathf.SmoothDamp(this.globalLookAtWeight, num, ref this.globalLookAtVelocity, 0.25f);
		this.shoulderLookAtWeight = Mathf.SmoothDamp(this.shoulderLookAtWeight, num2, ref this.shoulderLookAtVelocity, 0.25f);
		this.headLookAtWeight = Mathf.SmoothDamp(this.headLookAtWeight, num3, ref this.headLookAtVelocity, 0.25f);
	}

	// Token: 0x060003CA RID: 970 RVA: 0x0002749C File Offset: 0x0002569C
	public void OnAnimatorIK()
	{
		if (this.lookAtLerp > 0.01f && this.head != null && this.animator.enabled)
		{
			this.UpdateLookAtRotation(false, true);
			this.animator.SetLookAtPosition(this.head.position + this.lookAtRotation * (5f * Vector3.forward));
			this.animator.SetLookAtWeight(this.globalLookAtWeight, this.shoulderLookAtWeight, this.headLookAtWeight);
			return;
		}
		this.lookAtRotation = Quaternion.identity;
		this.animator.SetLookAtWeight(0f);
		this.shoulderLookAtWeight = (this.headLookAtWeight = (this.globalLookAtWeight = 0f));
		this.shoulderLookAtVelocity = (this.headLookAtVelocity = (this.globalLookAtVelocity = 0f));
		this.lookAtLerp = 0f;
	}

	// Token: 0x060003CB RID: 971 RVA: 0x0002758C File Offset: 0x0002578C
	public virtual void LateUpdate()
	{
		this.wasHeadRotatedThisFrame = false;
		bool flag = this.isPlayer || this.IsInDialogue;
		if (this.animator != null && this.animator.enabled != this.isAnimatorEnabled)
		{
			this.isAnimatorEnabled = this.animator.enabled;
			if (this.isAnimatorEnabled)
			{
				this.UpdateActorState(0.25f);
			}
		}
		if (this.lookAtAmount > 1E-05f || this.shoulderLookAmount > 1E-05f)
		{
			if (this.LookAt || this.LookAtDialogue || this.lookAtLerp > 1E-05f)
			{
				flag = true;
				this.lookAtLerp = Mathf.SmoothDamp(this.lookAtLerp, (this.LookAt || this.LookAtDialogue) ? 1f : 0f, ref this.lookAtLerpVelocity, (this.LookAt || this.LookAtDialogue) ? 0.4f : 0.25f);
			}
			if (this.animator != null && !this.animator.enabled)
			{
				if (this.lookAtLerp > 1E-05f)
				{
					this.DoManualLookAtRotations();
					flag = true;
				}
				else
				{
					this.shoulderLookAtWeight = (this.headLookAtWeight = (this.globalLookAtWeight = 0f));
					this.shoulderLookAtVelocity = (this.headLookAtVelocity = (this.globalLookAtVelocity = 0f));
				}
			}
		}
		if (this.mouthClose)
		{
			flag = true;
			if (this.mouth > 0.0001f)
			{
				this.mouth = Mathf.SmoothDamp(this.mouth, 0f, ref this.mouthVel, 0.05f);
			}
			else
			{
				this.mouth = 0f;
				this.mouthVel = 0f;
			}
			if (this.mouthOpen && this.mouth < 0.25f)
			{
				this.mouthClose = false;
			}
		}
		else if (this.mouthOpen)
		{
			flag = true;
			if (this.mouth < 1f)
			{
				this.mouth = Mathf.SmoothDamp(this.mouth, 1f, ref this.mouthVel, 0.05f);
			}
			else
			{
				this.mouth = 1f;
				this.mouthVel = 0f;
			}
		}
		if (Time.time - this.mouthOpenTime < 0.25f)
		{
			this.mouthControl = Mathf.MoveTowards(this.mouthControl, 1f, 20f * Time.deltaTime);
		}
		else
		{
			this.mouthControl = Mathf.MoveTowards(this.mouthControl, 0f, 5f * Time.deltaTime);
		}
		if (this.mouthControl > 0f)
		{
			this.UpdateMouthFlap();
		}
		else
		{
			this.mouthClose = false;
			this.mouthOpen = false;
		}
		if (this.doBlinks && this.blinkTime > 0f && this.blinkSprite != null && (this.leftEye != null || this.rightEye != null))
		{
			if (this.isBlinking)
			{
				if (Time.time - this.nextBlinkTime > 0.15f)
				{
					this.nextBlinkTime = Time.time + this.blinkTime * Random.Range(0.25f, 2f);
					this.isBlinking = false;
					this.UpdateEyeState();
				}
			}
			else if (Time.time >= this.nextBlinkTime)
			{
				this.isBlinking = true;
				this.leftEye.sprite = this.blinkSprite;
				this.rightEye.sprite = this.blinkSprite;
			}
		}
		if (this.isBlinking)
		{
			flag = true;
		}
		if (Time.time - this.lastProximityTime < 0.1f)
		{
			flag = true;
		}
		if (!flag)
		{
			base.enabled = false;
		}
	}

	// Token: 0x060003CC RID: 972 RVA: 0x00004E07 File Offset: 0x00003007
	public Vector3 GetDialoguePosition()
	{
		return this.DialogueAnchor.position;
	}

	// Token: 0x060003CD RID: 973 RVA: 0x0002791C File Offset: 0x00025B1C
	private void UpdateEyeState()
	{
		if (this.leftEye == null && this.rightEye == null)
		{
			return;
		}
		Sprite sprite = this.eyeSprite;
		bool flag = this.rightEye.flipX == this.leftEye.flipX;
		bool flag2 = this.rightEye.flipX;
		EyeProfile.EyeState eyeState;
		if (this.eyeProfile != null && this.eyeProfile.TryGetEyeState((ActorState)this.State, out eyeState))
		{
			sprite = eyeState.sprite;
			flag2 = eyeState.flip;
			eyeState.leftEyeSprite != null;
			flag = eyeState.sameDirection;
		}
		this.leftEye.sprite = sprite;
		this.rightEye.sprite = sprite;
		this.rightEye.flipX = flag2;
		this.leftEye.flipX = this.rightEye.flipX == flag;
	}

	// Token: 0x060003CE RID: 974 RVA: 0x00004E14 File Offset: 0x00003014
	public void MouthClose()
	{
		if (this.mouthOpen || this.mouthControl > 0f)
		{
			this.mouthClose = true;
			this.mouthOpen = false;
		}
	}

	// Token: 0x060003CF RID: 975 RVA: 0x000279F4 File Offset: 0x00025BF4
	public virtual void MouthOpen()
	{
		PlayAudio.p.PlayVoice((this.head != null) ? this.head.position : base.transform.position, this.voicePitchMultiplier, this.voiceVarianceMultiplier);
		if (this.head == null && this.jaw == null)
		{
			return;
		}
		this.mouthOpen = true;
		base.enabled = true;
		this.mouthOpenTime = Time.time;
	}

	// Token: 0x060003D0 RID: 976 RVA: 0x00027A74 File Offset: 0x00025C74
	protected virtual void UpdateMouthFlap()
	{
		float num = Mathf.SmoothStep(0f, 1f, this.mouthControl);
		if (this.head != null && !this.isRagdolling)
		{
			Quaternion quaternion = Quaternion.Euler(num * this.mouth * -5f * this.headRotationMod, 0f, 0f);
			if (this.animator.enabled || this.wasHeadRotatedThisFrame)
			{
				this.head.localRotation *= quaternion;
			}
			else
			{
				this.head.localRotation = this.initialHeadRotation * quaternion;
			}
		}
		if (this.jaw != null)
		{
			Quaternion quaternion2 = Quaternion.Euler(this.mouth * 20f * this.jawRotationMod, 0f, 0f);
			if (this.animator.enabled && !this.alwaysWriteJaw)
			{
				this.jaw.localRotation = Quaternion.Slerp(this.jaw.localRotation, this.jawClosed * quaternion2, num);
				return;
			}
			this.jaw.localRotation = Quaternion.Slerp(this.initialJawRotation, this.jawClosed * quaternion2, num);
		}
	}

	// Token: 0x060003D1 RID: 977 RVA: 0x00027BAC File Offset: 0x00025DAC
	public void SetJawOpen(float openAmount)
	{
		if (this.jaw == null)
		{
			return;
		}
		Quaternion quaternion = Quaternion.Euler(openAmount * 20f * this.jawRotationMod, 0f, 0f);
		this.jaw.localRotation = this.jawClosed * quaternion;
	}

	// Token: 0x060003D2 RID: 978 RVA: 0x00027C00 File Offset: 0x00025E00
	public void DoManualLookAtRotations()
	{
		if (this.isRagdolling)
		{
			return;
		}
		this.UpdateLookAtRotation(true, false);
		if (this.shoulderLookAtWeight > 0f)
		{
			if (this.lowerSpine != null)
			{
				this.lowerSpine.rotation = Quaternion.Slerp(this.lowerSpine.parent.rotation * this.initialLowerSpineRotation, this.lookAtRotation, 0.5f * this.shoulderLookAtWeight);
			}
			if (this.chest != null && this.chest.parent != null)
			{
				this.chest.rotation = Quaternion.Slerp(this.chest.parent.rotation * this.initialChestRotation, this.lookAtRotation, this.shoulderLookAtWeight);
			}
		}
		if (this.headLookAtWeight > 0f && this.head != null && this.head.parent != null)
		{
			this.head.rotation = Quaternion.Slerp(this.head.parent.rotation * this.initialHeadRotation, this.lookAtRotation, this.headLookAtWeight);
			this.wasHeadRotatedThisFrame = true;
		}
	}

	// Token: 0x0400051E RID: 1310
	public static List<DialogueActor> questNPCs = new List<DialogueActor>();

	// Token: 0x0400051F RID: 1311
	private static readonly int defaultEmote = Animator.StringToHash("None");

	// Token: 0x04000520 RID: 1312
	private static int[,] positionStateHashes = new int[10, 20];

	// Token: 0x04000521 RID: 1313
	private static int[,] transitionHashes = new int[10, 10];

	// Token: 0x04000522 RID: 1314
	public static DialogueActor playerActor;

	// Token: 0x04000523 RID: 1315
	private static readonly int headTrackingID = Animator.StringToHash("HeadTracking");

	// Token: 0x04000524 RID: 1316
	private static readonly int shoulderTrackingID = Animator.StringToHash("ShoulderTracking");

	// Token: 0x04000525 RID: 1317
	public bool isPlayer;

	// Token: 0x04000526 RID: 1318
	public CharacterProfile profile;

	// Token: 0x04000527 RID: 1319
	public bool ignoreUnlock;

	// Token: 0x04000528 RID: 1320
	public bool showNpcMarker = true;

	// Token: 0x04000529 RID: 1321
	[HideInInspector]
	public UnityEvent onChangeStateOrEmote;

	// Token: 0x0400052A RID: 1322
	public Animator animator;

	// Token: 0x0400052B RID: 1323
	public bool customAnchorPoint;

	// Token: 0x0400052C RID: 1324
	[ConditionalHide("customAnchorPoint", true)]
	public Transform customAnchorTransform;

	// Token: 0x0400052D RID: 1325
	[Header("Eyes")]
	public SpriteRenderer leftEye;

	// Token: 0x0400052E RID: 1326
	public SpriteRenderer rightEye;

	// Token: 0x0400052F RID: 1327
	public bool doBlinks = true;

	// Token: 0x04000530 RID: 1328
	private int stateID = Animator.StringToHash("State");

	// Token: 0x04000531 RID: 1329
	private int state;

	// Token: 0x04000532 RID: 1330
	[ConditionalHide("doBlinks", true)]
	public Sprite blinkSprite;

	// Token: 0x04000533 RID: 1331
	private Sprite eyeSprite;

	// Token: 0x04000534 RID: 1332
	public Sprite[] eyeSprites;

	// Token: 0x04000535 RID: 1333
	public EyeProfile eyeProfile;

	// Token: 0x04000536 RID: 1334
	[ConditionalHide("doBlinks", true)]
	public float blinkTime = 2f;

	// Token: 0x04000537 RID: 1335
	private const float blinkLength = 0.15f;

	// Token: 0x04000538 RID: 1336
	private float nextBlinkTime;

	// Token: 0x04000539 RID: 1337
	private bool isBlinking;

	// Token: 0x0400053A RID: 1338
	[Header("Bones")]
	public Transform head;

	// Token: 0x0400053B RID: 1339
	public Transform jaw;

	// Token: 0x0400053C RID: 1340
	public Transform lowerSpine;

	// Token: 0x0400053D RID: 1341
	public Transform chest;

	// Token: 0x0400053E RID: 1342
	public Transform leftHand;

	// Token: 0x0400053F RID: 1343
	public Transform rightHand;

	// Token: 0x04000540 RID: 1344
	private Quaternion modifiedChestRotation;

	// Token: 0x04000541 RID: 1345
	[Header("Mouth Flaps")]
	[Range(-2f, 3f)]
	public float headRotationMod = 1f;

	// Token: 0x04000542 RID: 1346
	[Range(-2f, 3f)]
	public float jawRotationMod = 1f;

	// Token: 0x04000543 RID: 1347
	public bool alwaysWriteJaw;

	// Token: 0x04000544 RID: 1348
	public Quaternion jawClosed = Quaternion.Euler(90f, 0f, 0f);

	// Token: 0x04000545 RID: 1349
	private const float headRotation = -5f;

	// Token: 0x04000546 RID: 1350
	private const float jawRotation = 20f;

	// Token: 0x04000547 RID: 1351
	protected bool mouthOpen;

	// Token: 0x04000548 RID: 1352
	protected bool mouthClose;

	// Token: 0x04000549 RID: 1353
	protected float mouth;

	// Token: 0x0400054A RID: 1354
	protected float mouthVel;

	// Token: 0x0400054B RID: 1355
	protected float mouthControl;

	// Token: 0x0400054C RID: 1356
	protected float mouthOpenTime = -1f;

	// Token: 0x0400054D RID: 1357
	private Quaternion initialHeadRotation;

	// Token: 0x0400054E RID: 1358
	private Quaternion initialJawRotation;

	// Token: 0x0400054F RID: 1359
	private Quaternion initialLowerSpineRotation;

	// Token: 0x04000550 RID: 1360
	private Quaternion initialChestRotation;

	// Token: 0x04000551 RID: 1361
	[Header("Voice")]
	[Range(0.2f, 2f)]
	public float voicePitchMultiplier = 1f;

	// Token: 0x04000552 RID: 1362
	[Range(0f, 2f)]
	public float voiceVarianceMultiplier = 1f;

	// Token: 0x04000553 RID: 1363
	[Header("Lookin'")]
	[Range(0f, 1f)]
	public float lookAtAmount = 1f;

	// Token: 0x04000554 RID: 1364
	[Range(0f, 1f)]
	public float shoulderLookAmount = 0.5f;

	// Token: 0x04000555 RID: 1365
	public bool customFocus;

	// Token: 0x04000556 RID: 1366
	[ConditionalHide("customFocus", true)]
	public Vector3 customFocusPosition;

	// Token: 0x04000557 RID: 1367
	private Vector3 localFocusPosition;

	// Token: 0x04000558 RID: 1368
	private bool lookAt;

	// Token: 0x04000559 RID: 1369
	private bool lookAtDialogue;

	// Token: 0x0400055A RID: 1370
	private float lastProximityTime = -1f;

	// Token: 0x0400055B RID: 1371
	[HideInInspector]
	public UnityEvent onEnterDialogue;

	// Token: 0x0400055C RID: 1372
	[HideInInspector]
	public UnityEvent onExitDialogue;

	// Token: 0x0400055D RID: 1373
	[HideInInspector]
	public UnityEvent onEnable;

	// Token: 0x0400055E RID: 1374
	private bool isInStandardDialogue;

	// Token: 0x0400055F RID: 1375
	private bool isInBubbleDialogue;

	// Token: 0x04000560 RID: 1376
	private float lookAtLerp;

	// Token: 0x04000561 RID: 1377
	private float lookAtLerpVelocity;

	// Token: 0x04000562 RID: 1378
	public Vector3 lookAtTarget;

	// Token: 0x04000563 RID: 1379
	public Vector3 lookAtDialogueTarget;

	// Token: 0x04000564 RID: 1380
	private DialogueActor lookAtActor;

	// Token: 0x04000565 RID: 1381
	public Quaternion lookAtRotation = Quaternion.identity;

	// Token: 0x04000566 RID: 1382
	public float globalLookAtWeight;

	// Token: 0x04000567 RID: 1383
	public float headLookAtWeight;

	// Token: 0x04000568 RID: 1384
	public float shoulderLookAtWeight;

	// Token: 0x04000569 RID: 1385
	private float globalLookAtVelocity;

	// Token: 0x0400056A RID: 1386
	private float headLookAtVelocity;

	// Token: 0x0400056B RID: 1387
	private float shoulderLookAtVelocity;

	// Token: 0x0400056C RID: 1388
	private GameObject stateEffect;

	// Token: 0x0400056D RID: 1389
	public bool forceDisable;

	// Token: 0x0400056E RID: 1390
	public bool isRagdolling;

	// Token: 0x0400056F RID: 1391
	private WaitForEndOfFrame waitForEndOfFrame;

	// Token: 0x04000570 RID: 1392
	public UnityBoolEvent onLoad;

	// Token: 0x04000571 RID: 1393
	[ReadOnly]
	public GameObject npcMarker;

	// Token: 0x04000572 RID: 1394
	private float awakeTime = -1f;

	// Token: 0x04000573 RID: 1395
	private bool isAnimatorEnabled;

	// Token: 0x04000574 RID: 1396
	private int upperBodyLayer = -1;

	// Token: 0x04000575 RID: 1397
	private int fullBodyLayer = -1;

	// Token: 0x04000576 RID: 1398
	private int stateLayer = -1;

	// Token: 0x04000577 RID: 1399
	private const float emoteCrossFadeLength = 0.25f;

	// Token: 0x04000578 RID: 1400
	private bool isEmoting;

	// Token: 0x04000579 RID: 1401
	private bool isCurrentlyEmoting;

	// Token: 0x0400057A RID: 1402
	private bool isUpperBodyEmote = true;

	// Token: 0x0400057B RID: 1403
	private int currentEmoteHash = -1;

	// Token: 0x0400057C RID: 1404
	private bool holdEmote;

	// Token: 0x0400057D RID: 1405
	private float lastEmoteStart = -1f;

	// Token: 0x0400057E RID: 1406
	private int currentState;

	// Token: 0x0400057F RID: 1407
	private int currentPosition;

	// Token: 0x04000580 RID: 1408
	private static WaitForSeconds waitForEmoteDelay;

	// Token: 0x04000581 RID: 1409
	private IEnumerator delayedSetLookCoroutine;

	// Token: 0x04000582 RID: 1410
	private DialogueActor delayedSetLookTarget;

	// Token: 0x04000583 RID: 1411
	private OnAnimatorIKHook animatorIKHook;

	// Token: 0x04000584 RID: 1412
	private float startTime = -1f;

	// Token: 0x04000585 RID: 1413
	private EyeProfile.EyeState eyeState;

	// Token: 0x04000586 RID: 1414
	private bool wasHeadRotatedThisFrame;
}
