using System;
using UnityEngine;

public class AttachSpringToHand : MonoBehaviour
{
	// Token: 0x06000B0F RID: 2831 RVA: 0x0000A736 File Offset: 0x00008936
	private void Start()
	{
		if (this.springJoint == null)
		{
			this.springJoint = base.GetComponent<SpringJoint>();
		}
		this.springJoint.connectedBody = Player.itemManager.armRigidbody;
		Player.ragdollController.isAttached = true;
	}

	// Token: 0x06000B10 RID: 2832 RVA: 0x0000A772 File Offset: 0x00008972
	private void OnDisable()
	{
		Player.ragdollController.isAttached = false;
	}

	public SpringJoint springJoint;
}
