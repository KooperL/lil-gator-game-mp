using System;
using UnityEngine;

public class PlayerOverrideAnimations : MonoBehaviour
{
	// Token: 0x06000AE0 RID: 2784 RVA: 0x0003638B File Offset: 0x0003458B
	private void OnValidate()
	{
		if (this.animator == null)
		{
			this.animator = base.GetComponent<Animator>();
		}
	}

	// Token: 0x06000AE1 RID: 2785 RVA: 0x000363A7 File Offset: 0x000345A7
	private void Awake()
	{
		if (this.overrides == null)
		{
			this.InitializeOverrides();
		}
	}

	// Token: 0x06000AE2 RID: 2786 RVA: 0x000363B8 File Offset: 0x000345B8
	private void InitializeOverrides()
	{
		this.overrideController = new AnimatorOverrideController(this.animator.runtimeAnimatorController);
		this.animator.runtimeAnimatorController = this.overrideController;
		this.overrides = new AnimationClipOverrides(this.overrideController.overridesCount);
		this.overrideController.GetOverrides(this.overrides);
	}

	// Token: 0x06000AE3 RID: 2787 RVA: 0x00036413 File Offset: 0x00034613
	public void SetStandardAnimation(ActorAnimationOverrides.StandardAnimation standardAnimation, AnimationClip animationOverride)
	{
		this.overrides.Set(ActorAnimationOverrides.GetStandardAnimation(standardAnimation, this.overrides), animationOverride);
		this.overrideController.ApplyOverrides(this.overrides);
	}

	// Token: 0x06000AE4 RID: 2788 RVA: 0x00036440 File Offset: 0x00034640
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

	// Token: 0x06000AE5 RID: 2789 RVA: 0x00036490 File Offset: 0x00034690
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

	// Token: 0x06000AE6 RID: 2790 RVA: 0x00036520 File Offset: 0x00034720
	public void ClearOverrides(AnimationOverride[] animations)
	{
		foreach (AnimationOverride animationOverride in animations)
		{
			this.overrides.Clear(animationOverride);
		}
		this.overrideController.ApplyOverrides(this.overrides);
	}

	public Animator animator;

	private AnimatorOverrideController overrideController;

	private AnimationClipOverrides overrides;

	public AnimationClip[] contextualAnimations;

	public AnimationClip blendMinus1;

	public AnimationClip blend0;

	public AnimationClip blendPlus1;
}
