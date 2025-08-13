using System;
using UnityEngine;

// Token: 0x0200003E RID: 62
public class FadeTrail : MonoBehaviour
{
	// Token: 0x060000FD RID: 253 RVA: 0x000068C4 File Offset: 0x00004AC4
	public void OnValidate()
	{
		if (this.rigidbody == null)
		{
			this.rigidbody = base.GetComponentInParent<Rigidbody>();
		}
		if (this.trailRenderer == null)
		{
			this.trailRenderer = base.GetComponent<TrailRenderer>();
		}
	}

	// Token: 0x060000FE RID: 254 RVA: 0x000068FA File Offset: 0x00004AFA
	private void Start()
	{
		this.fadeStartTime = Time.time + this.fadeDelay;
		this.fadeEndTime = this.fadeStartTime + this.fadeDuration;
		this.baseWidth = this.trailRenderer.widthMultiplier;
	}

	// Token: 0x060000FF RID: 255 RVA: 0x00006934 File Offset: 0x00004B34
	private void Update()
	{
		float magnitude = this.rigidbody.velocity.magnitude;
		float num = Mathf.InverseLerp(this.slowestSpeed, this.fastestSpeed, magnitude);
		if (Time.time > this.fadeStartTime)
		{
			num = Mathf.Min(num, Mathf.InverseLerp(this.fadeEndTime, this.fadeStartTime, Time.time));
		}
		this.smoothFade = Mathf.SmoothDamp(this.smoothFade, num, ref this.smoothFadeVel, 0.2f);
		this.trailRenderer.widthMultiplier = this.baseWidth * this.smoothFade;
	}

	// Token: 0x0400015F RID: 351
	public TrailRenderer trailRenderer;

	// Token: 0x04000160 RID: 352
	public Rigidbody rigidbody;

	// Token: 0x04000161 RID: 353
	public float fastestSpeed = 10f;

	// Token: 0x04000162 RID: 354
	public float slowestSpeed = 3f;

	// Token: 0x04000163 RID: 355
	public float fadeDelay = 1f;

	// Token: 0x04000164 RID: 356
	public float fadeDuration = 1f;

	// Token: 0x04000165 RID: 357
	private float fadeStartTime;

	// Token: 0x04000166 RID: 358
	private float fadeEndTime;

	// Token: 0x04000167 RID: 359
	private float smoothFade;

	// Token: 0x04000168 RID: 360
	private float smoothFadeVel;

	// Token: 0x04000169 RID: 361
	private float baseWidth;
}
