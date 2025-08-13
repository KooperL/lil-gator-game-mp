using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020003A0 RID: 928
public class UICharacterNotification : MonoBehaviour
{
	// Token: 0x0600119A RID: 4506 RVA: 0x00057E88 File Offset: 0x00056088
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

	// Token: 0x0600119B RID: 4507 RVA: 0x0000F088 File Offset: 0x0000D288
	public void Load(CharacterProfile character)
	{
		this.ResetThing();
		this.AddDisplay(character);
		this.characters = null;
		this.onLoad.Invoke();
	}

	// Token: 0x0600119C RID: 4508 RVA: 0x00057EC4 File Offset: 0x000560C4
	private void AddDisplay(CharacterProfile character)
	{
		UICharacterDisplay component = Object.Instantiate<GameObject>(this.displayPrefab, this.displayParent).GetComponent<UICharacterDisplay>();
		component.Load(character);
		this.displays.Add(component);
	}

	// Token: 0x0600119D RID: 4509 RVA: 0x00057EFC File Offset: 0x000560FC
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

	// Token: 0x040016AE RID: 5806
	public GameObject displayPrefab;

	// Token: 0x040016AF RID: 5807
	public Transform displayParent;

	// Token: 0x040016B0 RID: 5808
	private List<UICharacterDisplay> displays = new List<UICharacterDisplay>();

	// Token: 0x040016B1 RID: 5809
	public UIHideBehavior hideBehavior;

	// Token: 0x040016B2 RID: 5810
	private CharacterProfile[] characters;

	// Token: 0x040016B3 RID: 5811
	public UnityEvent onLoad = new UnityEvent();
}
