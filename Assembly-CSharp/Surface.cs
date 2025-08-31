using System;
using UnityEngine;

public class Surface : MonoBehaviour, ISurface
{
	// Token: 0x06000CBE RID: 3262 RVA: 0x0003DAF6 File Offset: 0x0003BCF6
	public SurfaceMaterial GetSurfaceMaterial(Vector3 position)
	{
		return this.GetSurfaceMaterial(position, Vector3.up);
	}

	// Token: 0x06000CBF RID: 3263 RVA: 0x0003DB04 File Offset: 0x0003BD04
	public SurfaceMaterial GetSurfaceMaterial(Vector3 position, Vector3 normal)
	{
		return this.surfaceMaterial;
	}

	public SurfaceMaterial surfaceMaterial;
}
