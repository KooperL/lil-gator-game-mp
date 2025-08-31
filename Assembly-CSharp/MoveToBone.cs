using System;
using UnityEngine;

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

	public MoveToBone.PlayerBone attachedBone;

	private Transform attachedTransform;

	public bool useInitialTransform;

	[ConditionalHide("useInitialTransform", true, Inverse = true)]
	public Vector3 localPosition;

	[ConditionalHide("useInitialTransform", true, Inverse = true)]
	public Quaternion localRotation;

	public Vector3 localScale = Vector3.one;

	private Transform originalParent;

	public enum PlayerBone
	{
		Head,
		Hand,
		Chest,
		Hips,
		LowerSpine,
		Shoulder
	}
}
