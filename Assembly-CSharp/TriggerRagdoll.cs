using System;
using UnityEngine;

// Token: 0x02000361 RID: 865
public class TriggerRagdoll : MonoBehaviour
{
	// Token: 0x06001096 RID: 4246 RVA: 0x0000E3D3 File Offset: 0x0000C5D3
	[ContextMenu("Ragdoll")]
	public void DoTheRagdoll()
	{
		Player.movement.Ragdoll(this.speed);
	}

	// Token: 0x0400159E RID: 5534
	public float speed;
}
