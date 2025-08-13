using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000327 RID: 807
[AddComponentMenu("Wobble/Bone")]
public class WobbleBone : WobbleBoneBase
{
	// Token: 0x06000FBB RID: 4027 RVA: 0x00051650 File Offset: 0x0004F850
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(base.transform.parent.position, base.transform.position);
		Gizmos.DrawWireSphere(base.transform.position, 0.02f);
	}

	// Token: 0x06000FBC RID: 4028 RVA: 0x0005169C File Offset: 0x0004F89C
	private void UpdateWindForce()
	{
		WobbleBone.windForceUpdateTime = Time.time;
		WobbleBone.windForce = ((Mathf.Sin(WobbleBone.windForceUpdateTime * 7f) + 1f) * 2.5f + (Mathf.Sin(WobbleBone.windForceUpdateTime * 5f) + 1f) * 3.5f) * WobbleBone.windDirection;
	}

	// Token: 0x06000FBD RID: 4029 RVA: 0x000516FC File Offset: 0x0004F8FC
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
		this.initialPosition = base.transform.localPosition;
		this.oldDirection = (this.interpolatedDirection = (this.direction = (base.transform.position - this.parent.position).normalized));
		this.initialDirectionLocal = this.parent.rotation.Inverse() * this.direction;
		this.oldDistance = (this.distance = (this.interpolatedDistance = (this.distanceGoal = Vector3.Distance(this.parent.position, base.transform.position))));
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
		this.minDistance = this.distanceGoal * this.compression;
		this.maxDistance = this.distanceGoal * this.expansion;
	}

	// Token: 0x06000FBE RID: 4030 RVA: 0x00051974 File Offset: 0x0004FB74
	private void UpdateState()
	{
		this.oldPosition = this.position;
		this.oldVisualRotation = this.visualRotation;
		this.oldDirection = this.direction;
		this.oldDistance = this.distance;
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
		this.directionGoal = this.parentRotation * this.initialDirectionLocal;
		this.positionGoal = this.parentPosition + this.distanceGoal * this.directionGoal;
	}

	// Token: 0x06000FBF RID: 4031 RVA: 0x0000DAD5 File Offset: 0x0000BCD5
	private Vector3 ProjectOnNormalized(Vector3 vector, Vector3 normal)
	{
		return vector - Vector3.Dot(vector, normal) * normal;
	}

	// Token: 0x06000FC0 RID: 4032 RVA: 0x0000DAEA File Offset: 0x0000BCEA
	private Vector3 LockAxisDelta(Vector3 delta, Vector3 axis)
	{
		axis = this.parentRotation * axis;
		return Vector3.ProjectOnPlane(delta, axis);
	}

	// Token: 0x06000FC1 RID: 4033 RVA: 0x00051A38 File Offset: 0x0004FC38
	private Vector3 LockVectorAxes(Vector3 position)
	{
		Vector3 vector = position - this.positionGoal;
		if (this.lockXPosition)
		{
			vector = this.LockAxisDelta(vector, Vector3.right);
		}
		if (this.lockYPosition)
		{
			vector = this.LockAxisDelta(vector, Vector3.up);
		}
		if (this.lockZPosition)
		{
			vector = this.LockAxisDelta(vector, Vector3.forward);
		}
		return this.positionGoal + vector;
	}

	// Token: 0x06000FC2 RID: 4034 RVA: 0x00051AA0 File Offset: 0x0004FCA0
	public override void RunWobbleUpdate()
	{
		if (!this.initialized)
		{
			return;
		}
		this.UpdateState();
		if (WobbleBone.windForceUpdateTime != Time.time)
		{
			this.UpdateWindForce();
		}
		float num = Time.deltaTime;
		if (!Time.inFixedTimeStep && num > Time.fixedDeltaTime)
		{
			num = Time.fixedDeltaTime;
		}
		this.forces += this.windStrength * WobbleBone.windForce;
		Vector3 vector = Vector3.zero;
		Vector3 vector2 = this.position - this.parentPosition;
		this.distance = vector2.magnitude;
		this.direction = vector2 / this.distance;
		float num2 = Vector3.Angle(this.directionGoal, this.direction);
		Vector3 vector3 = this.positionGoal - this.position;
		Vector3 vector4 = Vector3.Normalize(this.ProjectOnNormalized(vector3, this.direction));
		vector += this.angularSpring * num2 * vector4;
		float num3 = this.distanceGoal - this.distance;
		vector += ((num3 > 0f) ? this.pushSpring : this.pullSpring) * num3 * this.direction;
		this.forces += vector;
		this.forces += this.gravity * Physics.gravity - this.damper * this.velocity;
		this.velocity += num * this.forces;
		this.position += num * this.velocity;
		this.position = this.LockVectorAxes(this.position);
		vector2 = this.position - this.parentPosition;
		this.distance = vector2.magnitude;
		this.direction = vector2 / this.distance;
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		if (this.distance <= this.minDistance || this.distance >= this.maxDistance)
		{
			flag = true;
			flag3 = true;
			this.distance = Mathf.Clamp(this.distance, this.minDistance, this.maxDistance);
			num3 = this.distanceGoal - this.distance;
		}
		if (Vector3.Angle(this.direction, this.directionGoal) >= this.maxAngle)
		{
			flag = true;
			flag2 = true;
			this.direction = Vector3.RotateTowards(this.directionGoal, this.direction, this.maxAngle * 0.017453292f, 1f);
			vector4 = Vector3.Normalize(this.ProjectOnNormalized(vector3, this.direction));
		}
		if (flag)
		{
			this.position = this.parentPosition + this.distance * this.direction;
			Vector3 vector5 = Vector3.Cross(this.direction, vector4);
			float num4 = Vector3.Dot(this.velocity, this.direction);
			float num5 = Vector3.Dot(this.velocity, vector4);
			float num6 = Vector3.Dot(this.velocity, vector5);
			if (flag3 && Mathf.Sign(num3) * num4 < 0f)
			{
				num4 = this.maxAngleBounciness * -num4;
			}
			if (flag2 && num5 < 0f)
			{
				num5 = this.maxAngleBounciness * Mathf.Abs(num5);
			}
			this.velocity = num4 * this.direction + num5 * vector4 + num6 * vector5;
		}
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

	// Token: 0x06000FC3 RID: 4035 RVA: 0x00051EF8 File Offset: 0x000500F8
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

	// Token: 0x06000FC4 RID: 4036 RVA: 0x00051F54 File Offset: 0x00050154
	public override void Reacclimate()
	{
		this.velocity = Vector3.zero;
		this.distance = (this.oldDistance = (this.interpolatedDistance = this.distanceGoal));
		this.position = (this.oldPosition = (this.interpolatedPosition = base.transform.position));
		this.rotation = (this.oldVisualRotation = (this.visualRotation = base.transform.rotation));
	}

	// Token: 0x06000FC5 RID: 4037 RVA: 0x00051FD0 File Offset: 0x000501D0
	private void LockAxis()
	{
	}

	// Token: 0x06000FC6 RID: 4038 RVA: 0x00051FE0 File Offset: 0x000501E0
	public override void ApplyPosition()
	{
		Vector3 vector;
		if (this.hasParentBone)
		{
			vector = this.parentBone.interpolatedPosition;
		}
		else
		{
			vector = this.parent.position;
		}
		this.interpolatedDistance = this.distance;
		this.interpolatedDirection = this.direction;
		this.interpolatedPosition = vector + this.interpolatedDistance * this.interpolatedDirection;
		base.transform.position = this.interpolatedPosition;
		this.LockAxis();
	}

	// Token: 0x06000FC7 RID: 4039 RVA: 0x0005205C File Offset: 0x0005025C
	public override void ApplyPosition(float t)
	{
		Vector3 vector;
		if (this.hasParentBone)
		{
			vector = this.parentBone.interpolatedPosition;
		}
		else
		{
			vector = this.parent.position;
		}
		this.interpolatedDirection = Vector3.SlerpUnclamped(this.oldDirection, this.direction, t);
		this.interpolatedDistance = Mathf.LerpUnclamped(this.oldDistance, this.distance, t);
		base.transform.position = (this.interpolatedPosition = vector + this.interpolatedDistance * this.interpolatedDirection);
		this.LockAxis();
	}

	// Token: 0x06000FC8 RID: 4040 RVA: 0x0000DB01 File Offset: 0x0000BD01
	public override void ApplyRotation()
	{
		base.transform.rotation = this.visualRotation;
	}

	// Token: 0x06000FC9 RID: 4041 RVA: 0x0000DB14 File Offset: 0x0000BD14
	public override void ApplyRotation(float t)
	{
		base.transform.rotation = Quaternion.SlerpUnclamped(this.oldVisualRotation, this.visualRotation, t);
	}

	// Token: 0x04001442 RID: 5186
	private static Vector3 windForce;

	// Token: 0x04001443 RID: 5187
	private static float windForceUpdateTime;

	// Token: 0x04001444 RID: 5188
	private static readonly Vector3 windDirection = Vector3.right;

	// Token: 0x04001445 RID: 5189
	private const float strength1 = 2.5f;

	// Token: 0x04001446 RID: 5190
	private const float frequency1 = 7f;

	// Token: 0x04001447 RID: 5191
	private const float strength2 = 3.5f;

	// Token: 0x04001448 RID: 5192
	private const float frequency2 = 5f;

	// Token: 0x04001449 RID: 5193
	private Vector3 initialPosition;

	// Token: 0x0400144A RID: 5194
	private Vector3 initialDirectionLocal;

	// Token: 0x0400144B RID: 5195
	private Vector3 initialChildDirectionLocal;

	// Token: 0x0400144C RID: 5196
	private Quaternion initialLocalRotation;

	// Token: 0x0400144D RID: 5197
	private float distanceGoal;

	// Token: 0x0400144E RID: 5198
	private float distance;

	// Token: 0x0400144F RID: 5199
	private float oldDistance;

	// Token: 0x04001450 RID: 5200
	private float interpolatedDistance;

	// Token: 0x04001451 RID: 5201
	public Vector3 direction;

	// Token: 0x04001452 RID: 5202
	public Vector3 velocity;

	// Token: 0x04001453 RID: 5203
	private Vector3 oldPosition;

	// Token: 0x04001454 RID: 5204
	private Vector3 oldDirection;

	// Token: 0x04001455 RID: 5205
	private Quaternion oldVisualRotation;

	// Token: 0x04001456 RID: 5206
	private Quaternion visualRotation;

	// Token: 0x04001457 RID: 5207
	private Transform parent;

	// Token: 0x04001458 RID: 5208
	private WobbleBoneBase parentBone;

	// Token: 0x04001459 RID: 5209
	private bool hasParentBone;

	// Token: 0x0400145A RID: 5210
	[Space]
	[Tooltip("~0.25 - 2")]
	public float angularSpring = 1f;

	// Token: 0x0400145B RID: 5211
	[Tooltip("~50-200")]
	public float pushSpring = 100f;

	// Token: 0x0400145C RID: 5212
	[Tooltip("~500-1000")]
	public float pullSpring = 500f;

	// Token: 0x0400145D RID: 5213
	[Range(0f, 1f)]
	public float compression = 0.5f;

	// Token: 0x0400145E RID: 5214
	[Range(1f, 2f)]
	public float expansion = 1.1f;

	// Token: 0x0400145F RID: 5215
	private float minDistance;

	// Token: 0x04001460 RID: 5216
	private float maxDistance;

	// Token: 0x04001461 RID: 5217
	[Range(0f, 180f)]
	public float maxAngle = 70f;

	// Token: 0x04001462 RID: 5218
	[Range(0f, 1f)]
	public float maxAngleBounciness = 0.1f;

	// Token: 0x04001463 RID: 5219
	public float damper = 20f;

	// Token: 0x04001464 RID: 5220
	public float gravity = 1f;

	// Token: 0x04001465 RID: 5221
	[Range(0f, 5f)]
	public float windStrength;

	// Token: 0x04001466 RID: 5222
	[Space]
	public bool ignoreRotation;

	// Token: 0x04001467 RID: 5223
	private bool initialized;

	// Token: 0x04001468 RID: 5224
	private WobbleBoneBase[] childBones;

	// Token: 0x04001469 RID: 5225
	[Space]
	public bool lockXPosition;

	// Token: 0x0400146A RID: 5226
	public bool lockYPosition;

	// Token: 0x0400146B RID: 5227
	public bool lockZPosition;

	// Token: 0x0400146C RID: 5228
	private Vector3 parentPosition;

	// Token: 0x0400146D RID: 5229
	private Quaternion parentRotation;

	// Token: 0x0400146E RID: 5230
	private Vector3 directionGoal;

	// Token: 0x0400146F RID: 5231
	private Vector3 positionGoal;
}
