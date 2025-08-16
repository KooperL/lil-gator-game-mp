using System;
using UnityEngine;

public class PickupQuickly : MonoBehaviour
{
	// Token: 0x06000BE5 RID: 3045 RVA: 0x0000B199 File Offset: 0x00009399
	public void OnHit()
	{
		TriggerPickup.TriggerQuickPickup();
	}
}
