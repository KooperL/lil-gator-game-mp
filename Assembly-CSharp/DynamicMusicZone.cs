using System;
using UnityEngine;

// Token: 0x02000020 RID: 32
public class DynamicMusicZone : MonoBehaviour
{
	// Token: 0x06000066 RID: 102 RVA: 0x00003779 File Offset: 0x00001979
	private void Awake()
	{
		if (!string.IsNullOrEmpty(this.stateName))
		{
			this.dynamicStateIndex = this.dynamicStates.GetStateIndex(this.stateName);
		}
	}

	// Token: 0x06000067 RID: 103 RVA: 0x0000379F File Offset: 0x0000199F
	private void OnTriggerStay(Collider other)
	{
		this.dynamicStates.MarkStateEligible(this.dynamicStateIndex);
	}

	// Token: 0x0400008B RID: 139
	public MusicSystemDynamicStates dynamicStates;

	// Token: 0x0400008C RID: 140
	[DynamicStateLookup("dynamicStates")]
	public string stateName;

	// Token: 0x0400008D RID: 141
	private int dynamicStateIndex = -1;
}
