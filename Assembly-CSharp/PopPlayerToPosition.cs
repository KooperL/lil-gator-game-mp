using System;
using UnityEngine;

// Token: 0x020001FF RID: 511
public class PopPlayerToPosition : MonoBehaviour
{
	// Token: 0x06000B19 RID: 2841 RVA: 0x000376C2 File Offset: 0x000358C2
	private void OnTriggerEnter(Collider other)
	{
		if (Player.movement.ragdollController.enabled)
		{
			Player.movement.ragdollController.Deactivate();
		}
		Player.rigidbody.position = this.targetPosition;
	}

	// Token: 0x04000ECC RID: 3788
	public Vector3 targetPosition;
}
