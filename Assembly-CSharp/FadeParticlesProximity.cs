using System;
using UnityEngine;

public class FadeParticlesProximity : MonoBehaviour, IManagedUpdate
{
	// Token: 0x06000589 RID: 1417 RVA: 0x0001D327 File Offset: 0x0001B527
	public void OnValidate()
	{
		if (this.particleSystem == null)
		{
			this.particleSystem = base.GetComponent<ParticleSystem>();
		}
		this.maxSqrDistance = this.maxDistance * this.maxDistance;
	}

	// Token: 0x0600058A RID: 1418 RVA: 0x0001D356 File Offset: 0x0001B556
	private void Start()
	{
		this.SetFade(0f);
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x0001D363 File Offset: 0x0001B563
	private void OnEnable()
	{
		FastUpdateManager.updateEvery4.Add(this);
	}

	// Token: 0x0600058C RID: 1420 RVA: 0x0001D370 File Offset: 0x0001B570
	private void OnDisable()
	{
		FastUpdateManager.updateEvery4.Remove(this);
	}

	// Token: 0x0600058D RID: 1421 RVA: 0x0001D380 File Offset: 0x0001B580
	public void ManagedUpdate()
	{
		if (this.proximityTransform == null)
		{
			this.proximityTransform = Player.rawTransform;
		}
		float num = Vector3.Distance(base.transform.position, this.proximityTransform.position);
		float num2 = Mathf.InverseLerp(this.maxDistance, this.minDistance, num);
		this.SetFade(num2);
		if (num > this.maxDistance && !this.keepEnabled)
		{
			base.enabled = false;
		}
	}

	// Token: 0x0600058E RID: 1422 RVA: 0x0001D3F4 File Offset: 0x0001B5F4
	private void OnTriggerStay(Collider other)
	{
		if (base.enabled)
		{
			return;
		}
		if (this.proximityTransform == null || !this.proximityTransform.gameObject.activeInHierarchy)
		{
			this.proximityTransform = Player.rawTransform;
		}
		if (this.proximityTransform != null && Vector3.SqrMagnitude(base.transform.position - this.proximityTransform.position) < this.maxSqrDistance)
		{
			base.enabled = true;
		}
	}

	// Token: 0x0600058F RID: 1423 RVA: 0x0001D474 File Offset: 0x0001B674
	private void SetFade(float fadeAmount)
	{
		this.particleSystem.emission.rateOverTimeMultiplier = fadeAmount * this.maxRate;
	}

	public float maxDistance = 20f;

	[ReadOnly]
	public float maxSqrDistance;

	public float minDistance = 8f;

	private Transform proximityTransform;

	public bool keepEnabled;

	public ParticleSystem particleSystem;

	public float maxRate = 80f;
}
