using System;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterDisplay : MonoBehaviour
{
	// Token: 0x06000FEE RID: 4078 RVA: 0x0004C163 File Offset: 0x0004A363
	private void OnValidate()
	{
		if (this.rectTransform == null)
		{
			this.rectTransform = base.GetComponent<RectTransform>();
		}
	}

	// Token: 0x06000FEF RID: 4079 RVA: 0x0004C180 File Offset: 0x0004A380
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

	// Token: 0x06000FF0 RID: 4080 RVA: 0x0004C242 File Offset: 0x0004A442
	public void ClearItem()
	{
		if (this.nameDisplay != null)
		{
			this.nameDisplay.text = "";
		}
	}

	// Token: 0x06000FF1 RID: 4081 RVA: 0x0004C262 File Offset: 0x0004A462
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
