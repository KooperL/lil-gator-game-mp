using System;
using UnityEngine;

public class ScrapingSFX : MonoBehaviour
{
	// (get) Token: 0x06000681 RID: 1665 RVA: 0x00006B18 File Offset: 0x00004D18
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

	// (get) Token: 0x06000682 RID: 1666 RVA: 0x00006B2F File Offset: 0x00004D2F
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

	// (get) Token: 0x06000683 RID: 1667 RVA: 0x00006B46 File Offset: 0x00004D46
	private bool isScrapingSmooth
	{
		get
		{
			return this.isOverridden || this.lastScrapingTime + this.isScrapingAllowance > Time.time;
		}
	}

	// Token: 0x06000684 RID: 1668 RVA: 0x00006B66 File Offset: 0x00004D66
	private void OnEnable()
	{
		ScrapingSFX.s = this;
	}

	// Token: 0x06000685 RID: 1669 RVA: 0x00031BAC File Offset: 0x0002FDAC
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

	// Token: 0x06000686 RID: 1670 RVA: 0x00006B6E File Offset: 0x00004D6E
	private void OnTriggerEnter(Collider other)
	{
		this.HandleTrigger(other);
	}

	// Token: 0x06000687 RID: 1671 RVA: 0x00006B6E File Offset: 0x00004D6E
	private void OnTriggerStay(Collider other)
	{
		this.HandleTrigger(other);
	}

	// Token: 0x06000688 RID: 1672 RVA: 0x00031CEC File Offset: 0x0002FEEC
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

	// Token: 0x06000689 RID: 1673 RVA: 0x00031D50 File Offset: 0x0002FF50
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
			otherAudioSource.time = global::UnityEngine.Random.value * scrapingClip.length;
			otherAudioSource.volume = 0f;
			otherAudioSource.Play();
			this.isAudioAlternated = !this.isAudioAlternated;
		}
	}

	// Token: 0x0600068A RID: 1674 RVA: 0x00006B77 File Offset: 0x00004D77
	public void SetOverride(AudioClip scrapingClip)
	{
		this.isOverridden = true;
		this.SetScraping(scrapingClip, true);
	}

	// Token: 0x0600068B RID: 1675 RVA: 0x00006B88 File Offset: 0x00004D88
	public void ClearOverride()
	{
		this.isOverridden = false;
	}

	public static ScrapingSFX s;

	public AudioSource audioSource;

	public AudioSource altAudioSource;

	private bool isAudioAlternated;

	private float t;

	public Rigidbody rigidbody;

	[Range(0f, 1f)]
	public float volume = 0.5f;

	public float minSpeed = 2f;

	public float maxSpeed = 5f;

	public float fadeIn = 0.05f;

	public float fadeOut = 0.05f;

	public float strength;

	public bool isScraping;

	public float isScrapingAllowance = 0.05f;

	private float lastScrapingTime;

	private bool isOverridden;

	private AudioClip overriddenClip;
}
