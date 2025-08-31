using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct AnimationOverride
{
	// Token: 0x06000AD8 RID: 2776 RVA: 0x0003629A File Offset: 0x0003449A
	public KeyValuePair<AnimationClip, AnimationClip> GetPair()
	{
		return new KeyValuePair<AnimationClip, AnimationClip>(this.originalClip, this.overrideClip);
	}

	public AnimationClip originalClip;

	public AnimationClip overrideClip;
}
