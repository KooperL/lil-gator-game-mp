using System;
using UnityEngine;
using UnityEngine.Events;

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

	public float hitChargeAmount = 0.03f;

	[ReadOnly]
	public float charge;

	public float chargeDrainSpeed = 0.15f;

	public float chargeDrainAcc = 0.5f;

	[ReadOnly]
	public float chargeVelocity;

	public BlacksmithHit.HitParticles[] hitParticles;

	public BlacksmithHit.ChargeParticles[] chargeParticles;

	public BlacksmithHit.ChargeAudio[] chargeAudio;

	public UnityEvent onFullyCharged;

	public MultilingualTextDocument document;

	public BlacksmithHit.ChargeShout[] chargeShouts;

	public DialogueActor[] actors;

	[Serializable]
	public struct HitParticles
	{
		public ParticleSystem particles;

		[Range(0f, 1f)]
		public float lowThreshold;

		public int lowCount;

		[Range(0f, 1f)]
		public float highThreshold;

		public int highCount;
	}

	[Serializable]
	public struct ChargeParticles
	{
		public ParticleSystem particles;

		[Range(0f, 1f)]
		public float lowThreshold;

		public float lowEmission;

		[Range(0f, 1f)]
		public float highThreshold;

		public float highEmission;
	}

	[Serializable]
	public struct ChargeAudio
	{
		public AudioSource audioSource;

		[Range(0f, 1f)]
		public float lowThreshold;

		[Range(0f, 1f)]
		public float lowVolume;

		[Range(0f, 2f)]
		public float lowPitch;

		[Range(0f, 1f)]
		public float highThreshold;

		[Range(0f, 1f)]
		public float highVolume;

		[Range(0f, 2f)]
		public float highPitch;
	}

	[Serializable]
	public struct ChargeShout
	{
		public float chargeThreshold;

		public bool triggered;

		[ChunkLookup("document")]
		public string id;
	}
}
