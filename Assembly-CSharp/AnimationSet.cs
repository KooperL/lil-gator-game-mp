using System;
using UnityEngine;

// Token: 0x0200008F RID: 143
[CreateAssetMenu(fileName = "NewAnimationSet", menuName = "ScriptableObject/Animation Set")]
public class AnimationSet : ScriptableObject
{
	// Token: 0x0400031B RID: 795
	public AnimationClip[] animations;

	// Token: 0x0400031C RID: 796
	public AnimationClip blendMinus1;

	// Token: 0x0400031D RID: 797
	public AnimationClip blend0;

	// Token: 0x0400031E RID: 798
	public AnimationClip blendPlus1;
}
