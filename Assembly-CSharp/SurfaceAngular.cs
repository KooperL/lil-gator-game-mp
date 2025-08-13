using System;
using UnityEngine;

// Token: 0x02000312 RID: 786
public class SurfaceAngular : MonoBehaviour, ISurface
{
	// Token: 0x06000F6D RID: 3949 RVA: 0x0000D670 File Offset: 0x0000B870
	public SurfaceMaterial GetSurfaceMaterial(Vector3 position)
	{
		return this.GetSurfaceMaterial(position, Vector3.forward);
	}

	// Token: 0x06000F6E RID: 3950 RVA: 0x0000D67E File Offset: 0x0000B87E
	public SurfaceMaterial GetSurfaceMaterial(Vector3 position, Vector3 normal)
	{
		if (normal.y > 0.71f)
		{
			return this.groundMaterial;
		}
		return this.wallMaterial;
	}

	// Token: 0x040013F1 RID: 5105
	public SurfaceMaterial groundMaterial;

	// Token: 0x040013F2 RID: 5106
	public SurfaceMaterial wallMaterial;
}
