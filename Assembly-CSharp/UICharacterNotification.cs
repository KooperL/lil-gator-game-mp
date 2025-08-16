using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UICharacterNotification : MonoBehaviour
{
	// Token: 0x060011FA RID: 4602 RVA: 0x00059CB8 File Offset: 0x00057EB8
	public void Load(CharacterProfile[] characters)
	{
		this.ResetThing();
		for (int i = 0; i < characters.Length; i++)
		{
			this.AddDisplay(characters[i]);
		}
		this.characters = characters;
		this.onLoad.Invoke();
	}

	// Token: 0x060011FB RID: 4603 RVA: 0x0000F45C File Offset: 0x0000D65C
	public void Load(CharacterProfile character)
	{
		this.ResetThing();
		this.AddDisplay(character);
		this.characters = null;
		this.onLoad.Invoke();
	}

	// Token: 0x060011FC RID: 4604 RVA: 0x00059CF4 File Offset: 0x00057EF4
	private void AddDisplay(CharacterProfile character)
	{
		UICharacterDisplay component = global::UnityEngine.Object.Instantiate<GameObject>(this.displayPrefab, this.displayParent).GetComponent<UICharacterDisplay>();
		component.Load(character);
		this.displays.Add(component);
	}

	// Token: 0x060011FD RID: 4605 RVA: 0x00059D2C File Offset: 0x00057F2C
	private void ResetThing()
	{
		this.hideBehavior.Show();
		if (this.displays.Count > 0)
		{
			foreach (UICharacterDisplay uicharacterDisplay in this.displays)
			{
				global::UnityEngine.Object.Destroy(uicharacterDisplay.gameObject);
			}
			this.displays = new List<UICharacterDisplay>();
		}
	}

	public GameObject displayPrefab;

	public Transform displayParent;

	private List<UICharacterDisplay> displays = new List<UICharacterDisplay>();

	public UIHideBehavior hideBehavior;

	private CharacterProfile[] characters;

	public UnityEvent onLoad = new UnityEvent();
}
