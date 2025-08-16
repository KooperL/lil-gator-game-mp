using System;
using UnityEngine;

public class StaticMeshVariants : MonoBehaviour
{
	// Token: 0x06000FB6 RID: 4022 RVA: 0x0000D96C File Offset: 0x0000BB6C
	[ContextMenu("Randomize Variants")]
	public void RandomizeVariants()
	{
		this.meshFilter.mesh = this.meshVariants.RandomValue<Mesh>();
	}

	public MeshFilter meshFilter;

	public Mesh[] meshVariants;
}
