using System;
using UnityEngine;

public class UIHideMove : UIHideBehavior
{
	// Token: 0x06001224 RID: 4644 RVA: 0x0000F6B5 File Offset: 0x0000D8B5
	private void OnValidate()
	{
		if (this.rectTransform == null)
		{
			this.rectTransform = base.GetComponent<RectTransform>();
		}
	}

	// Token: 0x06001225 RID: 4645 RVA: 0x0000F6D1 File Offset: 0x0000D8D1
	private void Awake()
	{
		this.position = this.anchoredHidePosition;
	}

	// Token: 0x06001226 RID: 4646 RVA: 0x0000F6DF File Offset: 0x0000D8DF
	[ContextMenu("Hide")]
	public override void Hide()
	{
		this.isHiding = true;
		base.enabled = true;
	}

	// Token: 0x06001227 RID: 4647 RVA: 0x0005AD90 File Offset: 0x00058F90
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

	// Token: 0x06001228 RID: 4648 RVA: 0x0005ADF0 File Offset: 0x00058FF0
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

	public RectTransform rectTransform;

	public Vector2 anchoredHidePosition;

	public Vector2 anchoredShowPosition;

	private Vector2 velocity;

	public float smoothTime = 0.2f;

	public float hideTime;

	private Vector2 position;
}
