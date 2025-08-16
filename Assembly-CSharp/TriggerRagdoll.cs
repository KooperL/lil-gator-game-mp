using System;
using UnityEngine;

public class TriggerRagdoll : MonoBehaviour
{
	// Token: 0x060010F1 RID: 4337 RVA: 0x0000E727 File Offset: 0x0000C927
	[ContextMenu("Ragdoll")]
	public void DoTheRagdoll()
	{
		Player.movement.Ragdoll(this.speed);
	}

	public float speed;
}
