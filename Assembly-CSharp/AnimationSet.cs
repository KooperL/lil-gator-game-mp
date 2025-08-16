using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAnimationSet", menuName = "ScriptableObject/Animation Set")]
public class AnimationSet : ScriptableObject
{
	public AnimationClip[] animations;

	public AnimationClip blendMinus1;

	public AnimationClip blend0;

	public AnimationClip blendPlus1;
}
