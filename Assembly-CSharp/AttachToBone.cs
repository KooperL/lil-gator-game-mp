using System;
using UnityEngine;

// Token: 0x02000239 RID: 569
public class AttachToBone : MonoBehaviour
{
	// Token: 0x06000AC9 RID: 2761 RVA: 0x0003DF68 File Offset: 0x0003C168
	private void Start()
	{
		if (this.useInitialTransform)
		{
			this.localPosition = base.transform.localPosition;
			this.localRotation = base.transform.localRotation;
		}
		switch (this.attachedBone)
		{
		case AttachToBone.PlayerBone.Head:
			this.attachedTransform = Player.itemManager.headRigidbody.transform;
			return;
		case AttachToBone.PlayerBone.Hand:
			this.attachedTransform = Player.itemManager.leftHandAnchor;
			return;
		case AttachToBone.PlayerBone.Chest:
			this.attachedTransform = Player.itemManager.chestAnchor;
			return;
		default:
			return;
		}
	}

	// Token: 0x06000ACA RID: 2762 RVA: 0x0003DFF0 File Offset: 0x0003C1F0
	private Vector3 GetDirection(AttachToBone.DirectionSource source, Vector3 direction)
	{
		switch (source)
		{
		case AttachToBone.DirectionSource.LocalDirection:
			return this.attachedTransform.TransformDirection(direction);
		case AttachToBone.DirectionSource.TargetDirection:
			return this.target.TransformDirection(direction);
		case AttachToBone.DirectionSource.Target:
			return this.target.position - base.transform.position;
		default:
			return Vector3.zero;
		}
	}

	// Token: 0x06000ACB RID: 2763 RVA: 0x0003E04C File Offset: 0x0003C24C
	private void LateUpdate()
	{
		base.transform.position = this.attachedTransform.TransformPoint(this.localPosition);
		if (this.target != null)
		{
			Vector3 direction = this.GetDirection(this.forwardSource, this.forwardDirection);
			Vector3 direction2 = this.GetDirection(this.upSource, this.upDirection);
			Quaternion quaternion = Quaternion.LookRotation(direction, direction2);
			if (this.giveUpPriority)
			{
				quaternion = Quaternion.FromToRotation(quaternion * Vector3.up, direction2) * quaternion;
			}
			base.transform.rotation = quaternion;
			return;
		}
		base.transform.rotation = this.attachedTransform.rotation * this.localRotation;
	}

	// Token: 0x04000D9F RID: 3487
	public AttachToBone.PlayerBone attachedBone;

	// Token: 0x04000DA0 RID: 3488
	private Transform attachedTransform;

	// Token: 0x04000DA1 RID: 3489
	public bool useInitialTransform;

	// Token: 0x04000DA2 RID: 3490
	[ConditionalHide("useInitialTransform", true, Inverse = true)]
	public Vector3 localPosition;

	// Token: 0x04000DA3 RID: 3491
	[ConditionalHide("useInitialTransform", true, Inverse = true)]
	public Quaternion localRotation;

	// Token: 0x04000DA4 RID: 3492
	public Transform target;

	// Token: 0x04000DA5 RID: 3493
	public AttachToBone.DirectionSource forwardSource;

	// Token: 0x04000DA6 RID: 3494
	public Vector3 forwardDirection;

	// Token: 0x04000DA7 RID: 3495
	public AttachToBone.DirectionSource upSource;

	// Token: 0x04000DA8 RID: 3496
	public Vector3 upDirection;

	// Token: 0x04000DA9 RID: 3497
	public bool giveUpPriority;

	// Token: 0x0200023A RID: 570
	public enum PlayerBone
	{
		// Token: 0x04000DAB RID: 3499
		Head,
		// Token: 0x04000DAC RID: 3500
		Hand,
		// Token: 0x04000DAD RID: 3501
		Chest
	}

	// Token: 0x0200023B RID: 571
	public enum DirectionSource
	{
		// Token: 0x04000DAF RID: 3503
		LocalDirection,
		// Token: 0x04000DB0 RID: 3504
		TargetDirection,
		// Token: 0x04000DB1 RID: 3505
		Target
	}
}
