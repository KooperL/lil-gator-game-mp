using System;
using UnityEngine;

// Token: 0x02000310 RID: 784
public interface ISurface
{
	// Token: 0x06000F68 RID: 3944
	SurfaceMaterial GetSurfaceMaterial(Vector3 position);

	// Token: 0x06000F69 RID: 3945
	SurfaceMaterial GetSurfaceMaterial(Vector3 position, Vector3 normal);
}
