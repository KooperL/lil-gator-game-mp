using System;
using UnityEngine;

public class Surface : MonoBehaviour, ISurface
{
	// Token: 0x06000FC6 RID: 4038 RVA: 0x0000D9ED File Offset: 0x0000BBED
	public SurfaceMaterial GetSurfaceMaterial(Vector3 position)
	{
		return this.GetSurfaceMaterial(position, Vector3.up);
	}

	// Token: 0x06000FC7 RID: 4039 RVA: 0x0000D9FB File Offset: 0x0000BBFB
	public SurfaceMaterial GetSurfaceMaterial(Vector3 position, Vector3 normal)
	{
		return this.surfaceMaterial;
	}

	public SurfaceMaterial surfaceMaterial;
}
