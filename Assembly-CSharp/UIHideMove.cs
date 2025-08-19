using System;
using UnityEngine;

public class UIHideMove : UIHideBehavior
{
	// Token: 0x06001224 RID: 4644 RVA: 0x0000F6BF File Offset: 0x0000D8BF
	private void OnValidate()
	{
		if (this.rectTransform == null)
		{
			this.rectTransform = base.GetComponent<RectTransform>();
		}
	}

	// Token: 0x06001225 RID: 4645 RVA: 0x0000F6DB File Offset: 0x0000D8DB
	private void Awake()
	{
		this.position = this.anchoredHidePosition;
	}

	// Token: 0x06001226 RID: 4646 RVA: 0x0000F6E9 File Offset: 0x0000D8E9
	[ContextMenu("Hide")]
	public override void Hide()
	{
		this.isHiding = true;
		base.enabled = true;
	}

	// Token: 0x06001227 RID: 4647 RVA: 0x0005AD6C File Offset: 0x00058F6C
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

	// Token: 0x06001228 RID: 4648 RVA: 0x0005ADCC File Offset: 0x00058FCC
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
