using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Wobble/Rotating Bone")]
public class WobbleBoneRotate : WobbleBoneBase
{
	// Token: 0x06001031 RID: 4145 RVA: 0x00054350 File Offset: 0x00052550
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

	// Token: 0x06001032 RID: 4146 RVA: 0x000544B0 File Offset: 0x000526B0
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

	// Token: 0x06001033 RID: 4147 RVA: 0x000545B8 File Offset: 0x000527B8
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

	// Token: 0x06001034 RID: 4148 RVA: 0x0000DEB2 File Offset: 0x0000C0B2
	public override void ApplyRotation()
	{
		this.interpolatedRotation = this.visualRotation;
		base.transform.rotation = this.interpolatedRotation;
		this.LockAxis();
	}

	// Token: 0x06001035 RID: 4149 RVA: 0x0000DED7 File Offset: 0x0000C0D7
	public override void ApplyRotation(float t)
	{
		this.interpolatedRotation = Quaternion.SlerpUnclamped(this.oldRotation, this.visualRotation, t);
		base.transform.rotation = this.interpolatedRotation;
		this.LockAxis();
	}

	private Vector3 initialPosition;

	private Vector3 initialDirectionLocal;

	private Vector3 initialDirection;

	private Quaternion initialLocalRotation;

	private Quaternion oldRotation;

	private Quaternion visualRotation;

	private WobbleBoneBase[] childBones;

	private Transform parent;

	public float rotateAmount = 1f;

	public bool lockXAxis;

	public bool lockYAxis;

	public bool lockZAxis;

	public bool specifyReferenceBones;

	public WobbleBoneBase[] referenceBones;
}
