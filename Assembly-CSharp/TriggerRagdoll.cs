using System;
using UnityEngine;

public class TriggerRagdoll : MonoBehaviour
{
	// Token: 0x060010F1 RID: 4337 RVA: 0x0000E73C File Offset: 0x0000C93C
	[ContextMenu("Ragdoll")]
	public void DoTheRagdoll()
	{
		Player.movement.Ragdoll(this.speed);
	}

	public float speed;
}
