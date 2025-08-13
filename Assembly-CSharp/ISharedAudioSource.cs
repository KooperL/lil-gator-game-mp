using System;
using UnityEngine;

// Token: 0x0200009F RID: 159
public interface ISharedAudioSource
{
	// Token: 0x0600023C RID: 572
	void GetAudioData(Vector3 positionReference, out SharedAudioProfile profile, out Vector3 direction, out float strength);

	// Token: 0x0600023D RID: 573
	void WasRemoved();
}
