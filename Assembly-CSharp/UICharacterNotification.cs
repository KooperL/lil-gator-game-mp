using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UICharacterNotification : MonoBehaviour
{
	// Token: 0x06000EC2 RID: 3778 RVA: 0x00046900 File Offset: 0x00044B00
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

	// Token: 0x06000EC3 RID: 3779 RVA: 0x0004693C File Offset: 0x00044B3C
	public void Load(CharacterProfile character)
	{
		this.ResetThing();
		this.AddDisplay(character);
		this.characters = null;
		this.onLoad.Invoke();
	}

	// Token: 0x06000EC4 RID: 3780 RVA: 0x00046960 File Offset: 0x00044B60
	private void AddDisplay(CharacterProfile character)
	{
		UICharacterDisplay component = Object.Instantiate<GameObject>(this.displayPrefab, this.displayParent).GetComponent<UICharacterDisplay>();
		component.Load(character);
		this.displays.Add(component);
	}

	// Token: 0x06000EC5 RID: 3781 RVA: 0x00046998 File Offset: 0x00044B98
	private void ResetThing()
	{
		this.hideBehavior.Show();
		if (this.displays.Count > 0)
		{
			foreach (UICharacterDisplay uicharacterDisplay in this.displays)
			{
				Object.Destroy(uicharacterDisplay.gameObject);
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
