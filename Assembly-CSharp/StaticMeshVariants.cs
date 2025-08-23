using System;
using UnityEngine;

public class StaticMeshVariants : MonoBehaviour
{
	// Token: 0x06000FB7 RID: 4023 RVA: 0x0000D98B File Offset: 0x0000BB8B
	[ContextMenu("Randomize Variants")]
	public void RandomizeVariants()
	{
		this.meshFilter.mesh = this.meshVariants.RandomValue<Mesh>();
	}

	public MeshFilter meshFilter;

	public Mesh[] meshVariants;
}
