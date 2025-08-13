using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200015E RID: 350
public class ForkEventByItemCrafted : MonoBehaviour
{
	// Token: 0x06000748 RID: 1864 RVA: 0x00024514 File Offset: 0x00022714
	public void Fork()
	{
		if (this.item.IsUnlocked)
		{
			this.crafted.Execute();
			return;
		}
		if (this.item.IsShopUnlocked)
		{
			this.uncrafted.Execute();
			return;
		}
		this.unobtained.Execute();
	}

	// Token: 0x04000988 RID: 2440
	public ForkEventByItemCrafted.ChoiceEvent unobtained;

	// Token: 0x04000989 RID: 2441
	public ForkEventByItemCrafted.ChoiceEvent uncrafted;

	// Token: 0x0400098A RID: 2442
	public ForkEventByItemCrafted.ChoiceEvent crafted;

	// Token: 0x0400098B RID: 2443
	public ItemObject item;

	// Token: 0x020003C2 RID: 962
	[Serializable]
	public struct ChoiceEvent
	{
		// Token: 0x0600197E RID: 6526 RVA: 0x0006D024 File Offset: 0x0006B224
		public void Execute()
		{
			this.onChoose.Invoke();
		}

		// Token: 0x04001BBD RID: 7101
		public UnityEvent onChoose;
	}
}
