using System;
using UnityEngine;

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

	public TrailRenderer trailRenderer;

	public Rigidbody rigidbody;

	public float fastestSpeed = 10f;

	public float slowestSpeed = 3f;

	public float fadeDelay = 1f;

	public float fadeDuration = 1f;

	private float fadeStartTime;

	private float fadeEndTime;

	private float smoothFade;

	private float smoothFadeVel;

	private float baseWidth;
}
