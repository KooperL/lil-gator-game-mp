using System;
using UnityEngine;

// Token: 0x020001B2 RID: 434
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

	// Token: 0x04000B3B RID: 2875
	private static readonly int RightHandID = Animator.StringToHash("RightHand");

	// Token: 0x04000B3C RID: 2876
	private static readonly int LeftHandID = Animator.StringToHash("LeftHand");

	// Token: 0x04000B3D RID: 2877
	private Animator animator;

	// Token: 0x04000B3E RID: 2878
	public LayerMask layerMask;

	// Token: 0x04000B3F RID: 2879
	public Transform chest;

	// Token: 0x04000B40 RID: 2880
	public Transform rightArm;

	// Token: 0x04000B41 RID: 2881
	public Transform rightHand;

	// Token: 0x04000B42 RID: 2882
	public Transform leftArm;

	// Token: 0x04000B43 RID: 2883
	public Transform leftHand;

	// Token: 0x04000B44 RID: 2884
	public Vector3 rightOrigin;

	// Token: 0x04000B45 RID: 2885
	public Vector3 leftOrigin;

	// Token: 0x04000B46 RID: 2886
	public Vector3 rightArmDirection;

	// Token: 0x04000B47 RID: 2887
	public Vector3 leftArmDirection;

	// Token: 0x04000B48 RID: 2888
	public float castLength;

	// Token: 0x04000B49 RID: 2889
	public float armCastThickness;

	// Token: 0x04000B4A RID: 2890
	private float rightHandWeight;

	// Token: 0x04000B4B RID: 2891
	private float leftHandWeight;

	// Token: 0x04000B4C RID: 2892
	private float rightCounter;

	// Token: 0x04000B4D RID: 2893
	private float leftCounter;

	// Token: 0x04000B4E RID: 2894
	public float counterThreshold = 0.5f;

	// Token: 0x04000B4F RID: 2895
	private float rightSmoothWeight;

	// Token: 0x04000B50 RID: 2896
	private float rightVelocity;

	// Token: 0x04000B51 RID: 2897
	private float leftSmoothWeight;

	// Token: 0x04000B52 RID: 2898
	private float leftVelocity;

	// Token: 0x04000B53 RID: 2899
	private RaycastHit hit;

	// Token: 0x04000B54 RID: 2900
	private RaycastHit leftHit;

	// Token: 0x04000B55 RID: 2901
	private Vector3 rightPoint;

	// Token: 0x04000B56 RID: 2902
	private bool hasRightPoint;

	// Token: 0x04000B57 RID: 2903
	private Vector3 leftPoint;

	// Token: 0x04000B58 RID: 2904
	private bool hasLeftPoint;

	// Token: 0x04000B59 RID: 2905
	public PlayerMovement movement;

	// Token: 0x04000B5A RID: 2906
	public PlayerItemManager itemManager;

	// Token: 0x04000B5B RID: 2907
	[ReadOnly]
	public ActorHandIK actorHandIK;

	// Token: 0x04000B5C RID: 2908
	private bool overrideLeft;

	// Token: 0x04000B5D RID: 2909
	private bool overrideRight;

	// Token: 0x04000B5E RID: 2910
	private Vector3 overrideLeftPosition;

	// Token: 0x04000B5F RID: 2911
	private Vector3 overrideRightPosition;

	// Token: 0x04000B60 RID: 2912
	private Transform overrideLeftAnchor;

	// Token: 0x04000B61 RID: 2913
	private Transform overrideRightAnchor;

	// Token: 0x04000B62 RID: 2914
	private bool isLeftEnabled = true;

	// Token: 0x04000B63 RID: 2915
	private bool isRightEnabled = true;

	// Token: 0x04000B64 RID: 2916
	public ICustomHandIKPositions customIKPositions;
}
