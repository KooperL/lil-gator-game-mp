using System;
using UnityEngine;

// Token: 0x02000151 RID: 337
public class ScrapingSFX : MonoBehaviour
{
	// Token: 0x1700009F RID: 159
	// (get) Token: 0x06000647 RID: 1607 RVA: 0x00006852 File Offset: 0x00004A52
	private AudioSource CurrentAudioSource
	{
		get
		{
			if (!this.isAudioAlternated)
			{
				return this.audioSource;
			}
			return this.altAudioSource;
		}
	}

	// Token: 0x170000A0 RID: 160
	// (get) Token: 0x06000648 RID: 1608 RVA: 0x00006869 File Offset: 0x00004A69
	private AudioSource OtherAudioSource
	{
		get
		{
			if (!this.isAudioAlternated)
			{
				return this.altAudioSource;
			}
			return this.audioSource;
		}
	}

	// Token: 0x170000A1 RID: 161
	// (get) Token: 0x06000649 RID: 1609 RVA: 0x00006880 File Offset: 0x00004A80
	private bool isScrapingSmooth
	{
		get
		{
			return this.isOverridden || this.lastScrapingTime + this.isScrapingAllowance > Time.time;
		}
	}

	// Token: 0x0600064A RID: 1610 RVA: 0x000068A0 File Offset: 0x00004AA0
	private void OnEnable()
	{
		ScrapingSFX.s = this;
	}

	// Token: 0x0600064B RID: 1611 RVA: 0x000304B0 File Offset: 0x0002E6B0
	private void LateUpdate()
	{
		AudioSource currentAudioSource = this.CurrentAudioSource;
		this.strength = currentAudioSource.volume;
		float num = (this.isScrapingSmooth ? (this.volume * Mathf.InverseLerp(this.minSpeed, this.maxSpeed, this.rigidbody.velocity.magnitude)) : 0f);
		float num2 = ((num > this.strength) ? this.fadeIn : this.fadeOut);
		if (num2 > 0f)
		{
			this.strength = Mathf.MoveTowards(this.strength, num, Time.deltaTime / num2);
		}
		else
		{
			this.strength = num;
		}
		currentAudioSource.volume = this.strength;
		if (currentAudioSource.volume == 0f && currentAudioSource.isPlaying)
		{
			currentAudioSource.Pause();
		}
		else if (currentAudioSource.volume != 0f && !currentAudioSource.isPlaying)
		{
			currentAudioSource.Play();
		}
		AudioSource otherAudioSource = this.OtherAudioSource;
		if (otherAudioSource.volume > 0f)
		{
			otherAudioSource.volume = Mathf.MoveTowards(otherAudioSource.volume, 0f, Time.deltaTime / this.fadeOut);
		}
		else if (otherAudioSource.isPlaying)
		{
			otherAudioSource.Pause();
		}
		if (!currentAudioSource.isPlaying && !otherAudioSource.isPlaying)
		{
			base.enabled = false;
		}
	}

	// Token: 0x0600064C RID: 1612 RVA: 0x000068A8 File Offset: 0x00004AA8
	private void OnTriggerEnter(Collider other)
	{
		this.HandleTrigger(other);
	}

	// Token: 0x0600064D RID: 1613 RVA: 0x000068A8 File Offset: 0x00004AA8
	private void OnTriggerStay(Collider other)
	{
		this.HandleTrigger(other);
	}

	// Token: 0x0600064E RID: 1614 RVA: 0x000305F0 File Offset: 0x0002E7F0
	private void HandleTrigger(Collider other)
	{
		ISurface component = other.GetComponent<ISurface>();
		if (component != null)
		{
			SurfaceMaterial surfaceMaterial = component.GetSurfaceMaterial(this.rigidbody.position + 0.2f * Vector3.down);
			if (surfaceMaterial != null && surfaceMaterial.scraping != null)
			{
				this.SetScraping(surfaceMaterial.scraping, false);
			}
		}
	}

	// Token: 0x0600064F RID: 1615 RVA: 0x00030654 File Offset: 0x0002E854
	public void SetScraping(AudioClip scrapingClip, bool overridden = false)
	{
		if (this.isOverridden && !overridden)
		{
			return;
		}
		base.enabled = true;
		this.lastScrapingTime = Time.time;
		if (this.CurrentAudioSource.clip != scrapingClip)
		{
			AudioSource otherAudioSource = this.OtherAudioSource;
			otherAudioSource.Pause();
			otherAudioSource.clip = scrapingClip;
			otherAudioSource.time = Random.value * scrapingClip.length;
			otherAudioSource.volume = 0f;
			otherAudioSource.Play();
			this.isAudioAlternated = !this.isAudioAlternated;
		}
	}

	// Token: 0x06000650 RID: 1616 RVA: 0x000068B1 File Offset: 0x00004AB1
	public void SetOverride(AudioClip scrapingClip)
	{
		this.isOverridden = true;
		this.SetScraping(scrapingClip, true);
	}

	// Token: 0x06000651 RID: 1617 RVA: 0x000068C2 File Offset: 0x00004AC2
	public void ClearOverride()
	{
		this.isOverridden = false;
	}

	// Token: 0x0400086C RID: 2156
	public static ScrapingSFX s;

	// Token: 0x0400086D RID: 2157
	public AudioSource audioSource;

	// Token: 0x0400086E RID: 2158
	public AudioSource altAudioSource;

	// Token: 0x0400086F RID: 2159
	private bool isAudioAlternated;

	// Token: 0x04000870 RID: 2160
	private float t;

	// Token: 0x04000871 RID: 2161
	public Rigidbody rigidbody;

	// Token: 0x04000872 RID: 2162
	[Range(0f, 1f)]
	public float volume = 0.5f;

	// Token: 0x04000873 RID: 2163
	public float minSpeed = 2f;

	// Token: 0x04000874 RID: 2164
	public float maxSpeed = 5f;

	// Token: 0x04000875 RID: 2165
	public float fadeIn = 0.05f;

	// Token: 0x04000876 RID: 2166
	public float fadeOut = 0.05f;

	// Token: 0x04000877 RID: 2167
	public float strength;

	// Token: 0x04000878 RID: 2168
	public bool isScraping;

	// Token: 0x04000879 RID: 2169
	public float isScrapingAllowance = 0.05f;

	// Token: 0x0400087A RID: 2170
	private float lastScrapingTime;

	// Token: 0x0400087B RID: 2171
	private bool isOverridden;

	// Token: 0x0400087C RID: 2172
	private AudioClip overriddenClip;
}
