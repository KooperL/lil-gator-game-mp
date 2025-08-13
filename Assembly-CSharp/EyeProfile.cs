using System;
using UnityEngine;

// Token: 0x0200007D RID: 125
[CreateAssetMenu]
public class EyeProfile : ScriptableObject
{
	// Token: 0x06000198 RID: 408 RVA: 0x0001CA80 File Offset: 0x0001AC80
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

	// Token: 0x06000199 RID: 409 RVA: 0x0001CABC File Offset: 0x0001ACBC
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

	// Token: 0x0600019A RID: 410 RVA: 0x0001CB0C File Offset: 0x0001AD0C
	private void OnValidate()
	{
		for (int i = 0; i < this.eyeStates.Length; i++)
		{
			this.eyeStates[i].name = this.eyeStates[i].actorState.ToString();
		}
	}

	// Token: 0x0400027E RID: 638
	public bool overrideDefaultEyes;

	// Token: 0x0400027F RID: 639
	public EyeProfile.EyeState defaultState;

	// Token: 0x04000280 RID: 640
	public EyeProfile.EyeState[] eyeStates;

	// Token: 0x0200007E RID: 126
	[Serializable]
	public struct EyeState
	{
		// Token: 0x04000281 RID: 641
		[HideInInspector]
		public string name;

		// Token: 0x04000282 RID: 642
		public ActorState actorState;

		// Token: 0x04000283 RID: 643
		public Sprite sprite;

		// Token: 0x04000284 RID: 644
		public bool flip;

		// Token: 0x04000285 RID: 645
		public bool sameDirection;

		// Token: 0x04000286 RID: 646
		public Sprite leftEyeSprite;
	}
}
