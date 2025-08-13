using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000280 RID: 640
public class AnimationClipOverrides : List<KeyValuePair<AnimationClip, AnimationClip>>
{
	// Token: 0x06000C8E RID: 3214 RVA: 0x0000BC1B File Offset: 0x00009E1B
	public AnimationClipOverrides(int capacity)
		: base(capacity)
	{
	}

	// Token: 0x06000C8F RID: 3215 RVA: 0x0000BC24 File Offset: 0x00009E24
	public void Set(AnimationClip original, AnimationClip overrideClip)
	{
		this[original] = overrideClip;
	}

	// Token: 0x06000C90 RID: 3216 RVA: 0x0000BC2E File Offset: 0x00009E2E
	public void Clear(AnimationClip original)
	{
		this[original] = null;
	}

	// Token: 0x06000C91 RID: 3217 RVA: 0x0000BC38 File Offset: 0x00009E38
	public void Set(AnimationOverride animationOverride)
	{
		this[animationOverride.originalClip] = animationOverride.overrideClip;
	}

	// Token: 0x06000C92 RID: 3218 RVA: 0x0000BC4C File Offset: 0x00009E4C
	public void Clear(AnimationOverride animationOverride)
	{
		this[animationOverride.originalClip] = null;
	}

	// Token: 0x17000144 RID: 324
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
