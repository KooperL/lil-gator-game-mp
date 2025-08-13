using System;
using UnityEngine;

// Token: 0x0200022E RID: 558
public class HandIK : MonoBehaviour
{
	// Token: 0x06000A7F RID: 2687 RVA: 0x0000A11C File Offset: 0x0000831C
	public void SetEnabled(bool isLeft, bool isEnabled)
	{
		if (isLeft)
		{
			this.isLeftEnabled = isEnabled;
			return;
		}
		this.isRightEnabled = isEnabled;
	}

	// Token: 0x06000A80 RID: 2688 RVA: 0x0000A130 File Offset: 0x00008330
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

	// Token: 0x06000A81 RID: 2689 RVA: 0x0000A160 File Offset: 0x00008360
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

	// Token: 0x06000A82 RID: 2690 RVA: 0x0000A182 File Offset: 0x00008382
	private void Awake()
	{
		this.animator = base.GetComponent<Animator>();
	}

	// Token: 0x06000A83 RID: 2691 RVA: 0x0003D4EC File Offset: 0x0003B6EC
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
		this.animator.SetIKPositionWeight(3, this.rightSmoothWeight);
		this.animator.SetIKPositionWeight(2, this.leftSmoothWeight);
		this.animator.SetIKPosition(3, this.rightPoint);
		this.animator.SetIKPosition(2, this.leftPoint);
	}

	// Token: 0x06000A84 RID: 2692 RVA: 0x0003D720 File Offset: 0x0003B920
	private void SetHandIK(string oppositeFoot, bool isHandBusy, ref bool hasPoint, Transform handTransform, Vector3 origin, ref float counter, ref float handWeight, ref Vector3 grabPoint, float smoothWeight)
	{
		if (((Game.HasControl && this.movement.IsGrounded) || (this.movement.IsClimbing && this.movement.Stamina > 0f)) && this.animator.GetFloat(oppositeFoot) < 0.1f && !isHandBusy)
		{
			if (hasPoint && !this.movement.IsClimbing && (this.movement.velocity.sqrMagnitude > 0.25f || Vector3.SqrMagnitude(this.chest.TransformPoint(origin) - grabPoint) > 0.01f + this.castLength * this.castLength))
			{
				hasPoint = false;
			}
			if (((!hasPoint && smoothWeight < 0.1f) || this.movement.IsClimbing) && Physics.SphereCast(this.movement.IsClimbing ? (handTransform.position - 0.4f * this.chest.forward) : this.chest.TransformPoint(origin), this.armCastThickness, this.chest.forward, ref this.hit, this.castLength, this.layerMask, 1))
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

	// Token: 0x04000D43 RID: 3395
	private static readonly int RightHandID = Animator.StringToHash("RightHand");

	// Token: 0x04000D44 RID: 3396
	private static readonly int LeftHandID = Animator.StringToHash("LeftHand");

	// Token: 0x04000D45 RID: 3397
	private Animator animator;

	// Token: 0x04000D46 RID: 3398
	public LayerMask layerMask;

	// Token: 0x04000D47 RID: 3399
	public Transform chest;

	// Token: 0x04000D48 RID: 3400
	public Transform rightArm;

	// Token: 0x04000D49 RID: 3401
	public Transform rightHand;

	// Token: 0x04000D4A RID: 3402
	public Transform leftArm;

	// Token: 0x04000D4B RID: 3403
	public Transform leftHand;

	// Token: 0x04000D4C RID: 3404
	public Vector3 rightOrigin;

	// Token: 0x04000D4D RID: 3405
	public Vector3 leftOrigin;

	// Token: 0x04000D4E RID: 3406
	public Vector3 rightArmDirection;

	// Token: 0x04000D4F RID: 3407
	public Vector3 leftArmDirection;

	// Token: 0x04000D50 RID: 3408
	public float castLength;

	// Token: 0x04000D51 RID: 3409
	public float armCastThickness;

	// Token: 0x04000D52 RID: 3410
	private float rightHandWeight;

	// Token: 0x04000D53 RID: 3411
	private float leftHandWeight;

	// Token: 0x04000D54 RID: 3412
	private float rightCounter;

	// Token: 0x04000D55 RID: 3413
	private float leftCounter;

	// Token: 0x04000D56 RID: 3414
	public float counterThreshold = 0.5f;

	// Token: 0x04000D57 RID: 3415
	private float rightSmoothWeight;

	// Token: 0x04000D58 RID: 3416
	private float rightVelocity;

	// Token: 0x04000D59 RID: 3417
	private float leftSmoothWeight;

	// Token: 0x04000D5A RID: 3418
	private float leftVelocity;

	// Token: 0x04000D5B RID: 3419
	private RaycastHit hit;

	// Token: 0x04000D5C RID: 3420
	private RaycastHit leftHit;

	// Token: 0x04000D5D RID: 3421
	private Vector3 rightPoint;

	// Token: 0x04000D5E RID: 3422
	private bool hasRightPoint;

	// Token: 0x04000D5F RID: 3423
	private Vector3 leftPoint;

	// Token: 0x04000D60 RID: 3424
	private bool hasLeftPoint;

	// Token: 0x04000D61 RID: 3425
	public PlayerMovement movement;

	// Token: 0x04000D62 RID: 3426
	public PlayerItemManager itemManager;

	// Token: 0x04000D63 RID: 3427
	[ReadOnly]
	public ActorHandIK actorHandIK;

	// Token: 0x04000D64 RID: 3428
	private bool overrideLeft;

	// Token: 0x04000D65 RID: 3429
	private bool overrideRight;

	// Token: 0x04000D66 RID: 3430
	private Vector3 overrideLeftPosition;

	// Token: 0x04000D67 RID: 3431
	private Vector3 overrideRightPosition;

	// Token: 0x04000D68 RID: 3432
	private Transform overrideLeftAnchor;

	// Token: 0x04000D69 RID: 3433
	private Transform overrideRightAnchor;

	// Token: 0x04000D6A RID: 3434
	private bool isLeftEnabled = true;

	// Token: 0x04000D6B RID: 3435
	private bool isRightEnabled = true;

	// Token: 0x04000D6C RID: 3436
	public ICustomHandIKPositions customIKPositions;
}
