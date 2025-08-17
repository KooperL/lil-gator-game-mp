using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINameplate : MonoBehaviour
{
	// Token: 0x060012A2 RID: 4770 RVA: 0x0005C9B4 File Offset: 0x0005ABB4
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

	// Token: 0x060012A3 RID: 4771 RVA: 0x0000FB6E File Offset: 0x0000DD6E
	public void OnEnable()
	{
		UINameplate.currentNameplates.Add(this);
	}

	// Token: 0x060012A4 RID: 4772 RVA: 0x0000FB7B File Offset: 0x0000DD7B
	public void OnDisable()
	{
		UINameplate.currentNameplates.Remove(this);
	}

	// Token: 0x060012A5 RID: 4773 RVA: 0x0005CA14 File Offset: 0x0005AC14
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

	// Token: 0x060012A6 RID: 4774 RVA: 0x0000FB89 File Offset: 0x0000DD89
	public void Clear()
	{
		this.character = null;
		base.gameObject.SetActive(false);
	}

	public static List<UINameplate> currentNameplates = new List<UINameplate>();

	[HideInInspector]
	public CharacterProfile character;

	public Image invisibleImage;

	public Image visibleImage;

	public Text text;

	public Image backgroundColor;

	public Image midColor;

	public Image primaryColor;
}
