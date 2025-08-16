using System;
using UnityEngine;

public class FadeParticlesProximity : MonoBehaviour, IManagedUpdate
{
	// Token: 0x060006E7 RID: 1767 RVA: 0x00006FD5 File Offset: 0x000051D5
	public void OnValidate()
	{
		if (this.particleSystem == null)
		{
			this.particleSystem = base.GetComponent<ParticleSystem>();
		}
		this.maxSqrDistance = this.maxDistance * this.maxDistance;
	}

	// Token: 0x060006E8 RID: 1768 RVA: 0x00007004 File Offset: 0x00005204
	private void Start()
	{
		this.SetFade(0f);
	}

	// Token: 0x060006E9 RID: 1769 RVA: 0x000026C1 File Offset: 0x000008C1
	private void OnEnable()
	{
		FastUpdateManager.updateEvery4.Add(this);
	}

	// Token: 0x060006EA RID: 1770 RVA: 0x000026CE File Offset: 0x000008CE
	private void OnDisable()
	{
		FastUpdateManager.updateEvery4.Remove(this);
	}

	// Token: 0x060006EB RID: 1771 RVA: 0x00032E44 File Offset: 0x00031044
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

	// Token: 0x060006EC RID: 1772 RVA: 0x00032EB8 File Offset: 0x000310B8
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

	// Token: 0x060006ED RID: 1773 RVA: 0x00032F38 File Offset: 0x00031138
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
