using System;
using UnityEngine;

// Token: 0x020001BA RID: 442
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

	// Token: 0x04000B8B RID: 2955
	public AttachToBone.PlayerBone attachedBone;

	// Token: 0x04000B8C RID: 2956
	private Transform attachedTransform;

	// Token: 0x04000B8D RID: 2957
	public bool useInitialTransform;

	// Token: 0x04000B8E RID: 2958
	[ConditionalHide("useInitialTransform", true, Inverse = true)]
	public Vector3 localPosition;

	// Token: 0x04000B8F RID: 2959
	[ConditionalHide("useInitialTransform", true, Inverse = true)]
	public Quaternion localRotation;

	// Token: 0x04000B90 RID: 2960
	public Transform target;

	// Token: 0x04000B91 RID: 2961
	public AttachToBone.DirectionSource forwardSource;

	// Token: 0x04000B92 RID: 2962
	public Vector3 forwardDirection;

	// Token: 0x04000B93 RID: 2963
	public AttachToBone.DirectionSource upSource;

	// Token: 0x04000B94 RID: 2964
	public Vector3 upDirection;

	// Token: 0x04000B95 RID: 2965
	public bool giveUpPriority;

	// Token: 0x020003DC RID: 988
	public enum PlayerBone
	{
		// Token: 0x04001C40 RID: 7232
		Head,
		// Token: 0x04001C41 RID: 7233
		Hand,
		// Token: 0x04001C42 RID: 7234
		Chest
	}

	// Token: 0x020003DD RID: 989
	public enum DirectionSource
	{
		// Token: 0x04001C44 RID: 7236
		LocalDirection,
		// Token: 0x04001C45 RID: 7237
		TargetDirection,
		// Token: 0x04001C46 RID: 7238
		Target
	}
}
