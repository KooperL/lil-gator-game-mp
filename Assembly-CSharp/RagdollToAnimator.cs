using System;
using UnityEngine;

public class RagdollToAnimator : MonoBehaviour
{
	// Token: 0x06000AF9 RID: 2809 RVA: 0x00036EAC File Offset: 0x000350AC
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

	// Token: 0x06000AFA RID: 2810 RVA: 0x00036F4F File Offset: 0x0003514F
	private void Awake()
	{
		this.animatorPositions = new Vector3[this.transforms.Length];
		this.animatorRotations = new Quaternion[this.transforms.Length];
	}

	// Token: 0x06000AFB RID: 2811 RVA: 0x00036F77 File Offset: 0x00035177
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

	// Token: 0x06000AFC RID: 2812 RVA: 0x00036FB3 File Offset: 0x000351B3
	public void SkipTo(float newT = 1f)
	{
		this.t = newT;
		this.SetInterpolation(this.t);
	}

	// Token: 0x06000AFD RID: 2813 RVA: 0x00036FC8 File Offset: 0x000351C8
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

	public Transform[] transforms;

	private Vector3[] ragdollPositions;

	private Quaternion[] ragdollRotations;

	private Vector3[] animatorPositions;

	private Quaternion[] animatorRotations;

	public float interpolationTime = 0.25f;

	private float t;

	public Transform root;
}
