using System;
using UnityEngine;

public class AttachSpringToHand : MonoBehaviour
{
	// Token: 0x06000B0F RID: 2831 RVA: 0x0000A74B File Offset: 0x0000894B
	private void Start()
	{
		if (this.springJoint == null)
		{
			this.springJoint = base.GetComponent<SpringJoint>();
		}
		this.springJoint.connectedBody = Player.itemManager.armRigidbody;
		Player.ragdollController.isAttached = true;
	}

	// Token: 0x06000B10 RID: 2832 RVA: 0x0000A787 File Offset: 0x00008987
	private void OnDisable()
	{
		Player.ragdollController.isAttached = false;
	}

	public SpringJoint springJoint;
}
