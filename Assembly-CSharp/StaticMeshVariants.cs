using System;
using UnityEngine;

// Token: 0x02000247 RID: 583
public class StaticMeshVariants : MonoBehaviour
{
	// Token: 0x06000CAE RID: 3246 RVA: 0x0003D6CF File Offset: 0x0003B8CF
	[ContextMenu("Randomize Variants")]
	public void RandomizeVariants()
	{
		this.meshFilter.mesh = this.meshVariants.RandomValue<Mesh>();
	}

	// Token: 0x040010C3 RID: 4291
	public MeshFilter meshFilter;

	// Token: 0x040010C4 RID: 4292
	public Mesh[] meshVariants;
}
