using System;
using UnityEngine;

// Token: 0x0200033D RID: 829
public class FogLOD : MonoBehaviour
{
	// Token: 0x0600101D RID: 4125 RVA: 0x00052FD0 File Offset: 0x000511D0
	private void Awake()
	{
		this.lodTrees = base.GetComponentsInChildren<LODTree>();
		for (int i = 0; i < this.lodTrees.Length; i++)
		{
			this.lodTrees[i].UpdateLODs(this.fogDistance, -1f);
		}
	}

	// Token: 0x0600101E RID: 4126 RVA: 0x00052FD0 File Offset: 0x000511D0
	[ContextMenu("Update All")]
	public void UpdateAllFogDistance()
	{
		this.lodTrees = base.GetComponentsInChildren<LODTree>();
		for (int i = 0; i < this.lodTrees.Length; i++)
		{
			this.lodTrees[i].UpdateLODs(this.fogDistance, -1f);
		}
	}

	// Token: 0x0600101F RID: 4127 RVA: 0x0000DEC0 File Offset: 0x0000C0C0
	[ContextMenu("DebugCrossFade")]
	public void DebugCrossFade()
	{
		Debug.Log(LODGroup.crossFadeAnimationDuration);
	}

	// Token: 0x06001020 RID: 4128 RVA: 0x0000DED1 File Offset: 0x0000C0D1
	[ContextMenu("SetSlow")]
	public void SetSlowCrossFade()
	{
		LODGroup.crossFadeAnimationDuration = 5f;
	}

	// Token: 0x06001021 RID: 4129 RVA: 0x0000DEDD File Offset: 0x0000C0DD
	[ContextMenu("SetDefault")]
	public void SetDefaultCrossFade()
	{
		LODGroup.crossFadeAnimationDuration = 0.5f;
	}

	// Token: 0x040014E6 RID: 5350
	public float fogDistance = 60f;

	// Token: 0x040014E7 RID: 5351
	private LODTree[] lodTrees;
}
