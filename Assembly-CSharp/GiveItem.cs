using System;
using UnityEngine;

public class GiveItem : MonoBehaviour
{
	// Token: 0x060007C9 RID: 1993 RVA: 0x00007B8A File Offset: 0x00005D8A
	private void OnEnable()
	{
		ItemManager.i.GiveItem(this.item, this.equip);
	}

	public ItemObject item;

	public bool equip = true;
}
