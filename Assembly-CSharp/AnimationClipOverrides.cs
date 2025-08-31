using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimationClipOverrides : List<KeyValuePair<AnimationClip, AnimationClip>>
{
	// Token: 0x06000AD9 RID: 2777 RVA: 0x000362AD File Offset: 0x000344AD
	public AnimationClipOverrides(int capacity)
		: base(capacity)
	{
	}

	// Token: 0x06000ADA RID: 2778 RVA: 0x000362B6 File Offset: 0x000344B6
	public void Set(AnimationClip original, AnimationClip overrideClip)
	{
		this[original] = overrideClip;
	}

	// Token: 0x06000ADB RID: 2779 RVA: 0x000362C0 File Offset: 0x000344C0
	public void Clear(AnimationClip original)
	{
		this[original] = null;
	}

	// Token: 0x06000ADC RID: 2780 RVA: 0x000362CA File Offset: 0x000344CA
	public void Set(AnimationOverride animationOverride)
	{
		this[animationOverride.originalClip] = animationOverride.overrideClip;
	}

	// Token: 0x06000ADD RID: 2781 RVA: 0x000362DE File Offset: 0x000344DE
	public void Clear(AnimationOverride animationOverride)
	{
		this[animationOverride.originalClip] = null;
	}

	public AnimationClip this[AnimationClip clip]
	{
		get
		{
			int num = base.FindIndex((KeyValuePair<AnimationClip, AnimationClip> x) => x.Key.Equals(clip));
			if (num != -1)
			{
				return base[num].Value;
			}
			return null;
		}
		set
		{
			int num = base.FindIndex((KeyValuePair<AnimationClip, AnimationClip> x) => x.Key.Equals(clip));
			if (num != -1)
			{
				base[num] = new KeyValuePair<AnimationClip, AnimationClip>(clip, value);
				return;
			}
			base.Add(new KeyValuePair<AnimationClip, AnimationClip>(clip, value));
		}
	}
}
