using System;
using UnityEngine;

public class PlayerOverrideAnimations : MonoBehaviour
{
	// Token: 0x06000CE5 RID: 3301 RVA: 0x0000BF8B File Offset: 0x0000A18B
	private void OnValidate()
	{
		if (this.animator == null)
		{
			this.animator = base.GetComponent<Animator>();
		}
	}

	// Token: 0x06000CE6 RID: 3302 RVA: 0x0000BFA7 File Offset: 0x0000A1A7
	private void Awake()
	{
		if (this.overrides == null)
		{
			this.InitializeOverrides();
		}
	}

	// Token: 0x06000CE7 RID: 3303 RVA: 0x00048F00 File Offset: 0x00047100
	private void InitializeOverrides()
	{
		this.overrideController = new AnimatorOverrideController(this.animator.runtimeAnimatorController);
		this.animator.runtimeAnimatorController = this.overrideController;
		this.overrides = new AnimationClipOverrides(this.overrideController.overridesCount);
		this.overrideController.GetOverrides(this.overrides);
	}

	// Token: 0x06000CE8 RID: 3304 RVA: 0x0000BFB7 File Offset: 0x0000A1B7
	public void SetStandardAnimation(ActorAnimationOverrides.StandardAnimation standardAnimation, AnimationClip animationOverride)
	{
		this.overrides.Set(ActorAnimationOverrides.GetStandardAnimation(standardAnimation, this.overrides), animationOverride);
		this.overrideController.ApplyOverrides(this.overrides);
	}

	// Token: 0x06000CE9 RID: 3305 RVA: 0x00048F5C File Offset: 0x0004715C
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

	// Token: 0x06000CEA RID: 3306 RVA: 0x00048FAC File Offset: 0x000471AC
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

	// Token: 0x06000CEB RID: 3307 RVA: 0x0004903C File Offset: 0x0004723C
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
