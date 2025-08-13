using System;
using UnityEngine;

// Token: 0x02000262 RID: 610
public class SwapShieldMaterial : MonoBehaviour
{
	// Token: 0x06000B9B RID: 2971 RVA: 0x0000AEAD File Offset: 0x000090AD
	private void Start()
	{
		this.shieldCollider = Player.movement.sledCollider;
		this.oldMaterial = this.shieldCollider.sharedMaterial;
		this.shieldCollider.sharedMaterial = this.material;
	}

	// Token: 0x06000B9C RID: 2972 RVA: 0x0000AEE1 File Offset: 0x000090E1
	private void OnDestroy()
	{
		if (this.shieldCollider != null)
		{
			this.shieldCollider.sharedMaterial = this.oldMaterial;
		}
	}

	// Token: 0x04000E7E RID: 3710
	private Collider shieldCollider;

	// Token: 0x04000E7F RID: 3711
	public PhysicMaterial material;

	// Token: 0x04000E80 RID: 3712
	private PhysicMaterial oldMaterial;
}
