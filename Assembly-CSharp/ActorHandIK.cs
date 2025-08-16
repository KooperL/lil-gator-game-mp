using System;
using UnityEngine;

public class ActorHandIK : MonoBehaviour
{
	// Token: 0x0600036E RID: 878 RVA: 0x00025C7C File Offset: 0x00023E7C
	private void Awake()
	{
		this.actor = base.GetComponent<DialogueActor>();
		if (this.actor == null)
		{
			this.actor = base.transform.GetComponentInParent<DialogueActor>();
		}
		this.animator = base.GetComponent<Animator>();
		this.isRemoving = false;
		if (this.actor.isPlayer)
		{
			base.enabled = false;
		}
	}

	// Token: 0x0600036F RID: 879 RVA: 0x00025CDC File Offset: 0x00023EDC
	private void Update()
	{
		this.rightWeight = Mathf.MoveTowards(this.rightWeight, (!this.isRemoving && this.hasRightIK) ? 1f : 0f, 2f * Time.deltaTime);
		this.leftWeight = Mathf.MoveTowards(this.leftWeight, (!this.isRemoving && this.hasLeftIK) ? 1f : 0f, 2f * Time.deltaTime);
		if (this.isRemoving && this.rightWeight == 0f && this.leftWeight == 0f)
		{
			global::UnityEngine.Object.Destroy(this);
		}
	}

	// Token: 0x06000370 RID: 880 RVA: 0x00025D84 File Offset: 0x00023F84
	private void OnAnimatorIK()
	{
		float num = this.rightWeight * this.animator.GetFloat(ActorHandIK.RightHandID);
		float num2 = this.leftWeight * this.animator.GetFloat(ActorHandIK.LeftHandID);
		this.animator.SetIKPositionWeight(AvatarIKGoal.RightHand, Mathf.SmoothStep(0f, 1f, num));
		this.animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, Mathf.SmoothStep(0f, 1f, num2));
		this.animator.SetIKPosition(AvatarIKGoal.RightHand, this.GetIKPosition(false));
		this.animator.SetIKPosition(AvatarIKGoal.LeftHand, this.GetIKPosition(true));
	}

	// Token: 0x06000371 RID: 881 RVA: 0x00025E20 File Offset: 0x00024020
	public Vector3 GetIKPosition(bool isLeft)
	{
		Transform transform = (isLeft ? this.leftAnchor : this.rightAnchor);
		Vector3 vector = (isLeft ? this.leftPosition : this.rightPosition);
		if (isLeft ? this.allowLeftYAxis : this.allowRightYAxis)
		{
			Vector3 vector2 = (isLeft ? this.actor.leftHand : this.actor.rightHand).position;
			if (transform != null)
			{
				vector2 = transform.InverseTransformPoint(vector2);
			}
			vector.y = vector2.y;
		}
		if (transform != null)
		{
			vector = transform.TransformPoint(vector);
		}
		return vector;
	}

	// Token: 0x06000372 RID: 882 RVA: 0x00025EB8 File Offset: 0x000240B8
	public void SetHandIK(bool isLeft, Vector3 position, Transform anchor = null, bool allowYAxis = false)
	{
		if (this.actor == null)
		{
			this.actor = base.transform.GetComponentInParent<DialogueActor>();
		}
		if (this.actor.isPlayer)
		{
			Player.handIK.actorHandIK = this;
			Player.handIK.SetOverride(isLeft, position, anchor);
		}
		if (isLeft)
		{
			this.hasLeftIK = true;
			this.allowLeftYAxis = allowYAxis;
			this.leftPosition = position;
			this.leftAnchor = anchor;
		}
		else
		{
			this.hasRightIK = true;
			this.allowRightYAxis = allowYAxis;
			this.rightPosition = position;
			this.rightAnchor = anchor;
		}
		this.isRemoving = false;
	}

	// Token: 0x06000373 RID: 883 RVA: 0x00004AB9 File Offset: 0x00002CB9
	public void ClearAndRemove()
	{
		this.isRemoving = true;
		this.hasLeftIK = false;
		this.hasRightIK = false;
		if (this.actor.isPlayer)
		{
			Player.handIK.ClearOverride(true);
			Player.handIK.ClearOverride(false);
			global::UnityEngine.Object.Destroy(this);
		}
	}

	private static readonly int RightHandID = Animator.StringToHash("RightHand");

	private static readonly int LeftHandID = Animator.StringToHash("LeftHand");

	private DialogueActor actor;

	private Animator animator;

	private bool hasLeftIK;

	private float leftWeight;

	private Transform leftAnchor;

	private Vector3 leftPosition;

	private bool allowLeftYAxis;

	private bool hasRightIK;

	private float rightWeight;

	private Transform rightAnchor;

	private Vector3 rightPosition;

	private bool allowRightYAxis;

	private bool isRemoving;
}
