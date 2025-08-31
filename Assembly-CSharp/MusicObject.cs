using System;
using UnityEngine;

[AddComponentMenu("Music/MusicObject")]
public class MusicObject : MonoBehaviour
{
	// Token: 0x0600006D RID: 109 RVA: 0x0000385D File Offset: 0x00001A5D
	private void OnValidate()
	{
		if (this.musicStateManager == null)
		{
			this.musicStateManager = Object.FindObjectOfType<MusicStateManager>();
		}
		this.hasState = !string.IsNullOrEmpty(this.state);
	}

	// Token: 0x0600006E RID: 110 RVA: 0x0000388C File Offset: 0x00001A8C
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

	// Token: 0x0600006F RID: 111 RVA: 0x000038C0 File Offset: 0x00001AC0
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
