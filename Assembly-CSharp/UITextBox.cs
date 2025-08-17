using System;
using UnityEngine;
using UnityEngine.UI;

public class UITextBox : MonoBehaviour
{
	// (get) Token: 0x0600135B RID: 4955 RVA: 0x0005EEAC File Offset: 0x0005D0AC
	public float Height
	{
		get
		{
			return this.rectTransform.rect.height;
		}
	}

	// Token: 0x0600135C RID: 4956 RVA: 0x000105FD File Offset: 0x0000E7FD
	private void Awake()
	{
		this.rectTransform = base.GetComponent<RectTransform>();
	}

	// Token: 0x0600135D RID: 4957 RVA: 0x0005EECC File Offset: 0x0005D0CC
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

	// Token: 0x0600135E RID: 4958 RVA: 0x0001060B File Offset: 0x0000E80B
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
