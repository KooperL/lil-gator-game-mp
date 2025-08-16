using System;
using UnityEngine;

public class ItemRock : ItemThrowable
{
	// Token: 0x06000B78 RID: 2936 RVA: 0x0000AC48 File Offset: 0x00008E48
	public override float GetSolveSpeed(float charge = 1f)
	{
		return this.maxThrowSpeed;
	}

	// Token: 0x06000B79 RID: 2937 RVA: 0x00040740 File Offset: 0x0003E940
	public override void Throw(float charge, Vector3 direction)
	{
		Rigidbody component = global::UnityEngine.Object.Instantiate<GameObject>(this.thrownPrefab, Player.itemManager.thrownSpawnPoint.position, base.transform.rotation).GetComponent<Rigidbody>();
		Vector3 vector = Mathf.Lerp(this.minThrowSpeed, this.maxThrowSpeed, charge) * direction;
		component.velocity = vector;
		component.AddRelativeTorque(this.thrownAngularVelocity, ForceMode.VelocityChange);
		base.Throw(charge, direction);
	}

	// Token: 0x06000B7A RID: 2938 RVA: 0x000407AC File Offset: 0x0003E9AC
	public override void SetEquipped(bool isEquipped)
	{
		(this.isOnRight ? Player.itemManager.hipSatchel_r : Player.itemManager.hipSatchel).SetActive(true);
		Transform transform = (isEquipped ? Player.itemManager.leftHandAnchor : (this.isOnRight ? Player.itemManager.satchelAnchor_r : Player.itemManager.satchelAnchor));
		if (base.transform.parent != transform)
		{
			base.transform.ApplyParent(transform);
		}
		if (isEquipped)
		{
			base.transform.localPosition = this.heldPosition;
			base.transform.localRotation = this.heldRotation;
		}
		if (!isEquipped)
		{
			this.Cancel();
		}
	}

	// Token: 0x06000B7B RID: 2939 RVA: 0x0000AC50 File Offset: 0x00008E50
	public override void OnRemove()
	{
		(this.isOnRight ? Player.itemManager.hipSatchel_r : Player.itemManager.hipSatchel).SetActive(false);
		base.OnRemove();
	}

	public Vector3 heldPosition;

	public Quaternion heldRotation;

	public Renderer heldRock;

	public GameObject thrownPrefab;

	public float maxAngleVariance = 10f;

	public float minThrowSpeed = 5f;

	public float maxThrowSpeed = 15f;

	public Vector3 thrownAngularVelocity;
}
