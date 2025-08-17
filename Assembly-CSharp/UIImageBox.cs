using System;
using UnityEngine;
using UnityEngine.UI;

public class UIImageBox : MonoBehaviour
{
	// Token: 0x0600122A RID: 4650 RVA: 0x0005AEA8 File Offset: 0x000590A8
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

	// Token: 0x0600122B RID: 4651 RVA: 0x0000F702 File Offset: 0x0000D902
	public void SetColor(Color color)
	{
		this.coloredImage.color = color;
	}

	public Image invisibleImage;

	public Image displayImage;

	public Image coloredImage;

	public bool resizeToFit = true;

	[ConditionalHide("resizeToFit", true)]
	public RectTransform resizeTransform;

	[ConditionalHide("resizeToFit", true)]
	public float maxWidth = 1000f;

	[ConditionalHide("resizeToFit", true)]
	public float maxHeight = 1000f;
}
