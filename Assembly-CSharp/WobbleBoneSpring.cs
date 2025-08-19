using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Wobble/Spring Bone")]
public class WobbleBoneSpring : WobbleBoneBase
{
	// Token: 0x06001036 RID: 4150 RVA: 0x00053550 File Offset: 0x00051750
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(base.transform.parent.position, base.transform.position);
		Gizmos.DrawWireSphere(base.transform.position, 0.02f);
	}

	// Token: 0x06001037 RID: 4151 RVA: 0x00054364 File Offset: 0x00052564
	private void UpdateWindForce()
	{
		WobbleBoneSpring.windForceUpdateTime = Time.time;
		WobbleBoneSpring.windForce = ((Mathf.Sin(WobbleBoneSpring.windForceUpdateTime * 7f) + 1f) * 2.5f + (Mathf.Sin(WobbleBoneSpring.windForceUpdateTime * 5f) + 1f) * 3.5f) * WobbleBoneSpring.windDirection;
	}

	// Token: 0x06001038 RID: 4152 RVA: 0x000543C4 File Offset: 0x000525C4
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

	// Token: 0x06001039 RID: 4153 RVA: 0x00054604 File Offset: 0x00052804
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

	// Token: 0x0600103A RID: 4154 RVA: 0x0000DE48 File Offset: 0x0000C048
	private Vector3 ProjectOnNormalized(Vector3 vector, Vector3 normal)
	{
		return vector - Vector3.Dot(vector, normal) * normal;
	}

	// Token: 0x0600103B RID: 4155 RVA: 0x0005469C File Offset: 0x0005289C
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

	// Token: 0x0600103C RID: 4156 RVA: 0x000548A4 File Offset: 0x00052AA4
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

	// Token: 0x0600103D RID: 4157 RVA: 0x00054900 File Offset: 0x00052B00
	public override void Reacclimate()
	{
		this.velocity = Vector3.zero;
		this.position = (this.oldPosition = (this.interpolatedPosition = base.transform.position));
		this.rotation = (this.oldVisualRotation = (this.visualRotation = base.transform.rotation));
	}

	// Token: 0x0600103E RID: 4158 RVA: 0x00054960 File Offset: 0x00052B60
	public override void ApplyPosition()
	{
		base.transform.position = (this.interpolatedPosition = this.position);
	}

	// Token: 0x0600103F RID: 4159 RVA: 0x00054988 File Offset: 0x00052B88
	public override void ApplyPosition(float t)
	{
		base.transform.position = (this.interpolatedPosition = Vector3.Lerp(this.oldPosition, this.position, t));
	}

	// Token: 0x06001040 RID: 4160 RVA: 0x0000DF1B File Offset: 0x0000C11B
	public override void ApplyRotation()
	{
		base.transform.rotation = this.visualRotation;
	}

	// Token: 0x06001041 RID: 4161 RVA: 0x0000DF2E File Offset: 0x0000C12E
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
