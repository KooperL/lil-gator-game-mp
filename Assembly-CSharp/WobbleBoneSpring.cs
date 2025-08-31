using System;
using System.Collections.Generic;
using UnityEngine;

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

	private static Vector3 windForce;

	private static float windForceUpdateTime;

	private static readonly Vector3 windDirection = Vector3.right;

	private const float strength1 = 2.5f;

	private const float frequency1 = 7f;

	private const float strength2 = 3.5f;

	private const float frequency2 = 5f;

	private Vector3 initialPositionLocal;

	private Vector3 initialDirectionLocal;

	private Vector3 initialChildDirectionLocal;

	private Quaternion initialLocalRotation;

	public Vector3 oldPosition;

	public Vector3 velocity;

	private Quaternion oldVisualRotation;

	private Quaternion visualRotation;

	private Transform parent;

	private WobbleBoneBase parentBone;

	private bool hasParentBone;

	[Space]
	[Tooltip("~500-1000")]
	public float spring = 500f;

	[Range(0f, 1f)]
	public float compression = 0.5f;

	[Range(1f, 3f)]
	public float expansion = 1.5f;

	private float minDistance;

	private float maxDistance;

	public float damper = 20f;

	public float gravity = 1f;

	[Range(0f, 5f)]
	public float windStrength;

	[Space]
	public bool ignoreRotation;

	private bool initialized;

	private WobbleBoneBase[] childBones;

	private Vector3 parentPosition;

	private Quaternion parentRotation;

	private Vector3 directionGoal;

	private Vector3 positionGoal;
}
