using System;
using UnityEngine;

public class PickupQuickly : MonoBehaviour
{
	// Token: 0x06000BE6 RID: 3046 RVA: 0x0000B1B8 File Offset: 0x000093B8
	public void OnHit()
	{
		TriggerPickup.TriggerQuickPickup();
	}
}
