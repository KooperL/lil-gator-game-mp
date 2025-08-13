using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003A9 RID: 937
public class UIImageBox : MonoBehaviour
{
	// Token: 0x060011CA RID: 4554 RVA: 0x00058EE4 File Offset: 0x000570E4
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
			this.resizeTransform.SetSizeWithCurrentAnchors(0, num * vector.x);
			this.resizeTransform.SetSizeWithCurrentAnchors(1, num * vector.y);
		}
		if (this.invisibleImage != null)
		{
			this.invisibleImage.sprite = image;
		}
		this.displayImage.sprite = image;
	}

	// Token: 0x060011CB RID: 4555 RVA: 0x0000F319 File Offset: 0x0000D519
	public void SetColor(Color color)
	{
		this.coloredImage.color = color;
	}

	// Token: 0x040016FE RID: 5886
	public Image invisibleImage;

	// Token: 0x040016FF RID: 5887
	public Image displayImage;

	// Token: 0x04001700 RID: 5888
	public Image coloredImage;

	// Token: 0x04001701 RID: 5889
	public bool resizeToFit = true;

	// Token: 0x04001702 RID: 5890
	[ConditionalHide("resizeToFit", true)]
	public RectTransform resizeTransform;

	// Token: 0x04001703 RID: 5891
	[ConditionalHide("resizeToFit", true)]
	public float maxWidth = 1000f;

	// Token: 0x04001704 RID: 5892
	[ConditionalHide("resizeToFit", true)]
	public float maxHeight = 1000f;
}
