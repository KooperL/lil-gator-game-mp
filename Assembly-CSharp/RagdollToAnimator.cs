using System;
using UnityEngine;

public class RagdollToAnimator : MonoBehaviour
{
	// Token: 0x06000D04 RID: 3332 RVA: 0x00049B28 File Offset: 0x00047D28
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

	// Token: 0x06000D05 RID: 3333 RVA: 0x0000C0E6 File Offset: 0x0000A2E6
	private void Awake()
	{
		this.animatorPositions = new Vector3[this.transforms.Length];
		this.animatorRotations = new Quaternion[this.transforms.Length];
	}

	// Token: 0x06000D06 RID: 3334 RVA: 0x0000C10E File Offset: 0x0000A30E
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

	// Token: 0x06000D07 RID: 3335 RVA: 0x0000C14A File Offset: 0x0000A34A
	public void SkipTo(float newT = 1f)
	{
		this.t = newT;
		this.SetInterpolation(this.t);
	}

	// Token: 0x06000D08 RID: 3336 RVA: 0x00049BCC File Offset: 0x00047DCC
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
