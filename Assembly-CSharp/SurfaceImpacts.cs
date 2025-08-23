using System;
using UnityEngine;

public class SurfaceImpacts : MonoBehaviour
{
	// Token: 0x06000FCD RID: 4045 RVA: 0x0000DA4C File Offset: 0x0000BC4C
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06000FCE RID: 4046 RVA: 0x00052A64 File Offset: 0x00050C64
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
