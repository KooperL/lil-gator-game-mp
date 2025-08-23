using System;
using UnityEngine;

public class LockRigidbodyMouth : MonoBehaviour
{
	// Token: 0x06000C0D RID: 3085 RVA: 0x0000B3C7 File Offset: 0x000095C7
	private void OnEnable()
	{
		Player.ragdollController.lockMouth = true;
		Player.ragdollController.lockMouthOpenness = this.mouthOpenness;
	}

	// Token: 0x06000C0E RID: 3086 RVA: 0x0000B3E4 File Offset: 0x000095E4
	private void OnDisable()
	{
		Player.ragdollController.lockMouth = false;
	}

	public float mouthOpenness;
}
