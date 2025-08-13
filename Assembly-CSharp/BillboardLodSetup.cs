using System;
using UnityEngine;

// Token: 0x02000271 RID: 625
public class BillboardLodSetup : MonoBehaviour
{
	// Token: 0x040011AB RID: 4523
	public RenderTexture renderTexture;

	// Token: 0x040011AC RID: 4524
	public GameObject[] sourcePrefabs;

	// Token: 0x040011AD RID: 4525
	public GameObject[] sourcePrefabs_h;

	// Token: 0x040011AE RID: 4526
	public bool useHighPrefabs;

	// Token: 0x040011AF RID: 4527
	public GameObject[] placedPrefabs;

	// Token: 0x040011B0 RID: 4528
	public Mesh[] billboardMeshes;

	// Token: 0x040011B1 RID: 4529
	public GameObject[] lodPrefabs;

	// Token: 0x040011B2 RID: 4530
	public GameObject[] treePrefabs;

	// Token: 0x040011B3 RID: 4531
	public Material billboardMaterial;

	// Token: 0x040011B4 RID: 4532
	public GameObject billboardPrefab;

	// Token: 0x040011B5 RID: 4533
	public int rows = 2;

	// Token: 0x040011B6 RID: 4534
	public float size = 50f;

	// Token: 0x040011B7 RID: 4535
	public float buffer = 0.5f;

	// Token: 0x040011B8 RID: 4536
	public int[] rowIndices;

	// Token: 0x040011B9 RID: 4537
	public float[] rowWidths;
}
