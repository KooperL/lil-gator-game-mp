using System;
using UnityEngine;
using UnityEngine.Events;

public class ForkEventByItemCrafted : MonoBehaviour
{
	// Token: 0x060008D4 RID: 2260 RVA: 0x00008A3E File Offset: 0x00006C3E
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
		// Token: 0x060008D6 RID: 2262 RVA: 0x00008A7D File Offset: 0x00006C7D
		public void Execute()
		{
			this.onChoose.Invoke();
		}

		public UnityEvent onChoose;
	}
}
