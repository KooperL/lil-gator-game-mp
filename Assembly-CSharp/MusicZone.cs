using System;
using System.Collections.Generic;
using UnityEngine;

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

	public static Dictionary<string, float> stateIneligibleTimes = new Dictionary<string, float>();

	public static Dictionary<MusicSystem, float> songIneligibleTimes = new Dictionary<MusicSystem, float>();

	[HideInInspector]
	public MusicStateManager musicStateManager;

	[MusicStateLookup("musicStateManager")]
	public string state;

	[ReadOnly]
	public bool hasState;

	public MusicSystem musicSystem;

	public float ineligibleDelay = 0.5f;
}
