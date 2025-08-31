using System;
using UnityEngine;

public class Player : MonoBehaviour
{
	// (get) Token: 0x06000A11 RID: 2577
	public static Vector3 RawPosition
	{
		get
		{
			return Player.rawTransform.position;
		}
	}

	// (get) Token: 0x06000A12 RID: 2578
	public static Vector3 Position
	{
		get
		{
			return Player.transform.TransformPoint(0.5f * Vector3.up);
		}
	}

	// (get) Token: 0x06000A13 RID: 2579
	public static Vector3 Forward
	{
		get
		{
			return Player.transform.forward;
		}
	}

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

	private void Start()
	{
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
