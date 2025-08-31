using System;
using UnityEngine;

public class TriggerRagdoll : MonoBehaviour
{
	// Token: 0x06000DD3 RID: 3539 RVA: 0x0004308F File Offset: 0x0004128F
	[ContextMenu("Ragdoll")]
	public void DoTheRagdoll()
	{
		Player.movement.Ragdoll(this.speed);
	}

	public float speed;
}
