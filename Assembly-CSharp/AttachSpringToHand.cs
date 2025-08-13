using System;
using UnityEngine;

// Token: 0x02000237 RID: 567
public class AttachSpringToHand : MonoBehaviour
{
	// Token: 0x06000AC3 RID: 2755 RVA: 0x0000A417 File Offset: 0x00008617
	private void Start()
	{
		if (this.springJoint == null)
		{
			this.springJoint = base.GetComponent<SpringJoint>();
		}
		this.springJoint.connectedBody = Player.itemManager.armRigidbody;
		Player.ragdollController.isAttached = true;
	}

	// Token: 0x06000AC4 RID: 2756 RVA: 0x0000A453 File Offset: 0x00008653
	private void OnDisable()
	{
		Player.ragdollController.isAttached = false;
	}

	// Token: 0x04000D9B RID: 3483
	public SpringJoint springJoint;
}
