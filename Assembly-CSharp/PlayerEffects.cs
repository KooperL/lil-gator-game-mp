using System;
using UnityEngine;

// Token: 0x0200026D RID: 621
public class PlayerEffects : MonoBehaviour
{
	// Token: 0x06000BE1 RID: 3041 RVA: 0x0000B286 File Offset: 0x00009486
	private void Awake()
	{
		this.playerMaterial = this.playerRenderer.material;
	}

	// Token: 0x06000BE2 RID: 3042 RVA: 0x00040FA0 File Offset: 0x0003F1A0
	private void Start()
	{
		this.playerMaterial.DisableKeyword("CARTOON_SPEC");
		this.drippingEffect.emission.enabled = false;
	}

	// Token: 0x06000BE3 RID: 3043 RVA: 0x0000B299 File Offset: 0x00009499
	public void Scrape()
	{
		this.isScraping = true;
		this.dustCounter += Mathf.Abs(this.rigidbody.velocity.y) * this.climbingSlideDustSpeed * Time.deltaTime;
	}

	// Token: 0x06000BE4 RID: 3044 RVA: 0x00040FD4 File Offset: 0x0003F1D4
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

	// Token: 0x06000BE5 RID: 3045 RVA: 0x000414D4 File Offset: 0x0003F6D4
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

	// Token: 0x06000BE6 RID: 3046 RVA: 0x00041600 File Offset: 0x0003F800
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

	// Token: 0x06000BE7 RID: 3047 RVA: 0x0004168C File Offset: 0x0003F88C
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

	// Token: 0x06000BE8 RID: 3048 RVA: 0x0000B2D1 File Offset: 0x000094D1
	public void SledTransition()
	{
		PlayAudio.p.PlayAtPoint(this.sledTransition, base.transform.position);
	}

	// Token: 0x06000BE9 RID: 3049 RVA: 0x0000B2EE File Offset: 0x000094EE
	public void PlaySwimSound()
	{
		if (this.swimSound != null)
		{
			this.swimSound.Play();
		}
	}

	// Token: 0x04000EF3 RID: 3827
	public PlayerMovement movement;

	// Token: 0x04000EF4 RID: 3828
	public Rigidbody rigidbody;

	// Token: 0x04000EF5 RID: 3829
	public Vector3 dustOffset;

	// Token: 0x04000EF6 RID: 3830
	public float minDustSpeed = 0.2f;

	// Token: 0x04000EF7 RID: 3831
	public int jumpDustCount = 15;

	// Token: 0x04000EF8 RID: 3832
	public float walkDustSpeed = 1f;

	// Token: 0x04000EF9 RID: 3833
	public float walkDustSize = 0.25f;

	// Token: 0x04000EFA RID: 3834
	[Header("Sled")]
	public SoundEffect sledTransition;

	// Token: 0x04000EFB RID: 3835
	public float sledDustSpeed = 3f;

	// Token: 0x04000EFC RID: 3836
	public int sledLandDustCount = 15;

	// Token: 0x04000EFD RID: 3837
	public SoundEffect sledJump;

	// Token: 0x04000EFE RID: 3838
	public SoundEffect sledLand;

	// Token: 0x04000EFF RID: 3839
	public ScrapingSFX scrapingSFX;

	// Token: 0x04000F00 RID: 3840
	[Header("Water")]
	public SoundEffect waterJump;

	// Token: 0x04000F01 RID: 3841
	public SoundEffect waterLand;

	// Token: 0x04000F02 RID: 3842
	public AudioSourceVariance swimSound;

	// Token: 0x04000F03 RID: 3843
	public Transform sledWaterEffectRoot;

	// Token: 0x04000F04 RID: 3844
	public ParticleSystem[] sledWaterEffects;

	// Token: 0x04000F05 RID: 3845
	public ParticleSystem drippingEffect;

	// Token: 0x04000F06 RID: 3846
	[ReadOnly]
	public int overrideIsWet;

	// Token: 0x04000F07 RID: 3847
	private bool wasWet;

	// Token: 0x04000F08 RID: 3848
	private float isWetSmooth;

	// Token: 0x04000F09 RID: 3849
	public Renderer playerRenderer;

	// Token: 0x04000F0A RID: 3850
	private Material playerMaterial;

	// Token: 0x04000F0B RID: 3851
	private int _specularBrightness = Shader.PropertyToID("_SpecularBrightness");

	// Token: 0x04000F0C RID: 3852
	[Header("Climbing")]
	public ParticleSystem climbingEffect;

	// Token: 0x04000F0D RID: 3853
	public float climbingSlideDustSpeed = 3f;

	// Token: 0x04000F0E RID: 3854
	public Vector3 climbingDustOffset;

	// Token: 0x04000F0F RID: 3855
	private float dustCounter;

	// Token: 0x04000F10 RID: 3856
	private bool isScraping;

	// Token: 0x04000F11 RID: 3857
	private float sledSwimmingCounter;

	// Token: 0x04000F12 RID: 3858
	private const float sledSwimmingInterval = 1f;

	// Token: 0x04000F13 RID: 3859
	private bool updateSledWaterEffects;

	// Token: 0x04000F14 RID: 3860
	private float lastClimbGrab = -1f;
}
