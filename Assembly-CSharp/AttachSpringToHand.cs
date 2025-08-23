using System;
using UnityEngine;

public class AttachSpringToHand : MonoBehaviour
{
	// Token: 0x06000B10 RID: 2832 RVA: 0x0000A755 File Offset: 0x00008955
	private void Start()
	{
		if (this.springJoint == null)
		{
			this.springJoint = base.GetComponent<SpringJoint>();
		}
		this.springJoint.connectedBody = Player.itemManager.armRigidbody;
		Player.ragdollController.isAttached = true;
	}

	// Token: 0x06000B11 RID: 2833 RVA: 0x0000A791 File Offset: 0x00008991
	private void OnDisable()
	{
		Player.ragdollController.isAttached = false;
	}

	public SpringJoint springJoint;
}
