using System;
using UnityEngine;

// Token: 0x02000289 RID: 649
public class RagdollToAnimator : MonoBehaviour
{
	// Token: 0x06000CB8 RID: 3256 RVA: 0x00047FA0 File Offset: 0x000461A0
	[ContextMenu("Read Ragdoll Transforms")]
	public void ReadRagdollTransforms()
	{
		if (this.ragdollPositions == null)
		{
			this.ragdollPositions = new Vector3[this.transforms.Length];
		}
		if (this.ragdollRotations == null)
		{
			this.ragdollRotations = new Quaternion[this.transforms.Length];
		}
		for (int i = 0; i < this.transforms.Length; i++)
		{
			this.ragdollPositions[i] = this.transforms[i].position - this.root.position;
			this.ragdollRotations[i] = this.transforms[i].rotation;
		}
		this.t = 0f;
	}

	// Token: 0x06000CB9 RID: 3257 RVA: 0x0000BDDE File Offset: 0x00009FDE
	private void Awake()
	{
		this.animatorPositions = new Vector3[this.transforms.Length];
		this.animatorRotations = new Quaternion[this.transforms.Length];
	}

	// Token: 0x06000CBA RID: 3258 RVA: 0x0000BE06 File Offset: 0x0000A006
	private void LateUpdate()
	{
		this.t += Time.deltaTime / this.interpolationTime;
		if (this.t >= 1f)
		{
			base.enabled = false;
			return;
		}
		this.SetInterpolation(this.t);
	}

	// Token: 0x06000CBB RID: 3259 RVA: 0x0000BE42 File Offset: 0x0000A042
	public void SkipTo(float newT = 1f)
	{
		this.t = newT;
		this.SetInterpolation(this.t);
	}

	// Token: 0x06000CBC RID: 3260 RVA: 0x00048044 File Offset: 0x00046244
	private void SetInterpolation(float t)
	{
		t = Mathf.Clamp01(t);
		for (int i = 0; i < this.transforms.Length; i++)
		{
			this.animatorPositions[i] = this.transforms[i].position - this.root.position;
			this.animatorRotations[i] = this.transforms[i].rotation;
		}
		for (int j = 0; j < this.transforms.Length; j++)
		{
			if (this.ragdollPositions[j] != this.animatorPositions[j] && !this.ragdollPositions[j].IsNaN() && !this.animatorPositions[j].IsNaN())
			{
				this.transforms[j].position = this.root.position + Vector3.Lerp(this.ragdollPositions[j], this.animatorPositions[j], t);
			}
			if (this.ragdollRotations[j] != this.animatorRotations[j])
			{
				this.transforms[j].rotation = Quaternion.Slerp(this.ragdollRotations[j], this.animatorRotations[j], t);
			}
		}
	}

	// Token: 0x04001101 RID: 4353
	public Transform[] transforms;

	// Token: 0x04001102 RID: 4354
	private Vector3[] ragdollPositions;

	// Token: 0x04001103 RID: 4355
	private Quaternion[] ragdollRotations;

	// Token: 0x04001104 RID: 4356
	private Vector3[] animatorPositions;

	// Token: 0x04001105 RID: 4357
	private Quaternion[] animatorRotations;

	// Token: 0x04001106 RID: 4358
	public float interpolationTime = 0.25f;

	// Token: 0x04001107 RID: 4359
	private float t;

	// Token: 0x04001108 RID: 4360
	public Transform root;
}
