using System;
using UnityEngine;

public class SwapShieldMaterial : MonoBehaviour
{
	// Token: 0x060009F2 RID: 2546 RVA: 0x0002E4F5 File Offset: 0x0002C6F5
	private void Start()
	{
		this.shieldCollider = Player.movement.sledCollider;
		this.oldMaterial = this.shieldCollider.sharedMaterial;
		this.shieldCollider.sharedMaterial = this.material;
	}

	// Token: 0x060009F3 RID: 2547 RVA: 0x0002E529 File Offset: 0x0002C729
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
