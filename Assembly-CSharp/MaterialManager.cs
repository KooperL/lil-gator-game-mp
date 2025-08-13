using System;
using UnityEngine;

// Token: 0x0200030F RID: 783
public class MaterialManager : MonoBehaviour
{
	// Token: 0x06000F63 RID: 3939 RVA: 0x0000D646 File Offset: 0x0000B846
	private void OnEnable()
	{
		MaterialManager.m = this;
		this.lastSample = this.fallbackMaterial;
	}

	// Token: 0x06000F64 RID: 3940 RVA: 0x000506F4 File Offset: 0x0004E8F4
	public SurfaceMaterial SampleSurfaceMaterial(Vector3 point, Vector3 direction)
	{
		if (Vector3.SqrMagnitude(MainCamera.t.position - point) > 1600f)
		{
			return null;
		}
		RaycastHit raycastHit;
		SurfaceMaterial surfaceMaterial;
		if (Physics.SphereCast(point - this.probeBackward * direction, this.probeRadius, direction, ref raycastHit, this.probeForward + this.probeBackward, this.surfaceLayerMask))
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

	// Token: 0x06000F65 RID: 3941 RVA: 0x00050778 File Offset: 0x0004E978
	public bool SampleSurfaceMaterial(Vector3 point, Vector3 direction, out SurfaceMaterial surfaceMaterial, out RaycastHit hit)
	{
		if (Vector3.SqrMagnitude(MainCamera.t.position - point) > 1600f)
		{
			surfaceMaterial = null;
			hit = default(RaycastHit);
			return false;
		}
		surfaceMaterial = null;
		if (Physics.SphereCast(point - this.probeBackward * direction, this.probeRadius, direction, ref hit, this.probeForward + this.probeBackward, this.surfaceLayerMask))
		{
			surfaceMaterial = this.GetSurfaceMaterial(hit);
			return true;
		}
		surfaceMaterial = this.fallbackMaterial;
		return false;
	}

	// Token: 0x06000F66 RID: 3942 RVA: 0x00050808 File Offset: 0x0004EA08
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

	// Token: 0x040013E6 RID: 5094
	public static MaterialManager m;

	// Token: 0x040013E7 RID: 5095
	public SurfaceMaterial fallbackMaterial;

	// Token: 0x040013E8 RID: 5096
	public SurfaceMaterial airFallbackMaterial;

	// Token: 0x040013E9 RID: 5097
	public SurfaceMaterial waterMaterial;

	// Token: 0x040013EA RID: 5098
	private SurfaceMaterial lastSample;

	// Token: 0x040013EB RID: 5099
	private float lastSampleTime;

	// Token: 0x040013EC RID: 5100
	public LayerMask surfaceLayerMask;

	// Token: 0x040013ED RID: 5101
	public float probeForward;

	// Token: 0x040013EE RID: 5102
	public float probeBackward;

	// Token: 0x040013EF RID: 5103
	public float probeRadius;
}
