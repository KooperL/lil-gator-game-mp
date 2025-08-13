using System;
using UnityEngine;

// Token: 0x02000251 RID: 593
public class ItemRock : ItemThrowable
{
	// Token: 0x06000B2C RID: 2860 RVA: 0x0000A929 File Offset: 0x00008B29
	public override float GetSolveSpeed(float charge = 1f)
	{
		return this.maxThrowSpeed;
	}

	// Token: 0x06000B2D RID: 2861 RVA: 0x0003EDCC File Offset: 0x0003CFCC
	public override void Throw(float charge, Vector3 direction)
	{
		Rigidbody component = Object.Instantiate<GameObject>(this.thrownPrefab, Player.itemManager.thrownSpawnPoint.position, base.transform.rotation).GetComponent<Rigidbody>();
		Vector3 vector = Mathf.Lerp(this.minThrowSpeed, this.maxThrowSpeed, charge) * direction;
		component.velocity = vector;
		component.AddRelativeTorque(this.thrownAngularVelocity, 2);
		base.Throw(charge, direction);
	}

	// Token: 0x06000B2E RID: 2862 RVA: 0x0003EE38 File Offset: 0x0003D038
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

	// Token: 0x06000B2F RID: 2863 RVA: 0x0000A931 File Offset: 0x00008B31
	public override void OnRemove()
	{
		(this.isOnRight ? Player.itemManager.hipSatchel_r : Player.itemManager.hipSatchel).SetActive(false);
		base.OnRemove();
	}

	// Token: 0x04000E14 RID: 3604
	public Vector3 heldPosition;

	// Token: 0x04000E15 RID: 3605
	public Quaternion heldRotation;

	// Token: 0x04000E16 RID: 3606
	public Renderer heldRock;

	// Token: 0x04000E17 RID: 3607
	public GameObject thrownPrefab;

	// Token: 0x04000E18 RID: 3608
	public float maxAngleVariance = 10f;

	// Token: 0x04000E19 RID: 3609
	public float minThrowSpeed = 5f;

	// Token: 0x04000E1A RID: 3610
	public float maxThrowSpeed = 15f;

	// Token: 0x04000E1B RID: 3611
	public Vector3 thrownAngularVelocity;
}
