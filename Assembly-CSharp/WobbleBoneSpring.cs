using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200032A RID: 810
[AddComponentMenu("Wobble/Spring Bone")]
public class WobbleBoneSpring : WobbleBoneBase
{
	// Token: 0x06000FDB RID: 4059 RVA: 0x00051650 File Offset: 0x0004F850
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(base.transform.parent.position, base.transform.position);
		Gizmos.DrawWireSphere(base.transform.position, 0.02f);
	}

	// Token: 0x06000FDC RID: 4060 RVA: 0x00052464 File Offset: 0x00050664
	private void UpdateWindForce()
	{
		WobbleBoneSpring.windForceUpdateTime = Time.time;
		WobbleBoneSpring.windForce = ((Mathf.Sin(WobbleBoneSpring.windForceUpdateTime * 7f) + 1f) * 2.5f + (Mathf.Sin(WobbleBoneSpring.windForceUpdateTime * 5f) + 1f) * 3.5f) * WobbleBoneSpring.windDirection;
	}

	// Token: 0x06000FDD RID: 4061 RVA: 0x000524C4 File Offset: 0x000506C4
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

	// Token: 0x06000FDE RID: 4062 RVA: 0x00052704 File Offset: 0x00050904
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

	// Token: 0x06000FDF RID: 4063 RVA: 0x0000DAD5 File Offset: 0x0000BCD5
	private Vector3 ProjectOnNormalized(Vector3 vector, Vector3 normal)
	{
		return vector - Vector3.Dot(vector, normal) * normal;
	}

	// Token: 0x06000FE0 RID: 4064 RVA: 0x0005279C File Offset: 0x0005099C
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

	// Token: 0x06000FE1 RID: 4065 RVA: 0x000529A4 File Offset: 0x00050BA4
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

	// Token: 0x06000FE2 RID: 4066 RVA: 0x00052A00 File Offset: 0x00050C00
	public override void Reacclimate()
	{
		this.velocity = Vector3.zero;
		this.position = (this.oldPosition = (this.interpolatedPosition = base.transform.position));
		this.rotation = (this.oldVisualRotation = (this.visualRotation = base.transform.rotation));
	}

	// Token: 0x06000FE3 RID: 4067 RVA: 0x00052A60 File Offset: 0x00050C60
	public override void ApplyPosition()
	{
		base.transform.position = (this.interpolatedPosition = this.position);
	}

	// Token: 0x06000FE4 RID: 4068 RVA: 0x00052A88 File Offset: 0x00050C88
	public override void ApplyPosition(float t)
	{
		base.transform.position = (this.interpolatedPosition = Vector3.Lerp(this.oldPosition, this.position, t));
	}

	// Token: 0x06000FE5 RID: 4069 RVA: 0x0000DBA8 File Offset: 0x0000BDA8
	public override void ApplyRotation()
	{
		base.transform.rotation = this.visualRotation;
	}

	// Token: 0x06000FE6 RID: 4070 RVA: 0x0000DBBB File Offset: 0x0000BDBB
	public override void ApplyRotation(float t)
	{
		base.transform.rotation = Quaternion.SlerpUnclamped(this.oldVisualRotation, this.visualRotation, t);
	}

	// Token: 0x04001484 RID: 5252
	private static Vector3 windForce;

	// Token: 0x04001485 RID: 5253
	private static float windForceUpdateTime;

	// Token: 0x04001486 RID: 5254
	private static readonly Vector3 windDirection = Vector3.right;

	// Token: 0x04001487 RID: 5255
	private const float strength1 = 2.5f;

	// Token: 0x04001488 RID: 5256
	private const float frequency1 = 7f;

	// Token: 0x04001489 RID: 5257
	private const float strength2 = 3.5f;

	// Token: 0x0400148A RID: 5258
	private const float frequency2 = 5f;

	// Token: 0x0400148B RID: 5259
	private Vector3 initialPositionLocal;

	// Token: 0x0400148C RID: 5260
	private Vector3 initialDirectionLocal;

	// Token: 0x0400148D RID: 5261
	private Vector3 initialChildDirectionLocal;

	// Token: 0x0400148E RID: 5262
	private Quaternion initialLocalRotation;

	// Token: 0x0400148F RID: 5263
	public Vector3 oldPosition;

	// Token: 0x04001490 RID: 5264
	public Vector3 velocity;

	// Token: 0x04001491 RID: 5265
	private Quaternion oldVisualRotation;

	// Token: 0x04001492 RID: 5266
	private Quaternion visualRotation;

	// Token: 0x04001493 RID: 5267
	private Transform parent;

	// Token: 0x04001494 RID: 5268
	private WobbleBoneBase parentBone;

	// Token: 0x04001495 RID: 5269
	private bool hasParentBone;

	// Token: 0x04001496 RID: 5270
	[Space]
	[Tooltip("~500-1000")]
	public float spring = 500f;

	// Token: 0x04001497 RID: 5271
	[Range(0f, 1f)]
	public float compression = 0.5f;

	// Token: 0x04001498 RID: 5272
	[Range(1f, 3f)]
	public float expansion = 1.5f;

	// Token: 0x04001499 RID: 5273
	private float minDistance;

	// Token: 0x0400149A RID: 5274
	private float maxDistance;

	// Token: 0x0400149B RID: 5275
	public float damper = 20f;

	// Token: 0x0400149C RID: 5276
	public float gravity = 1f;

	// Token: 0x0400149D RID: 5277
	[Range(0f, 5f)]
	public float windStrength;

	// Token: 0x0400149E RID: 5278
	[Space]
	public bool ignoreRotation;

	// Token: 0x0400149F RID: 5279
	private bool initialized;

	// Token: 0x040014A0 RID: 5280
	private WobbleBoneBase[] childBones;

	// Token: 0x040014A1 RID: 5281
	private Vector3 parentPosition;

	// Token: 0x040014A2 RID: 5282
	private Quaternion parentRotation;

	// Token: 0x040014A3 RID: 5283
	private Vector3 directionGoal;

	// Token: 0x040014A4 RID: 5284
	private Vector3 positionGoal;
}
