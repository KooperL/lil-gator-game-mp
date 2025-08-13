using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003D9 RID: 985
public class UITextBox : MonoBehaviour
{
	// Token: 0x170001F7 RID: 503
	// (get) Token: 0x060012FB RID: 4859 RVA: 0x0005CE84 File Offset: 0x0005B084
	public float Height
	{
		get
		{
			return this.rectTransform.rect.height;
		}
	}

	// Token: 0x060012FC RID: 4860 RVA: 0x00010200 File Offset: 0x0000E400
	private void Awake()
	{
		this.rectTransform = base.GetComponent<RectTransform>();
	}

	// Token: 0x060012FD RID: 4861 RVA: 0x0005CEA4 File Offset: 0x0005B0A4
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

	// Token: 0x060012FE RID: 4862 RVA: 0x0001020E File Offset: 0x0000E40E
	public void SetColor(Color color)
	{
		this.coloredImage.color = color;
	}

	// Token: 0x04001870 RID: 6256
	public Text invisibleText;

	// Token: 0x04001871 RID: 6257
	public Text visibleText;

	// Token: 0x04001872 RID: 6258
	public Image coloredImage;

	// Token: 0x04001873 RID: 6259
	private RectTransform rectTransform;

	// Token: 0x04001874 RID: 6260
	public bool ignoreIndents;
}
