using System;
using UnityEngine;

// Token: 0x0200024D RID: 589
public class SurfaceAngular : MonoBehaviour, ISurface
{
	// Token: 0x06000CC1 RID: 3265 RVA: 0x0003DB14 File Offset: 0x0003BD14
	public SurfaceMaterial GetSurfaceMaterial(Vector3 position)
	{
		return this.GetSurfaceMaterial(position, Vector3.forward);
	}

	// Token: 0x06000CC2 RID: 3266 RVA: 0x0003DB22 File Offset: 0x0003BD22
	public SurfaceMaterial GetSurfaceMaterial(Vector3 position, Vector3 normal)
	{
		if (normal.y > 0.71f)
		{
			return this.groundMaterial;
		}
		return this.wallMaterial;
	}

	// Token: 0x040010D8 RID: 4312
	public SurfaceMaterial groundMaterial;

	// Token: 0x040010D9 RID: 4313
	public SurfaceMaterial wallMaterial;
}
