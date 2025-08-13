using System;
using UnityEngine;

// Token: 0x02000050 RID: 80
public class FadeTrail : MonoBehaviour
{
	// Token: 0x06000122 RID: 290 RVA: 0x00002F6B File Offset: 0x0000116B
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

	// Token: 0x06000123 RID: 291 RVA: 0x00002FA1 File Offset: 0x000011A1
	private void Start()
	{
		this.fadeStartTime = Time.time + this.fadeDelay;
		this.fadeEndTime = this.fadeStartTime + this.fadeDuration;
		this.baseWidth = this.trailRenderer.widthMultiplier;
	}

	// Token: 0x06000124 RID: 292 RVA: 0x0001B1A8 File Offset: 0x000193A8
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

	// Token: 0x040001A6 RID: 422
	public TrailRenderer trailRenderer;

	// Token: 0x040001A7 RID: 423
	public Rigidbody rigidbody;

	// Token: 0x040001A8 RID: 424
	public float fastestSpeed = 10f;

	// Token: 0x040001A9 RID: 425
	public float slowestSpeed = 3f;

	// Token: 0x040001AA RID: 426
	public float fadeDelay = 1f;

	// Token: 0x040001AB RID: 427
	public float fadeDuration = 1f;

	// Token: 0x040001AC RID: 428
	private float fadeStartTime;

	// Token: 0x040001AD RID: 429
	private float fadeEndTime;

	// Token: 0x040001AE RID: 430
	private float smoothFade;

	// Token: 0x040001AF RID: 431
	private float smoothFadeVel;

	// Token: 0x040001B0 RID: 432
	private float baseWidth;
}
