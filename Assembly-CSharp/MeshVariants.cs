using System;
using UnityEngine;

public class MeshVariants : MonoBehaviour
{
	// Token: 0x06000132 RID: 306 RVA: 0x000030B8 File Offset: 0x000012B8
	private void Start()
	{
		if (this.variants != null)
		{
			this.meshFilter.mesh = this.variants.RandomValue<Mesh>();
		}
	}

	public Mesh[] variants;

	public MeshFilter meshFilter;
}
