using System;
using UnityEngine;

public class DynamicMusicZone : MonoBehaviour
{
	// Token: 0x06000067 RID: 103 RVA: 0x00002573 File Offset: 0x00000773
	private void Awake()
	{
		if (!string.IsNullOrEmpty(this.stateName))
		{
			this.dynamicStateIndex = this.dynamicStates.GetStateIndex(this.stateName);
		}
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00002599 File Offset: 0x00000799
	private void OnTriggerStay(Collider other)
	{
		this.dynamicStates.MarkStateEligible(this.dynamicStateIndex);
	}

	public MusicSystemDynamicStates dynamicStates;

	[DynamicStateLookup("dynamicStates")]
	public string stateName;

	private int dynamicStateIndex = -1;
}
