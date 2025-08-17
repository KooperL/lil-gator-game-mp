using System;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Contextual/Bendy Pole")]
public class BendyClimbingPole : MonoBehaviour
{
	// Token: 0x060002E9 RID: 745 RVA: 0x00022988 File Offset: 0x00020B88
	private void Awake()
	{
		this.climbingPole.onEnable.AddListener(new UnityAction(this.OnClimbingPoleEnabled));
		this.weightPoint = (this.endPoint = this.climbingPole.GetPosition((float)(this.climbingPole.positions.Length - 1)));
		this.startPoint = this.dynamicPivot.position;
		this.totalLength = (this.endPoint - this.startPoint).magnitude;
		this.initialRotation = this.dynamicPivot.rotation;
		base.enabled = false;
	}

	// Token: 0x060002EA RID: 746 RVA: 0x000044D7 File Offset: 0x000026D7
	private void OnClimbingPoleEnabled()
	{
		base.enabled = true;
	}

	// Token: 0x060002EB RID: 747 RVA: 0x00022A24 File Offset: 0x00020C24
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

	// Token: 0x060002EC RID: 748 RVA: 0x00022A88 File Offset: 0x00020C88
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

	// Token: 0x060002ED RID: 749 RVA: 0x00022CF0 File Offset: 0x00020EF0
	private Vector3 AlongLine(Vector3 point)
	{
		Vector3 vector = this.climbingPole.ClosestPointOnPath(point);
		vector.y = point.y;
		return vector;
	}

	// Token: 0x060002EE RID: 750 RVA: 0x00022D18 File Offset: 0x00020F18
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

	public ClimbingPole climbingPole;

	public Renderer staticRenderer;

	public Renderer dynamicRenderer;

	public Transform dynamicPivot;

	public Vector3[] initialPositions;

	private Quaternion initialRotation;

	public bool lockToVertical;

	private Vector3 weightPoint;

	private float weightPointOnPath;

	private Vector3 startPoint;

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
