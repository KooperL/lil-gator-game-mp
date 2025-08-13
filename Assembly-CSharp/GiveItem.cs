using System;
using UnityEngine;

// Token: 0x02000191 RID: 401
public class GiveItem : MonoBehaviour
{
	// Token: 0x06000789 RID: 1929 RVA: 0x00007890 File Offset: 0x00005A90
	private void OnEnable()
	{
		ItemManager.i.GiveItem(this.item, this.equip);
	}

	// Token: 0x04000A0A RID: 2570
	public ItemObject item;

	// Token: 0x04000A0B RID: 2571
	public bool equip = true;
}
