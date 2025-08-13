using System;
using UnityEngine;

// Token: 0x02000053 RID: 83
public class MeshVariants : MonoBehaviour
{
	// Token: 0x0600012A RID: 298 RVA: 0x00003015 File Offset: 0x00001215
	private void Start()
	{
		if (this.variants != null)
		{
			this.meshFilter.mesh = this.variants.RandomValue<Mesh>();
		}
	}

	// Token: 0x040001B8 RID: 440
	public Mesh[] variants;

	// Token: 0x040001B9 RID: 441
	public MeshFilter meshFilter;
}
