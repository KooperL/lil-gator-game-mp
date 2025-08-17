using System;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Contextual/Bendy Beam")]
public class BendyPole : MonoBehaviour
{
	// Token: 0x060002F0 RID: 752 RVA: 0x00022DE0 File Offset: 0x00020FE0
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

	// Token: 0x060002F1 RID: 753 RVA: 0x000044D7 File Offset: 0x000026D7
	private void OnBalanceBeamEnabled()
	{
		base.enabled = true;
	}

	// Token: 0x060002F2 RID: 754 RVA: 0x00022EEC File Offset: 0x000210EC
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

	// Token: 0x060002F3 RID: 755 RVA: 0x00022F50 File Offset: 0x00021150
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

	// Token: 0x060002F4 RID: 756 RVA: 0x000231A8 File Offset: 0x000213A8
	private Vector3 AlongLine(Vector3 point)
	{
		Vector3 vector = this.balanceBeam.ClosestPointOnPath(point);
		vector.y = point.y;
		return vector;
	}

	// Token: 0x060002F5 RID: 757 RVA: 0x000231D0 File Offset: 0x000213D0
	public Vector3 ClosestPointOnPath(Vector3 point)
	{
		float closestInterpolated = this.balanceBeam.GetClosestInterpolated(point);
		return this.GetPointOnPath(closestInterpolated);
	}

	// Token: 0x060002F6 RID: 758 RVA: 0x000231F4 File Offset: 0x000213F4
	public Vector3 GetPointOnPath(float t)
	{
		Vector3 vector = Vector3.Lerp(this.initialPositions[Mathf.FloorToInt(t)], this.initialPositions[Mathf.CeilToInt(t)], t - Mathf.Floor(t));
		return base.transform.TransformPoint(vector);
	}

	// Token: 0x060002F7 RID: 759 RVA: 0x00023240 File Offset: 0x00021440
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

	public BalanceBeam balanceBeam;

	public BendyClimbingPole attachedPole;

	private Vector3 attachedPosition;

	private Quaternion attachedRotation;

	public Renderer staticRenderer;

	public Renderer dynamicRenderer;

	public Transform dynamicPivot;

	public Vector3[] initialPositions;

	private Quaternion initialRotation;

	public bool lockToVertical;

	private Vector3 weightPoint;

	private float weightPointOnPath;

	private Vector3 pivotPoint;

	private Vector3 endPoint;

	private float totalLength;

	public float springStrength = 4f;

	public float damperStrength = 0.5f;

	private Vector3 velocity;

	[Range(0.0001f, 1f)]
	public float stiffness = 0.5f;

	private float bendAmount;

	public UnityEvent onEnable;

	private bool isPlayerOnBalanceBeam;

	private bool lockToTightrope;
}
