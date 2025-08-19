using System;
using UnityEngine;

public class Player : MonoBehaviour
{
	// (get) Token: 0x06000C12 RID: 3090 RVA: 0x0000B42E File Offset: 0x0000962E
	public static Vector3 RawPosition
	{
		get
		{
			return Player.rawTransform.position;
		}
	}

	// (get) Token: 0x06000C13 RID: 3091 RVA: 0x0000B43A File Offset: 0x0000963A
	public static Vector3 Position
	{
		get
		{
			return Player.transform.TransformPoint(0.5f * Vector3.up);
		}
	}

	// (get) Token: 0x06000C14 RID: 3092 RVA: 0x0000B455 File Offset: 0x00009655
	public static Vector3 Forward
	{
		get
		{
			return Player.transform.forward;
		}
	}

	// Token: 0x06000C15 RID: 3093 RVA: 0x0000B461 File Offset: 0x00009661
	private void OnValidate()
	{
		if (this.pHandIK == null)
		{
			this.pHandIK = base.GetComponentInChildren<HandIK>();
		}
		if (this.pActorStates == null)
		{
			this.pActorStates = base.GetComponentInChildren<PlayerActorStates>();
		}
	}

	// Token: 0x06000C16 RID: 3094 RVA: 0x000421A0 File Offset: 0x000403A0
	private void OnEnable()
	{
		Player.player = this;
		Player.input = this.pInput;
		Player.movement = this.pMovement;
		Player.itemManager = this.pItemManager;
		Player.transform = this.pTransform;
		Player.animator = this.pAnimator;
		Player.rigidbody = this.pRigidbody;
		Player.reaction = this.pReaction;
		Player.overrideAnimations = this.pOverrideAnimations;
		Player.footIK = this.pFootIK;
		Player.footsteps = this.pFootsteps;
		Player.uiItemCamera = this.pUiItemCamera;
		Player.actor = this.pActor;
		Player.handIK = this.pHandIK;
		Player.effects = this.pEffects;
		Player.ragdollController = this.pRagdollController;
		Player.actorStates = this.pActorStates;
		Player.rawTransform = base.transform;
		Player.gameObject = base.gameObject;
	}

	public static Player player;

	public static PlayerInput input;

	public static PlayerMovement movement;

	public static PlayerItemManager itemManager;

	public new static Transform transform;

	public static Transform rawTransform;

	public static Animator animator;

	public static Rigidbody rigidbody;

	public new static GameObject gameObject;

	public static CharacterReactionController reaction;

	public static PlayerOverrideAnimations overrideAnimations;

	public static FootIKSmooth footIK;

	public static Footsteps footsteps;

	public static GameObject uiItemCamera;

	public static DialogueActor actor;

	public static HandIK handIK;

	public static PlayerEffects effects;

	public static RagdollController ragdollController;

	public static PlayerActorStates actorStates;

	public PlayerInput pInput;

	public PlayerMovement pMovement;

	public PlayerItemManager pItemManager;

	public Transform pTransform;

	public Animator pAnimator;

	public Rigidbody pRigidbody;

	public CharacterReactionController pReaction;

	public PlayerOverrideAnimations pOverrideAnimations;

	public FootIKSmooth pFootIK;

	public Footsteps pFootsteps;

	public GameObject pUiItemCamera;

	public DialogueActor pActor;

	public HandIK pHandIK;

	public PlayerEffects pEffects;

	public RagdollController pRagdollController;

	public PlayerActorStates pActorStates;
}
