using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
	// Token: 0x06001237 RID: 4663 RVA: 0x0005B1B4 File Offset: 0x000593B4
	public static void RefreshAll()
	{
		if (UIItem.allItems == null)
		{
			UIItem.allItems = new List<UIItem>();
		}
		for (int i = 0; i < UIItem.allItems.Count; i++)
		{
			UIItem.allItems[i].Refresh();
		}
	}

	// Token: 0x06001238 RID: 4664 RVA: 0x0000F782 File Offset: 0x0000D982
	private void OnEnable()
	{
		if (UIItem.allItems == null)
		{
			UIItem.allItems = new List<UIItem>();
		}
		UIItem.allItems.Add(this);
	}

	// Token: 0x06001239 RID: 4665 RVA: 0x0000F7A0 File Offset: 0x0000D9A0
	private void OnDisable()
	{
		UIItem.allItems.Remove(this);
	}

	// Token: 0x0600123A RID: 4666 RVA: 0x0000F7AE File Offset: 0x0000D9AE
	private void Start()
	{
		this.Refresh();
	}

	// Token: 0x0600123B RID: 4667 RVA: 0x0000F7B6 File Offset: 0x0000D9B6
	public void Refresh()
	{
		this.image.enabled = ItemManager.i.IsItemUnlocked(this.itemName);
	}

	public static List<UIItem> allItems;

	public Image image;

	public string itemName;
}
