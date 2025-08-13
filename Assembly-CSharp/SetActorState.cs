using System;
using UnityEngine;

// Token: 0x020000BB RID: 187
public class SetActorState : MonoBehaviour
{
	// Token: 0x06000407 RID: 1031 RVA: 0x00017850 File Offset: 0x00015A50
	private void OnValidate()
	{
		if (this.actor == null)
		{
			this.actor = base.GetComponent<DialogueActor>();
		}
		if (this.customActionAnimation && this.setActionEmote)
		{
			this.emote = (this.isLoopAction ? "LoopAction" : "Action");
		}
	}

	// Token: 0x06000408 RID: 1032 RVA: 0x000178A4 File Offset: 0x00015AA4
	[ContextMenu("ApplyState")]
	private void OnEnable()
	{
		if (this.actorIsPlayer)
		{
			this.actor = DialogueActor.playerActor;
		}
		this.actorTransform = this.actor.transform;
		if (this.resetTransformOnDisable && this.actorTransform != null)
		{
			this.initialPosition = this.actorTransform.position;
			this.initialRotation = this.actorTransform.rotation;
		}
		if (this.applyTransform)
		{
			this.skipTransition = true;
		}
		this.actor.SetStateAndPosition((int)this.state, (int)this.position, this.skipTransition, this.skipTransition);
		if (this.applyTransform)
		{
			IActorMover component = this.actor.GetComponent<IActorMover>();
			if (component != null)
			{
				component.CancelMove();
			}
			MountedActor component2 = this.actor.GetComponent<MountedActor>();
			if (component2 != null)
			{
				component2.CancelMount();
			}
			if (this.actorIsPlayer)
			{
				Player.movement.ApplyTransform(base.transform);
			}
			else
			{
				this.actorTransform.ApplyTransform(base.transform);
			}
		}
		if (this.replaceDefaultPose || this.replaceWalkAnimation || this.replaceRunAnimation || this.customActionAnimation || (this.animationOverrides != null && this.animationOverrides.Length != 0))
		{
			PlayerOverrideAnimations overrideAnimations = Player.overrideAnimations;
			ActorAnimationOverrides actorAnimationOverrides = this.actor.GetComponent<ActorAnimationOverrides>();
			if (!this.actor.isPlayer && actorAnimationOverrides == null)
			{
				actorAnimationOverrides = this.actor.gameObject.AddComponent<ActorAnimationOverrides>();
			}
			if (this.replaceDefaultPose)
			{
				if (this.actor.isPlayer)
				{
					overrideAnimations.SetStandardAnimation(ActorAnimationOverrides.StandardAnimation.Stand, this.newDefaultPose);
				}
				else
				{
					actorAnimationOverrides.SetStandardAnimation(ActorAnimationOverrides.StandardAnimation.Stand, this.newDefaultPose);
				}
			}
			if (this.replaceWalkAnimation)
			{
				if (this.actor.isPlayer)
				{
					overrideAnimations.SetStandardAnimation(ActorAnimationOverrides.StandardAnimation.Walk, this.newWalkAnimation);
				}
				else
				{
					actorAnimationOverrides.SetStandardAnimation(ActorAnimationOverrides.StandardAnimation.Walk, this.newWalkAnimation);
				}
			}
			if (this.replaceRunAnimation)
			{
				if (this.actor.isPlayer)
				{
					overrideAnimations.SetStandardAnimation(ActorAnimationOverrides.StandardAnimation.Run, this.newRunAnimation);
				}
				else
				{
					actorAnimationOverrides.SetStandardAnimation(ActorAnimationOverrides.StandardAnimation.Run, this.newRunAnimation);
				}
			}
			if (this.customActionAnimation)
			{
				if (this.actor.isPlayer)
				{
					overrideAnimations.SetStandardAnimation(this.isLoopAction ? ActorAnimationOverrides.StandardAnimation.LoopAction : ActorAnimationOverrides.StandardAnimation.Action, this.newActionAnimation);
				}
				else
				{
					actorAnimationOverrides.SetStandardAnimation(this.isLoopAction ? ActorAnimationOverrides.StandardAnimation.LoopAction : ActorAnimationOverrides.StandardAnimation.Action, this.newActionAnimation);
				}
			}
			if (this.animationOverrides != null && this.animationOverrides.Length != 0 && !this.actor.isPlayer)
			{
				actorAnimationOverrides.SetAnimations(this.animationOverrides);
			}
		}
		if (this.stopEmote)
		{
			this.actor.ClearEmote(true, true);
		}
		if (this.emote != "")
		{
			this.actor.SetEmote(Animator.StringToHash(this.emote), this.skipTransition);
		}
		if (this.holdEmote)
		{
			this.actor.HoldEmote();
		}
	}

	// Token: 0x06000409 RID: 1033 RVA: 0x00017B6D File Offset: 0x00015D6D
	public void SetOverrides()
	{
	}

	// Token: 0x0600040A RID: 1034 RVA: 0x00017B6F File Offset: 0x00015D6F
	private void OnDisable()
	{
		if (this.resetTransformOnDisable)
		{
			this.actorTransform.position = this.initialPosition;
			this.actorTransform.rotation = this.initialRotation;
		}
	}

	// Token: 0x0600040B RID: 1035 RVA: 0x00017B9C File Offset: 0x00015D9C
	[ContextMenu("Snap To Floor")]
	public void SnapToFloor()
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position + Vector3.up, Vector3.down, out raycastHit, 3f))
		{
			base.transform.position = raycastHit.point;
		}
	}

	// Token: 0x04000591 RID: 1425
	public bool actorIsPlayer;

	// Token: 0x04000592 RID: 1426
	[ConditionalHide("actorIsPlayer", true, Inverse = true)]
	public DialogueActor actor;

	// Token: 0x04000593 RID: 1427
	public ActorPosition position;

	// Token: 0x04000594 RID: 1428
	public ActorState state;

	// Token: 0x04000595 RID: 1429
	public bool skipTransition;

	// Token: 0x04000596 RID: 1430
	public string emote = "";

	// Token: 0x04000597 RID: 1431
	public bool holdEmote;

	// Token: 0x04000598 RID: 1432
	public bool stopEmote;

	// Token: 0x04000599 RID: 1433
	public bool applyTransform;

	// Token: 0x0400059A RID: 1434
	[HideInInspector]
	public Transform actorTransform;

	// Token: 0x0400059B RID: 1435
	[ConditionalHide("applyTransform", true)]
	public bool snapToFloor;

	// Token: 0x0400059C RID: 1436
	public bool replaceDefaultPose;

	// Token: 0x0400059D RID: 1437
	[ConditionalHide("replaceDefaultPose", true)]
	public AnimationClip newDefaultPose;

	// Token: 0x0400059E RID: 1438
	private static AnimationClip defaultPose;

	// Token: 0x0400059F RID: 1439
	public bool replaceWalkAnimation;

	// Token: 0x040005A0 RID: 1440
	[ConditionalHide("replaceWalkAnimation", true)]
	public AnimationClip newWalkAnimation;

	// Token: 0x040005A1 RID: 1441
	private static AnimationClip walkAnimation;

	// Token: 0x040005A2 RID: 1442
	public bool replaceRunAnimation;

	// Token: 0x040005A3 RID: 1443
	[ConditionalHide("replaceRunAnimation", true)]
	public AnimationClip newRunAnimation;

	// Token: 0x040005A4 RID: 1444
	private static AnimationClip runAnimation;

	// Token: 0x040005A5 RID: 1445
	public bool customActionAnimation;

	// Token: 0x040005A6 RID: 1446
	[ConditionalHide("customActionAnimation", true)]
	public AnimationClip newActionAnimation;

	// Token: 0x040005A7 RID: 1447
	[ConditionalHide("customActionAnimation", true)]
	public bool setActionEmote;

	// Token: 0x040005A8 RID: 1448
	[ConditionalHide("customActionAnimation", true)]
	public bool isLoopAction;

	// Token: 0x040005A9 RID: 1449
	public AnimationOverride[] animationOverrides;

	// Token: 0x040005AA RID: 1450
	public bool resetTransformOnDisable;

	// Token: 0x040005AB RID: 1451
	private Vector3 initialPosition;

	// Token: 0x040005AC RID: 1452
	private Quaternion initialRotation;

	// Token: 0x040005AD RID: 1453
	private int stateID = Animator.StringToHash("State");

	// Token: 0x040005AE RID: 1454
	private int positionID = Animator.StringToHash("Position");
}
