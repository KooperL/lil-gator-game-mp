using System;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
	// Token: 0x06000CB7 RID: 3255 RVA: 0x0003D955 File Offset: 0x0003BB55
	private void OnEnable()
	{
		MaterialManager.m = this;
		this.lastSample = this.fallbackMaterial;
	}

	// Token: 0x06000CB8 RID: 3256 RVA: 0x0003D96C File Offset: 0x0003BB6C
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

	// Token: 0x06000CB9 RID: 3257 RVA: 0x0003D9F0 File Offset: 0x0003BBF0
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

	// Token: 0x06000CBA RID: 3258 RVA: 0x0003DA80 File Offset: 0x0003BC80
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
