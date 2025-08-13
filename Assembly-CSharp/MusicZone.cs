using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000032 RID: 50
public class MusicZone : MonoBehaviour
{
	// Token: 0x060000AC RID: 172 RVA: 0x00002941 File Offset: 0x00000B41
	private void OnValidate()
	{
		if (this.musicStateManager == null)
		{
			this.musicStateManager = Object.FindObjectOfType<MusicStateManager>();
		}
		this.hasState = !string.IsNullOrEmpty(this.state);
	}

	// Token: 0x060000AD RID: 173 RVA: 0x00002970 File Offset: 0x00000B70
	private void OnTriggerStay(Collider other)
	{
		if (this.hasState)
		{
			MusicStateManager.m.MarkState(this.state);
		}
		if (this.musicSystem != null)
		{
			this.musicSystem.MarkEligible();
		}
	}

	// Token: 0x040000F9 RID: 249
	public static Dictionary<string, float> stateIneligibleTimes = new Dictionary<string, float>();

	// Token: 0x040000FA RID: 250
	public static Dictionary<MusicSystem, float> songIneligibleTimes = new Dictionary<MusicSystem, float>();

	// Token: 0x040000FB RID: 251
	[HideInInspector]
	public MusicStateManager musicStateManager;

	// Token: 0x040000FC RID: 252
	[MusicStateLookup("musicStateManager")]
	public string state;

	// Token: 0x040000FD RID: 253
	[ReadOnly]
	public bool hasState;

	// Token: 0x040000FE RID: 254
	public MusicSystem musicSystem;

	// Token: 0x040000FF RID: 255
	public float ineligibleDelay = 0.5f;
}
