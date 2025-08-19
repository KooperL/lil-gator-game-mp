using System;
using UnityEngine;

[CreateAssetMenu]
public class EyeProfile : ScriptableObject
{
	// Token: 0x060001A5 RID: 421 RVA: 0x0001D49C File Offset: 0x0001B69C
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

	// Token: 0x060001A6 RID: 422 RVA: 0x0001D4D8 File Offset: 0x0001B6D8
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

	// Token: 0x060001A7 RID: 423 RVA: 0x0001D528 File Offset: 0x0001B728
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
