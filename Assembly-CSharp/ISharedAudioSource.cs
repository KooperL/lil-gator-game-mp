using System;
using UnityEngine;

// Token: 0x0200007D RID: 125
public interface ISharedAudioSource
{
	// Token: 0x06000204 RID: 516
	void GetAudioData(Vector3 positionReference, out SharedAudioProfile profile, out Vector3 direction, out float strength);

	// Token: 0x06000205 RID: 517
	void WasRemoved();
}
