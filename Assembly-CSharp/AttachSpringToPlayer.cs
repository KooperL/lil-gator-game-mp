using System;
using UnityEngine;

// Token: 0x020001B9 RID: 441
public class AttachSpringToPlayer : MonoBehaviour
{
	// Token: 0x0600092F RID: 2351 RVA: 0x0002BBB0 File Offset: 0x00029DB0
	private void Start()
	{
		if (this.handJoint != null)
		{
			this.handJoint.connectedBody = Player.itemManager.armRigidbody;
		}
		if (this.chestJoint != null)
		{
			this.chestJoint.connectedBody = Player.itemManager.chestRigidbody;
		}
		if (this.headJoint != null)
		{
			this.headJoint.connectedBody = Player.itemManager.headRigidbody;
		}
		Player.ragdollController.isAttached = true;
	}

	// Token: 0x06000930 RID: 2352 RVA: 0x0002BC31 File Offset: 0x00029E31
	private void OnDisable()
	{
		Player.ragdollController.isAttached = false;
	}

	// Token: 0x04000B88 RID: 2952
	public SpringJoint handJoint;

	// Token: 0x04000B89 RID: 2953
	public SpringJoint headJoint;

	// Token: 0x04000B8A RID: 2954
	public SpringJoint chestJoint;
}
