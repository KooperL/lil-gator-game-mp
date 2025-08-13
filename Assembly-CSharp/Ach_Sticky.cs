using System;
using UnityEngine;

// Token: 0x0200006F RID: 111
public class Ach_Sticky : MonoBehaviour
{
	// Token: 0x060001B6 RID: 438 RVA: 0x00009AA7 File Offset: 0x00007CA7
	public void OnStick()
	{
		if (this.hasThisAlreadyStuck)
		{
			return;
		}
		this.hasThisAlreadyStuck = true;
		Ach_StickyComboTracker.Stick();
	}

	// Token: 0x04000258 RID: 600
	private bool hasThisAlreadyStuck;
}
