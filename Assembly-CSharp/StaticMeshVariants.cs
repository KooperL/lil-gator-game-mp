using System;
using UnityEngine;

public class StaticMeshVariants : MonoBehaviour
{
	// Token: 0x06000CAE RID: 3246 RVA: 0x0003D6CF File Offset: 0x0003B8CF
	[ContextMenu("Randomize Variants")]
	public void RandomizeVariants()
	{
		this.meshFilter.mesh = this.meshVariants.RandomValue<Mesh>();
	}

	public MeshFilter meshFilter;

	public Mesh[] meshVariants;
}
