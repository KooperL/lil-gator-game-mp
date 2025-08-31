using System;
using UnityEngine;

public class PickupQuickly : MonoBehaviour
{
	// Token: 0x060009F0 RID: 2544 RVA: 0x0002E4E6 File Offset: 0x0002C6E6
	public void OnHit()
	{
		TriggerPickup.TriggerQuickPickup();
	}
}
