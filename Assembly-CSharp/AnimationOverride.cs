using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200027F RID: 639
[Serializable]
public struct AnimationOverride
{
	// Token: 0x06000C8D RID: 3213 RVA: 0x0000BC08 File Offset: 0x00009E08
	public KeyValuePair<AnimationClip, AnimationClip> GetPair()
	{
		return new KeyValuePair<AnimationClip, AnimationClip>(this.originalClip, this.overrideClip);
	}

	// Token: 0x040010BC RID: 4284
	public AnimationClip originalClip;

	// Token: 0x040010BD RID: 4285
	public AnimationClip overrideClip;
}
