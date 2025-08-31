using System;
using UnityEngine;

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

	public bool actorIsPlayer;

	[ConditionalHide("actorIsPlayer", true, Inverse = true)]
	public DialogueActor actor;

	public ActorPosition position;

	public ActorState state;

	public bool skipTransition;

	public string emote = "";

	public bool holdEmote;

	public bool stopEmote;

	public bool applyTransform;

	[HideInInspector]
	public Transform actorTransform;

	[ConditionalHide("applyTransform", true)]
	public bool snapToFloor;

	public bool replaceDefaultPose;

	[ConditionalHide("replaceDefaultPose", true)]
	public AnimationClip newDefaultPose;

	private static AnimationClip defaultPose;

	public bool replaceWalkAnimation;

	[ConditionalHide("replaceWalkAnimation", true)]
	public AnimationClip newWalkAnimation;

	private static AnimationClip walkAnimation;

	public bool replaceRunAnimation;

	[ConditionalHide("replaceRunAnimation", true)]
	public AnimationClip newRunAnimation;

	private static AnimationClip runAnimation;

	public bool customActionAnimation;

	[ConditionalHide("customActionAnimation", true)]
	public AnimationClip newActionAnimation;

	[ConditionalHide("customActionAnimation", true)]
	public bool setActionEmote;

	[ConditionalHide("customActionAnimation", true)]
	public bool isLoopAction;

	public AnimationOverride[] animationOverrides;

	public bool resetTransformOnDisable;

	private Vector3 initialPosition;

	private Quaternion initialRotation;

	private int stateID = Animator.StringToHash("State");

	private int positionID = Animator.StringToHash("Position");
}
