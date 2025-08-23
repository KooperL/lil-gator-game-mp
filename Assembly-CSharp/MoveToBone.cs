using System;
using UnityEngine;

public class MoveToBone : MonoBehaviour
{
	// Token: 0x06000B27 RID: 2855 RVA: 0x0003FF38 File Offset: 0x0003E138
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

	// Token: 0x06000B28 RID: 2856 RVA: 0x0000A8C2 File Offset: 0x00008AC2
	private void LateUpdate()
	{
		if (this.originalParent == null)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
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
