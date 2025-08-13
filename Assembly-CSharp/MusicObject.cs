using System;
using UnityEngine;

// Token: 0x02000021 RID: 33
[AddComponentMenu("Music/MusicObject")]
public class MusicObject : MonoBehaviour
{
	// Token: 0x06000066 RID: 102 RVA: 0x0000258C File Offset: 0x0000078C
	private void OnValidate()
	{
		if (this.musicStateManager == null)
		{
			this.musicStateManager = Object.FindObjectOfType<MusicStateManager>();
		}
		this.hasState = !string.IsNullOrEmpty(this.state);
	}

	// Token: 0x06000067 RID: 103 RVA: 0x000025BB File Offset: 0x000007BB
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

	// Token: 0x06000068 RID: 104 RVA: 0x000025EF File Offset: 0x000007EF
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

	// Token: 0x0400007D RID: 125
	[HideInInspector]
	public MusicStateManager musicStateManager;

	// Token: 0x0400007E RID: 126
	[MusicStateLookup("musicStateManager")]
	public string state;

	// Token: 0x0400007F RID: 127
	[ReadOnly]
	public bool hasState;

	// Token: 0x04000080 RID: 128
	public MusicSystem musicSystem;
}
