using System;
using UnityEngine;

[CreateAssetMenu]
public class EyeProfile : ScriptableObject
{
	// Token: 0x0600016C RID: 364 RVA: 0x00008794 File Offset: 0x00006994
	public EyeProfile.EyeState GetEyeState(ActorState actorState)
	{
		foreach (EyeProfile.EyeState eyeState in this.eyeStates)
		{
			if (eyeState.actorState == actorState)
			{
				return eyeState;
			}
		}
		return this.defaultState;
	}

	// Token: 0x0600016D RID: 365 RVA: 0x000087D0 File Offset: 0x000069D0
	public bool TryGetEyeState(ActorState actorState, out EyeProfile.EyeState eyeState)
	{
		foreach (EyeProfile.EyeState eyeState2 in this.eyeStates)
		{
			if (eyeState2.actorState == actorState)
			{
				eyeState = eyeState2;
				return true;
			}
		}
		eyeState = this.defaultState;
		return this.overrideDefaultEyes;
	}

	// Token: 0x0600016E RID: 366 RVA: 0x00008820 File Offset: 0x00006A20
	private void OnValidate()
	{
		for (int i = 0; i < this.eyeStates.Length; i++)
		{
			this.eyeStates[i].name = this.eyeStates[i].actorState.ToString();
		}
	}

	public bool overrideDefaultEyes;

	public EyeProfile.EyeState defaultState;

	public EyeProfile.EyeState[] eyeStates;

	[Serializable]
	public struct EyeState
	{
		[HideInInspector]
		public string name;

		public ActorState actorState;

		public Sprite sprite;

		public bool flip;

		public bool sameDirection;

		public Sprite leftEyeSprite;
	}
}
