using System;
using UnityEngine;

public class SurfaceAngular : MonoBehaviour, ISurface
{
	// Token: 0x06000FC9 RID: 4041 RVA: 0x0000DA03 File Offset: 0x0000BC03
	public SurfaceMaterial GetSurfaceMaterial(Vector3 position)
	{
		return this.GetSurfaceMaterial(position, Vector3.forward);
	}

	// Token: 0x06000FCA RID: 4042 RVA: 0x0000DA11 File Offset: 0x0000BC11
	public SurfaceMaterial GetSurfaceMaterial(Vector3 position, Vector3 normal)
	{
		if (normal.y > 0.71f)
		{
			return this.groundMaterial;
		}
		return this.wallMaterial;
	}

	public SurfaceMaterial groundMaterial;

	public SurfaceMaterial wallMaterial;
}
