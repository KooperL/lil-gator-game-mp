using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001F1 RID: 497
[Serializable]
public struct AnimationOverride
{
	// Token: 0x06000AD8 RID: 2776 RVA: 0x0003629A File Offset: 0x0003449A
	public KeyValuePair<AnimationClip, AnimationClip> GetPair()
	{
		return new KeyValuePair<AnimationClip, AnimationClip>(this.originalClip, this.overrideClip);
	}

	// Token: 0x04000E66 RID: 3686
	public AnimationClip originalClip;

	// Token: 0x04000E67 RID: 3687
	public AnimationClip overrideClip;
}
