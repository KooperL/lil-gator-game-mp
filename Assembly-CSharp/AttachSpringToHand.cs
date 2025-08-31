using System;
using UnityEngine;

public class AttachSpringToHand : MonoBehaviour
{
	// Token: 0x0600092C RID: 2348 RVA: 0x0002BB5F File Offset: 0x00029D5F
	private void Start()
	{
		if (this.springJoint == null)
		{
			this.springJoint = base.GetComponent<SpringJoint>();
		}
		this.springJoint.connectedBody = Player.itemManager.armRigidbody;
		Player.ragdollController.isAttached = true;
	}

	// Token: 0x0600092D RID: 2349 RVA: 0x0002BB9B File Offset: 0x00029D9B
	private void OnDisable()
	{
		Player.ragdollController.isAttached = false;
	}

	public SpringJoint springJoint;
}
