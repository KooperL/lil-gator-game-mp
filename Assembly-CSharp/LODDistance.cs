using System;
using UnityEngine;

// Token: 0x020001BF RID: 447
public class LODDistance : MonoBehaviour
{
	// Token: 0x0600087D RID: 2173 RVA: 0x00037364 File Offset: 0x00035564
	private void OnValidate()
	{
		if (this.lodGroup == null)
		{
			this.lodGroup = base.GetComponent<LODGroup>();
		}
		LOD[] lods = this.lodGroup.GetLODs();
		if (lods.Length == 0 || lods[0].renderers.Length == 0)
		{
			return;
		}
		Bounds bounds = lods[0].renderers[0].bounds;
		for (int i = 1; i < lods[0].renderers.Length; i++)
		{
			bounds.Encapsulate(lods[0].renderers[i].bounds);
		}
		float num = bounds.size.y / (this.distance * Mathf.Tan(0.87266463f));
		lods[0].screenRelativeTransitionHeight = num;
		this.lodGroup.SetLODs(lods);
	}

	// Token: 0x04000B10 RID: 2832
	private LODGroup lodGroup;

	// Token: 0x04000B11 RID: 2833
	public float distance = 40f;
}
