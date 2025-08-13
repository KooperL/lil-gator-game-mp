using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000028 RID: 40
public class MusicZone : MonoBehaviour
{
	// Token: 0x060000A0 RID: 160 RVA: 0x000050EC File Offset: 0x000032EC
	private void OnValidate()
	{
		if (this.musicStateManager == null)
		{
			this.musicStateManager = Object.FindObjectOfType<MusicStateManager>();
		}
		this.hasState = !string.IsNullOrEmpty(this.state);
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x0000511B File Offset: 0x0000331B
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

	// Token: 0x040000D2 RID: 210
	public static Dictionary<string, float> stateIneligibleTimes = new Dictionary<string, float>();

	// Token: 0x040000D3 RID: 211
	public static Dictionary<MusicSystem, float> songIneligibleTimes = new Dictionary<MusicSystem, float>();

	// Token: 0x040000D4 RID: 212
	[HideInInspector]
	public MusicStateManager musicStateManager;

	// Token: 0x040000D5 RID: 213
	[MusicStateLookup("musicStateManager")]
	public string state;

	// Token: 0x040000D6 RID: 214
	[ReadOnly]
	public bool hasState;

	// Token: 0x040000D7 RID: 215
	public MusicSystem musicSystem;

	// Token: 0x040000D8 RID: 216
	public float ineligibleDelay = 0.5f;
}
