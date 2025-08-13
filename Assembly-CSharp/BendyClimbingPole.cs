using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000092 RID: 146
[AddComponentMenu("Contextual/Bendy Pole")]
public class BendyClimbingPole : MonoBehaviour
{
	// Token: 0x06000292 RID: 658 RVA: 0x0000E344 File Offset: 0x0000C544
	private void Awake()
	{
		this.climbingPole.onEnable.AddListener(new UnityAction(this.OnClimbingPoleEnabled));
		this.weightPoint = (this.endPoint = this.climbingPole.GetPosition((float)(this.climbingPole.positions.Length - 1)));
		this.startPoint = this.dynamicPivot.position;
		this.totalLength = (this.endPoint - this.startPoint).magnitude;
		this.initialRotation = this.dynamicPivot.rotation;
		base.enabled = false;
	}

	// Token: 0x06000293 RID: 659 RVA: 0x0000E3DF File Offset: 0x0000C5DF
	private void OnClimbingPoleEnabled()
	{
		base.enabled = true;
	}

	// Token: 0x06000294 RID: 660 RVA: 0x0000E3E8 File Offset: 0x0000C5E8
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

	// Token: 0x06000295 RID: 661 RVA: 0x0000E44C File Offset: 0x0000C64C
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

	// Token: 0x06000296 RID: 662 RVA: 0x0000E6B4 File Offset: 0x0000C8B4
	private Vector3 AlongLine(Vector3 point)
	{
		Vector3 vector = this.climbingPole.ClosestPointOnPath(point);
		vector.y = point.y;
		return vector;
	}

	// Token: 0x06000297 RID: 663 RVA: 0x0000E6DC File Offset: 0x0000C8DC
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

	// Token: 0x04000355 RID: 853
	public ClimbingPole climbingPole;

	// Token: 0x04000356 RID: 854
	public Renderer staticRenderer;

	// Token: 0x04000357 RID: 855
	public Renderer dynamicRenderer;

	// Token: 0x04000358 RID: 856
	public Transform dynamicPivot;

	// Token: 0x04000359 RID: 857
	public Vector3[] initialPositions;

	// Token: 0x0400035A RID: 858
	private Quaternion initialRotation;

	// Token: 0x0400035B RID: 859
	public bool lockToVertical;

	// Token: 0x0400035C RID: 860
	private Vector3 weightPoint;

	// Token: 0x0400035D RID: 861
	private float weightPointOnPath;

	// Token: 0x0400035E RID: 862
	private Vector3 startPoint;

	// Token: 0x0400035F RID: 863
	private Vector3 endPoint;

	// Token: 0x04000360 RID: 864
	private float totalLength;

	// Token: 0x04000361 RID: 865
	public float springStrength = 4f;

	// Token: 0x04000362 RID: 866
	public float damperStrength = 0.5f;

	// Token: 0x04000363 RID: 867
	private Vector3 velocity;

	// Token: 0x04000364 RID: 868
	[Range(0.0001f, 1f)]
	public float stiffness = 0.5f;

	// Token: 0x04000365 RID: 869
	private float bendAmount;

	// Token: 0x04000366 RID: 870
	public UnityEvent onEnable;

	// Token: 0x04000367 RID: 871
	private bool isPlayerOnBalanceBeam;

	// Token: 0x04000368 RID: 872
	private bool lockToTightrope;
}
