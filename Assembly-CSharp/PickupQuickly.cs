using System;
using UnityEngine;

// Token: 0x02000261 RID: 609
public class PickupQuickly : MonoBehaviour
{
	// Token: 0x06000B99 RID: 2969 RVA: 0x0000AEA6 File Offset: 0x000090A6
	public void OnHit()
	{
		TriggerPickup.TriggerQuickPickup();
	}
}
