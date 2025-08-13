using System;
using UnityEngine;

// Token: 0x0200016D RID: 365
public class FillPath : GenericPath
{
	// Token: 0x0400093D RID: 2365
	public GameObject prefab;

	// Token: 0x0400093E RID: 2366
	public float prefabWidth = 10f;

	// Token: 0x0400093F RID: 2367
	public float prefabSpacing;

	// Token: 0x04000940 RID: 2368
	public bool lockToFlat = true;

	// Token: 0x04000941 RID: 2369
	[ReadOnly]
	public GameObject[] filledObjects;
}
