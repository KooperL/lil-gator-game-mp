using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct AnimationOverride
{
	// Token: 0x06000CDA RID: 3290 RVA: 0x0000BF1A File Offset: 0x0000A11A
	public KeyValuePair<AnimationClip, AnimationClip> GetPair()
	{
		return new KeyValuePair<AnimationClip, AnimationClip>(this.originalClip, this.overrideClip);
	}

	public AnimationClip originalClip;

	public AnimationClip overrideClip;
}
