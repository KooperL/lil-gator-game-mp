using System;
using System.Collections.Generic;
using UnityEngine;

public class MusicZone : MonoBehaviour
{
	// Token: 0x060000B4 RID: 180 RVA: 0x000029A5 File Offset: 0x00000BA5
	private void OnValidate()
	{
		if (this.musicStateManager == null)
		{
			this.musicStateManager = global::UnityEngine.Object.FindObjectOfType<MusicStateManager>();
		}
		this.hasState = !string.IsNullOrEmpty(this.state);
	}

	// Token: 0x060000B5 RID: 181 RVA: 0x000029D4 File Offset: 0x00000BD4
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
