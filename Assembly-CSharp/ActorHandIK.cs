using System;
using UnityEngine;

// Token: 0x020000CB RID: 203
public class ActorHandIK : MonoBehaviour
{
	// Token: 0x06000348 RID: 840 RVA: 0x00024E54 File Offset: 0x00023054
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

	// Token: 0x06000349 RID: 841 RVA: 0x00024EB4 File Offset: 0x000230B4
	private void Update()
	{
		this.rightWeight = Mathf.MoveTowards(this.rightWeight, (!this.isRemoving && this.hasRightIK) ? 1f : 0f, 2f * Time.deltaTime);
		this.leftWeight = Mathf.MoveTowards(this.leftWeight, (!this.isRemoving && this.hasLeftIK) ? 1f : 0f, 2f * Time.deltaTime);
		if (this.isRemoving && this.rightWeight == 0f && this.leftWeight == 0f)
		{
			Object.Destroy(this);
		}
	}

	// Token: 0x0600034A RID: 842 RVA: 0x00024F5C File Offset: 0x0002315C
	private void OnAnimatorIK()
	{
		float num = this.rightWeight * this.animator.GetFloat(ActorHandIK.RightHandID);
		float num2 = this.leftWeight * this.animator.GetFloat(ActorHandIK.LeftHandID);
		this.animator.SetIKPositionWeight(3, Mathf.SmoothStep(0f, 1f, num));
		this.animator.SetIKPositionWeight(2, Mathf.SmoothStep(0f, 1f, num2));
		this.animator.SetIKPosition(3, this.GetIKPosition(false));
		this.animator.SetIKPosition(2, this.GetIKPosition(true));
	}

	// Token: 0x0600034B RID: 843 RVA: 0x00024FF8 File Offset: 0x000231F8
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

	// Token: 0x0600034C RID: 844 RVA: 0x00025090 File Offset: 0x00023290
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

	// Token: 0x0600034D RID: 845 RVA: 0x000048D5 File Offset: 0x00002AD5
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

	// Token: 0x040004B9 RID: 1209
	private static readonly int RightHandID = Animator.StringToHash("RightHand");

	// Token: 0x040004BA RID: 1210
	private static readonly int LeftHandID = Animator.StringToHash("LeftHand");

	// Token: 0x040004BB RID: 1211
	private DialogueActor actor;

	// Token: 0x040004BC RID: 1212
	private Animator animator;

	// Token: 0x040004BD RID: 1213
	private bool hasLeftIK;

	// Token: 0x040004BE RID: 1214
	private float leftWeight;

	// Token: 0x040004BF RID: 1215
	private Transform leftAnchor;

	// Token: 0x040004C0 RID: 1216
	private Vector3 leftPosition;

	// Token: 0x040004C1 RID: 1217
	private bool allowLeftYAxis;

	// Token: 0x040004C2 RID: 1218
	private bool hasRightIK;

	// Token: 0x040004C3 RID: 1219
	private float rightWeight;

	// Token: 0x040004C4 RID: 1220
	private Transform rightAnchor;

	// Token: 0x040004C5 RID: 1221
	private Vector3 rightPosition;

	// Token: 0x040004C6 RID: 1222
	private bool allowRightYAxis;

	// Token: 0x040004C7 RID: 1223
	private bool isRemoving;
}
