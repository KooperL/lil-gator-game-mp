using System;
using UnityEngine;

// Token: 0x02000267 RID: 615
public class LockRigidbodyMouth : MonoBehaviour
{
	// Token: 0x06000BC0 RID: 3008 RVA: 0x0000B0B5 File Offset: 0x000092B5
	private void OnEnable()
	{
		Player.ragdollController.lockMouth = true;
		Player.ragdollController.lockMouthOpenness = this.mouthOpenness;
	}

	// Token: 0x06000BC1 RID: 3009 RVA: 0x0000B0D2 File Offset: 0x000092D2
	private void OnDisable()
	{
		Player.ragdollController.lockMouth = false;
	}

	// Token: 0x04000EA0 RID: 3744
	public float mouthOpenness;
}
