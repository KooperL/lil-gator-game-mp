using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003BB RID: 955
public class UINameplate : MonoBehaviour
{
	// Token: 0x06001242 RID: 4674 RVA: 0x0005AA04 File Offset: 0x00058C04
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

	// Token: 0x06001243 RID: 4675 RVA: 0x0000F778 File Offset: 0x0000D978
	public void OnEnable()
	{
		UINameplate.currentNameplates.Add(this);
	}

	// Token: 0x06001244 RID: 4676 RVA: 0x0000F785 File Offset: 0x0000D985
	public void OnDisable()
	{
		UINameplate.currentNameplates.Remove(this);
	}

	// Token: 0x06001245 RID: 4677 RVA: 0x0005AA64 File Offset: 0x00058C64
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

	// Token: 0x06001246 RID: 4678 RVA: 0x0000F793 File Offset: 0x0000D993
	public void Clear()
	{
		this.character = null;
		base.gameObject.SetActive(false);
	}

	// Token: 0x040017A0 RID: 6048
	public static List<UINameplate> currentNameplates = new List<UINameplate>();

	// Token: 0x040017A1 RID: 6049
	[HideInInspector]
	public CharacterProfile character;

	// Token: 0x040017A2 RID: 6050
	public Image invisibleImage;

	// Token: 0x040017A3 RID: 6051
	public Image visibleImage;

	// Token: 0x040017A4 RID: 6052
	public Text text;

	// Token: 0x040017A5 RID: 6053
	public Image backgroundColor;

	// Token: 0x040017A6 RID: 6054
	public Image midColor;

	// Token: 0x040017A7 RID: 6055
	public Image primaryColor;
}
