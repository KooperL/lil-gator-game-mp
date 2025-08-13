using System;
using UnityEngine;

// Token: 0x02000291 RID: 657
public class PopPlayerToPosition : MonoBehaviour
{
	// Token: 0x06000CDE RID: 3294 RVA: 0x0000BFB4 File Offset: 0x0000A1B4
	private void OnTriggerEnter(Collider other)
	{
		if (Player.movement.ragdollController.enabled)
		{
			Player.movement.ragdollController.Deactivate();
		}
		Player.rigidbody.position = this.targetPosition;
	}

	// Token: 0x0400112B RID: 4395
	public Vector3 targetPosition;
}
