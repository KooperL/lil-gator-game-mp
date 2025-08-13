using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000B9 RID: 185
[AddComponentMenu("Contextual/Bendy Beam")]
public class BendyPole : MonoBehaviour
{
	// Token: 0x060002E3 RID: 739 RVA: 0x00022388 File Offset: 0x00020588
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

	// Token: 0x060002E4 RID: 740 RVA: 0x000043EB File Offset: 0x000025EB
	private void OnBalanceBeamEnabled()
	{
		base.enabled = true;
	}

	// Token: 0x060002E5 RID: 741 RVA: 0x00022494 File Offset: 0x00020694
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

	// Token: 0x060002E6 RID: 742 RVA: 0x000224F8 File Offset: 0x000206F8
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

	// Token: 0x060002E7 RID: 743 RVA: 0x00022750 File Offset: 0x00020950
	private Vector3 AlongLine(Vector3 point)
	{
		Vector3 vector = this.balanceBeam.ClosestPointOnPath(point);
		vector.y = point.y;
		return vector;
	}

	// Token: 0x060002E8 RID: 744 RVA: 0x00022778 File Offset: 0x00020978
	public Vector3 ClosestPointOnPath(Vector3 point)
	{
		float closestInterpolated = this.balanceBeam.GetClosestInterpolated(point);
		return this.GetPointOnPath(closestInterpolated);
	}

	// Token: 0x060002E9 RID: 745 RVA: 0x0002279C File Offset: 0x0002099C
	public Vector3 GetPointOnPath(float t)
	{
		Vector3 vector = Vector3.Lerp(this.initialPositions[Mathf.FloorToInt(t)], this.initialPositions[Mathf.CeilToInt(t)], t - Mathf.Floor(t));
		return base.transform.TransformPoint(vector);
	}

	// Token: 0x060002EA RID: 746 RVA: 0x000227E8 File Offset: 0x000209E8
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

	// Token: 0x04000402 RID: 1026
	public BalanceBeam balanceBeam;

	// Token: 0x04000403 RID: 1027
	public BendyClimbingPole attachedPole;

	// Token: 0x04000404 RID: 1028
	private Vector3 attachedPosition;

	// Token: 0x04000405 RID: 1029
	private Quaternion attachedRotation;

	// Token: 0x04000406 RID: 1030
	public Renderer staticRenderer;

	// Token: 0x04000407 RID: 1031
	public Renderer dynamicRenderer;

	// Token: 0x04000408 RID: 1032
	public Transform dynamicPivot;

	// Token: 0x04000409 RID: 1033
	public Vector3[] initialPositions;

	// Token: 0x0400040A RID: 1034
	private Quaternion initialRotation;

	// Token: 0x0400040B RID: 1035
	public bool lockToVertical;

	// Token: 0x0400040C RID: 1036
	private Vector3 weightPoint;

	// Token: 0x0400040D RID: 1037
	private float weightPointOnPath;

	// Token: 0x0400040E RID: 1038
	private Vector3 pivotPoint;

	// Token: 0x0400040F RID: 1039
	private Vector3 endPoint;

	// Token: 0x04000410 RID: 1040
	private float totalLength;

	// Token: 0x04000411 RID: 1041
	public float springStrength = 4f;

	// Token: 0x04000412 RID: 1042
	public float damperStrength = 0.5f;

	// Token: 0x04000413 RID: 1043
	private Vector3 velocity;

	// Token: 0x04000414 RID: 1044
	[Range(0.0001f, 1f)]
	public float stiffness = 0.5f;

	// Token: 0x04000415 RID: 1045
	private float bendAmount;

	// Token: 0x04000416 RID: 1046
	public UnityEvent onEnable;

	// Token: 0x04000417 RID: 1047
	private bool isPlayerOnBalanceBeam;

	// Token: 0x04000418 RID: 1048
	private bool lockToTightrope;
}
