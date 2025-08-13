using System;
using UnityEngine;

// Token: 0x02000111 RID: 273
public class FadeRendererProximity : FadeRenderer
{
	// Token: 0x0600059A RID: 1434 RVA: 0x0001D61F File Offset: 0x0001B81F
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

	// Token: 0x0600059B RID: 1435 RVA: 0x0001D660 File Offset: 0x0001B860
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

	// Token: 0x0600059C RID: 1436 RVA: 0x0001D6D8 File Offset: 0x0001B8D8
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

	// Token: 0x040007AC RID: 1964
	public float maxDistance = 20f;

	// Token: 0x040007AD RID: 1965
	[ReadOnly]
	public float maxSqrDistance;

	// Token: 0x040007AE RID: 1966
	public float minDistance = 8f;

	// Token: 0x040007AF RID: 1967
	public float maxFade = 0.8f;

	// Token: 0x040007B0 RID: 1968
	public bool usePlayerTransform = true;

	// Token: 0x040007B1 RID: 1969
	public Transform proximityTransform;

	// Token: 0x040007B2 RID: 1970
	public bool keepEnabled;
}
