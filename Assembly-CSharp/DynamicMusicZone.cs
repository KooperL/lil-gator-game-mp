using System;
using UnityEngine;

// Token: 0x0200001E RID: 30
public class DynamicMusicZone : MonoBehaviour
{
	// Token: 0x0600005F RID: 95 RVA: 0x0000250F File Offset: 0x0000070F
	private void Awake()
	{
		if (!string.IsNullOrEmpty(this.stateName))
		{
			this.dynamicStateIndex = this.dynamicStates.GetStateIndex(this.stateName);
		}
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00002535 File Offset: 0x00000735
	private void OnTriggerStay(Collider other)
	{
		this.dynamicStates.MarkStateEligible(this.dynamicStateIndex);
	}

	// Token: 0x04000075 RID: 117
	public MusicSystemDynamicStates dynamicStates;

	// Token: 0x04000076 RID: 118
	[DynamicStateLookup("dynamicStates")]
	public string stateName;

	// Token: 0x04000077 RID: 119
	private int dynamicStateIndex = -1;
}
