using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
