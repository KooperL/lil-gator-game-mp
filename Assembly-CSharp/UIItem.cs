using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
	// Token: 0x06000EFF RID: 3839 RVA: 0x00047FE4 File Offset: 0x000461E4
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

	// Token: 0x06000F00 RID: 3840 RVA: 0x00048027 File Offset: 0x00046227
	private void OnEnable()
	{
		if (UIItem.allItems == null)
		{
			UIItem.allItems = new List<UIItem>();
		}
		UIItem.allItems.Add(this);
	}

	// Token: 0x06000F01 RID: 3841 RVA: 0x00048045 File Offset: 0x00046245
	private void OnDisable()
	{
		UIItem.allItems.Remove(this);
	}

	// Token: 0x06000F02 RID: 3842 RVA: 0x00048053 File Offset: 0x00046253
	private void Start()
	{
		this.Refresh();
	}

	// Token: 0x06000F03 RID: 3843 RVA: 0x0004805B File Offset: 0x0004625B
	public void Refresh()
	{
		this.image.enabled = ItemManager.i.IsItemUnlocked(this.itemName);
	}

	public static List<UIItem> allItems;

	public Image image;

	public string itemName;
}
