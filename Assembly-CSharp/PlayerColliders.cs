using System;
using UnityEngine;

public class PlayerColliders : MonoBehaviour
{
	// Token: 0x06000D25 RID: 3365 RVA: 0x0004A38C File Offset: 0x0004858C
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

	public Collider[] colliders;
}
