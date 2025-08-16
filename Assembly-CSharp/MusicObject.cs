using System;
using UnityEngine;

[AddComponentMenu("Music/MusicObject")]
public class MusicObject : MonoBehaviour
{
	// Token: 0x0600006E RID: 110 RVA: 0x000025F0 File Offset: 0x000007F0
	private void OnValidate()
	{
		if (this.musicStateManager == null)
		{
			this.musicStateManager = global::UnityEngine.Object.FindObjectOfType<MusicStateManager>();
		}
		this.hasState = !string.IsNullOrEmpty(this.state);
	}

	// Token: 0x0600006F RID: 111 RVA: 0x0000261F File Offset: 0x0000081F
	private void OnEnable()
	{
		if (this.hasState)
		{
			MusicStateManager.m.LockState(this.state);
		}
		if (this.musicSystem != null)
		{
			this.musicSystem.isLocked = true;
		}
	}

	// Token: 0x06000070 RID: 112 RVA: 0x00002653 File Offset: 0x00000853
	private void OnDisable()
	{
		if (this.hasState)
		{
			MusicStateManager.m.ClearState(this.state);
		}
		if (this.musicSystem != null)
		{
			this.musicSystem.isLocked = false;
		}
	}

	[HideInInspector]
	public MusicStateManager musicStateManager;

	[MusicStateLookup("musicStateManager")]
	public string state;

	[ReadOnly]
	public bool hasState;

	public MusicSystem musicSystem;
}
