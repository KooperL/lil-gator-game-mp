using System;
using UnityEngine;

// Token: 0x0200024E RID: 590
public class SurfaceImpacts : MonoBehaviour
{
	// Token: 0x06000CC4 RID: 3268 RVA: 0x0003DB46 File Offset: 0x0003BD46
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06000CC5 RID: 3269 RVA: 0x0003DB54 File Offset: 0x0003BD54
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

	// Token: 0x040010DA RID: 4314
	public SurfaceMaterial surfaceMaterial;

	// Token: 0x040010DB RID: 4315
	[Range(0.25f, 2f)]
	public float pitch = 1f;

	// Token: 0x040010DC RID: 4316
	[Range(0f, 1f)]
	public float volume = 0.5f;

	// Token: 0x040010DD RID: 4317
	private const float minDelta = 0.25f;

	// Token: 0x040010DE RID: 4318
	private const float minDeltaSqr = 0.0625f;

	// Token: 0x040010DF RID: 4319
	private const float maxDelta = 10f;

	// Token: 0x040010E0 RID: 4320
	private const float maxPitch = 0.9f;

	// Token: 0x040010E1 RID: 4321
	private const float minPitch = 1.1f;

	// Token: 0x040010E2 RID: 4322
	private Rigidbody rigidbody;
}
