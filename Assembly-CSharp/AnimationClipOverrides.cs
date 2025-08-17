using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimationClipOverrides : List<KeyValuePair<AnimationClip, AnimationClip>>
{
	// Token: 0x06000CDA RID: 3290 RVA: 0x0000BF23 File Offset: 0x0000A123
	public AnimationClipOverrides(int capacity)
		: base(capacity)
	{
	}

	// Token: 0x06000CDB RID: 3291 RVA: 0x0000BF2C File Offset: 0x0000A12C
	public void Set(AnimationClip original, AnimationClip overrideClip)
	{
		this[original] = overrideClip;
	}

	// Token: 0x06000CDC RID: 3292 RVA: 0x0000BF36 File Offset: 0x0000A136
	public void Clear(AnimationClip original)
	{
		this[original] = null;
	}

	// Token: 0x06000CDD RID: 3293 RVA: 0x0000BF40 File Offset: 0x0000A140
	public void Set(AnimationOverride animationOverride)
	{
		this[animationOverride.originalClip] = animationOverride.overrideClip;
	}

	// Token: 0x06000CDE RID: 3294 RVA: 0x0000BF54 File Offset: 0x0000A154
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
