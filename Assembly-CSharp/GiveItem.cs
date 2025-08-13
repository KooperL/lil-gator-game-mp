using System;
using UnityEngine;

// Token: 0x02000135 RID: 309
public class GiveItem : MonoBehaviour
{
	// Token: 0x06000664 RID: 1636 RVA: 0x000210F6 File Offset: 0x0001F2F6
	private void OnEnable()
	{
		ItemManager.i.GiveItem(this.item, this.equip);
	}

	// Token: 0x0400089E RID: 2206
	public ItemObject item;

	// Token: 0x0400089F RID: 2207
	public bool equip = true;
}
