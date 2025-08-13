using System;
using UnityEngine;

// Token: 0x02000042 RID: 66
public class FenceBuilder : GenericPath
{
	// Token: 0x0600010A RID: 266 RVA: 0x00006B93 File Offset: 0x00004D93
	public override void UpdatePath()
	{
	}

	// Token: 0x04000172 RID: 370
	public GameObject postPrefab;

	// Token: 0x04000173 RID: 371
	public GameObject fencePrefab;

	// Token: 0x04000174 RID: 372
	public float fenceLength = 1f;

	// Token: 0x04000175 RID: 373
	public GameObject[] posts;

	// Token: 0x04000176 RID: 374
	public GameObject[] fences;

	// Token: 0x04000177 RID: 375
	public bool normalizeScale;

	// Token: 0x04000178 RID: 376
	public bool lockFenceToPost;
}
