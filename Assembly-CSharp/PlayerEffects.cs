using System;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
	// Token: 0x06000C2D RID: 3117 RVA: 0x0000B598 File Offset: 0x00009798
	private void Awake()
	{
		this.playerMaterial = this.playerRenderer.sharedMaterial;
	}

	// Token: 0x06000C2E RID: 3118 RVA: 0x00042B04 File Offset: 0x00040D04
	private void Start()
	{
		this.playerMaterial.DisableKeyword("CARTOON_SPEC");
		this.drippingEffect.emission.enabled = false;
	}

	// Token: 0x06000C2F RID: 3119 RVA: 0x0000B5AB File Offset: 0x000097AB
	public void Scrape()
	{
		this.isScraping = true;
		this.dustCounter += Mathf.Abs(this.rigidbody.velocity.y) * this.climbingSlideDustSpeed * Time.deltaTime;
	}

	// Token: 0x06000C30 RID: 3120 RVA: 0x00042B38 File Offset: 0x00040D38
	private void FixedUpdate()
	{
		SurfaceMaterial surfaceMaterial = null;
		float magnitude = this.rigidbody.velocity.magnitude;
		if ((!this.movement.enabled || (!this.movement.InAir && !this.movement.IsSubmerged && !this.movement.IsClimbing)) && magnitude > this.minDustSpeed && (!this.movement.enabled || this.movement.isSledding))
		{
			this.isScraping = true;
			this.dustCounter += magnitude * this.sledDustSpeed * Time.deltaTime;
		}
		if (this.movement.isSledding && this.movement.IsSubmerged && magnitude > this.minDustSpeed)
		{
			this.isScraping = true;
		}
		if (this.movement.IsClimbing && this.movement.Stamina == 0f)
		{
			this.Scrape();
		}
		this.climbingEffect.emission.enabled = this.movement.IsClimbing && magnitude > 0.5f;
		if (this.isScraping && (this.dustCounter >= 1f || !this.scrapingSFX.enabled || Player.movement.IsSubmerged))
		{
			if (Player.movement.IsSubmerged)
			{
				surfaceMaterial = MaterialManager.m.waterMaterial;
			}
			if (surfaceMaterial == null)
			{
				surfaceMaterial = MaterialManager.m.SampleSurfaceMaterial(base.transform.position, -this.movement.animGroundNormal);
			}
			if (surfaceMaterial != null && surfaceMaterial.scraping != null)
			{
				this.scrapingSFX.SetScraping(surfaceMaterial.scraping, false);
			}
		}
		if (this.dustCounter > 1f)
		{
			int num = Mathf.FloorToInt(this.dustCounter);
			Vector3 vector = base.transform.position + this.dustOffset;
			if (this.isScraping && !this.movement.IsClimbing)
			{
				vector = base.transform.TransformPoint(new Vector3(0f, this.climbingDustOffset.y, 0f));
			}
			if (this.movement.IsClimbing)
			{
				vector = base.transform.TransformPoint(this.climbingDustOffset);
			}
			EffectsManager.e.Dust(vector, num, -0.5f * this.rigidbody.velocity, 0f);
			this.dustCounter -= (float)num;
		}
		bool isSubmerged = this.movement.IsSubmerged;
		this.isWetSmooth = Mathf.MoveTowards(this.isWetSmooth, (isSubmerged || this.overrideIsWet > 0) ? 1f : 0f, Time.deltaTime * (isSubmerged ? 1f : 0.15f));
		if (this.isWetSmooth != 0f || this.wasWet)
		{
			ParticleSystem.EmissionModule emission = this.drippingEffect.emission;
			emission.enabled = !isSubmerged;
			if (this.wasWet != (this.isWetSmooth != 0f))
			{
				if (this.isWetSmooth > 0f)
				{
					this.playerMaterial.EnableKeyword("CARTOON_SPEC");
				}
				else
				{
					this.playerMaterial.DisableKeyword("CARTOON_SPEC");
					emission.enabled = false;
				}
			}
			this.playerMaterial.SetFloat(this._specularBrightness, this.isWetSmooth * 0.2f);
			this.wasWet = this.isWetSmooth != 0f;
		}
		this.isScraping = false;
		if (this.updateSledWaterEffects || (this.movement.isSledding && this.movement.IsInWater))
		{
			Vector3 vector2 = this.movement.velocity.Flat();
			float magnitude2 = vector2.magnitude;
			this.sledSwimmingCounter += Time.deltaTime * magnitude2 + Time.deltaTime;
			if (this.sledSwimmingCounter > 1f)
			{
				this.sledSwimmingCounter = 0f;
				this.swimSound.Play();
			}
			Vector3 position = base.transform.position;
			position.y = this.movement.waterHeight;
			this.sledWaterEffectRoot.position = position;
			this.sledWaterEffectRoot.rotation = Quaternion.LookRotation(vector2);
			float num2;
			if (this.movement.isSledding && this.movement.IsInWater)
			{
				num2 = Mathf.InverseLerp(this.movement.sledBouyancyMinSpeed, this.movement.sledBouyancyMaxSpeed, magnitude2);
			}
			else
			{
				num2 = 0f;
			}
			this.updateSledWaterEffects = num2 > 0f;
			foreach (ParticleSystem particleSystem in this.sledWaterEffects)
			{
				ParticleSystem.EmissionModule emission2 = particleSystem.emission;
				emission2.rateOverTimeMultiplier = Mathf.Lerp(10f, 20f, num2);
				emission2.enabled = this.updateSledWaterEffects;
				ParticleSystem.MainModule main = particleSystem.main;
				main.startSpeedMultiplier = 7f * num2;
				main.startSizeMultiplier = num2;
			}
		}
	}

	// Token: 0x06000C31 RID: 3121 RVA: 0x00043038 File Offset: 0x00041238
	public void Jump()
	{
		SurfaceMaterial surfaceMaterial = MaterialManager.m.SampleSurfaceMaterial(base.transform.position, Vector3.down);
		if (this.movement.IsSubmerged)
		{
			EffectsManager.e.Splash(new Vector3(base.transform.position.x, this.movement.waterHeight, base.transform.position.z), 0.8f);
		}
		else if (this.movement.isSledding)
		{
			EffectsManager.e.FloorDust(base.transform.position + this.dustOffset, this.jumpDustCount, this.movement.animGroundNormal);
			PlayAudio.p.PlayAtPoint(this.sledJump, base.transform.position);
		}
		else
		{
			EffectsManager.e.FloorDust(base.transform.position + this.dustOffset, this.jumpDustCount, this.movement.animGroundNormal);
		}
		if (surfaceMaterial != null)
		{
			surfaceMaterial.PlayImpact(base.transform.position, 1f, 1.1f);
		}
	}

	// Token: 0x06000C32 RID: 3122 RVA: 0x00043164 File Offset: 0x00041364
	public void Land(SurfaceMaterial surfaceMaterial, Vector3 normal)
	{
		if (this.movement.isSledding)
		{
			EffectsManager.e.FloorDust(base.transform.position + this.dustOffset, this.sledLandDustCount, normal);
			if (surfaceMaterial != null)
			{
				surfaceMaterial.PlayImpact(base.transform.position, 1f, 0.9f);
				return;
			}
		}
		else if (surfaceMaterial != null)
		{
			surfaceMaterial.PlayImpact(base.transform.position, 1f, 1f);
		}
	}

	// Token: 0x06000C33 RID: 3123 RVA: 0x000431F0 File Offset: 0x000413F0
	public void ClimbGrab()
	{
		if (this.lastClimbGrab + 0.1f > Time.time)
		{
			return;
		}
		this.lastClimbGrab = Time.time;
		SurfaceMaterial surfaceMaterial = MaterialManager.m.SampleSurfaceMaterial(base.transform.position, base.transform.forward);
		if (surfaceMaterial != null)
		{
			surfaceMaterial.PlayFootstep(base.transform.position, 0.5f, 1.2f);
		}
	}

	// Token: 0x06000C34 RID: 3124 RVA: 0x0000B5E3 File Offset: 0x000097E3
	public void SledTransition()
	{
		PlayAudio.p.PlayAtPoint(this.sledTransition, base.transform.position);
	}

	// Token: 0x06000C35 RID: 3125 RVA: 0x0000B600 File Offset: 0x00009800
	public void PlaySwimSound()
	{
		if (this.swimSound != null)
		{
			this.swimSound.Play();
		}
	}

	public PlayerMovement movement;

	public Rigidbody rigidbody;

	public Vector3 dustOffset;

	public float minDustSpeed = 0.2f;

	public int jumpDustCount = 15;

	public float walkDustSpeed = 1f;

	public float walkDustSize = 0.25f;

	[Header("Sled")]
	public SoundEffect sledTransition;

	public float sledDustSpeed = 3f;

	public int sledLandDustCount = 15;

	public SoundEffect sledJump;

	public SoundEffect sledLand;

	public ScrapingSFX scrapingSFX;

	[Header("Water")]
	public SoundEffect waterJump;

	public SoundEffect waterLand;

	public AudioSourceVariance swimSound;

	public Transform sledWaterEffectRoot;

	public ParticleSystem[] sledWaterEffects;

	public ParticleSystem drippingEffect;

	[ReadOnly]
	public int overrideIsWet;

	private bool wasWet;

	private float isWetSmooth;

	public Renderer playerRenderer;

	private Material playerMaterial;

	private int _specularBrightness = Shader.PropertyToID("_SpecularBrightness");

	[Header("Climbing")]
	public ParticleSystem climbingEffect;

	public float climbingSlideDustSpeed = 3f;

	public Vector3 climbingDustOffset;

	private float dustCounter;

	private bool isScraping;

	private float sledSwimmingCounter;

	private const float sledSwimmingInterval = 1f;

	private bool updateSledWaterEffects;

	private float lastClimbGrab = -1f;
}
