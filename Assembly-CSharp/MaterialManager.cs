using System;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
	// Token: 0x06000FBF RID: 4031 RVA: 0x0000D9EE File Offset: 0x0000BBEE
	private void OnEnable()
	{
		MaterialManager.m = this;
		this.lastSample = this.fallbackMaterial;
	}

	// Token: 0x06000FC0 RID: 4032 RVA: 0x00052618 File Offset: 0x00050818
	public SurfaceMaterial SampleSurfaceMaterial(Vector3 point, Vector3 direction)
	{
		if (Vector3.SqrMagnitude(MainCamera.t.position - point) > 1600f)
		{
			return null;
		}
		RaycastHit raycastHit;
		SurfaceMaterial surfaceMaterial;
		if (Physics.SphereCast(point - this.probeBackward * direction, this.probeRadius, direction, out raycastHit, this.probeForward + this.probeBackward, this.surfaceLayerMask))
		{
			surfaceMaterial = this.GetSurfaceMaterial(raycastHit);
			if (surfaceMaterial == null)
			{
				surfaceMaterial = this.fallbackMaterial;
			}
		}
		else
		{
			surfaceMaterial = null;
		}
		return surfaceMaterial;
	}

	// Token: 0x06000FC1 RID: 4033 RVA: 0x0005269C File Offset: 0x0005089C
	public bool SampleSurfaceMaterial(Vector3 point, Vector3 direction, out SurfaceMaterial surfaceMaterial, out RaycastHit hit)
	{
		if (Vector3.SqrMagnitude(MainCamera.t.position - point) > 1600f)
		{
			surfaceMaterial = null;
			hit = default(RaycastHit);
			return false;
		}
		surfaceMaterial = null;
		if (Physics.SphereCast(point - this.probeBackward * direction, this.probeRadius, direction, out hit, this.probeForward + this.probeBackward, this.surfaceLayerMask))
		{
			surfaceMaterial = this.GetSurfaceMaterial(hit);
			return true;
		}
		surfaceMaterial = this.fallbackMaterial;
		return false;
	}

	// Token: 0x06000FC2 RID: 4034 RVA: 0x0005272C File Offset: 0x0005092C
	public SurfaceMaterial GetSurfaceMaterial(RaycastHit hit)
	{
		SurfaceMaterial surfaceMaterial = null;
		ISurface component = hit.collider.GetComponent<ISurface>();
		if (component != null)
		{
			surfaceMaterial = component.GetSurfaceMaterial(hit.point);
		}
		if (surfaceMaterial == null)
		{
			if (Time.time - this.lastSampleTime < 0.5f)
			{
				surfaceMaterial = this.lastSample;
			}
			else
			{
				surfaceMaterial = this.fallbackMaterial;
			}
		}
		else
		{
			this.lastSample = surfaceMaterial;
			this.lastSampleTime = Time.time;
		}
		return surfaceMaterial;
	}

	public static MaterialManager m;

	public SurfaceMaterial fallbackMaterial;

	public SurfaceMaterial airFallbackMaterial;

	public SurfaceMaterial waterMaterial;

	private SurfaceMaterial lastSample;

	private float lastSampleTime;

	public LayerMask surfaceLayerMask;

	public float probeForward;

	public float probeBackward;

	public float probeRadius;
}
