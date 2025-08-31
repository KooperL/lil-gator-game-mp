using System;
using UnityEngine;

public class LockRigidbodyMouth : MonoBehaviour
{
	// Token: 0x06000A0B RID: 2571 RVA: 0x0002E968 File Offset: 0x0002CB68
	private void OnEnable()
	{
		Player.ragdollController.lockMouth = true;
		Player.ragdollController.lockMouthOpenness = this.mouthOpenness;
	}

	// Token: 0x06000A0C RID: 2572 RVA: 0x0002E985 File Offset: 0x0002CB85
	private void OnDisable()
	{
		Player.ragdollController.lockMouth = false;
	}

	public float mouthOpenness;
}
