using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000329 RID: 809
[AddComponentMenu("Wobble/Rotating Bone")]
public class WobbleBoneRotate : WobbleBoneBase
{
	// Token: 0x06000FD5 RID: 4053 RVA: 0x00052164 File Offset: 0x00050364
	public override void Initialize()
	{
		this.parent = base.transform.parent;
		this.position = base.transform.position;
		this.oldRotation = (this.visualRotation = (this.rotation = base.transform.rotation));
		this.initialLocalRotation = base.transform.localRotation;
		if (this.specifyReferenceBones)
		{
			this.childBones = this.referenceBones;
		}
		else
		{
			List<WobbleBoneBase> list = new List<WobbleBoneBase>();
			for (int i = 0; i < base.transform.childCount; i++)
			{
				WobbleBoneBase component = base.transform.GetChild(i).GetComponent<WobbleBoneBase>();
				if (component != null)
				{
					list.Add(component);
				}
			}
			this.childBones = list.ToArray();
		}
		Vector3 vector = Vector3.zero;
		if (this.childBones.Length != 0)
		{
			foreach (WobbleBoneBase wobbleBoneBase in this.childBones)
			{
				vector += wobbleBoneBase.transform.position;
			}
			vector /= (float)this.childBones.Length;
		}
		else
		{
			vector = this.position;
		}
		this.initialDirection = (vector - this.position).normalized;
		this.initialDirectionLocal = this.parent.rotation.Inverse() * this.initialDirection;
	}

	// Token: 0x06000FD6 RID: 4054 RVA: 0x000522C4 File Offset: 0x000504C4
	public override void RunWobbleUpdate()
	{
		this.oldRotation = this.visualRotation;
		this.position = (this.interpolatedPosition = base.transform.position);
		Quaternion rotation = base.transform.parent.rotation;
		this.rotation = rotation * this.initialLocalRotation;
		Vector3 vector = Vector3.zero;
		if (this.childBones.Length != 0)
		{
			foreach (WobbleBoneBase wobbleBoneBase in this.childBones)
			{
				vector += wobbleBoneBase.position;
			}
			vector /= (float)this.childBones.Length;
			this.visualRotation = rotation * (Quaternion.FromToRotation(this.initialDirectionLocal, rotation.Inverse() * (vector - this.position)) * this.initialLocalRotation);
			this.visualRotation = Quaternion.Slerp(this.rotation, this.visualRotation, this.rotateAmount);
			return;
		}
		this.visualRotation = this.rotation;
	}

	// Token: 0x06000FD7 RID: 4055 RVA: 0x000523CC File Offset: 0x000505CC
	private void LockAxis()
	{
		if (this.lockXAxis || this.lockYAxis || this.lockZAxis)
		{
			Vector3 eulerAngles = base.transform.localRotation.eulerAngles;
			Vector3 eulerAngles2 = this.initialLocalRotation.eulerAngles;
			if (this.lockXAxis)
			{
				eulerAngles.x = eulerAngles2.x;
			}
			if (this.lockYAxis)
			{
				eulerAngles.y = eulerAngles2.y;
			}
			if (this.lockZAxis)
			{
				eulerAngles.z = eulerAngles2.z;
			}
			base.transform.localRotation = Quaternion.Euler(eulerAngles);
		}
	}

	// Token: 0x06000FD8 RID: 4056 RVA: 0x0000DB3F File Offset: 0x0000BD3F
	public override void ApplyRotation()
	{
		this.interpolatedRotation = this.visualRotation;
		base.transform.rotation = this.interpolatedRotation;
		this.LockAxis();
	}

	// Token: 0x06000FD9 RID: 4057 RVA: 0x0000DB64 File Offset: 0x0000BD64
	public override void ApplyRotation(float t)
	{
		this.interpolatedRotation = Quaternion.SlerpUnclamped(this.oldRotation, this.visualRotation, t);
		base.transform.rotation = this.interpolatedRotation;
		this.LockAxis();
	}

	// Token: 0x04001476 RID: 5238
	private Vector3 initialPosition;

	// Token: 0x04001477 RID: 5239
	private Vector3 initialDirectionLocal;

	// Token: 0x04001478 RID: 5240
	private Vector3 initialDirection;

	// Token: 0x04001479 RID: 5241
	private Quaternion initialLocalRotation;

	// Token: 0x0400147A RID: 5242
	private Quaternion oldRotation;

	// Token: 0x0400147B RID: 5243
	private Quaternion visualRotation;

	// Token: 0x0400147C RID: 5244
	private WobbleBoneBase[] childBones;

	// Token: 0x0400147D RID: 5245
	private Transform parent;

	// Token: 0x0400147E RID: 5246
	public float rotateAmount = 1f;

	// Token: 0x0400147F RID: 5247
	public bool lockXAxis;

	// Token: 0x04001480 RID: 5248
	public bool lockYAxis;

	// Token: 0x04001481 RID: 5249
	public bool lockZAxis;

	// Token: 0x04001482 RID: 5250
	public bool specifyReferenceBones;

	// Token: 0x04001483 RID: 5251
	public WobbleBoneBase[] referenceBones;
}
