using System;
using UnityEngine;

// Token: 0x02000165 RID: 357
public class FadeParticlesProximity : MonoBehaviour, IManagedUpdate
{
	// Token: 0x060006AD RID: 1709 RVA: 0x00006D0F File Offset: 0x00004F0F
	public void OnValidate()
	{
		if (this.particleSystem == null)
		{
			this.particleSystem = base.GetComponent<ParticleSystem>();
		}
		this.maxSqrDistance = this.maxDistance * this.maxDistance;
	}

	// Token: 0x060006AE RID: 1710 RVA: 0x00006D3E File Offset: 0x00004F3E
	private void Start()
	{
		this.SetFade(0f);
	}

	// Token: 0x060006AF RID: 1711 RVA: 0x0000265D File Offset: 0x0000085D
	private void OnEnable()
	{
		FastUpdateManager.updateEvery4.Add(this);
	}

	// Token: 0x060006B0 RID: 1712 RVA: 0x0000266A File Offset: 0x0000086A
	private void OnDisable()
	{
		FastUpdateManager.updateEvery4.Remove(this);
	}

	// Token: 0x060006B1 RID: 1713 RVA: 0x000318C4 File Offset: 0x0002FAC4
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

	// Token: 0x060006B2 RID: 1714 RVA: 0x00031938 File Offset: 0x0002FB38
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

	// Token: 0x060006B3 RID: 1715 RVA: 0x000319B8 File Offset: 0x0002FBB8
	private void SetFade(float fadeAmount)
	{
		this.particleSystem.emission.rateOverTimeMultiplier = fadeAmount * this.maxRate;
	}

	// Token: 0x040008F5 RID: 2293
	public float maxDistance = 20f;

	// Token: 0x040008F6 RID: 2294
	[ReadOnly]
	public float maxSqrDistance;

	// Token: 0x040008F7 RID: 2295
	public float minDistance = 8f;

	// Token: 0x040008F8 RID: 2296
	private Transform proximityTransform;

	// Token: 0x040008F9 RID: 2297
	public bool keepEnabled;

	// Token: 0x040008FA RID: 2298
	public ParticleSystem particleSystem;

	// Token: 0x040008FB RID: 2299
	public float maxRate = 80f;
}
