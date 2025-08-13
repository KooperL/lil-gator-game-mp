using System;
using UnityEngine;

// Token: 0x020002C6 RID: 710
public class UIHideMove : UIHideBehavior
{
	// Token: 0x06000EEC RID: 3820 RVA: 0x00047ABE File Offset: 0x00045CBE
	private void OnValidate()
	{
		if (this.rectTransform == null)
		{
			this.rectTransform = base.GetComponent<RectTransform>();
		}
	}

	// Token: 0x06000EED RID: 3821 RVA: 0x00047ADA File Offset: 0x00045CDA
	private void Awake()
	{
		this.position = this.anchoredHidePosition;
	}

	// Token: 0x06000EEE RID: 3822 RVA: 0x00047AE8 File Offset: 0x00045CE8
	[ContextMenu("Hide")]
	public override void Hide()
	{
		this.isHiding = true;
		base.enabled = true;
	}

	// Token: 0x06000EEF RID: 3823 RVA: 0x00047AF8 File Offset: 0x00045CF8
	[ContextMenu("Show")]
	public override void Show()
	{
		if (this.isHiding)
		{
			base.gameObject.SetActive(true);
			this.isHiding = false;
			this.rectTransform.anchoredPosition = this.position;
			base.enabled = true;
		}
		if (this.autoHideDelay > 0f)
		{
			this.autoHideTime = Time.time + this.autoHideDelay;
		}
	}

	// Token: 0x06000EF0 RID: 3824 RVA: 0x00047B58 File Offset: 0x00045D58
	protected override void Update()
	{
		base.Update();
		Vector2 vector = (this.isHiding ? this.anchoredHidePosition : this.anchoredShowPosition);
		float num = this.smoothTime;
		if (this.isHiding && this.hideTime > 0f)
		{
			num = this.hideTime;
		}
		this.position = Vector2.SmoothDamp(this.position, vector, ref this.velocity, num);
		if (Vector2.SqrMagnitude(this.position - vector) < 0.05f)
		{
			if (this.autoHideTime < 0f)
			{
				base.enabled = false;
			}
			if (this.isHiding)
			{
				base.gameObject.SetActive(false);
			}
		}
		this.rectTransform.anchoredPosition = this.position;
	}

	// Token: 0x04001381 RID: 4993
	public RectTransform rectTransform;

	// Token: 0x04001382 RID: 4994
	public Vector2 anchoredHidePosition;

	// Token: 0x04001383 RID: 4995
	public Vector2 anchoredShowPosition;

	// Token: 0x04001384 RID: 4996
	private Vector2 velocity;

	// Token: 0x04001385 RID: 4997
	public float smoothTime = 0.2f;

	// Token: 0x04001386 RID: 4998
	public float hideTime;

	// Token: 0x04001387 RID: 4999
	private Vector2 position;
}
