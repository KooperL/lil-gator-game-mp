using System;
using UnityEngine;

public interface ISurface
{
	SurfaceMaterial GetSurfaceMaterial(Vector3 position);

	SurfaceMaterial GetSurfaceMaterial(Vector3 position, Vector3 normal);
}
