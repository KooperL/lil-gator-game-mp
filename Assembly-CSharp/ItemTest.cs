using System;
using UnityEngine;

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

	public float speed = 10f;

	public bool attachSpring = true;

	public bool projectSpring;

	public GameObject springObjectPrefab;

	private GameObject springObject;

	public LayerMask springProjectLayers;

	public float maxProjectDistance = 40f;
}
