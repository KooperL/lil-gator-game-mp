using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueActor : MonoBehaviour
{
	// Token: 0x060003BE RID: 958 RVA: 0x0002740C File Offset: 0x0002560C
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

	// Token: 0x060003BF RID: 959 RVA: 0x00027464 File Offset: 0x00025664
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

	// Token: 0x060003C0 RID: 960 RVA: 0x000274EC File Offset: 0x000256EC
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

	// Token: 0x060003C1 RID: 961 RVA: 0x0002755C File Offset: 0x0002575C
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

	// Token: 0x060003C2 RID: 962 RVA: 0x000275D0 File Offset: 0x000257D0
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

	// Token: 0x060003C3 RID: 963 RVA: 0x00004E1C File Offset: 0x0000301C
	private static GameObject GetStateEffect(ActorState state)
	{
		if (state == ActorState.S_Nervous)
		{
			return EffectsManager.e.sweatPrefab;
		}
		return null;
	}

	// (get) Token: 0x060003C4 RID: 964 RVA: 0x00004E2F File Offset: 0x0000302F
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

	// (get) Token: 0x060003C5 RID: 965 RVA: 0x00004E5B File Offset: 0x0000305B
	public Vector3 FocusPosition
	{
		get
		{
			return base.transform.TransformPoint(this.localFocusPosition);
		}
	}

	// (get) Token: 0x060003C6 RID: 966 RVA: 0x00004E6E File Offset: 0x0000306E
	// (set) Token: 0x060003C7 RID: 967 RVA: 0x00004E76 File Offset: 0x00003076
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

	// (get) Token: 0x060003C8 RID: 968 RVA: 0x00004E89 File Offset: 0x00003089
	// (set) Token: 0x060003C9 RID: 969 RVA: 0x00004E91 File Offset: 0x00003091
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

	// Token: 0x060003CA RID: 970 RVA: 0x00004EB9 File Offset: 0x000030B9
	public void ProximityTrigger()
	{
		this.lastProximityTime = Time.time;
	}

	// (get) Token: 0x060003CB RID: 971 RVA: 0x00004EC6 File Offset: 0x000030C6
	public bool IsInDialogue
	{
		get
		{
			return this.IsInStandardDialogue || this.IsInBubbleDialogue;
		}
	}

	// (get) Token: 0x060003CC RID: 972 RVA: 0x00004ED8 File Offset: 0x000030D8
	// (set) Token: 0x060003CD RID: 973 RVA: 0x00004EE0 File Offset: 0x000030E0
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

	// (get) Token: 0x060003CE RID: 974 RVA: 0x00004F1F File Offset: 0x0000311F
	// (set) Token: 0x060003CF RID: 975 RVA: 0x00004F27 File Offset: 0x00003127
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

	// Token: 0x060003D0 RID: 976 RVA: 0x00027628 File Offset: 0x00025828
	[ContextMenu("Snap To Floor")]
	public void SnapToFloor()
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position + Vector3.up, Vector3.down, out raycastHit, 3f, LayerMask.GetMask(new string[] { "Terrain", "Default" })))
		{
			base.transform.position = raycastHit.point;
		}
	}

	// Token: 0x060003D1 RID: 977 RVA: 0x00002229 File Offset: 0x00000429
	[ContextMenu("Reset Bones")]
	public void ResetBones()
	{
	}

	// Token: 0x060003D2 RID: 978 RVA: 0x0002768C File Offset: 0x0002588C
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

	// Token: 0x060003D3 RID: 979 RVA: 0x000277B8 File Offset: 0x000259B8
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
				this.npcMarker = global::UnityEngine.Object.Instantiate<GameObject>(Prefabs.p.npcMarker, this.DialogueAnchor);
				this.npcMarker.GetComponent<QuestMarker>().attachedActor = this;
				this.npcMarker.transform.localScale = 1f / base.transform.localScale.x * Vector3.one;
				this.profile.OnChange += this.OnProfileUnlockedChanged;
			}
		}
	}

	// Token: 0x060003D4 RID: 980 RVA: 0x00004F66 File Offset: 0x00003166
	private void OnProfileUnlockedChanged(object sender, bool isUnlocked)
	{
		if (isUnlocked && this.npcMarker != null)
		{
			global::UnityEngine.Object.Destroy(this.npcMarker);
		}
	}

	// Token: 0x060003D5 RID: 981 RVA: 0x00004F84 File Offset: 0x00003184
	private void OnDisable()
	{
		if (this.animatorIKHook != null)
		{
			this.animatorIKHook.enabled = false;
		}
	}

	// Token: 0x060003D6 RID: 982 RVA: 0x00004FA0 File Offset: 0x000031A0
	private void OnDestroy()
	{
		if (DialogueActor.questNPCs.Contains(this))
		{
			DialogueActor.questNPCs.Remove(this);
		}
		if (this.npcMarker != null)
		{
			global::UnityEngine.Object.Destroy(this.npcMarker);
		}
	}

	// Token: 0x060003D7 RID: 983 RVA: 0x00027934 File Offset: 0x00025B34
	private void GetAnimatorLayers()
	{
		if (this.animator != null)
		{
			this.upperBodyLayer = this.animator.GetLayerIndex("Upper Body Emotes");
			this.fullBodyLayer = this.animator.GetLayerIndex("Full Body Emotes");
			this.stateLayer = this.animator.GetLayerIndex("Actor States");
		}
	}

	// (get) Token: 0x060003D8 RID: 984 RVA: 0x00027994 File Offset: 0x00025B94
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

	// (get) Token: 0x060003D9 RID: 985 RVA: 0x000279F4 File Offset: 0x00025BF4
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

	// (get) Token: 0x060003DA RID: 986 RVA: 0x00027A70 File Offset: 0x00025C70
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

	// Token: 0x060003DB RID: 987 RVA: 0x00027AC4 File Offset: 0x00025CC4
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

	// Token: 0x060003DC RID: 988 RVA: 0x00004FD4 File Offset: 0x000031D4
	public void HoldEmote()
	{
		this.holdEmote = true;
	}

	// Token: 0x060003DD RID: 989 RVA: 0x00027C38 File Offset: 0x00025E38
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

	// (get) Token: 0x060003DE RID: 990 RVA: 0x00004FDD File Offset: 0x000031DD
	// (set) Token: 0x060003DF RID: 991 RVA: 0x00004FE5 File Offset: 0x000031E5
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

	// (get) Token: 0x060003E0 RID: 992 RVA: 0x00004FF1 File Offset: 0x000031F1
	// (set) Token: 0x060003E1 RID: 993 RVA: 0x00004FF9 File Offset: 0x000031F9
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

	// Token: 0x060003E2 RID: 994 RVA: 0x00027CD8 File Offset: 0x00025ED8
	public void SetStateString(string stateName)
	{
		ActorState actorState;
		if (Enum.TryParse<ActorState>(stateName, out actorState))
		{
			this.SetStateAndPosition((int)actorState, -1, false, false);
		}
	}

	// Token: 0x060003E3 RID: 995 RVA: 0x00027CFC File Offset: 0x00025EFC
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
				global::UnityEngine.Object.Destroy(this.stateEffect);
			}
			GameObject gameObject = DialogueActor.GetStateEffect((ActorState)newState);
			if (gameObject != null)
			{
				this.stateEffect = global::UnityEngine.Object.Instantiate<GameObject>(gameObject, this.head);
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

	// Token: 0x060003E4 RID: 996 RVA: 0x00027E24 File Offset: 0x00026024
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

	// Token: 0x060003E5 RID: 997 RVA: 0x00005005 File Offset: 0x00003205
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

	// Token: 0x060003E6 RID: 998 RVA: 0x00027E60 File Offset: 0x00026060
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

	// Token: 0x060003E7 RID: 999 RVA: 0x00027F34 File Offset: 0x00026134
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

	// Token: 0x060003E8 RID: 1000 RVA: 0x00005014 File Offset: 0x00003214
	public IEnumerator SetActorLookTargetDelayed()
	{
		yield return new WaitForSeconds(global::UnityEngine.Random.Range(1f, 0.2f));
		this.FinishSetActorLookTargetDelayed();
		this.delayedSetLookCoroutine = null;
		yield break;
	}

	// Token: 0x060003E9 RID: 1001 RVA: 0x0002800C File Offset: 0x0002620C
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

	// Token: 0x060003EA RID: 1002 RVA: 0x0002806C File Offset: 0x0002626C
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

	// Token: 0x060003EB RID: 1003 RVA: 0x00005023 File Offset: 0x00003223
	[ContextMenu("Generate Eyes")]
	public void GenerateEyes()
	{
		base.StartCoroutine(this.GenerateEyesAsync());
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x00005032 File Offset: 0x00003232
	public IEnumerator GenerateEyesAsync()
	{
		yield return null;
		yield break;
	}

	// Token: 0x060003ED RID: 1005 RVA: 0x00002229 File Offset: 0x00000429
	[ContextMenu("Copy Right Eye To Left")]
	public void CopyRightEyeToLeft()
	{
	}

	// Token: 0x060003EE RID: 1006 RVA: 0x000280FC File Offset: 0x000262FC
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

	// Token: 0x060003EF RID: 1007 RVA: 0x00028218 File Offset: 0x00026418
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

	// Token: 0x060003F0 RID: 1008 RVA: 0x000282AC File Offset: 0x000264AC
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

	// Token: 0x060003F1 RID: 1009 RVA: 0x00028448 File Offset: 0x00026648
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

	// Token: 0x060003F2 RID: 1010 RVA: 0x00028538 File Offset: 0x00026738
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
					this.nextBlinkTime = Time.time + this.blinkTime * global::UnityEngine.Random.Range(0.25f, 2f);
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

	// Token: 0x060003F3 RID: 1011 RVA: 0x0000503A File Offset: 0x0000323A
	public Vector3 GetDialoguePosition()
	{
		return this.DialogueAnchor.position;
	}

	// Token: 0x060003F4 RID: 1012 RVA: 0x000288C8 File Offset: 0x00026AC8
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

	// Token: 0x060003F5 RID: 1013 RVA: 0x00005047 File Offset: 0x00003247
	public void MouthClose()
	{
		if (this.mouthOpen || this.mouthControl > 0f)
		{
			this.mouthClose = true;
			this.mouthOpen = false;
		}
	}

	// Token: 0x060003F6 RID: 1014 RVA: 0x000289A0 File Offset: 0x00026BA0
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

	// Token: 0x060003F7 RID: 1015 RVA: 0x00028A20 File Offset: 0x00026C20
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

	// Token: 0x060003F8 RID: 1016 RVA: 0x00028B58 File Offset: 0x00026D58
	public void SetJawOpen(float openAmount)
	{
		if (this.jaw == null)
		{
			return;
		}
		Quaternion quaternion = Quaternion.Euler(openAmount * 20f * this.jawRotationMod, 0f, 0f);
		this.jaw.localRotation = this.jawClosed * quaternion;
	}

	// Token: 0x060003F9 RID: 1017 RVA: 0x00028BAC File Offset: 0x00026DAC
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

	public static List<DialogueActor> questNPCs = new List<DialogueActor>();

	private static readonly int defaultEmote = Animator.StringToHash("None");

	private static int[,] positionStateHashes = new int[10, 20];

	private static int[,] transitionHashes = new int[10, 10];

	public static DialogueActor playerActor;

	private static readonly int headTrackingID = Animator.StringToHash("HeadTracking");

	private static readonly int shoulderTrackingID = Animator.StringToHash("ShoulderTracking");

	public bool isPlayer;

	public CharacterProfile profile;

	public bool ignoreUnlock;

	public bool showNpcMarker = true;

	[HideInInspector]
	public UnityEvent onChangeStateOrEmote;

	public Animator animator;

	public bool customAnchorPoint;

	[ConditionalHide("customAnchorPoint", true)]
	public Transform customAnchorTransform;

	[Header("Eyes")]
	public SpriteRenderer leftEye;

	public SpriteRenderer rightEye;

	public bool doBlinks = true;

	private int stateID = Animator.StringToHash("State");

	private int state;

	[ConditionalHide("doBlinks", true)]
	public Sprite blinkSprite;

	private Sprite eyeSprite;

	public Sprite[] eyeSprites;

	public EyeProfile eyeProfile;

	[ConditionalHide("doBlinks", true)]
	public float blinkTime = 2f;

	private const float blinkLength = 0.15f;

	private float nextBlinkTime;

	private bool isBlinking;

	[Header("Bones")]
	public Transform head;

	public Transform jaw;

	public Transform lowerSpine;

	public Transform chest;

	public Transform leftHand;

	public Transform rightHand;

	private Quaternion modifiedChestRotation;

	[Header("Mouth Flaps")]
	[Range(-2f, 3f)]
	public float headRotationMod = 1f;

	[Range(-2f, 3f)]
	public float jawRotationMod = 1f;

	public bool alwaysWriteJaw;

	public Quaternion jawClosed = Quaternion.Euler(90f, 0f, 0f);

	private const float headRotation = -5f;

	private const float jawRotation = 20f;

	protected bool mouthOpen;

	protected bool mouthClose;

	protected float mouth;

	protected float mouthVel;

	protected float mouthControl;

	protected float mouthOpenTime = -1f;

	private Quaternion initialHeadRotation;

	private Quaternion initialJawRotation;

	private Quaternion initialLowerSpineRotation;

	private Quaternion initialChestRotation;

	[Header("Voice")]
	[Range(0.2f, 2f)]
	public float voicePitchMultiplier = 1f;

	[Range(0f, 2f)]
	public float voiceVarianceMultiplier = 1f;

	[Header("Lookin'")]
	[Range(0f, 1f)]
	public float lookAtAmount = 1f;

	[Range(0f, 1f)]
	public float shoulderLookAmount = 0.5f;

	public bool customFocus;

	[ConditionalHide("customFocus", true)]
	public Vector3 customFocusPosition;

	private Vector3 localFocusPosition;

	private bool lookAt;

	private bool lookAtDialogue;

	private float lastProximityTime = -1f;

	[HideInInspector]
	public UnityEvent onEnterDialogue;

	[HideInInspector]
	public UnityEvent onExitDialogue;

	[HideInInspector]
	public UnityEvent onEnable;

	private bool isInStandardDialogue;

	private bool isInBubbleDialogue;

	private float lookAtLerp;

	private float lookAtLerpVelocity;

	public Vector3 lookAtTarget;

	public Vector3 lookAtDialogueTarget;

	private DialogueActor lookAtActor;

	public Quaternion lookAtRotation = Quaternion.identity;

	public float globalLookAtWeight;

	public float headLookAtWeight;

	public float shoulderLookAtWeight;

	private float globalLookAtVelocity;

	private float headLookAtVelocity;

	private float shoulderLookAtVelocity;

	private GameObject stateEffect;

	public bool forceDisable;

	public bool isRagdolling;

	private WaitForEndOfFrame waitForEndOfFrame;

	public UnityBoolEvent onLoad;

	[ReadOnly]
	public GameObject npcMarker;

	private float awakeTime = -1f;

	private bool isAnimatorEnabled;

	private int upperBodyLayer = -1;

	private int fullBodyLayer = -1;

	private int stateLayer = -1;

	private const float emoteCrossFadeLength = 0.25f;

	private bool isEmoting;

	private bool isCurrentlyEmoting;

	private bool isUpperBodyEmote = true;

	private int currentEmoteHash = -1;

	private bool holdEmote;

	private float lastEmoteStart = -1f;

	private int currentState;

	private int currentPosition;

	private static WaitForSeconds waitForEmoteDelay;

	private IEnumerator delayedSetLookCoroutine;

	private DialogueActor delayedSetLookTarget;

	private OnAnimatorIKHook animatorIKHook;

	private float startTime = -1f;

	private EyeProfile.EyeState eyeState;

	private bool wasHeadRotatedThisFrame;
}
