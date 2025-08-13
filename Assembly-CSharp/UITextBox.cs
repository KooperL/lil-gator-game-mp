using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002E8 RID: 744
public class UITextBox : MonoBehaviour
{
	// Token: 0x170000E8 RID: 232
	// (get) Token: 0x06000FCF RID: 4047 RVA: 0x0004B8F8 File Offset: 0x00049AF8
	public float Height
	{
		get
		{
			return this.rectTransform.rect.height;
		}
	}

	// Token: 0x06000FD0 RID: 4048 RVA: 0x0004B918 File Offset: 0x00049B18
	private void Awake()
	{
		this.rectTransform = base.GetComponent<RectTransform>();
	}

	// Token: 0x06000FD1 RID: 4049 RVA: 0x0004B928 File Offset: 0x00049B28
	public void SetText(string newText)
	{
		if (this.ignoreIndents)
		{
			newText = newText.Replace('\n', ' ').Replace('\r', ' ');
		}
		if (this.invisibleText != null)
		{
			this.invisibleText.text = newText;
		}
		this.visibleText.text = newText;
	}

	// Token: 0x06000FD2 RID: 4050 RVA: 0x0004B978 File Offset: 0x00049B78
	public void SetColor(Color color)
	{
		this.coloredImage.color = color;
	}

	// Token: 0x040014B1 RID: 5297
	public Text invisibleText;

	// Token: 0x040014B2 RID: 5298
	public Text visibleText;

	// Token: 0x040014B3 RID: 5299
	public Image coloredImage;

	// Token: 0x040014B4 RID: 5300
	private RectTransform rectTransform;

	// Token: 0x040014B5 RID: 5301
	public bool ignoreIndents;
}
