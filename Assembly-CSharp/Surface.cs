using System;
using UnityEngine;

// Token: 0x02000311 RID: 785
public class Surface : MonoBehaviour, ISurface
{
	// Token: 0x06000F6A RID: 3946 RVA: 0x0000D65A File Offset: 0x0000B85A
	public SurfaceMaterial GetSurfaceMaterial(Vector3 position)
	{
		return this.GetSurfaceMaterial(position, Vector3.up);
	}

	// Token: 0x06000F6B RID: 3947 RVA: 0x0000D668 File Offset: 0x0000B868
	public SurfaceMaterial GetSurfaceMaterial(Vector3 position, Vector3 normal)
	{
		return this.surfaceMaterial;
	}

	// Token: 0x040013F0 RID: 5104
	public SurfaceMaterial surfaceMaterial;
}
