using System;
using UnityEngine;

public class UnlockItem : MonoBehaviour
{
	// Token: 0x0600087B RID: 2171 RVA: 0x000085F3 File Offset: 0x000067F3
	public void Unlock()
	{
		ItemManager.i.UnlockItem(this.item.id);
	}

	public ItemObject item;
}
