using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000323 RID: 803
public class WobbleBones : MonoBehaviour
{
	// Token: 0x06000FAF RID: 4015 RVA: 0x0000DA7C File Offset: 0x0000BC7C
	private void Start()
	{
		this.parent = base.transform.parent;
		this.LoadBones();
	}

	// Token: 0x06000FB0 RID: 4016 RVA: 0x000514B0 File Offset: 0x0004F6B0
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

	// Token: 0x06000FB1 RID: 4017 RVA: 0x000514F0 File Offset: 0x0004F6F0
	private void LateUpdate()
	{
		for (int i = 0; i < this.bones.Length; i++)
		{
			this.UpdateBone(this.bones[i]);
		}
	}

	// Token: 0x06000FB2 RID: 4018 RVA: 0x00051520 File Offset: 0x0004F720
	private void UpdateBone(WobbleBones.Bone bone)
	{
		bone.position = Vector3.SmoothDamp(bone.position, bone.transform.parent.TransformPoint(bone.tLocalPosition), ref bone.velocity, 0.05f);
		bone.transform.position = bone.position;
	}

	// Token: 0x04001434 RID: 5172
	public float gravityFactor;

	// Token: 0x04001435 RID: 5173
	public float springiness = 1f;

	// Token: 0x04001436 RID: 5174
	private Transform parent;

	// Token: 0x04001437 RID: 5175
	public WobbleBones.Bone[] bones;

	// Token: 0x02000324 RID: 804
	public class Bone
	{
		// Token: 0x06000FB4 RID: 4020 RVA: 0x00051570 File Offset: 0x0004F770
		public Bone(Transform transform)
		{
			this.transform = transform;
			this.tLocalPosition = transform.localPosition;
			this.position = transform.position;
			this.tLocalRotation = transform.localRotation;
			this.rotation = transform.rotation;
			this.length = transform.localPosition.magnitude;
		}

		// Token: 0x04001438 RID: 5176
		public Transform transform;

		// Token: 0x04001439 RID: 5177
		public Vector3 tLocalPosition;

		// Token: 0x0400143A RID: 5178
		public float length;

		// Token: 0x0400143B RID: 5179
		public Quaternion tLocalRotation;

		// Token: 0x0400143C RID: 5180
		public Vector3 position;

		// Token: 0x0400143D RID: 5181
		public Quaternion rotation;

		// Token: 0x0400143E RID: 5182
		public Vector3 velocity;
	}
}
