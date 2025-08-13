using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000B8 RID: 184
[AddComponentMenu("Contextual/Bendy Pole")]
public class BendyClimbingPole : MonoBehaviour
{
	// Token: 0x060002DC RID: 732 RVA: 0x00021F30 File Offset: 0x00020130
	private void Awake()
	{
		this.climbingPole.onEnable.AddListener(new UnityAction(this.OnClimbingPoleEnabled));
		this.weightPoint = (this.endPoint = this.climbingPole.GetPosition((float)(this.climbingPole.positions.Length - 1)));
		this.startPoint = this.dynamicPivot.position;
		this.totalLength = (this.endPoint - this.startPoint).magnitude;
		this.initialRotation = this.dynamicPivot.rotation;
		base.enabled = false;
	}

	// Token: 0x060002DD RID: 733 RVA: 0x000043EB File Offset: 0x000025EB
	private void OnClimbingPoleEnabled()
	{
		base.enabled = true;
	}

	// Token: 0x060002DE RID: 734 RVA: 0x00021FCC File Offset: 0x000201CC
	private void OnEnable()
	{
		this.staticRenderer.enabled = false;
		if (this.dynamicRenderer != null)
		{
			this.dynamicRenderer.enabled = true;
		}
		this.weightPoint = this.endPoint;
		this.velocity = Vector3.zero;
		this.lockToTightrope = false;
		this.UpdateAnchor();
		this.onEnable.Invoke();
	}

	// Token: 0x060002DF RID: 735 RVA: 0x00022030 File Offset: 0x00020230
	private void LateUpdate()
	{
		if (this.climbingPole.enabled)
		{
			Vector3 vector = Player.RawPosition - this.climbingPole.GetOffset(this.climbingPole.t) * this.climbingPole.direction;
			if (!this.lockToTightrope && (vector.y <= this.weightPoint.y || this.climbingPole.lerpToPosition == 1f))
			{
				this.lockToTightrope = true;
			}
			if (this.lockToTightrope)
			{
				this.weightPoint = vector;
				this.bendAmount = 1f;
			}
			else
			{
				float y = this.climbingPole.ClosestPointOnPath(vector).y;
				float num = Mathf.InverseLerp(y + 0.5f, y, vector.y);
				this.bendAmount = num;
				this.weightPoint = Vector3.Lerp(this.weightPoint, vector, num);
				if (num >= 0.95f)
				{
					this.lockToTightrope = true;
				}
			}
			this.velocity = Vector3.zero;
			this.isPlayerOnBalanceBeam = true;
		}
		else
		{
			this.lockToTightrope = false;
			if (Player.movement.JustJumped && this.isPlayerOnBalanceBeam)
			{
				this.velocity = 5f * Vector3.down;
			}
			this.isPlayerOnBalanceBeam = false;
			Vector3 vector2 = this.endPoint - this.weightPoint;
			Vector3 vector3 = this.springStrength * vector2;
			this.velocity = Vector3.MoveTowards(this.velocity, Vector3.zero, Time.deltaTime * this.damperStrength);
			this.velocity += Time.deltaTime * vector3;
			this.weightPoint += Time.deltaTime * this.velocity;
			Vector3 vector4 = this.weightPoint - this.startPoint;
			this.weightPoint = this.startPoint + Vector3.ClampMagnitude(vector4, this.totalLength);
			this.bendAmount = Mathf.MoveTowards(this.bendAmount, 0f, Time.deltaTime * 2f);
			if (this.bendAmount == 0f)
			{
				if (this.dynamicRenderer != null)
				{
					this.dynamicRenderer.enabled = false;
				}
				this.staticRenderer.enabled = true;
				base.enabled = false;
			}
		}
		if (this.lockToVertical)
		{
			this.weightPoint = this.AlongLine(this.weightPoint);
		}
		this.UpdateAnchor();
	}

	// Token: 0x060002E0 RID: 736 RVA: 0x00022298 File Offset: 0x00020498
	private Vector3 AlongLine(Vector3 point)
	{
		Vector3 vector = this.climbingPole.ClosestPointOnPath(point);
		vector.y = point.y;
		return vector;
	}

	// Token: 0x060002E1 RID: 737 RVA: 0x000222C0 File Offset: 0x000204C0
	private void UpdateAnchor()
	{
		float closestInterpolated = this.climbingPole.GetClosestInterpolated(this.weightPoint);
		if (closestInterpolated < 1f)
		{
			this.bendAmount = Mathf.InverseLerp(0f, 1f, closestInterpolated);
		}
		Vector3 vector = Vector3.Lerp(this.initialPositions[Mathf.FloorToInt(closestInterpolated)], this.initialPositions[Mathf.CeilToInt(closestInterpolated)], closestInterpolated - Mathf.Floor(closestInterpolated));
		vector = base.transform.TransformPoint(vector);
		Vector3 vector2 = vector - this.startPoint;
		Vector3 vector3 = this.weightPoint - this.startPoint;
		Quaternion quaternion = Quaternion.FromToRotation(vector2, vector3);
		this.dynamicPivot.rotation = Quaternion.Slerp(this.initialRotation, quaternion * this.initialRotation, this.bendAmount);
	}

	// Token: 0x040003EE RID: 1006
	public ClimbingPole climbingPole;

	// Token: 0x040003EF RID: 1007
	public Renderer staticRenderer;

	// Token: 0x040003F0 RID: 1008
	public Renderer dynamicRenderer;

	// Token: 0x040003F1 RID: 1009
	public Transform dynamicPivot;

	// Token: 0x040003F2 RID: 1010
	public Vector3[] initialPositions;

	// Token: 0x040003F3 RID: 1011
	private Quaternion initialRotation;

	// Token: 0x040003F4 RID: 1012
	public bool lockToVertical;

	// Token: 0x040003F5 RID: 1013
	private Vector3 weightPoint;

	// Token: 0x040003F6 RID: 1014
	private float weightPointOnPath;

	// Token: 0x040003F7 RID: 1015
	private Vector3 startPoint;

	// Token: 0x040003F8 RID: 1016
	private Vector3 endPoint;

	// Token: 0x040003F9 RID: 1017
	private float totalLength;

	// Token: 0x040003FA RID: 1018
	public float springStrength = 4f;

	// Token: 0x040003FB RID: 1019
	public float damperStrength = 0.5f;

	// Token: 0x040003FC RID: 1020
	private Vector3 velocity;

	// Token: 0x040003FD RID: 1021
	[Range(0.0001f, 1f)]
	public float stiffness = 0.5f;

	// Token: 0x040003FE RID: 1022
	private float bendAmount;

	// Token: 0x040003FF RID: 1023
	public UnityEvent onEnable;

	// Token: 0x04000400 RID: 1024
	private bool isPlayerOnBalanceBeam;

	// Token: 0x04000401 RID: 1025
	private bool lockToTightrope;
}
