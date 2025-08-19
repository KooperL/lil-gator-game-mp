using System;
using UnityEngine;
using UnityEngine.Events;

public class ImpactSound : MonoBehaviour
{
	// Token: 0x06000658 RID: 1624 RVA: 0x00002229 File Offset: 0x00000429
	private void Start()
	{
	}

	// Token: 0x06000659 RID: 1625 RVA: 0x000311F4 File Offset: 0x0002F3F4
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

	// Token: 0x0600065A RID: 1626 RVA: 0x0003125C File Offset: 0x0002F45C
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

	public float volume = 1f;

	public float maxVelocityDelta = 5f;

	public Vector3 triggerPoint;

	public UnityEvent onImpact;
}
