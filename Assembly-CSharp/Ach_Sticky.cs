using System;
using UnityEngine;

public class Ach_Sticky : MonoBehaviour
{
	// Token: 0x060001F9 RID: 505 RVA: 0x000039F9 File Offset: 0x00001BF9
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
