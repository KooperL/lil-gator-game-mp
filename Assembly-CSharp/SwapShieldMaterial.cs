using System;
using UnityEngine;

// Token: 0x020001DB RID: 475
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

	// Token: 0x04000C46 RID: 3142
	private Collider shieldCollider;

	// Token: 0x04000C47 RID: 3143
	public PhysicMaterial material;

	// Token: 0x04000C48 RID: 3144
	private PhysicMaterial oldMaterial;
}
