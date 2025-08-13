using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000260 RID: 608
[AddComponentMenu("Wobble/Bone")]
public class WobbleBone : WobbleBoneBase
{
	// Token: 0x06000D0D RID: 3341 RVA: 0x0003ED1C File Offset: 0x0003CF1C
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(base.transform.parent.position, base.transform.position);
		Gizmos.DrawWireSphere(base.transform.position, 0.02f);
	}

	// Token: 0x06000D0E RID: 3342 RVA: 0x0003ED68 File Offset: 0x0003CF68
	private void UpdateWindForce()
	{
		WobbleBone.windForceUpdateTime = Time.time;
		WobbleBone.windForce = ((Mathf.Sin(WobbleBone.windForceUpdateTime * 7f) + 1f) * 2.5f + (Mathf.Sin(WobbleBone.windForceUpdateTime * 5f) + 1f) * 3.5f) * WobbleBone.windDirection;
	}

	// Token: 0x06000D0F RID: 3343 RVA: 0x0003EDC8 File Offset: 0x0003CFC8
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

	// Token: 0x06000D10 RID: 3344 RVA: 0x0003F040 File Offset: 0x0003D240
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

	// Token: 0x06000D11 RID: 3345 RVA: 0x0003F104 File Offset: 0x0003D304
	private Vector3 ProjectOnNormalized(Vector3 vector, Vector3 normal)
	{
		return vector - Vector3.Dot(vector, normal) * normal;
	}

	// Token: 0x06000D12 RID: 3346 RVA: 0x0003F119 File Offset: 0x0003D319
	private Vector3 LockAxisDelta(Vector3 delta, Vector3 axis)
	{
		axis = this.parentRotation * axis;
		return Vector3.ProjectOnPlane(delta, axis);
	}

	// Token: 0x06000D13 RID: 3347 RVA: 0x0003F130 File Offset: 0x0003D330
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

	// Token: 0x06000D14 RID: 3348 RVA: 0x0003F198 File Offset: 0x0003D398
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

	// Token: 0x06000D15 RID: 3349 RVA: 0x0003F5F0 File Offset: 0x0003D7F0
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

	// Token: 0x06000D16 RID: 3350 RVA: 0x0003F64C File Offset: 0x0003D84C
	public override void Reacclimate()
	{
		this.velocity = Vector3.zero;
		this.distance = (this.oldDistance = (this.interpolatedDistance = this.distanceGoal));
		this.position = (this.oldPosition = (this.interpolatedPosition = base.transform.position));
		this.rotation = (this.oldVisualRotation = (this.visualRotation = base.transform.rotation));
	}

	// Token: 0x06000D17 RID: 3351 RVA: 0x0003F6C8 File Offset: 0x0003D8C8
	private void LockAxis()
	{
	}

	// Token: 0x06000D18 RID: 3352 RVA: 0x0003F6D8 File Offset: 0x0003D8D8
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

	// Token: 0x06000D19 RID: 3353 RVA: 0x0003F754 File Offset: 0x0003D954
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

	// Token: 0x06000D1A RID: 3354 RVA: 0x0003F7E4 File Offset: 0x0003D9E4
	public override void ApplyRotation()
	{
		base.transform.rotation = this.visualRotation;
	}

	// Token: 0x06000D1B RID: 3355 RVA: 0x0003F7F7 File Offset: 0x0003D9F7
	public override void ApplyRotation(float t)
	{
		base.transform.rotation = Quaternion.SlerpUnclamped(this.oldVisualRotation, this.visualRotation, t);
	}

	// Token: 0x0400111F RID: 4383
	private static Vector3 windForce;

	// Token: 0x04001120 RID: 4384
	private static float windForceUpdateTime;

	// Token: 0x04001121 RID: 4385
	private static readonly Vector3 windDirection = Vector3.right;

	// Token: 0x04001122 RID: 4386
	private const float strength1 = 2.5f;

	// Token: 0x04001123 RID: 4387
	private const float frequency1 = 7f;

	// Token: 0x04001124 RID: 4388
	private const float strength2 = 3.5f;

	// Token: 0x04001125 RID: 4389
	private const float frequency2 = 5f;

	// Token: 0x04001126 RID: 4390
	private Vector3 initialPosition;

	// Token: 0x04001127 RID: 4391
	private Vector3 initialDirectionLocal;

	// Token: 0x04001128 RID: 4392
	private Vector3 initialChildDirectionLocal;

	// Token: 0x04001129 RID: 4393
	private Quaternion initialLocalRotation;

	// Token: 0x0400112A RID: 4394
	private float distanceGoal;

	// Token: 0x0400112B RID: 4395
	private float distance;

	// Token: 0x0400112C RID: 4396
	private float oldDistance;

	// Token: 0x0400112D RID: 4397
	private float interpolatedDistance;

	// Token: 0x0400112E RID: 4398
	public Vector3 direction;

	// Token: 0x0400112F RID: 4399
	public Vector3 velocity;

	// Token: 0x04001130 RID: 4400
	private Vector3 oldPosition;

	// Token: 0x04001131 RID: 4401
	private Vector3 oldDirection;

	// Token: 0x04001132 RID: 4402
	private Quaternion oldVisualRotation;

	// Token: 0x04001133 RID: 4403
	private Quaternion visualRotation;

	// Token: 0x04001134 RID: 4404
	private Transform parent;

	// Token: 0x04001135 RID: 4405
	private WobbleBoneBase parentBone;

	// Token: 0x04001136 RID: 4406
	private bool hasParentBone;

	// Token: 0x04001137 RID: 4407
	[Space]
	[Tooltip("~0.25 - 2")]
	public float angularSpring = 1f;

	// Token: 0x04001138 RID: 4408
	[Tooltip("~50-200")]
	public float pushSpring = 100f;

	// Token: 0x04001139 RID: 4409
	[Tooltip("~500-1000")]
	public float pullSpring = 500f;

	// Token: 0x0400113A RID: 4410
	[Range(0f, 1f)]
	public float compression = 0.5f;

	// Token: 0x0400113B RID: 4411
	[Range(1f, 2f)]
	public float expansion = 1.1f;

	// Token: 0x0400113C RID: 4412
	private float minDistance;

	// Token: 0x0400113D RID: 4413
	private float maxDistance;

	// Token: 0x0400113E RID: 4414
	[Range(0f, 180f)]
	public float maxAngle = 70f;

	// Token: 0x0400113F RID: 4415
	[Range(0f, 1f)]
	public float maxAngleBounciness = 0.1f;

	// Token: 0x04001140 RID: 4416
	public float damper = 20f;

	// Token: 0x04001141 RID: 4417
	public float gravity = 1f;

	// Token: 0x04001142 RID: 4418
	[Range(0f, 5f)]
	public float windStrength;

	// Token: 0x04001143 RID: 4419
	[Space]
	public bool ignoreRotation;

	// Token: 0x04001144 RID: 4420
	private bool initialized;

	// Token: 0x04001145 RID: 4421
	private WobbleBoneBase[] childBones;

	// Token: 0x04001146 RID: 4422
	[Space]
	public bool lockXPosition;

	// Token: 0x04001147 RID: 4423
	public bool lockYPosition;

	// Token: 0x04001148 RID: 4424
	public bool lockZPosition;

	// Token: 0x04001149 RID: 4425
	private Vector3 parentPosition;

	// Token: 0x0400114A RID: 4426
	private Quaternion parentRotation;

	// Token: 0x0400114B RID: 4427
	private Vector3 directionGoal;

	// Token: 0x0400114C RID: 4428
	private Vector3 positionGoal;
}
