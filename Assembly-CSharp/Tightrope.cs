using System;
using UnityEngine;
using UnityEngine.Events;

public class Tightrope : MonoBehaviour
{
	// Token: 0x060002BD RID: 701 RVA: 0x000101E4 File Offset: 0x0000E3E4
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

	// Token: 0x060002BE RID: 702 RVA: 0x0001033C File Offset: 0x0000E53C
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

	// Token: 0x060002BF RID: 703 RVA: 0x000103FA File Offset: 0x0000E5FA
	private void OnBalanceBeamEnabled()
	{
		base.enabled = true;
	}

	// Token: 0x060002C0 RID: 704 RVA: 0x00010404 File Offset: 0x0000E604
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

	// Token: 0x060002C1 RID: 705 RVA: 0x000104D8 File Offset: 0x0000E6D8
	private float GetTForPoint(Vector3 point)
	{
		Vector3 vector = point - this.linePoint1;
		return Mathf.Clamp01(Mathf.InverseLerp(0f, this.lineLength, Vector3.Dot(vector, this.lineVectorNormalized)));
	}

	// Token: 0x060002C2 RID: 706 RVA: 0x00010514 File Offset: 0x0000E714
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

	// Token: 0x060002C3 RID: 707 RVA: 0x0001090C File Offset: 0x0000EB0C
	private Vector3 AlongLine(Vector3 point)
	{
		Vector3 vector = Vector3.Project(point - this.linePoint1, this.lineVectorNormalized) + this.linePoint1;
		vector.y = point.y;
		return vector;
	}

	// Token: 0x060002C4 RID: 708 RVA: 0x0001094A File Offset: 0x0000EB4A
	public Vector3 ClosestPointOnLine(Vector3 point)
	{
		this.UpdatePoints();
		return MathUtils.ClosestAlongLine(point, this.linePoint1, this.linePoint2);
	}

	// Token: 0x060002C5 RID: 709 RVA: 0x00010964 File Offset: 0x0000EB64
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

	// Token: 0x060002C6 RID: 710 RVA: 0x00010AB1 File Offset: 0x0000ECB1
	public Vector3 GetPointOnLine(float t)
	{
		return Vector3.Lerp(this.linePoint1, this.linePoint2, t);
	}

	// Token: 0x060002C7 RID: 711 RVA: 0x00010AC8 File Offset: 0x0000ECC8
	public void UpdatePoints()
	{
		this.linePoint1 = this.partial1.transform.position;
		this.linePoint2 = this.partial2.transform.position;
		this.isPointsInverted = (this.linePoint1 - this.balanceBeam.positions[0]).sqrMagnitude > 0.1f;
		this.lineVector = this.linePoint2 - this.linePoint1;
		this.lineVectorNormalized = this.lineVector.normalized;
		this.lineLength = this.lineVector.magnitude;
	}

	// Token: 0x060002C8 RID: 712 RVA: 0x00010B6C File Offset: 0x0000ED6C
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

	public BalanceBeam balanceBeam;

	public Renderer staticRenderer;

	public Renderer skinnedRenderer;

	public GameObject partial1;

	public GameObject partial2;

	public GameObject centerObj;

	private Vector3 partial1Direction;

	private Vector3 partial2Direction;

	private Quaternion partial1Rotation;

	private Quaternion partial2Rotation;

	[Tooltip("0 = X, 1 = Y, 2 = Z")]
	public int scaleAxis;

	private float scaleMultiplier;

	public bool lockWeightXAxis;

	private Vector3 center;

	private Vector3 centerWeightPoint;

	public float centerWeightT;

	public float springStrength = 4f;

	public float damperStrength = 0.5f;

	private Vector3 velocity;

	[Space]
	public Transform end1;

	public Transform end2;

	private Vector3 linePoint1;

	private Vector3 linePoint2;

	private Vector3 lineVector;

	private Vector3 lineVectorNormalized;

	private float lineLength;

	public bool waitForVisibility;

	public Renderer[] visibilityRenderers;

	private float looseOffset = -0.5f;

	public UnityEvent onEnable;

	private bool isPlayerOnBalanceBeam;

	private bool lockToTightrope;

	private static readonly int _leftfoot = Animator.StringToHash("leftfoot");

	private static readonly int _rightfoot = Animator.StringToHash("rightfoot");

	private bool updatedCenterTPoint;

	private float centerT;

	private bool isPointsInverted;
}
