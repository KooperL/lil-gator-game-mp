using System;
using UnityEngine;

// Token: 0x02000313 RID: 787
public class SurfaceImpacts : MonoBehaviour
{
	// Token: 0x06000F70 RID: 3952 RVA: 0x0000D69A File Offset: 0x0000B89A
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06000F71 RID: 3953 RVA: 0x00050878 File Offset: 0x0004EA78
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

	// Token: 0x040013F3 RID: 5107
	public SurfaceMaterial surfaceMaterial;

	// Token: 0x040013F4 RID: 5108
	[Range(0.25f, 2f)]
	public float pitch = 1f;

	// Token: 0x040013F5 RID: 5109
	[Range(0f, 1f)]
	public float volume = 0.5f;

	// Token: 0x040013F6 RID: 5110
	private const float minDelta = 0.25f;

	// Token: 0x040013F7 RID: 5111
	private const float minDeltaSqr = 0.0625f;

	// Token: 0x040013F8 RID: 5112
	private const float maxDelta = 10f;

	// Token: 0x040013F9 RID: 5113
	private const float maxPitch = 0.9f;

	// Token: 0x040013FA RID: 5114
	private const float minPitch = 1.1f;

	// Token: 0x040013FB RID: 5115
	private Rigidbody rigidbody;
}
