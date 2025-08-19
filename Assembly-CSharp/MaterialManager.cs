using System;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
	// Token: 0x06000FBF RID: 4031 RVA: 0x0000D9F8 File Offset: 0x0000BBF8
	private void OnEnable()
	{
		MaterialManager.m = this;
		this.lastSample = this.fallbackMaterial;
	}

	// Token: 0x06000FC0 RID: 4032 RVA: 0x000525F4 File Offset: 0x000507F4
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

	// Token: 0x06000FC1 RID: 4033 RVA: 0x00052678 File Offset: 0x00050878
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

	// Token: 0x06000FC2 RID: 4034 RVA: 0x00052708 File Offset: 0x00050908
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
