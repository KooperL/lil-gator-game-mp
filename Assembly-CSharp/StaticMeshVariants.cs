using System;
using UnityEngine;

// Token: 0x0200030B RID: 779
public class StaticMeshVariants : MonoBehaviour
{
	// Token: 0x06000F5A RID: 3930 RVA: 0x0000D5D9 File Offset: 0x0000B7D9
	[ContextMenu("Randomize Variants")]
	public void RandomizeVariants()
	{
		this.meshFilter.mesh = this.meshVariants.RandomValue<Mesh>();
	}

	// Token: 0x040013D9 RID: 5081
	public MeshFilter meshFilter;

	// Token: 0x040013DA RID: 5082
	public Mesh[] meshVariants;
}
