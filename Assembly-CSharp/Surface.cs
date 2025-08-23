using System;
using UnityEngine;

public class Surface : MonoBehaviour, ISurface
{
	// Token: 0x06000FC7 RID: 4039 RVA: 0x0000DA0C File Offset: 0x0000BC0C
	public SurfaceMaterial GetSurfaceMaterial(Vector3 position)
	{
		return this.GetSurfaceMaterial(position, Vector3.up);
	}

	// Token: 0x06000FC8 RID: 4040 RVA: 0x0000DA1A File Offset: 0x0000BC1A
	public SurfaceMaterial GetSurfaceMaterial(Vector3 position, Vector3 normal)
	{
		return this.surfaceMaterial;
	}

	public SurfaceMaterial surfaceMaterial;
}
