using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200004A RID: 74
public class BlacksmithHit : MonoBehaviour, IHit
{
	// Token: 0x06000120 RID: 288 RVA: 0x00006F04 File Offset: 0x00005104
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

	// Token: 0x06000121 RID: 289 RVA: 0x00007040 File Offset: 0x00005240
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

	// Token: 0x0400018D RID: 397
	public float hitChargeAmount = 0.03f;

	// Token: 0x0400018E RID: 398
	[ReadOnly]
	public float charge;

	// Token: 0x0400018F RID: 399
	public float chargeDrainSpeed = 0.15f;

	// Token: 0x04000190 RID: 400
	public float chargeDrainAcc = 0.5f;

	// Token: 0x04000191 RID: 401
	[ReadOnly]
	public float chargeVelocity;

	// Token: 0x04000192 RID: 402
	public BlacksmithHit.HitParticles[] hitParticles;

	// Token: 0x04000193 RID: 403
	public BlacksmithHit.ChargeParticles[] chargeParticles;

	// Token: 0x04000194 RID: 404
	public BlacksmithHit.ChargeAudio[] chargeAudio;

	// Token: 0x04000195 RID: 405
	public UnityEvent onFullyCharged;

	// Token: 0x04000196 RID: 406
	public MultilingualTextDocument document;

	// Token: 0x04000197 RID: 407
	public BlacksmithHit.ChargeShout[] chargeShouts;

	// Token: 0x04000198 RID: 408
	public DialogueActor[] actors;

	// Token: 0x02000362 RID: 866
	[Serializable]
	public struct HitParticles
	{
		// Token: 0x04001A1C RID: 6684
		public ParticleSystem particles;

		// Token: 0x04001A1D RID: 6685
		[Range(0f, 1f)]
		public float lowThreshold;

		// Token: 0x04001A1E RID: 6686
		public int lowCount;

		// Token: 0x04001A1F RID: 6687
		[Range(0f, 1f)]
		public float highThreshold;

		// Token: 0x04001A20 RID: 6688
		public int highCount;
	}

	// Token: 0x02000363 RID: 867
	[Serializable]
	public struct ChargeParticles
	{
		// Token: 0x04001A21 RID: 6689
		public ParticleSystem particles;

		// Token: 0x04001A22 RID: 6690
		[Range(0f, 1f)]
		public float lowThreshold;

		// Token: 0x04001A23 RID: 6691
		public float lowEmission;

		// Token: 0x04001A24 RID: 6692
		[Range(0f, 1f)]
		public float highThreshold;

		// Token: 0x04001A25 RID: 6693
		public float highEmission;
	}

	// Token: 0x02000364 RID: 868
	[Serializable]
	public struct ChargeAudio
	{
		// Token: 0x04001A26 RID: 6694
		public AudioSource audioSource;

		// Token: 0x04001A27 RID: 6695
		[Range(0f, 1f)]
		public float lowThreshold;

		// Token: 0x04001A28 RID: 6696
		[Range(0f, 1f)]
		public float lowVolume;

		// Token: 0x04001A29 RID: 6697
		[Range(0f, 2f)]
		public float lowPitch;

		// Token: 0x04001A2A RID: 6698
		[Range(0f, 1f)]
		public float highThreshold;

		// Token: 0x04001A2B RID: 6699
		[Range(0f, 1f)]
		public float highVolume;

		// Token: 0x04001A2C RID: 6700
		[Range(0f, 2f)]
		public float highPitch;
	}

	// Token: 0x02000365 RID: 869
	[Serializable]
	public struct ChargeShout
	{
		// Token: 0x04001A2D RID: 6701
		public float chargeThreshold;

		// Token: 0x04001A2E RID: 6702
		public bool triggered;

		// Token: 0x04001A2F RID: 6703
		[ChunkLookup("document")]
		public string id;
	}
}
