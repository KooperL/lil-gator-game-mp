using System;
using UnityEngine;

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

	public Vector3 targetPosition;
}
