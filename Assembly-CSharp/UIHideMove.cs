using System;
using UnityEngine;

// Token: 0x020003A8 RID: 936
public class UIHideMove : UIHideBehavior
{
	// Token: 0x060011C4 RID: 4548 RVA: 0x0000F2CC File Offset: 0x0000D4CC
	private void OnValidate()
	{
		if (this.rectTransform == null)
		{
			this.rectTransform = base.GetComponent<RectTransform>();
		}
	}

	// Token: 0x060011C5 RID: 4549 RVA: 0x0000F2E8 File Offset: 0x0000D4E8
	private void Awake()
	{
		this.position = this.anchoredHidePosition;
	}

	// Token: 0x060011C6 RID: 4550 RVA: 0x0000F2F6 File Offset: 0x0000D4F6
	[ContextMenu("Hide")]
	public override void Hide()
	{
		this.isHiding = true;
		base.enabled = true;
	}

	// Token: 0x060011C7 RID: 4551 RVA: 0x00058DCC File Offset: 0x00056FCC
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

	// Token: 0x060011C8 RID: 4552 RVA: 0x00058E2C File Offset: 0x0005702C
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

	// Token: 0x040016F7 RID: 5879
	public RectTransform rectTransform;

	// Token: 0x040016F8 RID: 5880
	public Vector2 anchoredHidePosition;

	// Token: 0x040016F9 RID: 5881
	public Vector2 anchoredShowPosition;

	// Token: 0x040016FA RID: 5882
	private Vector2 velocity;

	// Token: 0x040016FB RID: 5883
	public float smoothTime = 0.2f;

	// Token: 0x040016FC RID: 5884
	public float hideTime;

	// Token: 0x040016FD RID: 5885
	private Vector2 position;
}
