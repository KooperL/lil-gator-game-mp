using System;
using UnityEngine;

// Token: 0x020000A1 RID: 161
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

	// Token: 0x0400041B RID: 1051
	private static readonly int RightHandID = Animator.StringToHash("RightHand");

	// Token: 0x0400041C RID: 1052
	private static readonly int LeftHandID = Animator.StringToHash("LeftHand");

	// Token: 0x0400041D RID: 1053
	private DialogueActor actor;

	// Token: 0x0400041E RID: 1054
	private Animator animator;

	// Token: 0x0400041F RID: 1055
	private bool hasLeftIK;

	// Token: 0x04000420 RID: 1056
	private float leftWeight;

	// Token: 0x04000421 RID: 1057
	private Transform leftAnchor;

	// Token: 0x04000422 RID: 1058
	private Vector3 leftPosition;

	// Token: 0x04000423 RID: 1059
	private bool allowLeftYAxis;

	// Token: 0x04000424 RID: 1060
	private bool hasRightIK;

	// Token: 0x04000425 RID: 1061
	private float rightWeight;

	// Token: 0x04000426 RID: 1062
	private Transform rightAnchor;

	// Token: 0x04000427 RID: 1063
	private Vector3 rightPosition;

	// Token: 0x04000428 RID: 1064
	private bool allowRightYAxis;

	// Token: 0x04000429 RID: 1065
	private bool isRemoving;
}
