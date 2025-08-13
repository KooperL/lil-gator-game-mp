using System;
using UnityEngine;

// Token: 0x0200033B RID: 827
public class BillboardLodSetup : MonoBehaviour
{
	// Token: 0x040014D7 RID: 5335
	public RenderTexture renderTexture;

	// Token: 0x040014D8 RID: 5336
	public GameObject[] sourcePrefabs;

	// Token: 0x040014D9 RID: 5337
	public GameObject[] sourcePrefabs_h;

	// Token: 0x040014DA RID: 5338
	public bool useHighPrefabs;

	// Token: 0x040014DB RID: 5339
	public GameObject[] placedPrefabs;

	// Token: 0x040014DC RID: 5340
	public Mesh[] billboardMeshes;

	// Token: 0x040014DD RID: 5341
	public GameObject[] lodPrefabs;

	// Token: 0x040014DE RID: 5342
	public GameObject[] treePrefabs;

	// Token: 0x040014DF RID: 5343
	public Material billboardMaterial;

	// Token: 0x040014E0 RID: 5344
	public GameObject billboardPrefab;

	// Token: 0x040014E1 RID: 5345
	public int rows = 2;

	// Token: 0x040014E2 RID: 5346
	public float size = 50f;

	// Token: 0x040014E3 RID: 5347
	public float buffer = 0.5f;

	// Token: 0x040014E4 RID: 5348
	public int[] rowIndices;

	// Token: 0x040014E5 RID: 5349
	public float[] rowWidths;
}
