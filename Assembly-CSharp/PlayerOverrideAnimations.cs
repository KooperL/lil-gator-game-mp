using System;
using UnityEngine;

// Token: 0x02000283 RID: 643
public class PlayerOverrideAnimations : MonoBehaviour
{
	// Token: 0x06000C99 RID: 3225 RVA: 0x0000BC83 File Offset: 0x00009E83
	private void OnValidate()
	{
		if (this.animator == null)
		{
			this.animator = base.GetComponent<Animator>();
		}
	}

	// Token: 0x06000C9A RID: 3226 RVA: 0x0000BC9F File Offset: 0x00009E9F
	private void Awake()
	{
		if (this.overrides == null)
		{
			this.InitializeOverrides();
		}
	}

	// Token: 0x06000C9B RID: 3227 RVA: 0x00047378 File Offset: 0x00045578
	private void InitializeOverrides()
	{
		this.overrideController = new AnimatorOverrideController(this.animator.runtimeAnimatorController);
		this.animator.runtimeAnimatorController = this.overrideController;
		this.overrides = new AnimationClipOverrides(this.overrideController.overridesCount);
		this.overrideController.GetOverrides(this.overrides);
	}

	// Token: 0x06000C9C RID: 3228 RVA: 0x0000BCAF File Offset: 0x00009EAF
	public void SetStandardAnimation(ActorAnimationOverrides.StandardAnimation standardAnimation, AnimationClip animationOverride)
	{
		this.overrides.Set(ActorAnimationOverrides.GetStandardAnimation(standardAnimation, this.overrides), animationOverride);
		this.overrideController.ApplyOverrides(this.overrides);
	}

	// Token: 0x06000C9D RID: 3229 RVA: 0x000473D4 File Offset: 0x000455D4
	public void SetOverrides(AnimationOverride[] animations)
	{
		if (this.overrides == null)
		{
			this.InitializeOverrides();
		}
		foreach (AnimationOverride animationOverride in animations)
		{
			this.overrides.Set(animationOverride);
		}
		this.overrideController.ApplyOverrides(this.overrides);
	}

	// Token: 0x06000C9E RID: 3230 RVA: 0x00047424 File Offset: 0x00045624
	public void SetContextualAnimations(AnimationSet animationSet)
	{
		AnimationClip[] animations = animationSet.animations;
		for (int i = 0; i < animations.Length; i++)
		{
			this.overrides.Set(this.contextualAnimations[i], animations[i]);
		}
		this.overrides.Set(this.blendMinus1, animationSet.blendMinus1);
		this.overrides.Set(this.blend0, animationSet.blend0);
		this.overrides.Set(this.blendPlus1, animationSet.blendPlus1);
		this.overrideController.ApplyOverrides(this.overrides);
	}

	// Token: 0x06000C9F RID: 3231 RVA: 0x000474B4 File Offset: 0x000456B4
	public void ClearOverrides(AnimationOverride[] animations)
	{
		foreach (AnimationOverride animationOverride in animations)
		{
			this.overrides.Clear(animationOverride);
		}
		this.overrideController.ApplyOverrides(this.overrides);
	}

	// Token: 0x040010C0 RID: 4288
	public Animator animator;

	// Token: 0x040010C1 RID: 4289
	private AnimatorOverrideController overrideController;

	// Token: 0x040010C2 RID: 4290
	private AnimationClipOverrides overrides;

	// Token: 0x040010C3 RID: 4291
	public AnimationClip[] contextualAnimations;

	// Token: 0x040010C4 RID: 4292
	public AnimationClip blendMinus1;

	// Token: 0x040010C5 RID: 4293
	public AnimationClip blend0;

	// Token: 0x040010C6 RID: 4294
	public AnimationClip blendPlus1;
}
