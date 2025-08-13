using System;
using UnityEngine;

// Token: 0x02000115 RID: 277
public class FillPath : GenericPath
{
	// Token: 0x040007E1 RID: 2017
	public GameObject prefab;

	// Token: 0x040007E2 RID: 2018
	public float prefabWidth = 10f;

	// Token: 0x040007E3 RID: 2019
	public float prefabSpacing;

	// Token: 0x040007E4 RID: 2020
	public bool lockToFlat = true;

	// Token: 0x040007E5 RID: 2021
	[ReadOnly]
	public GameObject[] filledObjects;
}
