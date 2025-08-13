using System;
using UnityEngine;

// Token: 0x02000156 RID: 342
public class LODDistance : MonoBehaviour
{
	// Token: 0x06000733 RID: 1843 RVA: 0x00023FEC File Offset: 0x000221EC
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

	// Token: 0x04000972 RID: 2418
	private LODGroup lodGroup;

	// Token: 0x04000973 RID: 2419
	public float distance = 40f;
}
