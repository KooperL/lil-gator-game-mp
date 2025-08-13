using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002D2 RID: 722
public class UINameplate : MonoBehaviour
{
	// Token: 0x06000F40 RID: 3904 RVA: 0x000496A4 File Offset: 0x000478A4
	public static void UpdateNameplates(CharacterProfile changedCharacter)
	{
		foreach (UINameplate uinameplate in UINameplate.currentNameplates)
		{
			if (uinameplate.character == changedCharacter)
			{
				uinameplate.SetNameplate(changedCharacter);
			}
		}
	}

	// Token: 0x06000F41 RID: 3905 RVA: 0x00049704 File Offset: 0x00047904
	public void OnEnable()
	{
		UINameplate.currentNameplates.Add(this);
	}

	// Token: 0x06000F42 RID: 3906 RVA: 0x00049711 File Offset: 0x00047911
	public void OnDisable()
	{
		UINameplate.currentNameplates.Remove(this);
	}

	// Token: 0x06000F43 RID: 3907 RVA: 0x00049720 File Offset: 0x00047920
	public void SetNameplate(CharacterProfile character)
	{
		this.character = character;
		if (this.text != null && string.IsNullOrEmpty(character.Name))
		{
			base.gameObject.SetActive(false);
			return;
		}
		if (this.invisibleImage != null)
		{
			this.invisibleImage.sprite = character.nameplate;
		}
		if (this.visibleImage != null)
		{
			this.visibleImage.sprite = character.nameplate;
		}
		if (this.text != null)
		{
			this.text.text = character.Name;
		}
		if (this.backgroundColor != null)
		{
			this.backgroundColor.color = character.darkColor;
		}
		if (this.midColor != null)
		{
			this.midColor.color = character.midColor;
		}
		if (this.primaryColor != null)
		{
			this.primaryColor.color = character.brightColor;
		}
		base.gameObject.SetActive(true);
	}

	// Token: 0x06000F44 RID: 3908 RVA: 0x00049822 File Offset: 0x00047A22
	public void Clear()
	{
		this.character = null;
		base.gameObject.SetActive(false);
	}

	// Token: 0x0400140E RID: 5134
	public static List<UINameplate> currentNameplates = new List<UINameplate>();

	// Token: 0x0400140F RID: 5135
	[HideInInspector]
	public CharacterProfile character;

	// Token: 0x04001410 RID: 5136
	public Image invisibleImage;

	// Token: 0x04001411 RID: 5137
	public Image visibleImage;

	// Token: 0x04001412 RID: 5138
	public Text text;

	// Token: 0x04001413 RID: 5139
	public Image backgroundColor;

	// Token: 0x04001414 RID: 5140
	public Image midColor;

	// Token: 0x04001415 RID: 5141
	public Image primaryColor;
}
