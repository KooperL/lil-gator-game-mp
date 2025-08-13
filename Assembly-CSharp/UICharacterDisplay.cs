using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003DF RID: 991
public class UICharacterDisplay : MonoBehaviour
{
	// Token: 0x0600131B RID: 4891 RVA: 0x00010397 File Offset: 0x0000E597
	private void OnValidate()
	{
		if (this.rectTransform == null)
		{
			this.rectTransform = base.GetComponent<RectTransform>();
		}
	}

	// Token: 0x0600131C RID: 4892 RVA: 0x0005D544 File Offset: 0x0005B744
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

	// Token: 0x0600131D RID: 4893 RVA: 0x000103B3 File Offset: 0x0000E5B3
	public void ClearItem()
	{
		if (this.nameDisplay != null)
		{
			this.nameDisplay.text = "";
		}
	}

	// Token: 0x0600131E RID: 4894 RVA: 0x000103D3 File Offset: 0x0000E5D3
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

	// Token: 0x04001895 RID: 6293
	public RectTransform rectTransform;

	// Token: 0x04001896 RID: 6294
	public Text nameDisplay;

	// Token: 0x04001897 RID: 6295
	public bool coloredName = true;

	// Token: 0x04001898 RID: 6296
	[ConditionalHide("nameCharacterColor", true)]
	public CharacterProfile.CharacterColor nameColor;

	// Token: 0x04001899 RID: 6297
	[Space]
	public Image darkColor;

	// Token: 0x0400189A RID: 6298
	public Image midColor;

	// Token: 0x0400189B RID: 6299
	public Image brightColor;
}
