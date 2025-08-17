using System;
using UnityEngine;

public class FogLOD : MonoBehaviour
{
	// Token: 0x06001078 RID: 4216 RVA: 0x00054EF4 File Offset: 0x000530F4
	private void Awake()
	{
		this.lodTrees = base.GetComponentsInChildren<LODTree>();
		for (int i = 0; i < this.lodTrees.Length; i++)
		{
			this.lodTrees[i].UpdateLODs(this.fogDistance, -1f);
		}
	}

	// Token: 0x06001079 RID: 4217 RVA: 0x00054EF4 File Offset: 0x000530F4
	[ContextMenu("Update All")]
	public void UpdateAllFogDistance()
	{
		this.lodTrees = base.GetComponentsInChildren<LODTree>();
		for (int i = 0; i < this.lodTrees.Length; i++)
		{
			this.lodTrees[i].UpdateLODs(this.fogDistance, -1f);
		}
	}

	// Token: 0x0600107A RID: 4218 RVA: 0x0000E229 File Offset: 0x0000C429
	[ContextMenu("DebugCrossFade")]
	public void DebugCrossFade()
	{
		Debug.Log(LODGroup.crossFadeAnimationDuration);
	}

	// Token: 0x0600107B RID: 4219 RVA: 0x0000E23A File Offset: 0x0000C43A
	[ContextMenu("SetSlow")]
	public void SetSlowCrossFade()
	{
		LODGroup.crossFadeAnimationDuration = 5f;
	}

	// Token: 0x0600107C RID: 4220 RVA: 0x0000E246 File Offset: 0x0000C446
	[ContextMenu("SetDefault")]
	public void SetDefaultCrossFade()
	{
		LODGroup.crossFadeAnimationDuration = 0.5f;
	}

	public float fogDistance = 60f;

	private LODTree[] lodTrees;
}
