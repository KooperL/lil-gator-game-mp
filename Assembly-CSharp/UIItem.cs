using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003AD RID: 941
public class UIItem : MonoBehaviour
{
	// Token: 0x060011D7 RID: 4567 RVA: 0x00059214 File Offset: 0x00057414
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

	// Token: 0x060011D8 RID: 4568 RVA: 0x0000F38F File Offset: 0x0000D58F
	private void OnEnable()
	{
		if (UIItem.allItems == null)
		{
			UIItem.allItems = new List<UIItem>();
		}
		UIItem.allItems.Add(this);
	}

	// Token: 0x060011D9 RID: 4569 RVA: 0x0000F3AD File Offset: 0x0000D5AD
	private void OnDisable()
	{
		UIItem.allItems.Remove(this);
	}

	// Token: 0x060011DA RID: 4570 RVA: 0x0000F3BB File Offset: 0x0000D5BB
	private void Start()
	{
		this.Refresh();
	}

	// Token: 0x060011DB RID: 4571 RVA: 0x0000F3C3 File Offset: 0x0000D5C3
	public void Refresh()
	{
		this.image.enabled = ItemManager.i.IsItemUnlocked(this.itemName);
	}

	// Token: 0x04001710 RID: 5904
	public static List<UIItem> allItems;

	// Token: 0x04001711 RID: 5905
	public Image image;

	// Token: 0x04001712 RID: 5906
	public string itemName;
}
