using System;
using UnityEngine;

public class FogLOD : MonoBehaviour
{
	// Token: 0x06001079 RID: 4217 RVA: 0x000551BC File Offset: 0x000533BC
	private void Awake()
	{
		this.lodTrees = base.GetComponentsInChildren<LODTree>();
		for (int i = 0; i < this.lodTrees.Length; i++)
		{
			this.lodTrees[i].UpdateLODs(this.fogDistance, -1f);
		}
	}

	// Token: 0x0600107A RID: 4218 RVA: 0x000551BC File Offset: 0x000533BC
	[ContextMenu("Update All")]
	public void UpdateAllFogDistance()
	{
		this.lodTrees = base.GetComponentsInChildren<LODTree>();
		for (int i = 0; i < this.lodTrees.Length; i++)
		{
			this.lodTrees[i].UpdateLODs(this.fogDistance, -1f);
		}
	}

	// Token: 0x0600107B RID: 4219 RVA: 0x0000E233 File Offset: 0x0000C433
	[ContextMenu("DebugCrossFade")]
	public void DebugCrossFade()
	{
		Debug.Log(LODGroup.crossFadeAnimationDuration);
	}

	// Token: 0x0600107C RID: 4220 RVA: 0x0000E244 File Offset: 0x0000C444
	[ContextMenu("SetSlow")]
	public void SetSlowCrossFade()
	{
		LODGroup.crossFadeAnimationDuration = 5f;
	}

	// Token: 0x0600107D RID: 4221 RVA: 0x0000E250 File Offset: 0x0000C450
	[ContextMenu("SetDefault")]
	public void SetDefaultCrossFade()
	{
		LODGroup.crossFadeAnimationDuration = 0.5f;
	}

	public float fogDistance = 60f;

	private LODTree[] lodTrees;
}
