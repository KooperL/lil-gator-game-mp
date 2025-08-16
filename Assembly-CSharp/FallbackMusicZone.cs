using System;
using UnityEngine;

public class FallbackMusicZone : MonoBehaviour
{
	// Token: 0x0600006A RID: 106 RVA: 0x000025BB File Offset: 0x000007BB
	private void Awake()
	{
		if (!string.IsNullOrEmpty(this.stateName))
		{
			this.dynamicStateIndex = this.dynamicStates.GetStateIndex(this.stateName);
		}
	}

	// Token: 0x0600006B RID: 107 RVA: 0x000186F4 File Offset: 0x000168F4
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

	public MusicSystem musicToOverride;

	public MusicSystem musicToUse;

	public MusicSystemDynamicStates dynamicStates;

	[DynamicStateLookup("dynamicStates")]
	public string stateName;

	private int dynamicStateIndex = -1;
}
