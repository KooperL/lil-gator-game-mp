using System;
using UnityEngine;

// Token: 0x02000172 RID: 370
public class FootIKSmooth : MonoBehaviour
{
	// Token: 0x170000AC RID: 172
	// (get) Token: 0x060006F5 RID: 1781 RVA: 0x0000713D File Offset: 0x0000533D
	public float smoothDeltaY
	{
		get
		{
			return this.minHeightSmooth - base.transform.parent.position.y;
		}
	}

	// Token: 0x060006F6 RID: 1782 RVA: 0x0000715B File Offset: 0x0000535B
	public void ClearOverrides()
	{
		this.overrideIK = false;
		this.customIKPositions = null;
		this.overrideLock = false;
	}

	// Token: 0x060006F7 RID: 1783 RVA: 0x00007172 File Offset: 0x00005372
	private void Awake()
	{
		this.animator = base.GetComponent<Animator>();
	}

	// Token: 0x060006F8 RID: 1784 RVA: 0x00007180 File Offset: 0x00005380
	private void Start()
	{
		this.minHeightSmooth = base.transform.position.y;
	}

	// Token: 0x060006F9 RID: 1785 RVA: 0x000325A4 File Offset: 0x000307A4
	public void ResetHeight()
	{
		this.minHeightSmooth = base.transform.position.y;
		this.heightVelocity = (this.heightGravityVelocity = 0f);
	}

	// Token: 0x060006FA RID: 1786 RVA: 0x000325DC File Offset: 0x000307DC
	private bool FootRaycast(Vector3 footPos, Vector3 rayDirection, float parentHeight, out RaycastHit hit)
	{
		bool flag = false;
		RaycastHit raycastHit = default(RaycastHit);
		Vector3 vector = footPos - rayDirection;
		vector.y = parentHeight + this.maxDelta;
		RaycastHit raycastHit2;
		bool flag2 = Physics.SphereCast(vector, 0.05f, rayDirection, ref raycastHit2, 2f * this.maxDelta, this.RayMask);
		if (!flag2 || raycastHit2.point.y - this.minHeightSmooth < -0.25f)
		{
			flag = Physics.SphereCast(vector, 0.2f, rayDirection, ref raycastHit, 2f * this.maxDelta, this.RayMask);
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

	// Token: 0x060006FB RID: 1787 RVA: 0x00032698 File Offset: 0x00030898
	public Vector3 GetFootCenter()
	{
		Vector3 vector = Vector3.Lerp(this.leftPosition, this.rightPosition, 0.5f);
		vector.y = Mathf.Max(vector.y, this.minHeightSmooth);
		return vector;
	}

	// Token: 0x060006FC RID: 1788 RVA: 0x000326D8 File Offset: 0x000308D8
	private void OnAnimatorIK()
	{
		if (!this.ikActive)
		{
			this.minHeightSmooth = base.transform.parent.position.y;
			this.animator.SetIKPositionWeight(1, 0f);
			this.animator.SetIKRotationWeight(1, 0f);
			this.animator.SetIKPositionWeight(0, 0f);
			this.animator.SetIKRotationWeight(0, 0f);
			this.animator.SetIKHintPositionWeight(1, 0f);
			this.animator.SetIKHintPositionWeight(0, 0f);
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
				this.animator.SetIKPosition(1, this.rightPosition);
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
			this.animator.SetIKPosition(1, this.rightPosition);
			Quaternion quaternion = Quaternion.FromToRotation(Vector3.up, this.hit.normal) * Quaternion.LookRotation(base.transform.forward);
			this.animator.SetIKRotation(1, quaternion);
			this.animator.SetIKHintPosition(1, this.hit.point + this.offset + base.transform.forward);
		}
		else
		{
			num2 = 0f;
		}
		this.rightWeightSmooth = Mathf.SmoothDamp(this.rightWeightSmooth, num2, ref this.rightWeightVelocity, 0.025f);
		this.animator.SetIKPositionWeight(1, this.rightWeightSmooth);
		this.animator.SetIKRotationWeight(1, this.rightWeightSmooth);
		this.animator.SetIKHintPositionWeight(1, this.rightWeightSmooth);
		vector2 = this.leftFoot.position;
		if (this.overrideIK)
		{
			if (this.customIKPositions != null && num3 > 0f)
			{
				this.leftPosition = this.customIKPositions.GetLeftFootTarget(vector2);
				this.animator.SetIKPosition(0, this.leftPosition);
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
			this.animator.SetIKPosition(0, this.leftPosition);
			Quaternion quaternion2 = Quaternion.FromToRotation(Vector3.up, this.hit.normal) * Quaternion.LookRotation(base.transform.forward);
			this.animator.SetIKRotation(0, quaternion2);
			this.animator.SetIKHintPosition(0, this.hit.point + this.offset + base.transform.forward);
		}
		else
		{
			num3 = 0f;
		}
		this.leftWeightSmooth = Mathf.SmoothDamp(this.leftWeightSmooth, num3, ref this.leftWeightVelocity, 0.025f);
		this.animator.SetIKPositionWeight(0, this.leftWeightSmooth);
		this.animator.SetIKRotationWeight(0, this.leftWeightSmooth);
		this.animator.SetIKHintPositionWeight(0, this.leftWeightSmooth);
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

	// Token: 0x04000950 RID: 2384
	private const float legLength = 0.35f;

	// Token: 0x04000951 RID: 2385
	public bool ikActive = true;

	// Token: 0x04000952 RID: 2386
	private Animator animator;

	// Token: 0x04000953 RID: 2387
	public Vector3 offset;

	// Token: 0x04000954 RID: 2388
	public LayerMask RayMask;

	// Token: 0x04000955 RID: 2389
	public Transform leftFoot;

	// Token: 0x04000956 RID: 2390
	public Transform rightFoot;

	// Token: 0x04000957 RID: 2391
	private float leftWeightSmooth;

	// Token: 0x04000958 RID: 2392
	private float leftWeightVelocity;

	// Token: 0x04000959 RID: 2393
	private Vector3 leftPosition;

	// Token: 0x0400095A RID: 2394
	private float rightWeightSmooth;

	// Token: 0x0400095B RID: 2395
	private float rightWeightVelocity;

	// Token: 0x0400095C RID: 2396
	private Vector3 rightPosition;

	// Token: 0x0400095D RID: 2397
	public float minHeightSmooth;

	// Token: 0x0400095E RID: 2398
	private float heightVelocity;

	// Token: 0x0400095F RID: 2399
	private float currentDeltaY;

	// Token: 0x04000960 RID: 2400
	public float smoothTime = 0.05f;

	// Token: 0x04000961 RID: 2401
	public float maxDelta = 0.25f;

	// Token: 0x04000962 RID: 2402
	private float heightGravityVelocity;

	// Token: 0x04000963 RID: 2403
	[Space]
	public bool overrideIK;

	// Token: 0x04000964 RID: 2404
	public ICustomFootIKPositions customIKPositions;

	// Token: 0x04000965 RID: 2405
	public bool overrideLock;

	// Token: 0x04000966 RID: 2406
	private RaycastHit hit;

	// Token: 0x04000967 RID: 2407
	public PlayerMovement movement;
}
