using System;
using UnityEngine;

// Token: 0x02000062 RID: 98
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

	// Token: 0x04000212 RID: 530
	public bool overrideDefaultEyes;

	// Token: 0x04000213 RID: 531
	public EyeProfile.EyeState defaultState;

	// Token: 0x04000214 RID: 532
	public EyeProfile.EyeState[] eyeStates;

	// Token: 0x0200036C RID: 876
	[Serializable]
	public struct EyeState
	{
		// Token: 0x04001A4A RID: 6730
		[HideInInspector]
		public string name;

		// Token: 0x04001A4B RID: 6731
		public ActorState actorState;

		// Token: 0x04001A4C RID: 6732
		public Sprite sprite;

		// Token: 0x04001A4D RID: 6733
		public bool flip;

		// Token: 0x04001A4E RID: 6734
		public bool sameDirection;

		// Token: 0x04001A4F RID: 6735
		public Sprite leftEyeSprite;
	}
}
