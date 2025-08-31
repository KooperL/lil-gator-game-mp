using System;
using UnityEngine;

public class ActorHandIK : MonoBehaviour
{
	// Token: 0x06000303 RID: 771 RVA: 0x00011928 File Offset: 0x0000FB28
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

	// Token: 0x06000304 RID: 772 RVA: 0x00011988 File Offset: 0x0000FB88
	private void Update()
	{
		this.rightWeight = Mathf.MoveTowards(this.rightWeight, (!this.isRemoving && this.hasRightIK) ? 1f : 0f, 2f * Time.deltaTime);
		this.leftWeight = Mathf.MoveTowards(this.leftWeight, (!this.isRemoving && this.hasLeftIK) ? 1f : 0f, 2f * Time.deltaTime);
		if (this.isRemoving && this.rightWeight == 0f && this.leftWeight == 0f)
		{
			Object.Destroy(this);
		}
	}

	// Token: 0x06000305 RID: 773 RVA: 0x00011A30 File Offset: 0x0000FC30
	private void OnAnimatorIK()
	{
		float num = this.rightWeight * this.animator.GetFloat(ActorHandIK.RightHandID);
		float num2 = this.leftWeight * this.animator.GetFloat(ActorHandIK.LeftHandID);
		this.animator.SetIKPositionWeight(AvatarIKGoal.RightHand, Mathf.SmoothStep(0f, 1f, num));
		this.animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, Mathf.SmoothStep(0f, 1f, num2));
		this.animator.SetIKPosition(AvatarIKGoal.RightHand, this.GetIKPosition(false));
		this.animator.SetIKPosition(AvatarIKGoal.LeftHand, this.GetIKPosition(true));
	}

	// Token: 0x06000306 RID: 774 RVA: 0x00011ACC File Offset: 0x0000FCCC
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

	// Token: 0x06000307 RID: 775 RVA: 0x00011B64 File Offset: 0x0000FD64
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

	// Token: 0x06000308 RID: 776 RVA: 0x00011BFB File Offset: 0x0000FDFB
	public void ClearAndRemove()
	{
		this.isRemoving = true;
		this.hasLeftIK = false;
		this.hasRightIK = false;
		if (this.actor.isPlayer)
		{
			Player.handIK.ClearOverride(true);
			Player.handIK.ClearOverride(false);
			Object.Destroy(this);
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
