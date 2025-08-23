using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimationClipOverrides : List<KeyValuePair<AnimationClip, AnimationClip>>
{
	// Token: 0x06000CDB RID: 3291 RVA: 0x0000BF2D File Offset: 0x0000A12D
	public AnimationClipOverrides(int capacity)
		: base(capacity)
	{
	}

	// Token: 0x06000CDC RID: 3292 RVA: 0x0000BF36 File Offset: 0x0000A136
	public void Set(AnimationClip original, AnimationClip overrideClip)
	{
		this[original] = overrideClip;
	}

	// Token: 0x06000CDD RID: 3293 RVA: 0x0000BF40 File Offset: 0x0000A140
	public void Clear(AnimationClip original)
	{
		this[original] = null;
	}

	// Token: 0x06000CDE RID: 3294 RVA: 0x0000BF4A File Offset: 0x0000A14A
	public void Set(AnimationOverride animationOverride)
	{
		this[animationOverride.originalClip] = animationOverride.overrideClip;
	}

	// Token: 0x06000CDF RID: 3295 RVA: 0x0000BF5E File Offset: 0x0000A15E
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
