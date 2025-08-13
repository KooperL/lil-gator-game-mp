using System;
using UnityEngine;

// Token: 0x020001E0 RID: 480
public class Player : MonoBehaviour
{
	// Token: 0x17000084 RID: 132
	// (get) Token: 0x06000A11 RID: 2577 RVA: 0x0002EA83 File Offset: 0x0002CC83
	public static Vector3 RawPosition
	{
		get
		{
			return Player.rawTransform.position;
		}
	}

	// Token: 0x17000085 RID: 133
	// (get) Token: 0x06000A12 RID: 2578 RVA: 0x0002EA8F File Offset: 0x0002CC8F
	public static Vector3 Position
	{
		get
		{
			return Player.transform.TransformPoint(0.5f * Vector3.up);
		}
	}

	// Token: 0x17000086 RID: 134
	// (get) Token: 0x06000A13 RID: 2579 RVA: 0x0002EAAA File Offset: 0x0002CCAA
	public static Vector3 Forward
	{
		get
		{
			return Player.transform.forward;
		}
	}

	// Token: 0x06000A14 RID: 2580 RVA: 0x0002EAB6 File Offset: 0x0002CCB6
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

	// Token: 0x06000A15 RID: 2581 RVA: 0x0002EAEC File Offset: 0x0002CCEC
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

	// Token: 0x04000C68 RID: 3176
	public static Player player;

	// Token: 0x04000C69 RID: 3177
	public static PlayerInput input;

	// Token: 0x04000C6A RID: 3178
	public static PlayerMovement movement;

	// Token: 0x04000C6B RID: 3179
	public static PlayerItemManager itemManager;

	// Token: 0x04000C6C RID: 3180
	public new static Transform transform;

	// Token: 0x04000C6D RID: 3181
	public static Transform rawTransform;

	// Token: 0x04000C6E RID: 3182
	public static Animator animator;

	// Token: 0x04000C6F RID: 3183
	public static Rigidbody rigidbody;

	// Token: 0x04000C70 RID: 3184
	public new static GameObject gameObject;

	// Token: 0x04000C71 RID: 3185
	public static CharacterReactionController reaction;

	// Token: 0x04000C72 RID: 3186
	public static PlayerOverrideAnimations overrideAnimations;

	// Token: 0x04000C73 RID: 3187
	public static FootIKSmooth footIK;

	// Token: 0x04000C74 RID: 3188
	public static Footsteps footsteps;

	// Token: 0x04000C75 RID: 3189
	public static GameObject uiItemCamera;

	// Token: 0x04000C76 RID: 3190
	public static DialogueActor actor;

	// Token: 0x04000C77 RID: 3191
	public static HandIK handIK;

	// Token: 0x04000C78 RID: 3192
	public static PlayerEffects effects;

	// Token: 0x04000C79 RID: 3193
	public static RagdollController ragdollController;

	// Token: 0x04000C7A RID: 3194
	public static PlayerActorStates actorStates;

	// Token: 0x04000C7B RID: 3195
	public PlayerInput pInput;

	// Token: 0x04000C7C RID: 3196
	public PlayerMovement pMovement;

	// Token: 0x04000C7D RID: 3197
	public PlayerItemManager pItemManager;

	// Token: 0x04000C7E RID: 3198
	public Transform pTransform;

	// Token: 0x04000C7F RID: 3199
	public Animator pAnimator;

	// Token: 0x04000C80 RID: 3200
	public Rigidbody pRigidbody;

	// Token: 0x04000C81 RID: 3201
	public CharacterReactionController pReaction;

	// Token: 0x04000C82 RID: 3202
	public PlayerOverrideAnimations pOverrideAnimations;

	// Token: 0x04000C83 RID: 3203
	public FootIKSmooth pFootIK;

	// Token: 0x04000C84 RID: 3204
	public Footsteps pFootsteps;

	// Token: 0x04000C85 RID: 3205
	public GameObject pUiItemCamera;

	// Token: 0x04000C86 RID: 3206
	public DialogueActor pActor;

	// Token: 0x04000C87 RID: 3207
	public HandIK pHandIK;

	// Token: 0x04000C88 RID: 3208
	public PlayerEffects pEffects;

	// Token: 0x04000C89 RID: 3209
	public RagdollController pRagdollController;

	// Token: 0x04000C8A RID: 3210
	public PlayerActorStates pActorStates;
}
