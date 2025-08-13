using System;
using UnityEngine;

// Token: 0x020000FE RID: 254
public class ScrapingSFX : MonoBehaviour
{
	// Token: 0x17000045 RID: 69
	// (get) Token: 0x06000535 RID: 1333 RVA: 0x0001BDAB File Offset: 0x00019FAB
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

	// Token: 0x17000046 RID: 70
	// (get) Token: 0x06000536 RID: 1334 RVA: 0x0001BDC2 File Offset: 0x00019FC2
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

	// Token: 0x17000047 RID: 71
	// (get) Token: 0x06000537 RID: 1335 RVA: 0x0001BDD9 File Offset: 0x00019FD9
	private bool isScrapingSmooth
	{
		get
		{
			return this.isOverridden || this.lastScrapingTime + this.isScrapingAllowance > Time.time;
		}
	}

	// Token: 0x06000538 RID: 1336 RVA: 0x0001BDF9 File Offset: 0x00019FF9
	private void OnEnable()
	{
		ScrapingSFX.s = this;
	}

	// Token: 0x06000539 RID: 1337 RVA: 0x0001BE04 File Offset: 0x0001A004
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

	// Token: 0x0600053A RID: 1338 RVA: 0x0001BF43 File Offset: 0x0001A143
	private void OnTriggerEnter(Collider other)
	{
		this.HandleTrigger(other);
	}

	// Token: 0x0600053B RID: 1339 RVA: 0x0001BF4C File Offset: 0x0001A14C
	private void OnTriggerStay(Collider other)
	{
		this.HandleTrigger(other);
	}

	// Token: 0x0600053C RID: 1340 RVA: 0x0001BF58 File Offset: 0x0001A158
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

	// Token: 0x0600053D RID: 1341 RVA: 0x0001BFBC File Offset: 0x0001A1BC
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

	// Token: 0x0600053E RID: 1342 RVA: 0x0001C03E File Offset: 0x0001A23E
	public void SetOverride(AudioClip scrapingClip)
	{
		this.isOverridden = true;
		this.SetScraping(scrapingClip, true);
	}

	// Token: 0x0600053F RID: 1343 RVA: 0x0001C04F File Offset: 0x0001A24F
	public void ClearOverride()
	{
		this.isOverridden = false;
	}

	// Token: 0x04000725 RID: 1829
	public static ScrapingSFX s;

	// Token: 0x04000726 RID: 1830
	public AudioSource audioSource;

	// Token: 0x04000727 RID: 1831
	public AudioSource altAudioSource;

	// Token: 0x04000728 RID: 1832
	private bool isAudioAlternated;

	// Token: 0x04000729 RID: 1833
	private float t;

	// Token: 0x0400072A RID: 1834
	public Rigidbody rigidbody;

	// Token: 0x0400072B RID: 1835
	[Range(0f, 1f)]
	public float volume = 0.5f;

	// Token: 0x0400072C RID: 1836
	public float minSpeed = 2f;

	// Token: 0x0400072D RID: 1837
	public float maxSpeed = 5f;

	// Token: 0x0400072E RID: 1838
	public float fadeIn = 0.05f;

	// Token: 0x0400072F RID: 1839
	public float fadeOut = 0.05f;

	// Token: 0x04000730 RID: 1840
	public float strength;

	// Token: 0x04000731 RID: 1841
	public bool isScraping;

	// Token: 0x04000732 RID: 1842
	public float isScrapingAllowance = 0.05f;

	// Token: 0x04000733 RID: 1843
	private float lastScrapingTime;

	// Token: 0x04000734 RID: 1844
	private bool isOverridden;

	// Token: 0x04000735 RID: 1845
	private AudioClip overriddenClip;
}
