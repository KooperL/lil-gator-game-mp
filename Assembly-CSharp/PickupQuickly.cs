using System;
using UnityEngine;

public class PickupQuickly : MonoBehaviour
{
	// Token: 0x06000BE5 RID: 3045 RVA: 0x0000B1AE File Offset: 0x000093AE
	public void OnHit()
	{
		TriggerPickup.TriggerQuickPickup();
	}
}
