using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020001C9 RID: 457
public class ForkEventByItemCrafted : MonoBehaviour
{
	// Token: 0x06000893 RID: 2195 RVA: 0x00008717 File Offset: 0x00006917
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
		}
	}

	// Token: 0x04000B29 RID: 2857
	public ForkEventByItemCrafted.ChoiceEvent uncrafted;

	// Token: 0x04000B2A RID: 2858
	public ForkEventByItemCrafted.ChoiceEvent crafted;

	// Token: 0x04000B2B RID: 2859
	public ItemObject item;

	// Token: 0x020001CA RID: 458
	[Serializable]
	public struct ChoiceEvent
	{
		// Token: 0x06000895 RID: 2197 RVA: 0x0000874A File Offset: 0x0000694A
		public void Execute()
		{
			this.onChoose.Invoke();
		}

		// Token: 0x04000B2C RID: 2860
		public UnityEvent onChoose;
	}
}
