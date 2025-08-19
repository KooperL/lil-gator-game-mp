using System;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterDisplay : MonoBehaviour
{
	// Token: 0x0600137B RID: 4987 RVA: 0x0001079E File Offset: 0x0000E99E
	private void OnValidate()
	{
		if (this.rectTransform == null)
		{
			this.rectTransform = base.GetComponent<RectTransform>();
		}
	}

	// Token: 0x0600137C RID: 4988 RVA: 0x0005F548 File Offset: 0x0005D748
	public void Load(CharacterProfile profile)
	{
		if (!base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(true);
		}
		if (this.nameDisplay != null)
		{
			this.SetText(this.nameDisplay, profile.Name);
			if (this.coloredName)
			{
				this.nameDisplay.color = profile.GetColor(this.nameColor);
			}
		}
		if (this.darkColor != null)
		{
			this.darkColor.color = profile.darkColor;
		}
		if (this.midColor != null)
		{
			this.midColor.color = profile.midColor;
		}
		if (this.brightColor != null)
		{
			this.brightColor.color = profile.brightColor;
		}
	}

	// Token: 0x0600137D RID: 4989 RVA: 0x000107BA File Offset: 0x0000E9BA
	public void ClearItem()
	{
		if (this.nameDisplay != null)
		{
			this.nameDisplay.text = "";
		}
	}

	// Token: 0x0600137E RID: 4990 RVA: 0x000107DA File Offset: 0x0000E9DA
	private void SetText(Text textObject, string textString)
	{
		if (!string.IsNullOrEmpty(textString))
		{
			textObject.gameObject.SetActive(true);
			textObject.text = textString;
			return;
		}
		textObject.gameObject.SetActive(false);
	}

	public RectTransform rectTransform;

	public Text nameDisplay;

	public bool coloredName = true;

	[ConditionalHide("nameCharacterColor", true)]
	public CharacterProfile.CharacterColor nameColor;

	[Space]
	public Image darkColor;

	public Image midColor;

	public Image brightColor;
}
