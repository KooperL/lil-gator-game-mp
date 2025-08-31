using System;
using UnityEngine;
using UnityEngine.Events;

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

	public ForkEventByItemCrafted.ChoiceEvent unobtained;

	public ForkEventByItemCrafted.ChoiceEvent uncrafted;

	public ForkEventByItemCrafted.ChoiceEvent crafted;

	public ItemObject item;

	[Serializable]
	public struct ChoiceEvent
	{
		// Token: 0x0600197E RID: 6526 RVA: 0x0006D024 File Offset: 0x0006B224
		public void Execute()
		{
			this.onChoose.Invoke();
		}

		public UnityEvent onChoose;
	}
}
