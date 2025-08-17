using System;
using UnityEngine;

public class FadeRendererProximity : FadeRenderer
{
	// Token: 0x060006F8 RID: 1784 RVA: 0x00007118 File Offset: 0x00005318
	public override void OnValidate()
	{
		this.maxSqrDistance = this.maxDistance * this.maxDistance;
		base.OnValidate();
		if (this.proximityTransform == null)
		{
			PlayerMovement playerMovement = global::UnityEngine.Object.FindObjectOfType<PlayerMovement>();
			this.proximityTransform = ((playerMovement != null) ? playerMovement.transform : null);
		}
	}

	// Token: 0x060006F9 RID: 1785 RVA: 0x00033158 File Offset: 0x00031358
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

	// Token: 0x060006FA RID: 1786 RVA: 0x000331D0 File Offset: 0x000313D0
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

	public float maxDistance = 20f;

	[ReadOnly]
	public float maxSqrDistance;

	public float minDistance = 8f;

	public float maxFade = 0.8f;

	public bool usePlayerTransform = true;

	public Transform proximityTransform;

	public bool keepEnabled;
}
