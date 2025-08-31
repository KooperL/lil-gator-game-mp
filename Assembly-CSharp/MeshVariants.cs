using System;
using UnityEngine;

public class MeshVariants : MonoBehaviour
{
	// Token: 0x06000105 RID: 261 RVA: 0x00006B05 File Offset: 0x00004D05
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
