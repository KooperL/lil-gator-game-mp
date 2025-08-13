using System;
using UnityEngine;

// Token: 0x02000241 RID: 577
public class MoveToBone : MonoBehaviour
{
	// Token: 0x06000ADA RID: 2778 RVA: 0x0003E168 File Offset: 0x0003C368
	private void Start()
	{
		this.originalParent = base.transform.parent;
		if (this.useInitialTransform)
		{
			this.localPosition = base.transform.localPosition;
			this.localRotation = base.transform.localRotation;
		}
		switch (this.attachedBone)
		{
		case MoveToBone.PlayerBone.Head:
			this.attachedTransform = Player.itemManager.headRigidbody.transform;
			break;
		case MoveToBone.PlayerBone.Hand:
			this.attachedTransform = Player.itemManager.leftHandAnchor;
			break;
		case MoveToBone.PlayerBone.Chest:
			this.attachedTransform = Player.itemManager.chestAnchor;
			break;
		case MoveToBone.PlayerBone.Hips:
			this.attachedTransform = Player.itemManager.hipsAnchor;
			break;
		case MoveToBone.PlayerBone.LowerSpine:
			this.attachedTransform = Player.itemManager.lowerSpineAnchor;
			break;
		case MoveToBone.PlayerBone.Shoulder:
			this.attachedTransform = Player.itemManager.shoulderAnchor;
			break;
		}
		base.transform.parent = this.attachedTransform;
		base.transform.localPosition = this.localPosition;
		base.transform.localRotation = this.localRotation;
		base.transform.localScale = this.localScale;
	}

	// Token: 0x06000ADB RID: 2779 RVA: 0x0000A584 File Offset: 0x00008784
	private void LateUpdate()
	{
		if (this.originalParent == null)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04000DBC RID: 3516
	public MoveToBone.PlayerBone attachedBone;

	// Token: 0x04000DBD RID: 3517
	private Transform attachedTransform;

	// Token: 0x04000DBE RID: 3518
	public bool useInitialTransform;

	// Token: 0x04000DBF RID: 3519
	[ConditionalHide("useInitialTransform", true, Inverse = true)]
	public Vector3 localPosition;

	// Token: 0x04000DC0 RID: 3520
	[ConditionalHide("useInitialTransform", true, Inverse = true)]
	public Quaternion localRotation;

	// Token: 0x04000DC1 RID: 3521
	public Vector3 localScale = Vector3.one;

	// Token: 0x04000DC2 RID: 3522
	private Transform originalParent;

	// Token: 0x02000242 RID: 578
	public enum PlayerBone
	{
		// Token: 0x04000DC4 RID: 3524
		Head,
		// Token: 0x04000DC5 RID: 3525
		Hand,
		// Token: 0x04000DC6 RID: 3526
		Chest,
		// Token: 0x04000DC7 RID: 3527
		Hips,
		// Token: 0x04000DC8 RID: 3528
		LowerSpine,
		// Token: 0x04000DC9 RID: 3529
		Shoulder
	}
}
