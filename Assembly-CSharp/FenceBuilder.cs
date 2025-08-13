using System;
using UnityEngine;

// Token: 0x02000055 RID: 85
public class FenceBuilder : GenericPath
{
	// Token: 0x0600012F RID: 303 RVA: 0x00002229 File Offset: 0x00000429
	public override void UpdatePath()
	{
	}

	// Token: 0x040001BC RID: 444
	public GameObject postPrefab;

	// Token: 0x040001BD RID: 445
	public GameObject fencePrefab;

	// Token: 0x040001BE RID: 446
	public float fenceLength = 1f;

	// Token: 0x040001BF RID: 447
	public GameObject[] posts;

	// Token: 0x040001C0 RID: 448
	public GameObject[] fences;

	// Token: 0x040001C1 RID: 449
	public bool normalizeScale;

	// Token: 0x040001C2 RID: 450
	public bool lockFenceToPost;
}
