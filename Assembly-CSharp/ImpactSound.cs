using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000147 RID: 327
public class ImpactSound : MonoBehaviour
{
	// Token: 0x0600061E RID: 1566 RVA: 0x00002229 File Offset: 0x00000429
	private void Start()
	{
	}

	// Token: 0x0600061F RID: 1567 RVA: 0x0002FB1C File Offset: 0x0002DD1C
	private void OnTriggerEnter(Collider other)
	{
		if (!base.enabled)
		{
			return;
		}
		this.onImpact.Invoke();
		ISurface component = other.GetComponent<ISurface>();
		if (component != null)
		{
			SurfaceMaterial surfaceMaterial = component.GetSurfaceMaterial(base.transform.TransformPoint(this.triggerPoint));
			if (surfaceMaterial != null)
			{
				surfaceMaterial.PlayImpact(base.transform.position, this.volume, 1f);
			}
		}
	}

	// Token: 0x06000620 RID: 1568 RVA: 0x0002FB84 File Offset: 0x0002DD84
	private void OnCollisionEnter(Collision collision)
	{
		if (!base.enabled)
		{
			return;
		}
		ISurface surface = collision.collider.GetComponent<ISurface>();
		if (surface == null)
		{
			surface = collision.contacts[0].otherCollider.GetComponent<ISurface>();
		}
		if (surface != null)
		{
			SurfaceMaterial surfaceMaterial = surface.GetSurfaceMaterial(collision.contacts[0].point, collision.contacts[0].normal);
			if (surfaceMaterial != null)
			{
				float num = Mathf.Min(1f, collision.relativeVelocity.magnitude / this.maxVelocityDelta);
				surfaceMaterial.PlayImpact(base.transform.position, num * this.volume, 1f);
			}
		}
	}

	// Token: 0x0400083E RID: 2110
	public float volume = 1f;

	// Token: 0x0400083F RID: 2111
	public float maxVelocityDelta = 5f;

	// Token: 0x04000840 RID: 2112
	public Vector3 triggerPoint;

	// Token: 0x04000841 RID: 2113
	public UnityEvent onImpact;
}
