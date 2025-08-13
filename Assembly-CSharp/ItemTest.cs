using System;
using UnityEngine;

// Token: 0x0200025E RID: 606
public class ItemTest : ItemThrowable
{
	// Token: 0x06000B7C RID: 2940 RVA: 0x0003F98C File Offset: 0x0003DB8C
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
			if (this.projectSpring && Physics.Raycast(MainCamera.t.position, direction, ref raycastHit, this.maxProjectDistance, this.springProjectLayers))
			{
				vector = raycastHit.point;
			}
			this.springObject = Object.Instantiate<GameObject>(this.springObjectPrefab, vector, Quaternion.identity);
			SpringJoint component = this.springObject.GetComponent<SpringJoint>();
			component.connectedBody = Player.itemManager.armRigidbody;
			component.maxDistance = Vector3.Distance(component.connectedBody.position, vector) - 5f;
		}
	}

	// Token: 0x06000B7D RID: 2941 RVA: 0x0000AD08 File Offset: 0x00008F08
	public override void LateUpdate()
	{
		if (this.springObject != null && !Player.movement.isRagdolling)
		{
			Object.Destroy(this.springObject);
		}
		base.LateUpdate();
	}

	// Token: 0x04000E56 RID: 3670
	public float speed = 10f;

	// Token: 0x04000E57 RID: 3671
	public bool attachSpring = true;

	// Token: 0x04000E58 RID: 3672
	public bool projectSpring;

	// Token: 0x04000E59 RID: 3673
	public GameObject springObjectPrefab;

	// Token: 0x04000E5A RID: 3674
	private GameObject springObject;

	// Token: 0x04000E5B RID: 3675
	public LayerMask springProjectLayers;

	// Token: 0x04000E5C RID: 3676
	public float maxProjectDistance = 40f;
}
