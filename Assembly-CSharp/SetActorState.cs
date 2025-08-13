using System;
using UnityEngine;

// Token: 0x020000FA RID: 250
public class SetActorState : MonoBehaviour
{
	// Token: 0x060004BB RID: 1211 RVA: 0x0002B9D0 File Offset: 0x00029BD0
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

	// Token: 0x060004BC RID: 1212 RVA: 0x0002BA24 File Offset: 0x00029C24
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

	// Token: 0x060004BD RID: 1213 RVA: 0x00002229 File Offset: 0x00000429
	public void SetOverrides()
	{
	}

	// Token: 0x060004BE RID: 1214 RVA: 0x00005750 File Offset: 0x00003950
	private void OnDisable()
	{
		if (this.resetTransformOnDisable)
		{
			this.actorTransform.position = this.initialPosition;
			this.actorTransform.rotation = this.initialRotation;
		}
	}

	// Token: 0x060004BF RID: 1215 RVA: 0x0002BCF0 File Offset: 0x00029EF0
	[ContextMenu("Snap To Floor")]
	public void SnapToFloor()
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position + Vector3.up, Vector3.down, ref raycastHit, 3f))
		{
			base.transform.position = raycastHit.point;
		}
	}

	// Token: 0x040006A3 RID: 1699
	public bool actorIsPlayer;

	// Token: 0x040006A4 RID: 1700
	[ConditionalHide("actorIsPlayer", true, Inverse = true)]
	public DialogueActor actor;

	// Token: 0x040006A5 RID: 1701
	public ActorPosition position;

	// Token: 0x040006A6 RID: 1702
	public ActorState state;

	// Token: 0x040006A7 RID: 1703
	public bool skipTransition;

	// Token: 0x040006A8 RID: 1704
	public string emote = "";

	// Token: 0x040006A9 RID: 1705
	public bool holdEmote;

	// Token: 0x040006AA RID: 1706
	public bool stopEmote;

	// Token: 0x040006AB RID: 1707
	public bool applyTransform;

	// Token: 0x040006AC RID: 1708
	[HideInInspector]
	public Transform actorTransform;

	// Token: 0x040006AD RID: 1709
	[ConditionalHide("applyTransform", true)]
	public bool snapToFloor;

	// Token: 0x040006AE RID: 1710
	public bool replaceDefaultPose;

	// Token: 0x040006AF RID: 1711
	[ConditionalHide("replaceDefaultPose", true)]
	public AnimationClip newDefaultPose;

	// Token: 0x040006B0 RID: 1712
	private static AnimationClip defaultPose;

	// Token: 0x040006B1 RID: 1713
	public bool replaceWalkAnimation;

	// Token: 0x040006B2 RID: 1714
	[ConditionalHide("replaceWalkAnimation", true)]
	public AnimationClip newWalkAnimation;

	// Token: 0x040006B3 RID: 1715
	private static AnimationClip walkAnimation;

	// Token: 0x040006B4 RID: 1716
	public bool replaceRunAnimation;

	// Token: 0x040006B5 RID: 1717
	[ConditionalHide("replaceRunAnimation", true)]
	public AnimationClip newRunAnimation;

	// Token: 0x040006B6 RID: 1718
	private static AnimationClip runAnimation;

	// Token: 0x040006B7 RID: 1719
	public bool customActionAnimation;

	// Token: 0x040006B8 RID: 1720
	[ConditionalHide("customActionAnimation", true)]
	public AnimationClip newActionAnimation;

	// Token: 0x040006B9 RID: 1721
	[ConditionalHide("customActionAnimation", true)]
	public bool setActionEmote;

	// Token: 0x040006BA RID: 1722
	[ConditionalHide("customActionAnimation", true)]
	public bool isLoopAction;

	// Token: 0x040006BB RID: 1723
	public AnimationOverride[] animationOverrides;

	// Token: 0x040006BC RID: 1724
	public bool resetTransformOnDisable;

	// Token: 0x040006BD RID: 1725
	private Vector3 initialPosition;

	// Token: 0x040006BE RID: 1726
	private Quaternion initialRotation;

	// Token: 0x040006BF RID: 1727
	private int stateID = Animator.StringToHash("State");

	// Token: 0x040006C0 RID: 1728
	private int positionID = Animator.StringToHash("Position");
}
