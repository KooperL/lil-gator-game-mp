using System;
using UnityEngine;

public class SwapShieldMaterial : MonoBehaviour
{
	// Token: 0x06000BE8 RID: 3048 RVA: 0x0000B1BF File Offset: 0x000093BF
	private void Start()
	{
		this.shieldCollider = Player.movement.sledCollider;
		this.oldMaterial = this.shieldCollider.sharedMaterial;
		this.shieldCollider.sharedMaterial = this.material;
	}

	// Token: 0x06000BE9 RID: 3049 RVA: 0x0000B1F3 File Offset: 0x000093F3
	private void OnDestroy()
	{
		if (this.shieldCollider != null)
		{
			this.shieldCollider.sharedMaterial = this.oldMaterial;
		}
	}

	private Collider shieldCollider;

	public PhysicMaterial material;

	private PhysicMaterial oldMaterial;
}
