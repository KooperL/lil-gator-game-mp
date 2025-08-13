using System;
using UnityEngine;

// Token: 0x02000040 RID: 64
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

	// Token: 0x0400016E RID: 366
	public Mesh[] variants;

	// Token: 0x0400016F RID: 367
	public MeshFilter meshFilter;
}
