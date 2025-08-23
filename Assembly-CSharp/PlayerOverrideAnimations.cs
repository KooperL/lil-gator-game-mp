using System;
using UnityEngine;

public class PlayerOverrideAnimations : MonoBehaviour
{
	// Token: 0x06000CE6 RID: 3302 RVA: 0x0000BF95 File Offset: 0x0000A195
	private void OnValidate()
	{
		if (this.animator == null)
		{
			this.animator = base.GetComponent<Animator>();
		}
	}

	// Token: 0x06000CE7 RID: 3303 RVA: 0x0000BFB1 File Offset: 0x0000A1B1
	private void Awake()
	{
		if (this.overrides == null)
		{
			this.InitializeOverrides();
		}
	}

	// Token: 0x06000CE8 RID: 3304 RVA: 0x000491C8 File Offset: 0x000473C8
	private void InitializeOverrides()
	{
		this.overrideController = new AnimatorOverrideController(this.animator.runtimeAnimatorController);
		this.animator.runtimeAnimatorController = this.overrideController;
		this.overrides = new AnimationClipOverrides(this.overrideController.overridesCount);
		this.overrideController.GetOverrides(this.overrides);
	}

	// Token: 0x06000CE9 RID: 3305 RVA: 0x0000BFC1 File Offset: 0x0000A1C1
	public void SetStandardAnimation(ActorAnimationOverrides.StandardAnimation standardAnimation, AnimationClip animationOverride)
	{
		this.overrides.Set(ActorAnimationOverrides.GetStandardAnimation(standardAnimation, this.overrides), animationOverride);
		this.overrideController.ApplyOverrides(this.overrides);
	}

	// Token: 0x06000CEA RID: 3306 RVA: 0x00049224 File Offset: 0x00047424
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

	// Token: 0x06000CEB RID: 3307 RVA: 0x00049274 File Offset: 0x00047474
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

	// Token: 0x06000CEC RID: 3308 RVA: 0x00049304 File Offset: 0x00047504
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
