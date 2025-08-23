using System;
using System.Collections.Generic;
using UnityEngine;

public class WobbleBones : MonoBehaviour
{
	// Token: 0x0600100B RID: 4107 RVA: 0x0000DDEF File Offset: 0x0000BFEF
	private void Start()
	{
		this.parent = base.transform.parent;
		this.LoadBones();
	}

	// Token: 0x0600100C RID: 4108 RVA: 0x0005369C File Offset: 0x0005189C
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

	// Token: 0x0600100D RID: 4109 RVA: 0x000536DC File Offset: 0x000518DC
	private void LateUpdate()
	{
		for (int i = 0; i < this.bones.Length; i++)
		{
			this.UpdateBone(this.bones[i]);
		}
	}

	// Token: 0x0600100E RID: 4110 RVA: 0x0005370C File Offset: 0x0005190C
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
		// Token: 0x06001010 RID: 4112 RVA: 0x0005375C File Offset: 0x0005195C
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
