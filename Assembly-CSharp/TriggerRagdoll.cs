using System;
using UnityEngine;

// Token: 0x02000288 RID: 648
public class TriggerRagdoll : MonoBehaviour
{
	// Token: 0x06000DD3 RID: 3539 RVA: 0x0004308F File Offset: 0x0004128F
	[ContextMenu("Ragdoll")]
	public void DoTheRagdoll()
	{
		Player.movement.Ragdoll(this.speed);
	}

	// Token: 0x0400123E RID: 4670
	public float speed;
}
