using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000263 RID: 611
[AddComponentMenu("Wobble/Spring Bone")]
public class WobbleBoneSpring : WobbleBoneBase
{
	// Token: 0x06000D2D RID: 3373 RVA: 0x0003FC1C File Offset: 0x0003DE1C
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(base.transform.parent.position, base.transform.position);
		Gizmos.DrawWireSphere(base.transform.position, 0.02f);
	}

	// Token: 0x06000D2E RID: 3374 RVA: 0x0003FC68 File Offset: 0x0003DE68
	private void UpdateWindForce()
	{
		WobbleBoneSpring.windForceUpdateTime = Time.time;
		WobbleBoneSpring.windForce = ((Mathf.Sin(WobbleBoneSpring.windForceUpdateTime * 7f) + 1f) * 2.5f + (Mathf.Sin(WobbleBoneSpring.windForceUpdateTime * 5f) + 1f) * 3.5f) * WobbleBoneSpring.windDirection;
	}

	// Token: 0x06000D2F RID: 3375 RVA: 0x0003FCC8 File Offset: 0x0003DEC8
	public override void Initialize()
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
		this.parent = base.transform.parent;
		this.parentBone = this.parent.GetComponent<WobbleBoneBase>();
		this.hasParentBone = this.parentBone != null;
		this.initialPositionLocal = base.transform.localPosition;
		Vector3 normalized = (base.transform.position - this.parent.position).normalized;
		this.initialDirectionLocal = this.parent.rotation.Inverse() * normalized;
		if (this.childBones.Length != 0)
		{
			Vector3 vector = Vector3.zero;
			foreach (WobbleBoneBase wobbleBoneBase in this.childBones)
			{
				vector += wobbleBoneBase.transform.position;
			}
			vector /= (float)this.childBones.Length;
			this.initialChildDirectionLocal = this.parent.rotation.Inverse() * (vector - this.parent.position).normalized;
		}
		else
		{
			this.initialChildDirectionLocal = this.initialDirectionLocal;
		}
		this.initialLocalRotation = base.transform.localRotation;
		this.oldPosition = (this.position = (this.interpolatedPosition = base.transform.position));
		this.velocity = Vector3.zero;
		this.visualRotation = (this.oldVisualRotation = (this.interpolatedRotation = (this.rotation = base.transform.rotation)));
		this.initialized = true;
		float magnitude = (base.transform.position - this.parent.position).magnitude;
		this.minDistance = magnitude * this.compression;
		this.maxDistance = magnitude * this.expansion;
	}

	// Token: 0x06000D30 RID: 3376 RVA: 0x0003FF08 File Offset: 0x0003E108
	private void UpdateState()
	{
		this.oldPosition = this.position;
		this.oldVisualRotation = this.visualRotation;
		if (this.hasParentBone)
		{
			this.parentPosition = this.parentBone.position;
			this.parentRotation = this.parentBone.rotation;
		}
		else
		{
			this.parentPosition = this.parent.position;
			this.parentRotation = this.parent.rotation;
		}
		this.positionGoal = this.parentPosition + this.parentRotation * this.initialPositionLocal;
	}

	// Token: 0x06000D31 RID: 3377 RVA: 0x0003FF9D File Offset: 0x0003E19D
	private Vector3 ProjectOnNormalized(Vector3 vector, Vector3 normal)
	{
		return vector - Vector3.Dot(vector, normal) * normal;
	}

	// Token: 0x06000D32 RID: 3378 RVA: 0x0003FFB4 File Offset: 0x0003E1B4
	public override void RunWobbleUpdate()
	{
		if (!this.initialized)
		{
			return;
		}
		this.UpdateState();
		if (WobbleBoneSpring.windForceUpdateTime != Time.time)
		{
			this.UpdateWindForce();
		}
		float num = Time.deltaTime;
		if (!Time.inFixedTimeStep && num > Time.fixedDeltaTime)
		{
			num = Time.fixedDeltaTime;
		}
		this.forces += this.windStrength * WobbleBoneSpring.windForce;
		this.position - this.parentPosition;
		Vector3 vector = this.positionGoal - this.position;
		Vector3 vector2 = this.spring * vector;
		this.forces += vector2;
		this.forces += this.gravity * Physics.gravity - this.damper * this.velocity;
		this.velocity += num * this.forces;
		this.position += num * this.velocity;
		this.position - this.parentPosition;
		if (!this.ignoreRotation)
		{
			Quaternion quaternion;
			if (this.parentBone != null)
			{
				quaternion = this.parentBone.rotation;
			}
			else
			{
				quaternion = this.parent.rotation;
			}
			this.rotation = quaternion * (Quaternion.FromToRotation(this.initialDirectionLocal, quaternion.Inverse() * (this.position - this.parentPosition)) * this.initialLocalRotation);
			if (this.childBones.Length != 0)
			{
				Vector3 averageChildPosition = this.GetAverageChildPosition();
				this.visualRotation = quaternion * (Quaternion.FromToRotation(this.initialChildDirectionLocal, quaternion.Inverse() * (averageChildPosition - this.parentPosition)) * this.initialLocalRotation);
			}
			else
			{
				this.visualRotation = this.rotation;
			}
		}
		this.forces = Vector3.zero;
	}

	// Token: 0x06000D33 RID: 3379 RVA: 0x000401BC File Offset: 0x0003E3BC
	private Vector3 GetAverageChildPosition()
	{
		if (this.childBones.Length != 0)
		{
			Vector3 vector = Vector3.zero;
			foreach (WobbleBoneBase wobbleBoneBase in this.childBones)
			{
				vector += wobbleBoneBase.position;
			}
			return vector / (float)this.childBones.Length;
		}
		return this.position;
	}

	// Token: 0x06000D34 RID: 3380 RVA: 0x00040218 File Offset: 0x0003E418
	public override void Reacclimate()
	{
		this.velocity = Vector3.zero;
		this.position = (this.oldPosition = (this.interpolatedPosition = base.transform.position));
		this.rotation = (this.oldVisualRotation = (this.visualRotation = base.transform.rotation));
	}

	// Token: 0x06000D35 RID: 3381 RVA: 0x00040278 File Offset: 0x0003E478
	public override void ApplyPosition()
	{
		base.transform.position = (this.interpolatedPosition = this.position);
	}

	// Token: 0x06000D36 RID: 3382 RVA: 0x000402A0 File Offset: 0x0003E4A0
	public override void ApplyPosition(float t)
	{
		base.transform.position = (this.interpolatedPosition = Vector3.Lerp(this.oldPosition, this.position, t));
	}

	// Token: 0x06000D37 RID: 3383 RVA: 0x000402D3 File Offset: 0x0003E4D3
	public override void ApplyRotation()
	{
		base.transform.rotation = this.visualRotation;
	}

	// Token: 0x06000D38 RID: 3384 RVA: 0x000402E6 File Offset: 0x0003E4E6
	public override void ApplyRotation(float t)
	{
		base.transform.rotation = Quaternion.SlerpUnclamped(this.oldVisualRotation, this.visualRotation, t);
	}

	// Token: 0x04001161 RID: 4449
	private static Vector3 windForce;

	// Token: 0x04001162 RID: 4450
	private static float windForceUpdateTime;

	// Token: 0x04001163 RID: 4451
	private static readonly Vector3 windDirection = Vector3.right;

	// Token: 0x04001164 RID: 4452
	private const float strength1 = 2.5f;

	// Token: 0x04001165 RID: 4453
	private const float frequency1 = 7f;

	// Token: 0x04001166 RID: 4454
	private const float strength2 = 3.5f;

	// Token: 0x04001167 RID: 4455
	private const float frequency2 = 5f;

	// Token: 0x04001168 RID: 4456
	private Vector3 initialPositionLocal;

	// Token: 0x04001169 RID: 4457
	private Vector3 initialDirectionLocal;

	// Token: 0x0400116A RID: 4458
	private Vector3 initialChildDirectionLocal;

	// Token: 0x0400116B RID: 4459
	private Quaternion initialLocalRotation;

	// Token: 0x0400116C RID: 4460
	public Vector3 oldPosition;

	// Token: 0x0400116D RID: 4461
	public Vector3 velocity;

	// Token: 0x0400116E RID: 4462
	private Quaternion oldVisualRotation;

	// Token: 0x0400116F RID: 4463
	private Quaternion visualRotation;

	// Token: 0x04001170 RID: 4464
	private Transform parent;

	// Token: 0x04001171 RID: 4465
	private WobbleBoneBase parentBone;

	// Token: 0x04001172 RID: 4466
	private bool hasParentBone;

	// Token: 0x04001173 RID: 4467
	[Space]
	[Tooltip("~500-1000")]
	public float spring = 500f;

	// Token: 0x04001174 RID: 4468
	[Range(0f, 1f)]
	public float compression = 0.5f;

	// Token: 0x04001175 RID: 4469
	[Range(1f, 3f)]
	public float expansion = 1.5f;

	// Token: 0x04001176 RID: 4470
	private float minDistance;

	// Token: 0x04001177 RID: 4471
	private float maxDistance;

	// Token: 0x04001178 RID: 4472
	public float damper = 20f;

	// Token: 0x04001179 RID: 4473
	public float gravity = 1f;

	// Token: 0x0400117A RID: 4474
	[Range(0f, 5f)]
	public float windStrength;

	// Token: 0x0400117B RID: 4475
	[Space]
	public bool ignoreRotation;

	// Token: 0x0400117C RID: 4476
	private bool initialized;

	// Token: 0x0400117D RID: 4477
	private WobbleBoneBase[] childBones;

	// Token: 0x0400117E RID: 4478
	private Vector3 parentPosition;

	// Token: 0x0400117F RID: 4479
	private Quaternion parentRotation;

	// Token: 0x04001180 RID: 4480
	private Vector3 directionGoal;

	// Token: 0x04001181 RID: 4481
	private Vector3 positionGoal;
}
