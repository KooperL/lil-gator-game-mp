using System;
using UnityEngine;

public class HandIK : MonoBehaviour
{
	// Token: 0x060008F8 RID: 2296 RVA: 0x0002AF28 File Offset: 0x00029128
	public void SetEnabled(bool isLeft, bool isEnabled)
	{
		if (isLeft)
		{
			this.isLeftEnabled = isEnabled;
			return;
		}
		this.isRightEnabled = isEnabled;
	}

	// Token: 0x060008F9 RID: 2297 RVA: 0x0002AF3C File Offset: 0x0002913C
	public void SetOverride(bool isLeft, Vector3 position, Transform anchor)
	{
		if (isLeft)
		{
			this.overrideLeft = true;
			this.overrideLeftPosition = position;
			this.overrideLeftAnchor = anchor;
			return;
		}
		this.overrideRight = true;
		this.overrideRightPosition = position;
		this.overrideRightAnchor = anchor;
	}

	// Token: 0x060008FA RID: 2298 RVA: 0x0002AF6C File Offset: 0x0002916C
	public void ClearOverride(bool isLeft)
	{
		if (isLeft)
		{
			this.overrideLeft = false;
			this.overrideLeftAnchor = null;
			return;
		}
		this.overrideRight = false;
		this.overrideRightAnchor = null;
	}

	// Token: 0x060008FB RID: 2299 RVA: 0x0002AF8E File Offset: 0x0002918E
	private void Awake()
	{
		this.animator = base.GetComponent<Animator>();
	}

	// Token: 0x060008FC RID: 2300 RVA: 0x0002AF9C File Offset: 0x0002919C
	private void OnAnimatorIK()
	{
		if (!this.isRightEnabled)
		{
			this.rightHandWeight = 0f;
		}
		else if (this.overrideRight || this.customIKPositions != null)
		{
			this.rightHandWeight = this.animator.GetFloat(HandIK.RightHandID);
			if (this.customIKPositions != null)
			{
				this.rightPoint = this.customIKPositions.GetRightHandTarget(this.rightHand.position);
			}
			else
			{
				this.rightPoint = this.actorHandIK.GetIKPosition(false);
			}
		}
		else
		{
			this.SetHandIK("leftfoot", this.itemManager.RightHandBusy, ref this.hasRightPoint, this.rightHand, this.rightOrigin, ref this.rightCounter, ref this.rightHandWeight, ref this.rightPoint, this.rightSmoothWeight);
		}
		if (!this.isLeftEnabled)
		{
			this.leftHandWeight = 0f;
		}
		else if (this.overrideLeft || this.customIKPositions != null)
		{
			this.leftHandWeight = this.animator.GetFloat(HandIK.LeftHandID);
			if (this.customIKPositions != null)
			{
				this.leftPoint = this.customIKPositions.GetLeftHandTarget(this.leftHand.position);
			}
			else
			{
				this.leftPoint = this.actorHandIK.GetIKPosition(true);
			}
		}
		else
		{
			this.SetHandIK("rightfoot", this.itemManager.LeftHandBusy, ref this.hasLeftPoint, this.leftHand, this.leftOrigin, ref this.leftCounter, ref this.leftHandWeight, ref this.leftPoint, this.leftSmoothWeight);
		}
		this.rightSmoothWeight = Mathf.SmoothDamp(this.rightSmoothWeight, this.rightHandWeight, ref this.rightVelocity, this.movement.IsClimbing ? 0.1f : 0.2f);
		this.leftSmoothWeight = Mathf.SmoothDamp(this.leftSmoothWeight, this.leftHandWeight, ref this.leftVelocity, this.movement.IsClimbing ? 0.1f : 0.2f);
		this.animator.SetIKPositionWeight(AvatarIKGoal.RightHand, this.rightSmoothWeight);
		this.animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, this.leftSmoothWeight);
		this.animator.SetIKPosition(AvatarIKGoal.RightHand, this.rightPoint);
		this.animator.SetIKPosition(AvatarIKGoal.LeftHand, this.leftPoint);
	}

	// Token: 0x060008FD RID: 2301 RVA: 0x0002B1D0 File Offset: 0x000293D0
	private void SetHandIK(string oppositeFoot, bool isHandBusy, ref bool hasPoint, Transform handTransform, Vector3 origin, ref float counter, ref float handWeight, ref Vector3 grabPoint, float smoothWeight)
	{
		if (((Game.HasControl && this.movement.IsGrounded) || (this.movement.IsClimbing && this.movement.Stamina > 0f)) && this.animator.GetFloat(oppositeFoot) < 0.1f && !isHandBusy)
		{
			if (hasPoint && !this.movement.IsClimbing && (this.movement.velocity.sqrMagnitude > 0.25f || Vector3.SqrMagnitude(this.chest.TransformPoint(origin) - grabPoint) > 0.01f + this.castLength * this.castLength))
			{
				hasPoint = false;
			}
			if (((!hasPoint && smoothWeight < 0.1f) || this.movement.IsClimbing) && Physics.SphereCast(this.movement.IsClimbing ? (handTransform.position - 0.4f * this.chest.forward) : this.chest.TransformPoint(origin), this.armCastThickness, this.chest.forward, out this.hit, this.castLength, this.layerMask, QueryTriggerInteraction.Ignore))
			{
				counter += Time.deltaTime;
				if (counter >= this.counterThreshold || this.movement.IsClimbing)
				{
					grabPoint = this.hit.point;
					handWeight = 1f;
					hasPoint = true;
					counter = 0f;
				}
			}
			if (!hasPoint)
			{
				handWeight = 0f;
				return;
			}
		}
		else
		{
			hasPoint = false;
			handWeight = 0f;
			counter = 0f;
		}
	}

	private static readonly int RightHandID = Animator.StringToHash("RightHand");

	private static readonly int LeftHandID = Animator.StringToHash("LeftHand");

	private Animator animator;

	public LayerMask layerMask;

	public Transform chest;

	public Transform rightArm;

	public Transform rightHand;

	public Transform leftArm;

	public Transform leftHand;

	public Vector3 rightOrigin;

	public Vector3 leftOrigin;

	public Vector3 rightArmDirection;

	public Vector3 leftArmDirection;

	public float castLength;

	public float armCastThickness;

	private float rightHandWeight;

	private float leftHandWeight;

	private float rightCounter;

	private float leftCounter;

	public float counterThreshold = 0.5f;

	private float rightSmoothWeight;

	private float rightVelocity;

	private float leftSmoothWeight;

	private float leftVelocity;

	private RaycastHit hit;

	private RaycastHit leftHit;

	private Vector3 rightPoint;

	private bool hasRightPoint;

	private Vector3 leftPoint;

	private bool hasLeftPoint;

	public PlayerMovement movement;

	public PlayerItemManager itemManager;

	[ReadOnly]
	public ActorHandIK actorHandIK;

	private bool overrideLeft;

	private bool overrideRight;

	private Vector3 overrideLeftPosition;

	private Vector3 overrideRightPosition;

	private Transform overrideLeftAnchor;

	private Transform overrideRightAnchor;

	private bool isLeftEnabled = true;

	private bool isRightEnabled = true;

	public ICustomHandIKPositions customIKPositions;
}
