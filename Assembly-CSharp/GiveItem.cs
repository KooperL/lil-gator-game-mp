using System;
using UnityEngine;

public class GiveItem : MonoBehaviour
{
	// Token: 0x06000664 RID: 1636 RVA: 0x000210F6 File Offset: 0x0001F2F6
	private void OnEnable()
	{
		ItemManager.i.GiveItem(this.item, this.equip);
	}

	public ItemObject item;

	public bool equip = true;
}
