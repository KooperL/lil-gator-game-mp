using System;
using UnityEngine;
using UnityEngine.UI;

public class UITextBox : MonoBehaviour
{
	// (get) Token: 0x0600135B RID: 4955 RVA: 0x0005EE88 File Offset: 0x0005D088
	public float Height
	{
		get
		{
			return this.rectTransform.rect.height;
		}
	}

	// Token: 0x0600135C RID: 4956 RVA: 0x00010607 File Offset: 0x0000E807
	private void Awake()
	{
		this.rectTransform = base.GetComponent<RectTransform>();
	}

	// Token: 0x0600135D RID: 4957 RVA: 0x0005EEA8 File Offset: 0x0005D0A8
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

	// Token: 0x0600135E RID: 4958 RVA: 0x00010615 File Offset: 0x0000E815
	public void SetColor(Color color)
	{
		this.coloredImage.color = color;
	}

	public Text invisibleText;

	public Text visibleText;

	public Image coloredImage;

	private RectTransform rectTransform;

	public bool ignoreIndents;
}
