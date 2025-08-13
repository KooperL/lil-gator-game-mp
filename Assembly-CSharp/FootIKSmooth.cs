using System;
using UnityEngine;

// Token: 0x0200011A RID: 282
public class FootIKSmooth : MonoBehaviour
{
	// Token: 0x1700004A RID: 74
	// (get) Token: 0x060005CF RID: 1487 RVA: 0x0001E592 File Offset: 0x0001C792
	public float smoothDeltaY
	{
		get
		{
			return this.minHeightSmooth - base.transform.parent.position.y;
		}
	}

	// Token: 0x060005D0 RID: 1488 RVA: 0x0001E5B0 File Offset: 0x0001C7B0
	public void ClearOverrides()
	{
		this.overrideIK = false;
		this.customIKPositions = null;
		this.overrideLock = false;
	}

	// Token: 0x060005D1 RID: 1489 RVA: 0x0001E5C7 File Offset: 0x0001C7C7
	private void Awake()
	{
		this.animator = base.GetComponent<Animator>();
	}

	// Token: 0x060005D2 RID: 1490 RVA: 0x0001E5D5 File Offset: 0x0001C7D5
	private void Start()
	{
		this.minHeightSmooth = base.transform.position.y;
	}

	// Token: 0x060005D3 RID: 1491 RVA: 0x0001E5F0 File Offset: 0x0001C7F0
	public void ResetHeight()
	{
		this.minHeightSmooth = base.transform.position.y;
		this.heightVelocity = (this.heightGravityVelocity = 0f);
	}

	// Token: 0x060005D4 RID: 1492 RVA: 0x0001E628 File Offset: 0x0001C828
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

	// Token: 0x060005D5 RID: 1493 RVA: 0x0001E6E4 File Offset: 0x0001C8E4
	public Vector3 GetFootCenter()
	{
		Vector3 vector = Vector3.Lerp(this.leftPosition, this.rightPosition, 0.5f);
		vector.y = Mathf.Max(vector.y, this.minHeightSmooth);
		return vector;
	}

	// Token: 0x060005D6 RID: 1494 RVA: 0x0001E724 File Offset: 0x0001C924
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

	// Token: 0x040007F4 RID: 2036
	private const float legLength = 0.35f;

	// Token: 0x040007F5 RID: 2037
	public bool ikActive = true;

	// Token: 0x040007F6 RID: 2038
	private Animator animator;

	// Token: 0x040007F7 RID: 2039
	public Vector3 offset;

	// Token: 0x040007F8 RID: 2040
	public LayerMask RayMask;

	// Token: 0x040007F9 RID: 2041
	public Transform leftFoot;

	// Token: 0x040007FA RID: 2042
	public Transform rightFoot;

	// Token: 0x040007FB RID: 2043
	private float leftWeightSmooth;

	// Token: 0x040007FC RID: 2044
	private float leftWeightVelocity;

	// Token: 0x040007FD RID: 2045
	private Vector3 leftPosition;

	// Token: 0x040007FE RID: 2046
	private float rightWeightSmooth;

	// Token: 0x040007FF RID: 2047
	private float rightWeightVelocity;

	// Token: 0x04000800 RID: 2048
	private Vector3 rightPosition;

	// Token: 0x04000801 RID: 2049
	public float minHeightSmooth;

	// Token: 0x04000802 RID: 2050
	private float heightVelocity;

	// Token: 0x04000803 RID: 2051
	private float currentDeltaY;

	// Token: 0x04000804 RID: 2052
	public float smoothTime = 0.05f;

	// Token: 0x04000805 RID: 2053
	public float maxDelta = 0.25f;

	// Token: 0x04000806 RID: 2054
	private float heightGravityVelocity;

	// Token: 0x04000807 RID: 2055
	[Space]
	public bool overrideIK;

	// Token: 0x04000808 RID: 2056
	public ICustomFootIKPositions customIKPositions;

	// Token: 0x04000809 RID: 2057
	public bool overrideLock;

	// Token: 0x0400080A RID: 2058
	private RaycastHit hit;

	// Token: 0x0400080B RID: 2059
	public PlayerMovement movement;
}
