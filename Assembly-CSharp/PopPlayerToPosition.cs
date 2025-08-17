using System;
using UnityEngine;

public class PopPlayerToPosition : MonoBehaviour
{
	// Token: 0x06000D2A RID: 3370 RVA: 0x0000C2BC File Offset: 0x0000A4BC
	private void OnTriggerEnter(Collider other)
	{
		if (Player.movement.ragdollController.enabled)
		{
			Player.movement.ragdollController.Deactivate();
		}
		Player.rigidbody.position = this.targetPosition;
	}

	public Vector3 targetPosition;
}
