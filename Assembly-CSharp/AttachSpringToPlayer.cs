using System;
using UnityEngine;

public class AttachSpringToPlayer : MonoBehaviour
{
	// Token: 0x06000B12 RID: 2834 RVA: 0x0003F858 File Offset: 0x0003DA58
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

	// Token: 0x06000B13 RID: 2835 RVA: 0x0000A772 File Offset: 0x00008972
	private void OnDisable()
	{
		Player.ragdollController.isAttached = false;
	}

	public SpringJoint handJoint;

	public SpringJoint headJoint;

	public SpringJoint chestJoint;
}
