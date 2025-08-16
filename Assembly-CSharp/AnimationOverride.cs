using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct AnimationOverride
{
	// Token: 0x06000CD9 RID: 3289 RVA: 0x0000BEFB File Offset: 0x0000A0FB
	public KeyValuePair<AnimationClip, AnimationClip> GetPair()
	{
		return new KeyValuePair<AnimationClip, AnimationClip>(this.originalClip, this.overrideClip);
	}

	public AnimationClip originalClip;

	public AnimationClip overrideClip;
}
