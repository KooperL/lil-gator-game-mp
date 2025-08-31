using System;
using UnityEngine;

public class AttachToBone : MonoBehaviour
{
	// Token: 0x06000932 RID: 2354 RVA: 0x0002BC48 File Offset: 0x00029E48
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

	// Token: 0x06000933 RID: 2355 RVA: 0x0002BCD0 File Offset: 0x00029ED0
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

	// Token: 0x06000934 RID: 2356 RVA: 0x0002BD2C File Offset: 0x00029F2C
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

	public AttachToBone.PlayerBone attachedBone;

	private Transform attachedTransform;

	public bool useInitialTransform;

	[ConditionalHide("useInitialTransform", true, Inverse = true)]
	public Vector3 localPosition;

	[ConditionalHide("useInitialTransform", true, Inverse = true)]
	public Quaternion localRotation;

	public Transform target;

	public AttachToBone.DirectionSource forwardSource;

	public Vector3 forwardDirection;

	public AttachToBone.DirectionSource upSource;

	public Vector3 upDirection;

	public bool giveUpPriority;

	public enum PlayerBone
	{
		Head,
		Hand,
		Chest
	}

	public enum DirectionSource
	{
		LocalDirection,
		TargetDirection,
		Target
	}
}
