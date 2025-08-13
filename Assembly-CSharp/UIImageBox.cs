using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002C7 RID: 711
public class UIImageBox : MonoBehaviour
{
	// Token: 0x06000EF2 RID: 3826 RVA: 0x00047C24 File Offset: 0x00045E24
	public void SetImage(Sprite image)
	{
		if (this.resizeToFit)
		{
			Vector3 vector = image.pixelsPerUnit * image.bounds.size;
			float num = 1f;
			if (vector.x > this.maxWidth)
			{
				num = this.maxWidth / vector.x;
			}
			if (vector.y > this.maxHeight)
			{
				num = Mathf.Min(num, this.maxHeight / vector.y);
			}
			this.resizeTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, num * vector.x);
			this.resizeTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, num * vector.y);
		}
		if (this.invisibleImage != null)
		{
			this.invisibleImage.sprite = image;
		}
		this.displayImage.sprite = image;
	}

	// Token: 0x06000EF3 RID: 3827 RVA: 0x00047CE8 File Offset: 0x00045EE8
	public void SetColor(Color color)
	{
		this.coloredImage.color = color;
	}

	// Token: 0x04001388 RID: 5000
	public Image invisibleImage;

	// Token: 0x04001389 RID: 5001
	public Image displayImage;

	// Token: 0x0400138A RID: 5002
	public Image coloredImage;

	// Token: 0x0400138B RID: 5003
	public bool resizeToFit = true;

	// Token: 0x0400138C RID: 5004
	[ConditionalHide("resizeToFit", true)]
	public RectTransform resizeTransform;

	// Token: 0x0400138D RID: 5005
	[ConditionalHide("resizeToFit", true)]
	public float maxWidth = 1000f;

	// Token: 0x0400138E RID: 5006
	[ConditionalHide("resizeToFit", true)]
	public float maxHeight = 1000f;
}
