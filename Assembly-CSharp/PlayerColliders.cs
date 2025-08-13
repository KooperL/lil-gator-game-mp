using System;
using UnityEngine;

// Token: 0x0200028E RID: 654
public class PlayerColliders : MonoBehaviour
{
	// Token: 0x06000CD8 RID: 3288 RVA: 0x0004853C File Offset: 0x0004673C
	private void Awake()
	{
		if (IgnorePlayerCollision.playerColliders == null || IgnorePlayerCollision.playerColliders.Length == 0)
		{
			IgnorePlayerCollision.playerColliders = this.colliders;
			return;
		}
		Collider[] array = new Collider[this.colliders.Length + IgnorePlayerCollision.playerColliders.Length];
		Array.Copy(this.colliders, array, this.colliders.Length);
		Array.Copy(IgnorePlayerCollision.playerColliders, 0, array, this.colliders.Length, IgnorePlayerCollision.playerColliders.Length);
		IgnorePlayerCollision.playerColliders = array;
	}

	// Token: 0x04001126 RID: 4390
	public Collider[] colliders;
}
