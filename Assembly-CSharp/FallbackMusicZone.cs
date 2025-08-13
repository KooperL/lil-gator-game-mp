using System;
using UnityEngine;

// Token: 0x0200001F RID: 31
public class FallbackMusicZone : MonoBehaviour
{
	// Token: 0x06000062 RID: 98 RVA: 0x00002557 File Offset: 0x00000757
	private void Awake()
	{
		if (!string.IsNullOrEmpty(this.stateName))
		{
			this.dynamicStateIndex = this.dynamicStates.GetStateIndex(this.stateName);
		}
	}

	// Token: 0x06000063 RID: 99 RVA: 0x00018010 File Offset: 0x00016210
	private void OnTriggerStay(Collider other)
	{
		if (this.musicToOverride.isPlaying && !this.musicToOverride.isLocked && !this.musicToOverride.IsEligible)
		{
			this.musicToUse.MarkEligible();
			if (this.dynamicStates != null)
			{
				this.dynamicStates.MarkStateEligible(this.dynamicStateIndex);
			}
		}
	}

	// Token: 0x04000078 RID: 120
	public MusicSystem musicToOverride;

	// Token: 0x04000079 RID: 121
	public MusicSystem musicToUse;

	// Token: 0x0400007A RID: 122
	public MusicSystemDynamicStates dynamicStates;

	// Token: 0x0400007B RID: 123
	[DynamicStateLookup("dynamicStates")]
	public string stateName;

	// Token: 0x0400007C RID: 124
	private int dynamicStateIndex = -1;
}
