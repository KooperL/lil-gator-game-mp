using System;
using UnityEngine;
using UnityEngine.UI;

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
