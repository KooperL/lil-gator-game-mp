using System;
using UnityEngine;

public class SwapShieldMaterial : MonoBehaviour
{
	// Token: 0x06000BE7 RID: 3047 RVA: 0x0000B1B5 File Offset: 0x000093B5
	private void Start()
	{
		this.shieldCollider = Player.movement.sledCollider;
		this.oldMaterial = this.shieldCollider.sharedMaterial;
		this.shieldCollider.sharedMaterial = this.material;
	}

	// Token: 0x06000BE8 RID: 3048 RVA: 0x0000B1E9 File Offset: 0x000093E9
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
