using System;
using System.Collections.Generic;
using UnityEngine;

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

	private static Vector3 windForce;

	private static float windForceUpdateTime;

	private static readonly Vector3 windDirection = Vector3.right;

	private const float strength1 = 2.5f;

	private const float frequency1 = 7f;

	private const float strength2 = 3.5f;

	private const float frequency2 = 5f;

	private Vector3 initialPosition;

	private Vector3 initialDirectionLocal;

	private Vector3 initialChildDirectionLocal;

	private Quaternion initialLocalRotation;

	private float distanceGoal;

	private float distance;

	private float oldDistance;

	private float interpolatedDistance;

	public Vector3 direction;

	public Vector3 velocity;

	private Vector3 oldPosition;

	private Vector3 oldDirection;

	private Quaternion oldVisualRotation;

	private Quaternion visualRotation;

	private Transform parent;

	private WobbleBoneBase parentBone;

	private bool hasParentBone;

	[Space]
	[Tooltip("~0.25 - 2")]
	public float angularSpring = 1f;

	[Tooltip("~50-200")]
	public float pushSpring = 100f;

	[Tooltip("~500-1000")]
	public float pullSpring = 500f;

	[Range(0f, 1f)]
	public float compression = 0.5f;

	[Range(1f, 2f)]
	public float expansion = 1.1f;

	private float minDistance;

	private float maxDistance;

	[Range(0f, 180f)]
	public float maxAngle = 70f;

	[Range(0f, 1f)]
	public float maxAngleBounciness = 0.1f;

	public float damper = 20f;

	public float gravity = 1f;

	[Range(0f, 5f)]
	public float windStrength;

	[Space]
	public bool ignoreRotation;

	private bool initialized;

	private WobbleBoneBase[] childBones;

	[Space]
	public bool lockXPosition;

	public bool lockYPosition;

	public bool lockZPosition;

	private Vector3 parentPosition;

	private Quaternion parentRotation;

	private Vector3 directionGoal;

	private Vector3 positionGoal;
}
