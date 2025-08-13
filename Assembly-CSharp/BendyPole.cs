using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000093 RID: 147
[AddComponentMenu("Contextual/Bendy Beam")]
public class BendyPole : MonoBehaviour
{
	// Token: 0x06000299 RID: 665 RVA: 0x0000E7CC File Offset: 0x0000C9CC
	private void Awake()
	{
		this.balanceBeam.onEnable.AddListener(new UnityAction(this.OnBalanceBeamEnabled));
		this.weightPoint = (this.endPoint = base.transform.TransformPoint(this.initialPositions[this.initialPositions.Length - 1]));
		this.pivotPoint = this.dynamicPivot.position;
		this.totalLength = (this.endPoint - this.pivotPoint).magnitude;
		this.initialRotation = this.dynamicPivot.rotation;
		if (this.attachedPole != null)
		{
			this.attachedPole.onEnable.AddListener(new UnityAction(this.OnBalanceBeamEnabled));
			this.attachedPosition = this.attachedPole.dynamicPivot.InverseTransformPoint(this.dynamicPivot.transform.position);
			this.attachedRotation = this.attachedPole.dynamicPivot.rotation.Inverse();
		}
		base.enabled = false;
	}

	// Token: 0x0600029A RID: 666 RVA: 0x0000E8D7 File Offset: 0x0000CAD7
	private void OnBalanceBeamEnabled()
	{
		base.enabled = true;
	}

	// Token: 0x0600029B RID: 667 RVA: 0x0000E8E0 File Offset: 0x0000CAE0
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

	// Token: 0x0600029C RID: 668 RVA: 0x0000E944 File Offset: 0x0000CB44
	private void LateUpdate()
	{
		if (this.balanceBeam.enabled)
		{
			Vector3 rawPosition = Player.RawPosition;
			if (!this.lockToTightrope && (rawPosition.y <= this.weightPoint.y || this.balanceBeam.lerpToPosition == 1f))
			{
				this.lockToTightrope = true;
			}
			if (this.lockToTightrope)
			{
				this.weightPoint = rawPosition;
				this.bendAmount = 1f;
			}
			else
			{
				float y = this.balanceBeam.ClosestPointOnPath(rawPosition).y;
				float num = Mathf.InverseLerp(y + 0.5f, y, rawPosition.y);
				this.bendAmount = num;
				this.weightPoint = Vector3.Lerp(this.weightPoint, rawPosition, num);
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
			Vector3 vector = this.endPoint - this.weightPoint;
			Vector3 vector2 = this.springStrength * vector;
			this.velocity = Vector3.MoveTowards(this.velocity, Vector3.zero, Time.deltaTime * this.damperStrength);
			this.velocity += Time.deltaTime * vector2;
			this.weightPoint += Time.deltaTime * this.velocity;
			Vector3 vector3 = this.weightPoint - this.pivotPoint;
			this.weightPoint = this.pivotPoint + Vector3.ClampMagnitude(vector3, this.totalLength);
			this.bendAmount = Mathf.MoveTowards(this.bendAmount, 0f, Time.deltaTime * 2f);
			if (this.bendAmount == 0f && (this.attachedPole == null || !this.attachedPole.enabled))
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

	// Token: 0x0600029D RID: 669 RVA: 0x0000EB9C File Offset: 0x0000CD9C
	private Vector3 AlongLine(Vector3 point)
	{
		Vector3 vector = this.balanceBeam.ClosestPointOnPath(point);
		vector.y = point.y;
		return vector;
	}

	// Token: 0x0600029E RID: 670 RVA: 0x0000EBC4 File Offset: 0x0000CDC4
	public Vector3 ClosestPointOnPath(Vector3 point)
	{
		float closestInterpolated = this.balanceBeam.GetClosestInterpolated(point);
		return this.GetPointOnPath(closestInterpolated);
	}

	// Token: 0x0600029F RID: 671 RVA: 0x0000EBE8 File Offset: 0x0000CDE8
	public Vector3 GetPointOnPath(float t)
	{
		Vector3 vector = Vector3.Lerp(this.initialPositions[Mathf.FloorToInt(t)], this.initialPositions[Mathf.CeilToInt(t)], t - Mathf.Floor(t));
		return base.transform.TransformPoint(vector);
	}

	// Token: 0x060002A0 RID: 672 RVA: 0x0000EC34 File Offset: 0x0000CE34
	private void UpdateAnchor()
	{
		float closestInterpolated = this.balanceBeam.GetClosestInterpolated(this.weightPoint);
		Vector3 vector = this.GetPointOnPath(closestInterpolated) - this.dynamicPivot.position;
		Vector3 vector2 = this.weightPoint - this.dynamicPivot.position;
		Quaternion quaternion = Quaternion.FromToRotation(vector, vector2);
		this.dynamicPivot.rotation = Quaternion.Slerp(this.initialRotation, quaternion * this.initialRotation, this.bendAmount);
		if (this.attachedPole != null)
		{
			this.dynamicPivot.position = this.attachedPole.dynamicPivot.TransformPoint(this.attachedPosition);
			this.dynamicPivot.rotation = this.attachedRotation * this.attachedPole.dynamicPivot.rotation * this.dynamicPivot.rotation;
		}
	}

	// Token: 0x04000369 RID: 873
	public BalanceBeam balanceBeam;

	// Token: 0x0400036A RID: 874
	public BendyClimbingPole attachedPole;

	// Token: 0x0400036B RID: 875
	private Vector3 attachedPosition;

	// Token: 0x0400036C RID: 876
	private Quaternion attachedRotation;

	// Token: 0x0400036D RID: 877
	public Renderer staticRenderer;

	// Token: 0x0400036E RID: 878
	public Renderer dynamicRenderer;

	// Token: 0x0400036F RID: 879
	public Transform dynamicPivot;

	// Token: 0x04000370 RID: 880
	public Vector3[] initialPositions;

	// Token: 0x04000371 RID: 881
	private Quaternion initialRotation;

	// Token: 0x04000372 RID: 882
	public bool lockToVertical;

	// Token: 0x04000373 RID: 883
	private Vector3 weightPoint;

	// Token: 0x04000374 RID: 884
	private float weightPointOnPath;

	// Token: 0x04000375 RID: 885
	private Vector3 pivotPoint;

	// Token: 0x04000376 RID: 886
	private Vector3 endPoint;

	// Token: 0x04000377 RID: 887
	private float totalLength;

	// Token: 0x04000378 RID: 888
	public float springStrength = 4f;

	// Token: 0x04000379 RID: 889
	public float damperStrength = 0.5f;

	// Token: 0x0400037A RID: 890
	private Vector3 velocity;

	// Token: 0x0400037B RID: 891
	[Range(0.0001f, 1f)]
	public float stiffness = 0.5f;

	// Token: 0x0400037C RID: 892
	private float bendAmount;

	// Token: 0x0400037D RID: 893
	public UnityEvent onEnable;

	// Token: 0x0400037E RID: 894
	private bool isPlayerOnBalanceBeam;

	// Token: 0x0400037F RID: 895
	private bool lockToTightrope;
}
