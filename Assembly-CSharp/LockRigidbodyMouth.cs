using System;
using UnityEngine;

public class LockRigidbodyMouth : MonoBehaviour
{
	// Token: 0x06000C0C RID: 3084 RVA: 0x0000B3A8 File Offset: 0x000095A8
	private void OnEnable()
	{
		Player.ragdollController.lockMouth = true;
		Player.ragdollController.lockMouthOpenness = this.mouthOpenness;
	}

	// Token: 0x06000C0D RID: 3085 RVA: 0x0000B3C5 File Offset: 0x000095C5
	private void OnDisable()
	{
		Player.ragdollController.lockMouth = false;
	}

	public float mouthOpenness;
}
