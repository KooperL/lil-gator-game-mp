using System;
using UnityEngine;

// Token: 0x020001CE RID: 462
public class ItemRock : ItemThrowable
{
	// Token: 0x06000995 RID: 2453 RVA: 0x0002D004 File Offset: 0x0002B204
	public override float GetSolveSpeed(float charge = 1f)
	{
		return this.maxThrowSpeed;
	}

	// Token: 0x06000996 RID: 2454 RVA: 0x0002D00C File Offset: 0x0002B20C
	public override void Throw(float charge, Vector3 direction)
	{
		Rigidbody component = Object.Instantiate<GameObject>(this.thrownPrefab, Player.itemManager.thrownSpawnPoint.position, base.transform.rotation).GetComponent<Rigidbody>();
		Vector3 vector = Mathf.Lerp(this.minThrowSpeed, this.maxThrowSpeed, charge) * direction;
		component.velocity = vector;
		component.AddRelativeTorque(this.thrownAngularVelocity, ForceMode.VelocityChange);
		base.Throw(charge, direction);
	}

	// Token: 0x06000997 RID: 2455 RVA: 0x0002D078 File Offset: 0x0002B278
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

	// Token: 0x06000998 RID: 2456 RVA: 0x0002D125 File Offset: 0x0002B325
	public override void OnRemove()
	{
		(this.isOnRight ? Player.itemManager.hipSatchel_r : Player.itemManager.hipSatchel).SetActive(false);
		base.OnRemove();
	}

	// Token: 0x04000BEE RID: 3054
	public Vector3 heldPosition;

	// Token: 0x04000BEF RID: 3055
	public Quaternion heldRotation;

	// Token: 0x04000BF0 RID: 3056
	public Renderer heldRock;

	// Token: 0x04000BF1 RID: 3057
	public GameObject thrownPrefab;

	// Token: 0x04000BF2 RID: 3058
	public float maxAngleVariance = 10f;

	// Token: 0x04000BF3 RID: 3059
	public float minThrowSpeed = 5f;

	// Token: 0x04000BF4 RID: 3060
	public float maxThrowSpeed = 15f;

	// Token: 0x04000BF5 RID: 3061
	public Vector3 thrownAngularVelocity;
}
