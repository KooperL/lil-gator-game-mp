using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000BC RID: 188
public class Tightrope : MonoBehaviour
{
	// Token: 0x06000307 RID: 775 RVA: 0x00023C10 File Offset: 0x00021E10
	[ContextMenu("Snap to ends")]
	public void SnapToEnds()
	{
		if (this.end1 == null || this.end2 == null)
		{
			return;
		}
		Vector3 vector = this.end1.transform.position - this.end2.transform.position;
		base.transform.position = Vector3.Lerp(this.end1.transform.position, this.end2.transform.position, 0.5f) + new Vector3(0f, 0.05f, 0f);
		base.transform.localScale = new Vector3(0.5f * vector.magnitude, 1f, 1f);
		Quaternion quaternion = Quaternion.FromToRotation(base.transform.rotation * Vector3.right, vector) * base.transform.rotation;
		int num = 5;
		while (Mathf.Abs(quaternion.eulerAngles.x) > 1f && num > 0)
		{
			num--;
			Vector3 eulerAngles = quaternion.eulerAngles;
			eulerAngles.x = 0f;
			quaternion.eulerAngles = eulerAngles;
			quaternion = Quaternion.FromToRotation(quaternion * Vector3.right, vector) * quaternion;
		}
		base.transform.rotation = quaternion;
	}

	// Token: 0x06000308 RID: 776 RVA: 0x00023D68 File Offset: 0x00021F68
	private void Awake()
	{
		this.balanceBeam.onEnable.AddListener(new UnityAction(this.OnBalanceBeamEnabled));
		this.centerWeightPoint = (this.center = base.transform.position);
		this.partial1Rotation = this.partial1.transform.rotation;
		this.partial2Rotation = this.partial2.transform.rotation;
		this.partial1Direction = this.center - this.partial1.transform.position;
		this.partial2Direction = this.center - this.partial2.transform.position;
		this.UpdatePoints();
		base.enabled = false;
	}

	// Token: 0x06000309 RID: 777 RVA: 0x000043EB File Offset: 0x000025EB
	private void OnBalanceBeamEnabled()
	{
		base.enabled = true;
	}

	// Token: 0x0600030A RID: 778 RVA: 0x00023E28 File Offset: 0x00022028
	private void OnEnable()
	{
		this.staticRenderer.enabled = false;
		if (this.skinnedRenderer != null)
		{
			this.skinnedRenderer.enabled = true;
		}
		this.partial1.SetActive(true);
		this.partial2.SetActive(true);
		if (this.centerObj != null)
		{
			this.centerObj.SetActive(true);
		}
		this.partial1.transform.parent = null;
		this.partial2.transform.parent = null;
		if (this.centerObj != null)
		{
			this.centerObj.transform.parent = null;
		}
		this.centerWeightPoint = Player.RawPosition;
		this.velocity = Vector3.zero;
		this.lockToTightrope = false;
		this.UpdateAnchor();
		this.onEnable.Invoke();
	}

	// Token: 0x0600030B RID: 779 RVA: 0x00023EFC File Offset: 0x000220FC
	private float GetTForPoint(Vector3 point)
	{
		Vector3 vector = point - this.linePoint1;
		return Mathf.Clamp01(Mathf.InverseLerp(0f, this.lineLength, Vector3.Dot(vector, this.lineVectorNormalized)));
	}

	// Token: 0x0600030C RID: 780 RVA: 0x00023F38 File Offset: 0x00022138
	private void LateUpdate()
	{
		if (this.balanceBeam.enabled)
		{
			Vector3 rawPosition = Player.RawPosition;
			if (this.lockToTightrope)
			{
				this.centerWeightT = this.GetTForPoint(rawPosition);
				Vector3 pointOnLine = this.GetPointOnLine(this.centerWeightT);
				float @float = Player.animator.GetFloat(Tightrope._leftfoot);
				float float2 = Player.animator.GetFloat(Tightrope._rightfoot);
				float num = 0.2f * Mathf.Min(@float, float2);
				this.centerWeightPoint = Vector3.MoveTowards(rawPosition, pointOnLine, num);
				float num2 = 0.02f * Mathf.Clamp(float2 - @float, -1f, 1f);
				this.centerWeightPoint += num2 * Player.transform.right;
			}
			else
			{
				float closestInterpolated = this.balanceBeam.GetClosestInterpolated(rawPosition);
				this.centerWeightT = closestInterpolated / ((float)this.balanceBeam.positions.Length - 1f);
				if (this.isPointsInverted)
				{
					this.centerWeightT = 1f - this.centerWeightT;
				}
				float y = this.balanceBeam.GetPosition(closestInterpolated).y;
				float num3 = Mathf.InverseLerp(y + 0.5f, y, rawPosition.y);
				this.centerWeightPoint = Vector3.Lerp(this.centerWeightPoint, rawPosition, num3);
				if (num3 >= 0.95f)
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
			Vector3 vector = Vector3.ProjectOnPlane(this.centerWeightPoint - this.GetPointOnLine(this.centerWeightT), this.lineVectorNormalized);
			this.centerWeightT = Mathf.Lerp(this.centerWeightT, 0.5f, Time.deltaTime);
			this.centerWeightPoint = this.GetPointOnLine(this.centerWeightT) + vector;
			if (this.waitForVisibility)
			{
				vector.y += 0.5f;
			}
			Vector3 vector2 = this.springStrength * -vector;
			this.velocity = Vector3.MoveTowards(this.velocity, Vector3.zero, Time.deltaTime * this.damperStrength);
			this.velocity += Time.deltaTime * vector2;
			this.velocity = Vector3.ClampMagnitude(this.velocity, 20f);
			this.centerWeightPoint += Time.deltaTime * this.velocity;
			bool flag;
			if (this.waitForVisibility)
			{
				flag = true;
				Renderer[] array = this.visibilityRenderers;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].isVisible)
					{
						flag = false;
						break;
					}
				}
			}
			else
			{
				flag = vector.sqrMagnitude < 0.005f && this.velocity.sqrMagnitude < 0.005f && Mathf.Abs(this.centerWeightT - 0.5f) < 0.01f;
			}
			if (flag)
			{
				this.partial1.SetActive(false);
				this.partial2.SetActive(false);
				if (this.centerObj != null)
				{
					this.centerObj.SetActive(false);
				}
				this.partial1.transform.parent = base.transform;
				this.partial2.transform.parent = base.transform;
				if (this.centerObj != null)
				{
					this.centerObj.transform.parent = base.transform;
				}
				if (this.skinnedRenderer != null)
				{
					this.skinnedRenderer.enabled = false;
				}
				this.staticRenderer.enabled = true;
				base.enabled = false;
			}
		}
		if (this.lockWeightXAxis)
		{
			this.centerWeightPoint = this.AlongLine(this.centerWeightPoint);
		}
		this.UpdateAnchor();
		this.updatedCenterTPoint = false;
	}

	// Token: 0x0600030D RID: 781 RVA: 0x00024330 File Offset: 0x00022530
	private Vector3 AlongLine(Vector3 point)
	{
		Vector3 vector = Vector3.Project(point - this.linePoint1, this.lineVectorNormalized) + this.linePoint1;
		vector.y = point.y;
		return vector;
	}

	// Token: 0x0600030E RID: 782 RVA: 0x000045A0 File Offset: 0x000027A0
	public Vector3 ClosestPointOnLine(Vector3 point)
	{
		this.UpdatePoints();
		return MathUtils.ClosestAlongLine(point, this.linePoint1, this.linePoint2);
	}

	// Token: 0x0600030F RID: 783 RVA: 0x00024370 File Offset: 0x00022570
	public Vector3 GetPointOnTightrope(float t)
	{
		Vector3 vector = this.linePoint1;
		Vector3 vector2 = this.linePoint2;
		if (!base.enabled || !Application.isPlaying)
		{
			return Vector3.Lerp(vector, vector2, t);
		}
		if (this.centerWeightPoint == Vector3.zero)
		{
			this.centerWeightPoint = Vector3.Lerp(vector, vector2, 0.5f);
		}
		if (!this.updatedCenterTPoint)
		{
			Vector3 vector3 = this.centerWeightPoint - vector;
			this.centerT = Mathf.Clamp01(Mathf.InverseLerp(0f, this.lineLength, Vector3.Dot(vector3, this.lineVectorNormalized)));
			float num = (Vector3.Lerp(vector, vector2, this.centerT) - this.centerWeightPoint).magnitude;
			num *= (0.5f - Mathf.Abs(this.centerT - 0.5f)) * 2f;
			this.centerT = Mathf.Lerp(this.centerT, 0.5f, Mathf.Min(0.1f, num / 10f));
			this.updatedCenterTPoint = true;
		}
		if (t < this.centerT)
		{
			t = Mathf.InverseLerp(0f, this.centerT, t);
			vector2 = this.centerWeightPoint;
		}
		else
		{
			t = Mathf.InverseLerp(this.centerT, 1f, t);
			vector = this.centerWeightPoint;
		}
		return Vector3.Lerp(vector, vector2, t);
	}

	// Token: 0x06000310 RID: 784 RVA: 0x000045BA File Offset: 0x000027BA
	public Vector3 GetPointOnLine(float t)
	{
		return Vector3.Lerp(this.linePoint1, this.linePoint2, t);
	}

	// Token: 0x06000311 RID: 785 RVA: 0x000244C0 File Offset: 0x000226C0
	public void UpdatePoints()
	{
		this.linePoint1 = this.partial1.transform.position;
		this.linePoint2 = this.partial2.transform.position;
		this.isPointsInverted = (this.linePoint1 - this.balanceBeam.positions[0]).sqrMagnitude > 0.1f;
		this.lineVector = this.linePoint2 - this.linePoint1;
		this.lineVectorNormalized = this.lineVector.normalized;
		this.lineLength = this.lineVector.magnitude;
	}

	// Token: 0x06000312 RID: 786 RVA: 0x00024564 File Offset: 0x00022764
	private void UpdateAnchor()
	{
		Vector3 vector = this.centerWeightPoint - this.linePoint1;
		Vector3 vector2 = this.centerWeightPoint - this.linePoint2;
		this.partial1.transform.rotation = Quaternion.FromToRotation(this.partial1Direction, vector) * this.partial1Rotation;
		this.partial2.transform.rotation = Quaternion.FromToRotation(this.partial2Direction, vector2) * this.partial2Rotation;
		Vector3 one = Vector3.one;
		one[this.scaleAxis] = vector.magnitude;
		Vector3 one2 = Vector3.one;
		one2[this.scaleAxis] = vector2.magnitude;
		this.partial1.transform.localScale = one;
		this.partial2.transform.localScale = one2;
		if (this.centerObj != null)
		{
			this.centerObj.transform.localScale = Vector3.one;
			this.centerObj.transform.position = this.centerWeightPoint;
		}
	}

	// Token: 0x04000455 RID: 1109
	public BalanceBeam balanceBeam;

	// Token: 0x04000456 RID: 1110
	public Renderer staticRenderer;

	// Token: 0x04000457 RID: 1111
	public Renderer skinnedRenderer;

	// Token: 0x04000458 RID: 1112
	public GameObject partial1;

	// Token: 0x04000459 RID: 1113
	public GameObject partial2;

	// Token: 0x0400045A RID: 1114
	public GameObject centerObj;

	// Token: 0x0400045B RID: 1115
	private Vector3 partial1Direction;

	// Token: 0x0400045C RID: 1116
	private Vector3 partial2Direction;

	// Token: 0x0400045D RID: 1117
	private Quaternion partial1Rotation;

	// Token: 0x0400045E RID: 1118
	private Quaternion partial2Rotation;

	// Token: 0x0400045F RID: 1119
	[Tooltip("0 = X, 1 = Y, 2 = Z")]
	public int scaleAxis;

	// Token: 0x04000460 RID: 1120
	private float scaleMultiplier;

	// Token: 0x04000461 RID: 1121
	public bool lockWeightXAxis;

	// Token: 0x04000462 RID: 1122
	private Vector3 center;

	// Token: 0x04000463 RID: 1123
	private Vector3 centerWeightPoint;

	// Token: 0x04000464 RID: 1124
	public float centerWeightT;

	// Token: 0x04000465 RID: 1125
	public float springStrength = 4f;

	// Token: 0x04000466 RID: 1126
	public float damperStrength = 0.5f;

	// Token: 0x04000467 RID: 1127
	private Vector3 velocity;

	// Token: 0x04000468 RID: 1128
	[Space]
	public Transform end1;

	// Token: 0x04000469 RID: 1129
	public Transform end2;

	// Token: 0x0400046A RID: 1130
	private Vector3 linePoint1;

	// Token: 0x0400046B RID: 1131
	private Vector3 linePoint2;

	// Token: 0x0400046C RID: 1132
	private Vector3 lineVector;

	// Token: 0x0400046D RID: 1133
	private Vector3 lineVectorNormalized;

	// Token: 0x0400046E RID: 1134
	private float lineLength;

	// Token: 0x0400046F RID: 1135
	public bool waitForVisibility;

	// Token: 0x04000470 RID: 1136
	public Renderer[] visibilityRenderers;

	// Token: 0x04000471 RID: 1137
	private float looseOffset = -0.5f;

	// Token: 0x04000472 RID: 1138
	public UnityEvent onEnable;

	// Token: 0x04000473 RID: 1139
	private bool isPlayerOnBalanceBeam;

	// Token: 0x04000474 RID: 1140
	private bool lockToTightrope;

	// Token: 0x04000475 RID: 1141
	private static readonly int _leftfoot = Animator.StringToHash("leftfoot");

	// Token: 0x04000476 RID: 1142
	private static readonly int _rightfoot = Animator.StringToHash("rightfoot");

	// Token: 0x04000477 RID: 1143
	private bool updatedCenterTPoint;

	// Token: 0x04000478 RID: 1144
	private float centerT;

	// Token: 0x04000479 RID: 1145
	private bool isPointsInverted;
}
