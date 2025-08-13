using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200025D RID: 605
public class WobbleBones : MonoBehaviour
{
	// Token: 0x06000D02 RID: 3330 RVA: 0x0003EB72 File Offset: 0x0003CD72
	private void Start()
	{
		this.parent = base.transform.parent;
		this.LoadBones();
	}

	// Token: 0x06000D03 RID: 3331 RVA: 0x0003EB8C File Offset: 0x0003CD8C
	private void LoadBones()
	{
		List<WobbleBones.Bone> list = new List<WobbleBones.Bone>();
		list.Add(new WobbleBones.Bone(base.transform));
		Transform transform = base.transform;
		while (transform.childCount > 0)
		{
		}
		this.bones = list.ToArray();
	}

	// Token: 0x06000D04 RID: 3332 RVA: 0x0003EBCC File Offset: 0x0003CDCC
	private void LateUpdate()
	{
		for (int i = 0; i < this.bones.Length; i++)
		{
			this.UpdateBone(this.bones[i]);
		}
	}

	// Token: 0x06000D05 RID: 3333 RVA: 0x0003EBFC File Offset: 0x0003CDFC
	private void UpdateBone(WobbleBones.Bone bone)
	{
		bone.position = Vector3.SmoothDamp(bone.position, bone.transform.parent.TransformPoint(bone.tLocalPosition), ref bone.velocity, 0.05f);
		bone.transform.position = bone.position;
	}

	// Token: 0x04001118 RID: 4376
	public float gravityFactor;

	// Token: 0x04001119 RID: 4377
	public float springiness = 1f;

	// Token: 0x0400111A RID: 4378
	private Transform parent;

	// Token: 0x0400111B RID: 4379
	public WobbleBones.Bone[] bones;

	// Token: 0x02000424 RID: 1060
	public class Bone
	{
		// Token: 0x06001AFB RID: 6907 RVA: 0x00072B9C File Offset: 0x00070D9C
		public Bone(Transform transform)
		{
			this.transform = transform;
			this.tLocalPosition = transform.localPosition;
			this.position = transform.position;
			this.tLocalRotation = transform.localRotation;
			this.rotation = transform.rotation;
			this.length = transform.localPosition.magnitude;
		}

		// Token: 0x04001D50 RID: 7504
		public Transform transform;

		// Token: 0x04001D51 RID: 7505
		public Vector3 tLocalPosition;

		// Token: 0x04001D52 RID: 7506
		public float length;

		// Token: 0x04001D53 RID: 7507
		public Quaternion tLocalRotation;

		// Token: 0x04001D54 RID: 7508
		public Vector3 position;

		// Token: 0x04001D55 RID: 7509
		public Quaternion rotation;

		// Token: 0x04001D56 RID: 7510
		public Vector3 velocity;
	}
}
