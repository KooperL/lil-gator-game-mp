using System;
using UnityEngine;

// Token: 0x020001C0 RID: 448
public class MoveToBone : MonoBehaviour
{
	// Token: 0x06000943 RID: 2371 RVA: 0x0002BF84 File Offset: 0x0002A184
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

	// Token: 0x06000944 RID: 2372 RVA: 0x0002C0A6 File Offset: 0x0002A2A6
	private void LateUpdate()
	{
		if (this.originalParent == null)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04000BA0 RID: 2976
	public MoveToBone.PlayerBone attachedBone;

	// Token: 0x04000BA1 RID: 2977
	private Transform attachedTransform;

	// Token: 0x04000BA2 RID: 2978
	public bool useInitialTransform;

	// Token: 0x04000BA3 RID: 2979
	[ConditionalHide("useInitialTransform", true, Inverse = true)]
	public Vector3 localPosition;

	// Token: 0x04000BA4 RID: 2980
	[ConditionalHide("useInitialTransform", true, Inverse = true)]
	public Quaternion localRotation;

	// Token: 0x04000BA5 RID: 2981
	public Vector3 localScale = Vector3.one;

	// Token: 0x04000BA6 RID: 2982
	private Transform originalParent;

	// Token: 0x020003DE RID: 990
	public enum PlayerBone
	{
		// Token: 0x04001C48 RID: 7240
		Head,
		// Token: 0x04001C49 RID: 7241
		Hand,
		// Token: 0x04001C4A RID: 7242
		Chest,
		// Token: 0x04001C4B RID: 7243
		Hips,
		// Token: 0x04001C4C RID: 7244
		LowerSpine,
		// Token: 0x04001C4D RID: 7245
		Shoulder
	}
}
