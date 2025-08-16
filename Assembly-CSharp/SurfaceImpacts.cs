using System;
using UnityEngine;

public class SurfaceImpacts : MonoBehaviour
{
	// Token: 0x06000FCC RID: 4044 RVA: 0x0000DA2D File Offset: 0x0000BC2D
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06000FCD RID: 4045 RVA: 0x00052608 File Offset: 0x00050808
	private void OnCollisionEnter(Collision collision)
	{
		float sqrMagnitude = collision.relativeVelocity.sqrMagnitude;
		if (sqrMagnitude < 0.0625f)
		{
			return;
		}
		float num = Mathf.InverseLerp(0.25f, 10f, Mathf.Sqrt(sqrMagnitude));
		num *= num;
		this.surfaceMaterial.PlayImpact(this.rigidbody.position, num * this.volume, this.pitch * Mathf.Lerp(1.1f, 0.9f, num));
	}

	public SurfaceMaterial surfaceMaterial;

	[Range(0.25f, 2f)]
	public float pitch = 1f;

	[Range(0f, 1f)]
	public float volume = 0.5f;

	private const float minDelta = 0.25f;

	private const float minDeltaSqr = 0.0625f;

	private const float maxDelta = 10f;

	private const float maxPitch = 0.9f;

	private const float minPitch = 1.1f;

	private Rigidbody rigidbody;
}
