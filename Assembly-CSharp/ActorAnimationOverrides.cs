using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActorAnimationOverrides : MonoBehaviour
{
	// Token: 0x06000364 RID: 868 RVA: 0x00025BF8 File Offset: 0x00023DF8
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

	// Token: 0x06000365 RID: 869 RVA: 0x00004A4B File Offset: 0x00002C4B
	private static void OnActiveSceneChanged(Scene arg0, Scene arg1)
	{
		ActorAnimationOverrides.standardAnimations = null;
	}

	// Token: 0x06000366 RID: 870 RVA: 0x00025C98 File Offset: 0x00023E98
	public void SetAnimations(AnimationOverride[] animationOverrides)
	{
		foreach (AnimationOverride animationOverride in animationOverrides)
		{
			this.SetAnimation(animationOverride, false);
		}
		this.overrideController.ApplyOverrides(this.overrides);
	}

	// Token: 0x06000367 RID: 871 RVA: 0x00004A53 File Offset: 0x00002C53
	public void SetAnimation(AnimationOverride animationOverride, bool applyOverrides = true)
	{
		this.overrides.Set(animationOverride);
		if (applyOverrides)
		{
			this.overrideController.ApplyOverrides(this.overrides);
		}
	}

	// Token: 0x06000368 RID: 872 RVA: 0x00004A75 File Offset: 0x00002C75
	public void SetStandardAnimation(ActorAnimationOverrides.StandardAnimation standardAnimation, AnimationClip animationOverride)
	{
		this.overrides.Set(ActorAnimationOverrides.GetStandardAnimation(standardAnimation, this.overrides), animationOverride);
		this.overrideController.ApplyOverrides(this.overrides);
	}

	// Token: 0x06000369 RID: 873 RVA: 0x00025CD8 File Offset: 0x00023ED8
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

	// Token: 0x0600036A RID: 874 RVA: 0x00025DAC File Offset: 0x00023FAC
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

	public DialogueActor actor;

	public Animator animator;

	private AnimatorOverrideController overrideController;

	private AnimationClipOverrides overrides;

	private static AnimationClip[] standardAnimations;

	private bool hasActiveSceneHook;

	public enum StandardAnimation
	{
		Stand,
		Walk,
		Run,
		Action,
		LoopAction,
		RaiseArms
	}
}
