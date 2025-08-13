using System;
using UnityEngine;

// Token: 0x02000269 RID: 617
public class Player : MonoBehaviour
{
	// Token: 0x17000116 RID: 278
	// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x0000B11C File Offset: 0x0000931C
	public static Vector3 RawPosition
	{
		get
		{
			return Player.rawTransform.position;
		}
	}

	// Token: 0x17000117 RID: 279
	// (get) Token: 0x06000BC7 RID: 3015 RVA: 0x0000B128 File Offset: 0x00009328
	public static Vector3 Position
	{
		get
		{
			return Player.transform.TransformPoint(0.5f * Vector3.up);
		}
	}

	// Token: 0x17000118 RID: 280
	// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x0000B143 File Offset: 0x00009343
	public static Vector3 Forward
	{
		get
		{
			return Player.transform.forward;
		}
	}

	// Token: 0x06000BC9 RID: 3017 RVA: 0x0000B14F File Offset: 0x0000934F
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

	// Token: 0x06000BCA RID: 3018 RVA: 0x0004063C File Offset: 0x0003E83C
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

	// Token: 0x04000EA6 RID: 3750
	public static Player player;

	// Token: 0x04000EA7 RID: 3751
	public static PlayerInput input;

	// Token: 0x04000EA8 RID: 3752
	public static PlayerMovement movement;

	// Token: 0x04000EA9 RID: 3753
	public static PlayerItemManager itemManager;

	// Token: 0x04000EAA RID: 3754
	public static Transform transform;

	// Token: 0x04000EAB RID: 3755
	public static Transform rawTransform;

	// Token: 0x04000EAC RID: 3756
	public static Animator animator;

	// Token: 0x04000EAD RID: 3757
	public static Rigidbody rigidbody;

	// Token: 0x04000EAE RID: 3758
	public static GameObject gameObject;

	// Token: 0x04000EAF RID: 3759
	public static CharacterReactionController reaction;

	// Token: 0x04000EB0 RID: 3760
	public static PlayerOverrideAnimations overrideAnimations;

	// Token: 0x04000EB1 RID: 3761
	public static FootIKSmooth footIK;

	// Token: 0x04000EB2 RID: 3762
	public static Footsteps footsteps;

	// Token: 0x04000EB3 RID: 3763
	public static GameObject uiItemCamera;

	// Token: 0x04000EB4 RID: 3764
	public static DialogueActor actor;

	// Token: 0x04000EB5 RID: 3765
	public static HandIK handIK;

	// Token: 0x04000EB6 RID: 3766
	public static PlayerEffects effects;

	// Token: 0x04000EB7 RID: 3767
	public static RagdollController ragdollController;

	// Token: 0x04000EB8 RID: 3768
	public static PlayerActorStates actorStates;

	// Token: 0x04000EB9 RID: 3769
	public PlayerInput pInput;

	// Token: 0x04000EBA RID: 3770
	public PlayerMovement pMovement;

	// Token: 0x04000EBB RID: 3771
	public PlayerItemManager pItemManager;

	// Token: 0x04000EBC RID: 3772
	public Transform pTransform;

	// Token: 0x04000EBD RID: 3773
	public Animator pAnimator;

	// Token: 0x04000EBE RID: 3774
	public Rigidbody pRigidbody;

	// Token: 0x04000EBF RID: 3775
	public CharacterReactionController pReaction;

	// Token: 0x04000EC0 RID: 3776
	public PlayerOverrideAnimations pOverrideAnimations;

	// Token: 0x04000EC1 RID: 3777
	public FootIKSmooth pFootIK;

	// Token: 0x04000EC2 RID: 3778
	public Footsteps pFootsteps;

	// Token: 0x04000EC3 RID: 3779
	public GameObject pUiItemCamera;

	// Token: 0x04000EC4 RID: 3780
	public DialogueActor pActor;

	// Token: 0x04000EC5 RID: 3781
	public HandIK pHandIK;

	// Token: 0x04000EC6 RID: 3782
	public PlayerEffects pEffects;

	// Token: 0x04000EC7 RID: 3783
	public RagdollController pRagdollController;

	// Token: 0x04000EC8 RID: 3784
	public PlayerActorStates pActorStates;
}
