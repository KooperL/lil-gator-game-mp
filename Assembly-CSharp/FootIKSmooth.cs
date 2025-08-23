using System;
using UnityEngine;

public class FootIKSmooth : MonoBehaviour
{
	// (get) Token: 0x06000733 RID: 1843 RVA: 0x0000743E File Offset: 0x0000563E
	public float smoothDeltaY
	{
		get
		{
			return this.minHeightSmooth - base.transform.parent.position.y;
		}
	}

	// Token: 0x06000734 RID: 1844 RVA: 0x0000745C File Offset: 0x0000565C
	public void ClearOverrides()
	{
		this.overrideIK = false;
		this.customIKPositions = null;
		this.overrideLock = false;
	}

	// Token: 0x06000735 RID: 1845 RVA: 0x00007473 File Offset: 0x00005673
	private void Awake()
	{
		this.animator = base.GetComponent<Animator>();
	}

	// Token: 0x06000736 RID: 1846 RVA: 0x00007481 File Offset: 0x00005681
	private void Start()
	{
		this.minHeightSmooth = base.transform.position.y;
	}

	// Token: 0x06000737 RID: 1847 RVA: 0x00033E30 File Offset: 0x00032030
	public void ResetHeight()
	{
		this.minHeightSmooth = base.transform.position.y;
		this.heightVelocity = (this.heightGravityVelocity = 0f);
	}

	// Token: 0x06000738 RID: 1848 RVA: 0x00033E68 File Offset: 0x00032068
	private bool FootRaycast(Vector3 footPos, Vector3 rayDirection, float parentHeight, out RaycastHit hit)
	{
		bool flag = false;
		RaycastHit raycastHit = default(RaycastHit);
		Vector3 vector = footPos - rayDirection;
		vector.y = parentHeight + this.maxDelta;
		RaycastHit raycastHit2;
		bool flag2 = Physics.SphereCast(vector, 0.05f, rayDirection, out raycastHit2, 2f * this.maxDelta, this.RayMask);
		if (!flag2 || raycastHit2.point.y - this.minHeightSmooth < -0.25f)
		{
			flag = Physics.SphereCast(vector, 0.2f, rayDirection, out raycastHit, 2f * this.maxDelta, this.RayMask);
		}
		if (flag2 && flag)
		{
			hit = raycastHit;
		}
		else if (flag)
		{
			hit = raycastHit;
		}
		else
		{
			hit = raycastHit2;
		}
		return flag2 || flag;
	}

	// Token: 0x06000739 RID: 1849 RVA: 0x00033F24 File Offset: 0x00032124
	public Vector3 GetFootCenter()
	{
		Vector3 vector = Vector3.Lerp(this.leftPosition, this.rightPosition, 0.5f);
		vector.y = Mathf.Max(vector.y, this.minHeightSmooth);
		return vector;
	}

	// Token: 0x0600073A RID: 1850 RVA: 0x00033F64 File Offset: 0x00032164
	private void OnAnimatorIK()
	{
		if (!this.ikActive)
		{
			this.minHeightSmooth = base.transform.parent.position.y;
			this.animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0f);
			this.animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 0f);
			this.animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0f);
			this.animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 0f);
			this.animator.SetIKHintPositionWeight(AvatarIKHint.RightKnee, 0f);
			this.animator.SetIKHintPositionWeight(AvatarIKHint.LeftKnee, 0f);
			return;
		}
		bool flag = true;
		if (this.movement != null && !this.movement.IsGrounded)
		{
			flag = false;
		}
		Vector3 vector = (this.movement.IsClimbing ? base.transform.forward : Vector3.down);
		float y = base.transform.parent.position.y;
		float num = y + 2f;
		float num2 = 1f - this.animator.GetFloat("rightfoot");
		float num3 = 1f - this.animator.GetFloat("leftfoot");
		Vector3 vector2 = this.rightFoot.position;
		if (this.overrideIK)
		{
			if (this.customIKPositions != null && num2 > 0f)
			{
				this.rightPosition = this.customIKPositions.GetRightFootTarget(vector2);
				this.animator.SetIKPosition(AvatarIKGoal.RightFoot, this.rightPosition);
				num = Mathf.Min(num, Mathf.Lerp(this.minHeightSmooth, this.rightPosition.y + this.offset.y, this.rightWeightSmooth));
			}
			else
			{
				num2 = 0f;
			}
		}
		else if (flag && (num2 > 0f || this.rightWeightSmooth > 0f) && this.FootRaycast(vector2, vector, y, out this.hit))
		{
			num = Mathf.Min(num, Mathf.Lerp(this.minHeightSmooth, this.hit.point.y + this.offset.y, this.rightWeightSmooth));
			this.rightPosition = this.hit.point + this.offset;
			this.animator.SetIKPosition(AvatarIKGoal.RightFoot, this.rightPosition);
			Quaternion quaternion = Quaternion.FromToRotation(Vector3.up, this.hit.normal) * Quaternion.LookRotation(base.transform.forward);
			this.animator.SetIKRotation(AvatarIKGoal.RightFoot, quaternion);
			this.animator.SetIKHintPosition(AvatarIKHint.RightKnee, this.hit.point + this.offset + base.transform.forward);
		}
		else
		{
			num2 = 0f;
		}
		this.rightWeightSmooth = Mathf.SmoothDamp(this.rightWeightSmooth, num2, ref this.rightWeightVelocity, 0.025f);
		this.animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, this.rightWeightSmooth);
		this.animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, this.rightWeightSmooth);
		this.animator.SetIKHintPositionWeight(AvatarIKHint.RightKnee, this.rightWeightSmooth);
		vector2 = this.leftFoot.position;
		if (this.overrideIK)
		{
			if (this.customIKPositions != null && num3 > 0f)
			{
				this.leftPosition = this.customIKPositions.GetLeftFootTarget(vector2);
				this.animator.SetIKPosition(AvatarIKGoal.LeftFoot, this.leftPosition);
				num = Mathf.Min(num, Mathf.Lerp(this.minHeightSmooth, this.leftPosition.y + this.offset.y, this.leftWeightSmooth));
			}
			else
			{
				num3 = 0f;
			}
		}
		else if (flag && (num3 > 0f || this.leftWeightSmooth > 0f) && this.FootRaycast(vector2, vector, y, out this.hit))
		{
			num = Mathf.Min(num, Mathf.Lerp(this.minHeightSmooth, this.hit.point.y + this.offset.y, this.leftWeightSmooth));
			this.leftPosition = this.hit.point + this.offset;
			this.animator.SetIKPosition(AvatarIKGoal.LeftFoot, this.leftPosition);
			Quaternion quaternion2 = Quaternion.FromToRotation(Vector3.up, this.hit.normal) * Quaternion.LookRotation(base.transform.forward);
			this.animator.SetIKRotation(AvatarIKGoal.LeftFoot, quaternion2);
			this.animator.SetIKHintPosition(AvatarIKHint.LeftKnee, this.hit.point + this.offset + base.transform.forward);
		}
		else
		{
			num3 = 0f;
		}
		this.leftWeightSmooth = Mathf.SmoothDamp(this.leftWeightSmooth, num3, ref this.leftWeightVelocity, 0.025f);
		this.animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, this.leftWeightSmooth);
		this.animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, this.leftWeightSmooth);
		this.animator.SetIKHintPositionWeight(AvatarIKHint.LeftKnee, this.leftWeightSmooth);
		if (num3 == 0f && num2 == 0f)
		{
			num = y;
		}
		if (this.overrideIK)
		{
			bool flag2 = this.overrideLock;
		}
		float num4 = 0f;
		if (num3 <= 0.8f)
		{
			bool flag3 = num2 > 0.8f;
		}
		float num5 = Mathf.Max(new float[] { this.leftWeightSmooth, this.rightWeightSmooth, num4 });
		float num6 = this.minHeightSmooth;
		if (flag && !this.overrideLock && Player.movement.velocity.sqrMagnitude < 1f && !Player.itemManager.IsAiming)
		{
			this.heightGravityVelocity += 1.3f * Time.deltaTime * Physics.gravity.y;
			float num7 = this.minHeightSmooth + this.heightGravityVelocity * Time.deltaTime;
			float num8 = Mathf.InverseLerp(0.3f, 0.8f, num5);
			num = Mathf.Lerp(y, num, num8);
			float num9 = this.smoothTime * Mathf.Lerp(1f, 0.25f, Mathf.Abs(this.minHeightSmooth - num - 0.175f) / 0.175f);
			if (num > this.minHeightSmooth)
			{
				this.minHeightSmooth = Mathf.SmoothDamp(this.minHeightSmooth, num, ref this.heightVelocity, num9);
				this.heightGravityVelocity = (this.minHeightSmooth - num6) / Time.deltaTime;
			}
			else
			{
				this.minHeightSmooth = Mathf.SmoothDamp(this.minHeightSmooth, num, ref this.heightVelocity, num9);
				if (num7 > this.minHeightSmooth)
				{
					this.minHeightSmooth = num7;
					this.heightVelocity = this.heightGravityVelocity;
				}
				else
				{
					this.heightGravityVelocity = (this.minHeightSmooth - num6) / Time.deltaTime;
				}
			}
			if (Mathf.Abs(this.minHeightSmooth - y) > this.maxDelta)
			{
				this.minHeightSmooth = Mathf.Clamp(this.minHeightSmooth, y - this.maxDelta, y + this.maxDelta);
			}
			this.currentDeltaY = this.minHeightSmooth - y;
			return;
		}
		this.heightVelocity = (this.heightGravityVelocity = this.movement.velocity.y);
		this.currentDeltaY = Mathf.MoveTowards(this.currentDeltaY, 0f, 5f * Time.deltaTime);
		this.minHeightSmooth = y + this.currentDeltaY;
	}

	private const float legLength = 0.35f;

	public bool ikActive = true;

	private Animator animator;

	public Vector3 offset;

	public LayerMask RayMask;

	public Transform leftFoot;

	public Transform rightFoot;

	private float leftWeightSmooth;

	private float leftWeightVelocity;

	private Vector3 leftPosition;

	private float rightWeightSmooth;

	private float rightWeightVelocity;

	private Vector3 rightPosition;

	public float minHeightSmooth;

	private float heightVelocity;

	private float currentDeltaY;

	public float smoothTime = 0.05f;

	public float maxDelta = 0.25f;

	private float heightGravityVelocity;

	[Space]
	public bool overrideIK;

	public ICustomFootIKPositions customIKPositions;

	public bool overrideLock;

	private RaycastHit hit;

	public PlayerMovement movement;
}
