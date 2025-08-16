using System;
using UnityEngine;

public class FogLOD : MonoBehaviour
{
	// Token: 0x06001078 RID: 4216 RVA: 0x00054D60 File Offset: 0x00052F60
	private void Awake()
	{
		this.lodTrees = base.GetComponentsInChildren<LODTree>();
		for (int i = 0; i < this.lodTrees.Length; i++)
		{
			this.lodTrees[i].UpdateLODs(this.fogDistance, -1f);
		}
	}

	// Token: 0x06001079 RID: 4217 RVA: 0x00054D60 File Offset: 0x00052F60
	[ContextMenu("Update All")]
	public void UpdateAllFogDistance()
	{
		this.lodTrees = base.GetComponentsInChildren<LODTree>();
		for (int i = 0; i < this.lodTrees.Length; i++)
		{
			this.lodTrees[i].UpdateLODs(this.fogDistance, -1f);
		}
	}

	// Token: 0x0600107A RID: 4218 RVA: 0x0000E214 File Offset: 0x0000C414
	[ContextMenu("DebugCrossFade")]
	public void DebugCrossFade()
	{
		Debug.Log(LODGroup.crossFadeAnimationDuration);
	}

	// Token: 0x0600107B RID: 4219 RVA: 0x0000E225 File Offset: 0x0000C425
	[ContextMenu("SetSlow")]
	public void SetSlowCrossFade()
	{
		LODGroup.crossFadeAnimationDuration = 5f;
	}

	// Token: 0x0600107C RID: 4220 RVA: 0x0000E231 File Offset: 0x0000C431
	[ContextMenu("SetDefault")]
	public void SetDefaultCrossFade()
	{
		LODGroup.crossFadeAnimationDuration = 0.5f;
	}

	public float fogDistance = 60f;

	private LODTree[] lodTrees;
}
