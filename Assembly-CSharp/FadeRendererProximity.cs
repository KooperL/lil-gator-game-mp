using System;
using UnityEngine;

// Token: 0x02000168 RID: 360
public class FadeRendererProximity : FadeRenderer
{
	// Token: 0x060006BE RID: 1726 RVA: 0x00006E52 File Offset: 0x00005052
	public override void OnValidate()
	{
		this.maxSqrDistance = this.maxDistance * this.maxDistance;
		base.OnValidate();
		if (this.proximityTransform == null)
		{
			PlayerMovement playerMovement = Object.FindObjectOfType<PlayerMovement>();
			this.proximityTransform = ((playerMovement != null) ? playerMovement.transform : null);
		}
	}

	// Token: 0x060006BF RID: 1727 RVA: 0x00031A5C File Offset: 0x0002FC5C
	private void LateUpdate()
	{
		if (this.usePlayerTransform)
		{
			this.proximityTransform = Player.rawTransform;
		}
		float num = Vector3.Distance(base.transform.position, this.proximityTransform.position);
		float num2 = Mathf.InverseLerp(this.maxDistance, this.minDistance, num);
		this.SetFade(num2 * this.maxFade);
		if (num > this.maxDistance && !this.keepEnabled)
		{
			base.enabled = false;
		}
	}

	// Token: 0x060006C0 RID: 1728 RVA: 0x00031AD4 File Offset: 0x0002FCD4
	private void OnTriggerStay(Collider other)
	{
		if (base.enabled)
		{
			return;
		}
		if (this.usePlayerTransform)
		{
			this.proximityTransform = Player.rawTransform;
		}
		if (this.proximityTransform != null && Vector3.SqrMagnitude(base.transform.position - this.proximityTransform.position) < this.maxSqrDistance)
		{
			base.enabled = true;
		}
	}

	// Token: 0x04000906 RID: 2310
	public float maxDistance = 20f;

	// Token: 0x04000907 RID: 2311
	[ReadOnly]
	public float maxSqrDistance;

	// Token: 0x04000908 RID: 2312
	public float minDistance = 8f;

	// Token: 0x04000909 RID: 2313
	public float maxFade = 0.8f;

	// Token: 0x0400090A RID: 2314
	public bool usePlayerTransform = true;

	// Token: 0x0400090B RID: 2315
	public Transform proximityTransform;

	// Token: 0x0400090C RID: 2316
	public bool keepEnabled;
}
