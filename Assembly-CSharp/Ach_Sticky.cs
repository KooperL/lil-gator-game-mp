using System;
using UnityEngine;

// Token: 0x0200008F RID: 143
public class Ach_Sticky : MonoBehaviour
{
	// Token: 0x060001EC RID: 492 RVA: 0x0000390D File Offset: 0x00001B0D
	public void OnStick()
	{
		if (this.hasThisAlreadyStuck)
		{
			return;
		}
		this.hasThisAlreadyStuck = true;
		Ach_StickyComboTracker.Stick();
	}

	// Token: 0x040002DC RID: 732
	private bool hasThisAlreadyStuck;
}
