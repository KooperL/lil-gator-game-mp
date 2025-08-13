using System;
using UnityEngine;

// Token: 0x0200024B RID: 587
public interface ISurface
{
	// Token: 0x06000CBC RID: 3260
	SurfaceMaterial GetSurfaceMaterial(Vector3 position);

	// Token: 0x06000CBD RID: 3261
	SurfaceMaterial GetSurfaceMaterial(Vector3 position, Vector3 normal);
}
