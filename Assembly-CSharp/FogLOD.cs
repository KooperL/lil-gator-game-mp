using System;
using UnityEngine;

// Token: 0x02000273 RID: 627
public class FogLOD : MonoBehaviour
{
	// Token: 0x06000D68 RID: 3432 RVA: 0x000409E4 File Offset: 0x0003EBE4
	private void Awake()
	{
		this.lodTrees = base.GetComponentsInChildren<LODTree>();
		for (int i = 0; i < this.lodTrees.Length; i++)
		{
			this.lodTrees[i].UpdateLODs(this.fogDistance, -1f);
		}
	}

	// Token: 0x06000D69 RID: 3433 RVA: 0x00040A28 File Offset: 0x0003EC28
	[ContextMenu("Update All")]
	public void UpdateAllFogDistance()
	{
		this.lodTrees = base.GetComponentsInChildren<LODTree>();
		for (int i = 0; i < this.lodTrees.Length; i++)
		{
			this.lodTrees[i].UpdateLODs(this.fogDistance, -1f);
		}
	}

	// Token: 0x06000D6A RID: 3434 RVA: 0x00040A6C File Offset: 0x0003EC6C
	[ContextMenu("DebugCrossFade")]
	public void DebugCrossFade()
	{
		Debug.Log(LODGroup.crossFadeAnimationDuration);
	}

	// Token: 0x06000D6B RID: 3435 RVA: 0x00040A7D File Offset: 0x0003EC7D
	[ContextMenu("SetSlow")]
	public void SetSlowCrossFade()
	{
		LODGroup.crossFadeAnimationDuration = 5f;
	}

	// Token: 0x06000D6C RID: 3436 RVA: 0x00040A89 File Offset: 0x0003EC89
	[ContextMenu("SetDefault")]
	public void SetDefaultCrossFade()
	{
		LODGroup.crossFadeAnimationDuration = 0.5f;
	}

	// Token: 0x040011BA RID: 4538
	public float fogDistance = 60f;

	// Token: 0x040011BB RID: 4539
	private LODTree[] lodTrees;
}
