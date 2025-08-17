using System;
using UnityEngine;
using UnityEngine.Events;

public class ForkEventByItemCrafted : MonoBehaviour
{
	// Token: 0x060008D3 RID: 2259 RVA: 0x00008A34 File Offset: 0x00006C34
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
		// Token: 0x060008D5 RID: 2261 RVA: 0x00008A73 File Offset: 0x00006C73
		public void Execute()
		{
			this.onChoose.Invoke();
		}

		public UnityEvent onChoose;
	}
}
