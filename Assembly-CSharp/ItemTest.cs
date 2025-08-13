using System;
using UnityEngine;

// Token: 0x020001D7 RID: 471
public class ItemTest : ItemThrowable
{
	// Token: 0x060009D3 RID: 2515 RVA: 0x0002DB04 File Offset: 0x0002BD04
	public override void Throw(float charge, Vector3 direction)
	{
		Player.movement.Ragdoll(charge * this.speed * (direction + Vector3.up));
		if (this.springObject != null)
		{
			Object.Destroy(this.springObject);
		}
		if (this.attachSpring)
		{
			Vector3 vector = base.transform.position + 5f * direction + 3f * Vector3.up;
			RaycastHit raycastHit;
			if (this.projectSpring && Physics.Raycast(MainCamera.t.position, direction, out raycastHit, this.maxProjectDistance, this.springProjectLayers))
			{
				vector = raycastHit.point;
			}
			this.springObject = Object.Instantiate<GameObject>(this.springObjectPrefab, vector, Quaternion.identity);
			SpringJoint component = this.springObject.GetComponent<SpringJoint>();
			component.connectedBody = Player.itemManager.armRigidbody;
			component.maxDistance = Vector3.Distance(component.connectedBody.position, vector) - 5f;
		}
	}

	// Token: 0x060009D4 RID: 2516 RVA: 0x0002DC0A File Offset: 0x0002BE0A
	public override void LateUpdate()
	{
		if (this.springObject != null && !Player.movement.isRagdolling)
		{
			Object.Destroy(this.springObject);
		}
		base.LateUpdate();
	}

	// Token: 0x04000C1E RID: 3102
	public float speed = 10f;

	// Token: 0x04000C1F RID: 3103
	public bool attachSpring = true;

	// Token: 0x04000C20 RID: 3104
	public bool projectSpring;

	// Token: 0x04000C21 RID: 3105
	public GameObject springObjectPrefab;

	// Token: 0x04000C22 RID: 3106
	private GameObject springObject;

	// Token: 0x04000C23 RID: 3107
	public LayerMask springProjectLayers;

	// Token: 0x04000C24 RID: 3108
	public float maxProjectDistance = 40f;
}
