using System;
using UnityEngine;

public class ItemTest : ItemThrowable
{
	// Token: 0x06000BC8 RID: 3016 RVA: 0x00041380 File Offset: 0x0003F580
	public override void Throw(float charge, Vector3 direction)
	{
		Player.movement.Ragdoll(charge * this.speed * (direction + Vector3.up));
		if (this.springObject != null)
		{
			global::UnityEngine.Object.Destroy(this.springObject);
		}
		if (this.attachSpring)
		{
			Vector3 vector = base.transform.position + 5f * direction + 3f * Vector3.up;
			RaycastHit raycastHit;
			if (this.projectSpring && Physics.Raycast(MainCamera.t.position, direction, out raycastHit, this.maxProjectDistance, this.springProjectLayers))
			{
				vector = raycastHit.point;
			}
			this.springObject = global::UnityEngine.Object.Instantiate<GameObject>(this.springObjectPrefab, vector, Quaternion.identity);
			SpringJoint component = this.springObject.GetComponent<SpringJoint>();
			component.connectedBody = Player.itemManager.armRigidbody;
			component.maxDistance = Vector3.Distance(component.connectedBody.position, vector) - 5f;
		}
	}

	// Token: 0x06000BC9 RID: 3017 RVA: 0x0000AFFB File Offset: 0x000091FB
	public override void LateUpdate()
	{
		if (this.springObject != null && !Player.movement.isRagdolling)
		{
			global::UnityEngine.Object.Destroy(this.springObject);
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
