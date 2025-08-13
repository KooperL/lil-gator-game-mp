using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

// Token: 0x020000C8 RID: 200
public class ActorAnimationOverrides : MonoBehaviour
{
	// Token: 0x0600033E RID: 830 RVA: 0x00024C54 File Offset: 0x00022E54
	private void Awake()
	{
		if (!this.hasActiveSceneHook)
		{
			SceneManager.activeSceneChanged += new UnityAction<Scene, Scene>(ActorAnimationOverrides.OnActiveSceneChanged);
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

	// Token: 0x0600033F RID: 831 RVA: 0x00004867 File Offset: 0x00002A67
	private static void OnActiveSceneChanged(Scene arg0, Scene arg1)
	{
		ActorAnimationOverrides.standardAnimations = null;
	}

	// Token: 0x06000340 RID: 832 RVA: 0x00024CF4 File Offset: 0x00022EF4
	public void SetAnimations(AnimationOverride[] animationOverrides)
	{
		foreach (AnimationOverride animationOverride in animationOverrides)
		{
			this.SetAnimation(animationOverride, false);
		}
		this.overrideController.ApplyOverrides(this.overrides);
	}

	// Token: 0x06000341 RID: 833 RVA: 0x0000486F File Offset: 0x00002A6F
	public void SetAnimation(AnimationOverride animationOverride, bool applyOverrides = true)
	{
		this.overrides.Set(animationOverride);
		if (applyOverrides)
		{
			this.overrideController.ApplyOverrides(this.overrides);
		}
	}

	// Token: 0x06000342 RID: 834 RVA: 0x00004891 File Offset: 0x00002A91
	public void SetStandardAnimation(ActorAnimationOverrides.StandardAnimation standardAnimation, AnimationClip animationOverride)
	{
		this.overrides.Set(ActorAnimationOverrides.GetStandardAnimation(standardAnimation, this.overrides), animationOverride);
		this.overrideController.ApplyOverrides(this.overrides);
	}

	// Token: 0x06000343 RID: 835 RVA: 0x00024D34 File Offset: 0x00022F34
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

	// Token: 0x06000344 RID: 836 RVA: 0x00024E08 File Offset: 0x00023008
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

	// Token: 0x040004AB RID: 1195
	public DialogueActor actor;

	// Token: 0x040004AC RID: 1196
	public Animator animator;

	// Token: 0x040004AD RID: 1197
	private AnimatorOverrideController overrideController;

	// Token: 0x040004AE RID: 1198
	private AnimationClipOverrides overrides;

	// Token: 0x040004AF RID: 1199
	private static AnimationClip[] standardAnimations;

	// Token: 0x040004B0 RID: 1200
	private bool hasActiveSceneHook;

	// Token: 0x020000C9 RID: 201
	public enum StandardAnimation
	{
		// Token: 0x040004B2 RID: 1202
		Stand,
		// Token: 0x040004B3 RID: 1203
		Walk,
		// Token: 0x040004B4 RID: 1204
		Run,
		// Token: 0x040004B5 RID: 1205
		Action,
		// Token: 0x040004B6 RID: 1206
		LoopAction,
		// Token: 0x040004B7 RID: 1207
		RaiseArms
	}
}
