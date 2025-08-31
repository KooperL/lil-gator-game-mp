using System;
using UnityEngine;

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

	private bool hasThisAlreadyStuck;
}
