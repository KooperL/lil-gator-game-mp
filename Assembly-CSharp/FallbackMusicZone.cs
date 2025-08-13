using System;
using UnityEngine;

// Token: 0x02000021 RID: 33
public class FallbackMusicZone : MonoBehaviour
{
	// Token: 0x06000069 RID: 105 RVA: 0x000037C1 File Offset: 0x000019C1
	private void Awake()
	{
		if (!string.IsNullOrEmpty(this.stateName))
		{
			this.dynamicStateIndex = this.dynamicStates.GetStateIndex(this.stateName);
		}
	}

	// Token: 0x0600006A RID: 106 RVA: 0x000037E8 File Offset: 0x000019E8
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

	// Token: 0x0400008E RID: 142
	public MusicSystem musicToOverride;

	// Token: 0x0400008F RID: 143
	public MusicSystem musicToUse;

	// Token: 0x04000090 RID: 144
	public MusicSystemDynamicStates dynamicStates;

	// Token: 0x04000091 RID: 145
	[DynamicStateLookup("dynamicStates")]
	public string stateName;

	// Token: 0x04000092 RID: 146
	private int dynamicStateIndex = -1;
}
