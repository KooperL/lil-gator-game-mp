using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020000A0 RID: 160
public class ActorAnimationOverrides : MonoBehaviour
{
	// Token: 0x060002FB RID: 763 RVA: 0x000116CC File Offset: 0x0000F8CC
	private void Awake()
	{
		if (!this.hasActiveSceneHook)
		{
			SceneManager.activeSceneChanged += ActorAnimationOverrides.OnActiveSceneChanged;
		}
		if (this.actor == null)
		{
			this.actor = base.GetComponent<DialogueActor>();
		}
		this.animator = this.actor.animator;
		this.overrideController = new AnimatorOverrideController(this.animator.runtimeAnimatorController);
		this.animator.runtimeAnimatorController = this.overrideController;
		this.overrides = new AnimationClipOverrides(this.overrideController.overridesCount);
		this.overrideController.GetOverrides(this.overrides);
	}

	// Token: 0x060002FC RID: 764 RVA: 0x0001176B File Offset: 0x0000F96B
	private static void OnActiveSceneChanged(Scene arg0, Scene arg1)
	{
		ActorAnimationOverrides.standardAnimations = null;
	}

	// Token: 0x060002FD RID: 765 RVA: 0x00011774 File Offset: 0x0000F974
	public void SetAnimations(AnimationOverride[] animationOverrides)
	{
		foreach (AnimationOverride animationOverride in animationOverrides)
		{
			this.SetAnimation(animationOverride, false);
		}
		this.overrideController.ApplyOverrides(this.overrides);
	}

	// Token: 0x060002FE RID: 766 RVA: 0x000117B2 File Offset: 0x0000F9B2
	public void SetAnimation(AnimationOverride animationOverride, bool applyOverrides = true)
	{
		this.overrides.Set(animationOverride);
		if (applyOverrides)
		{
			this.overrideController.ApplyOverrides(this.overrides);
		}
	}

	// Token: 0x060002FF RID: 767 RVA: 0x000117D4 File Offset: 0x0000F9D4
	public void SetStandardAnimation(ActorAnimationOverrides.StandardAnimation standardAnimation, AnimationClip animationOverride)
	{
		this.overrides.Set(ActorAnimationOverrides.GetStandardAnimation(standardAnimation, this.overrides), animationOverride);
		this.overrideController.ApplyOverrides(this.overrides);
	}

	// Token: 0x06000300 RID: 768 RVA: 0x00011800 File Offset: 0x0000FA00
	public static AnimationClip GetStandardAnimation(ActorAnimationOverrides.StandardAnimation standardAnimation, AnimationClipOverrides overrides)
	{
		if (ActorAnimationOverrides.standardAnimations == null || (float)ActorAnimationOverrides.standardAnimations.Length == 0f)
		{
			ActorAnimationOverrides.standardAnimations = new AnimationClip[6];
		}
		if (ActorAnimationOverrides.standardAnimations[(int)standardAnimation] == null)
		{
			string animationName = ActorAnimationOverrides.GetStandardAnimationName(standardAnimation);
			KeyValuePair<AnimationClip, AnimationClip> keyValuePair = overrides.Find((KeyValuePair<AnimationClip, AnimationClip> x) => x.Key.name.Equals(animationName));
			if (keyValuePair.Key == null)
			{
				Debug.Log(string.Concat(new string[]
				{
					"uh oh, could find ",
					standardAnimation.ToString(),
					" (",
					(overrides != null) ? overrides.ToString() : null,
					")"
				}));
				return null;
			}
			ActorAnimationOverrides.standardAnimations[(int)standardAnimation] = keyValuePair.Key;
		}
		return ActorAnimationOverrides.standardAnimations[(int)standardAnimation];
	}

	// Token: 0x06000301 RID: 769 RVA: 0x000118D4 File Offset: 0x0000FAD4
	public static string GetStandardAnimationName(ActorAnimationOverrides.StandardAnimation standardAnimation)
	{
		switch (standardAnimation)
		{
		case ActorAnimationOverrides.StandardAnimation.Stand:
			return "StandIdle";
		case ActorAnimationOverrides.StandardAnimation.Walk:
			return "WalkGeneric";
		case ActorAnimationOverrides.StandardAnimation.Run:
			return "RunGeneric";
		case ActorAnimationOverrides.StandardAnimation.Action:
			return "Action";
		case ActorAnimationOverrides.StandardAnimation.LoopAction:
			return "LoopAction";
		default:
			return "";
		}
	}

	// Token: 0x04000415 RID: 1045
	public DialogueActor actor;

	// Token: 0x04000416 RID: 1046
	public Animator animator;

	// Token: 0x04000417 RID: 1047
	private AnimatorOverrideController overrideController;

	// Token: 0x04000418 RID: 1048
	private AnimationClipOverrides overrides;

	// Token: 0x04000419 RID: 1049
	private static AnimationClip[] standardAnimations;

	// Token: 0x0400041A RID: 1050
	private bool hasActiveSceneHook;

	// Token: 0x0200037E RID: 894
	public enum StandardAnimation
	{
		// Token: 0x04001A96 RID: 6806
		Stand,
		// Token: 0x04001A97 RID: 6807
		Walk,
		// Token: 0x04001A98 RID: 6808
		Run,
		// Token: 0x04001A99 RID: 6809
		Action,
		// Token: 0x04001A9A RID: 6810
		LoopAction,
		// Token: 0x04001A9B RID: 6811
		RaiseArms
	}
}
