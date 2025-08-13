using System;
using UnityEngine;

// Token: 0x020000B5 RID: 181
[CreateAssetMenu(fileName = "NewAnimationSet", menuName = "ScriptableObject/Animation Set")]
public class AnimationSet : ScriptableObject
{
	// Token: 0x040003B4 RID: 948
	public AnimationClip[] animations;

	// Token: 0x040003B5 RID: 949
	public AnimationClip blendMinus1;

	// Token: 0x040003B6 RID: 950
	public AnimationClip blend0;

	// Token: 0x040003B7 RID: 951
	public AnimationClip blendPlus1;
}
