using System;
using System.Collections.Generic;
using UnityEngine;

public class WobbleBones : MonoBehaviour
{
	// Token: 0x0600100A RID: 4106 RVA: 0x0000DDEF File Offset: 0x0000BFEF
	private void Start()
	{
		this.parent = base.transform.parent;
		this.LoadBones();
	}

	// Token: 0x0600100B RID: 4107 RVA: 0x000533B0 File Offset: 0x000515B0
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

	// Token: 0x0600100C RID: 4108 RVA: 0x000533F0 File Offset: 0x000515F0
	private void LateUpdate()
	{
		for (int i = 0; i < this.bones.Length; i++)
		{
			this.UpdateBone(this.bones[i]);
		}
	}

	// Token: 0x0600100D RID: 4109 RVA: 0x00053420 File Offset: 0x00051620
	private void UpdateBone(WobbleBones.Bone bone)
	{
		bone.position = Vector3.SmoothDamp(bone.position, bone.transform.parent.TransformPoint(bone.tLocalPosition), ref bone.velocity, 0.05f);
		bone.transform.position = bone.position;
	}

	public float gravityFactor;

	public float springiness = 1f;

	private Transform parent;

	public WobbleBones.Bone[] bones;

	public class Bone
	{
		// Token: 0x0600100F RID: 4111 RVA: 0x00053470 File Offset: 0x00051670
		public Bone(Transform transform)
		{
			this.transform = transform;
			this.tLocalPosition = transform.localPosition;
			this.position = transform.position;
			this.tLocalRotation = transform.localRotation;
			this.rotation = transform.rotation;
			this.length = transform.localPosition.magnitude;
		}

		public Transform transform;

		public Vector3 tLocalPosition;

		public float length;

		public Quaternion tLocalRotation;

		public Vector3 position;

		public Quaternion rotation;

		public Vector3 velocity;
	}
}
