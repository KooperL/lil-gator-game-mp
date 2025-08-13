using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200005D RID: 93
public class BlacksmithHit : MonoBehaviour, IHit
{
	// Token: 0x06000145 RID: 325 RVA: 0x0001B588 File Offset: 0x00019788
	public void Hit(Vector3 velocity, bool isHeavy = false)
	{
		this.charge = Mathf.MoveTowards(this.charge, 1f, this.hitChargeAmount);
		this.chargeVelocity = 0f;
		for (int i = 0; i < this.chargeShouts.Length; i++)
		{
			if (!this.chargeShouts[i].triggered && this.charge > this.chargeShouts[i].chargeThreshold)
			{
				this.chargeShouts[i].triggered = true;
				DialogueManager.d.Bubble(this.document.FetchChunk(this.chargeShouts[i].id), this.actors, 0f, true, false, true);
			}
		}
		if (this.charge == 1f)
		{
			this.onFullyCharged.Invoke();
		}
		foreach (BlacksmithHit.HitParticles hitParticles in this.hitParticles)
		{
			if (this.charge > hitParticles.lowThreshold)
			{
				float num = Mathf.InverseLerp(hitParticles.lowThreshold, hitParticles.highThreshold, this.charge);
				hitParticles.particles.Emit((int)Mathf.Lerp((float)hitParticles.lowCount, (float)hitParticles.highCount, num));
			}
		}
	}

	// Token: 0x06000146 RID: 326 RVA: 0x0001B6C4 File Offset: 0x000198C4
	private void Update()
	{
		if (this.charge < 1f && this.charge > 0f)
		{
			this.chargeVelocity = Mathf.MoveTowards(this.chargeVelocity, this.chargeDrainSpeed, Time.deltaTime * this.chargeDrainAcc);
			this.charge = Mathf.MoveTowards(this.charge, 0f, Time.deltaTime * this.chargeVelocity);
			if (this.charge == 1f)
			{
				this.onFullyCharged.Invoke();
			}
		}
		foreach (BlacksmithHit.ChargeParticles chargeParticles in this.chargeParticles)
		{
			ParticleSystem.EmissionModule emission = chargeParticles.particles.emission;
			if (this.charge > chargeParticles.lowThreshold)
			{
				emission.enabled = true;
				float num = Mathf.InverseLerp(chargeParticles.lowThreshold, chargeParticles.highThreshold, this.charge);
				emission.rateOverTime = Mathf.Lerp(chargeParticles.lowEmission, chargeParticles.highEmission, num);
			}
			else
			{
				emission.enabled = false;
			}
		}
		foreach (BlacksmithHit.ChargeAudio chargeAudio in this.chargeAudio)
		{
			if (this.charge > chargeAudio.lowThreshold)
			{
				Mathf.InverseLerp(chargeAudio.lowThreshold, chargeAudio.highThreshold, this.charge);
				chargeAudio.audioSource.volume = Mathf.Lerp(chargeAudio.lowVolume, chargeAudio.highVolume, this.charge);
				chargeAudio.audioSource.pitch = Mathf.Lerp(chargeAudio.lowPitch, chargeAudio.highPitch, this.charge);
				if (!chargeAudio.audioSource.isPlaying)
				{
					chargeAudio.audioSource.Play();
				}
			}
			else if (chargeAudio.audioSource.isPlaying)
			{
				chargeAudio.audioSource.Stop();
			}
		}
	}

	// Token: 0x040001D7 RID: 471
	public float hitChargeAmount = 0.03f;

	// Token: 0x040001D8 RID: 472
	[ReadOnly]
	public float charge;

	// Token: 0x040001D9 RID: 473
	public float chargeDrainSpeed = 0.15f;

	// Token: 0x040001DA RID: 474
	public float chargeDrainAcc = 0.5f;

	// Token: 0x040001DB RID: 475
	[ReadOnly]
	public float chargeVelocity;

	// Token: 0x040001DC RID: 476
	public BlacksmithHit.HitParticles[] hitParticles;

	// Token: 0x040001DD RID: 477
	public BlacksmithHit.ChargeParticles[] chargeParticles;

	// Token: 0x040001DE RID: 478
	public BlacksmithHit.ChargeAudio[] chargeAudio;

	// Token: 0x040001DF RID: 479
	public UnityEvent onFullyCharged;

	// Token: 0x040001E0 RID: 480
	public MultilingualTextDocument document;

	// Token: 0x040001E1 RID: 481
	public BlacksmithHit.ChargeShout[] chargeShouts;

	// Token: 0x040001E2 RID: 482
	public DialogueActor[] actors;

	// Token: 0x0200005E RID: 94
	[Serializable]
	public struct HitParticles
	{
		// Token: 0x040001E3 RID: 483
		public ParticleSystem particles;

		// Token: 0x040001E4 RID: 484
		[Range(0f, 1f)]
		public float lowThreshold;

		// Token: 0x040001E5 RID: 485
		public int lowCount;

		// Token: 0x040001E6 RID: 486
		[Range(0f, 1f)]
		public float highThreshold;

		// Token: 0x040001E7 RID: 487
		public int highCount;
	}

	// Token: 0x0200005F RID: 95
	[Serializable]
	public struct ChargeParticles
	{
		// Token: 0x040001E8 RID: 488
		public ParticleSystem particles;

		// Token: 0x040001E9 RID: 489
		[Range(0f, 1f)]
		public float lowThreshold;

		// Token: 0x040001EA RID: 490
		public float lowEmission;

		// Token: 0x040001EB RID: 491
		[Range(0f, 1f)]
		public float highThreshold;

		// Token: 0x040001EC RID: 492
		public float highEmission;
	}

	// Token: 0x02000060 RID: 96
	[Serializable]
	public struct ChargeAudio
	{
		// Token: 0x040001ED RID: 493
		public AudioSource audioSource;

		// Token: 0x040001EE RID: 494
		[Range(0f, 1f)]
		public float lowThreshold;

		// Token: 0x040001EF RID: 495
		[Range(0f, 1f)]
		public float lowVolume;

		// Token: 0x040001F0 RID: 496
		[Range(0f, 2f)]
		public float lowPitch;

		// Token: 0x040001F1 RID: 497
		[Range(0f, 1f)]
		public float highThreshold;

		// Token: 0x040001F2 RID: 498
		[Range(0f, 1f)]
		public float highVolume;

		// Token: 0x040001F3 RID: 499
		[Range(0f, 2f)]
		public float highPitch;
	}

	// Token: 0x02000061 RID: 97
	[Serializable]
	public struct ChargeShout
	{
		// Token: 0x040001F4 RID: 500
		public float chargeThreshold;

		// Token: 0x040001F5 RID: 501
		public bool triggered;

		// Token: 0x040001F6 RID: 502
		[ChunkLookup("document")]
		public string id;
	}
}
