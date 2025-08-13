using System;
using UnityEngine;

// Token: 0x02000238 RID: 568
public class AttachSpringToPlayer : MonoBehaviour
{
	// Token: 0x06000AC6 RID: 2758 RVA: 0x0003DEE4 File Offset: 0x0003C0E4
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

	// Token: 0x06000AC7 RID: 2759 RVA: 0x0000A453 File Offset: 0x00008653
	private void OnDisable()
	{
		Player.ragdollController.isAttached = false;
	}

	// Token: 0x04000D9C RID: 3484
	public SpringJoint handJoint;

	// Token: 0x04000D9D RID: 3485
	public SpringJoint headJoint;

	// Token: 0x04000D9E RID: 3486
	public SpringJoint chestJoint;
}
