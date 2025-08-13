using System;
using UnityEngine;

// Token: 0x0200022B RID: 555
public class CharacterReactionController : MonoBehaviour
{
	// Token: 0x06000A6B RID: 2667 RVA: 0x0000A093 File Offset: 0x00008293
	private void Awake()
	{
		CharacterReactionController.c = this;
		this.playerInput = base.GetComponent<PlayerInput>();
		this.movement = base.GetComponent<PlayerMovement>();
		this.animator = this.movement.animator;
	}

	// Token: 0x06000A6C RID: 2668 RVA: 0x0000A0C4 File Offset: 0x000082C4
	private void OnEnable()
	{
		CharacterReactionController.c = this;
	}

	// Token: 0x06000A6D RID: 2669 RVA: 0x0003C2B8 File Offset: 0x0003A4B8
	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawRay(this.hips.position, this.bodyForward);
		Gizmos.DrawRay(this.hips.position, this.bodyUp);
		Gizmos.DrawRay(this.neck.position, this.neckForward);
	}

	// Token: 0x06000A6E RID: 2670 RVA: 0x0003C308 File Offset: 0x0003A508
	private void Start()
	{
		this.lastNeckForward = (this.nextNeckForward = (this.neckForward = this.neck.forward));
		this.lastBodyForward = (this.nextBodyForward = (this.bodyForward = this.body.forward));
		this.lastBodyUp = (this.nextBodyUp = (this.bodyUp = Vector3.up));
		this.bodyRotationCenter = this.body.parent.InverseTransformPoint(base.transform.TransformPoint(this.localRotationCenter));
		this.bodyDisplacement = this.body.localPosition - this.bodyRotationCenter;
	}

	// Token: 0x06000A6F RID: 2671 RVA: 0x0003C3BC File Offset: 0x0003A5BC
	private void UpdateReactions()
	{
		this.lastBodyForward = this.nextBodyForward;
		this.lastBodyUp = this.nextBodyUp;
		this.lastNeckForward = this.nextNeckForward;
		if (this.movement.enabled)
		{
			this.climbingMulti = Mathf.MoveTowards(this.climbingMulti, this.movement.IsClimbing ? 0.5f : 1f, 2f * Time.deltaTime);
			if (this.movement.modCustomMovement)
			{
				Vector3 moddedForward = this.movement.moddedForward;
				Vector3 moddedUp = this.movement.moddedUp;
				this.bodyForward = MathUtils.SlerpFlat(this.bodyForward, moddedForward, 2f * this.bodyRotationSpeed * Time.deltaTime, false);
				this.bodyUp = Vector3.Slerp(this.bodyUp, moddedUp, 2f * this.bodyRotationSpeed * Time.deltaTime);
				this.StandardNeckForward();
			}
			else if (this.movement.IsClimbing)
			{
				Vector3 climbingDirection = this.movement.climbingDirection;
				climbingDirection.y = 0f;
				this.bodyForward = Vector3.Slerp(this.bodyForward, climbingDirection, this.climbingMulti * this.bodyRotationSpeed * Time.deltaTime);
				this.bodyUp = Vector3.Slerp(this.bodyUp, Vector3.up, 10f * Time.deltaTime);
				if (this.movement.Stamina > 0f)
				{
					this.StandardNeckForward();
				}
				else
				{
					this.neckForward = Vector3.Slerp(this.neckForward, (this.bodyForward.normalized + 2f * Vector3.up).normalized, this.neckRotationSpeed * Time.deltaTime);
				}
			}
			else if (this.movement.isGliding)
			{
				Vector3 velocity = this.rigidbody.velocity;
				velocity.y = 0f;
				if (velocity != Vector3.zero)
				{
					this.bodyForward = Vector3.Slerp(this.bodyForward, velocity, this.glidingRotationSpeed * Time.deltaTime);
				}
				this.bodyUp = Vector3.Slerp(this.bodyUp, Vector3.up + 0.5f * this.playerInput.smoothedInputDirection, 3f * Time.deltaTime);
				this.StandardNeckForward();
			}
			else if (this.movement.isSledding)
			{
				Vector3 velocity2 = this.rigidbody.velocity;
				velocity2.y = 0f;
				if (velocity2 != Vector3.zero)
				{
					this.bodyForward = MathUtils.SlerpFlat(this.bodyForward, 0.1f * base.transform.forward + velocity2, this.sleddingBodySpeed * Time.deltaTime, false);
				}
				this.bodyUp = Vector3.Slerp(this.bodyUp, this.movement.animGroundNormal + this.sleddingBodyAngling * this.playerInput.smoothedInputDirection, 10f * Time.deltaTime);
				this.neckForward = Vector3.Slerp(this.neckForward, Vector3.RotateTowards(this.bodyForward.normalized, 0.1f * this.bodyForward + this.playerInput.smoothedInputDirection, 1f, 1f), this.sleddingNeckSpeed * Time.deltaTime);
			}
			else if (this.movement.IsSwimming)
			{
				this.rigidbody.velocity.y = 0f;
				Vector3 smoothedInputDirection = this.playerInput.smoothedInputDirection;
				if (smoothedInputDirection.sqrMagnitude > 0.1f)
				{
					this.bodyForward = MathUtils.SlerpFlat(this.bodyForward, smoothedInputDirection.normalized, this.swimmingBodySpeed * Time.deltaTime, false);
				}
				this.bodyUp = Vector3.Slerp(this.bodyUp, Vector3.up, 10f * Time.deltaTime);
				this.neckForward = MathUtils.SlerpFlat(this.neckForward, Vector3.RotateTowards(this.bodyForward.normalized, 0.1f * this.bodyForward + this.playerInput.smoothedInputDirection, 1f, 1f), this.sleddingNeckSpeed * Time.deltaTime, false);
			}
			else
			{
				Vector3 vector = this.playerInput.inputDirection;
				Vector3 velocity3 = this.rigidbody.velocity;
				velocity3.y = 0f;
				if (vector == Vector3.zero && velocity3.sqrMagnitude > 2f)
				{
					vector = velocity3.normalized;
				}
				if (vector.sqrMagnitude > 0.2f)
				{
					Vector3.Dot(this.bodyForward, vector);
					this.bodyForward = MathUtils.SlerpFlat(this.bodyForward, vector, this.climbingMulti * this.bodyRotationSpeed * Time.deltaTime, false);
					if (!this.movement.InAir && this.movement.animatorSpeed > this.movement.Speed * 0.8f)
					{
						float num = -Vector3.SignedAngle(velocity3, vector, Vector3.up);
						num = Mathf.Clamp(num, -90f, 90f);
						if (Mathf.Abs(num) > Mathf.Abs(this.lean))
						{
							this.lean = Mathf.MoveTowards(this.lean, num, Time.deltaTime * 500f);
						}
						else
						{
							this.lean = Mathf.MoveTowards(this.lean, num, Time.deltaTime * 90f);
						}
					}
					else
					{
						this.lean = Mathf.MoveTowards(this.lean, 0f, 90f * Time.deltaTime);
					}
					float num2 = Mathf.Clamp(this.lean * this.leanAmount, -this.maxLean, this.maxLean);
					this.bodyUp = Vector3.Slerp(this.bodyUp, Quaternion.AngleAxis(num2, velocity3) * Vector3.up, 5f * Time.deltaTime);
					this.StandardNeckForward();
				}
				else
				{
					this.bodyUp = Vector3.Slerp(this.bodyUp, Vector3.up, 10f * Time.deltaTime);
					if (this.bodyForward.y != 0f)
					{
						this.bodyForward = MathUtils.SlerpFlat(this.bodyForward, new Vector3(this.bodyForward.x, 0f, this.bodyForward.z), this.bodyRotationSpeed * Time.deltaTime, false);
					}
					this.StandardNeckForward();
				}
			}
			if (!this.movement.IsGrounded || this.movement.modCustomMovement)
			{
				this.lean = 0f;
			}
		}
		else if (this.movement.isRagdolling)
		{
			Vector3 vector2 = Quaternion.FromToRotation(this.hips.up, Vector3.up) * this.hips.forward;
			vector2.y = 0f;
			if (vector2 != Vector3.zero)
			{
				vector2.Normalize();
				this.bodyForward = vector2;
				this.neckForward = vector2;
			}
		}
		else
		{
			this.bodyForward = Vector3.Slerp(this.bodyForward, this.rigidbody.velocity.normalized, 10f * Time.deltaTime);
			this.bodyUp = Vector3.Slerp(this.bodyUp, (Vector3.up + 0.2f * this.playerInput.inputDirection).normalized, 10f * Time.deltaTime);
			this.StandardNeckForward();
		}
		this.neckForward = this.ClampNeckDirection(this.neckForward);
		this.lastPosition = this.body.position;
	}

	// Token: 0x06000A70 RID: 2672 RVA: 0x0000A0CC File Offset: 0x000082CC
	private Vector3 ClampNeckDirection(Vector3 direction)
	{
		return Vector3.RotateTowards(this.bodyForward, direction, 1.2217305f, 1f);
	}

	// Token: 0x06000A71 RID: 2673 RVA: 0x0003CB6C File Offset: 0x0003AD6C
	private void StandardNeckForward()
	{
		Vector3 vector = this.bodyForward;
		vector = Vector3.Slerp(vector, this.playerInput.smoothedInputDirection, 0.5f * this.playerInput.smoothedInputDirection.magnitude);
		if (this.movement.modCustomMovement)
		{
			vector += 0.5f * Vector3.ClampMagnitude(0.25f * this.rigidbody.velocity, 1f).y * Vector3.up;
		}
		vector = this.ClampNeckDirection(vector);
		this.neckForward = Vector3.Slerp(this.neckForward, vector, this.neckRotationSpeed * Time.deltaTime);
	}

	// Token: 0x06000A72 RID: 2674 RVA: 0x0003CC18 File Offset: 0x0003AE18
	private void LateUpdate()
	{
		this.UpdateReactions();
		if (this.isAiming || this.isAimingSmooth > 0f)
		{
			if (this.isAiming)
			{
				this.aimEndTime = Time.time + 0.5f;
			}
			bool flag = Time.time < this.aimEndTime;
			this.isAimingSmooth = Mathf.MoveTowards(this.isAimingSmooth, flag ? 1f : 0f, (flag ? 5f : 2f) * Time.deltaTime);
		}
		else
		{
			this.upperBodyForward = this.bodyForward;
			this.upperBodyUp = this.bodyUp;
		}
		this.UpdateRotations();
	}

	// Token: 0x06000A73 RID: 2675 RVA: 0x0003CCBC File Offset: 0x0003AEBC
	private void UpdateRotations()
	{
		if (this.movement == null)
		{
			return;
		}
		if (!this.movement.isRagdolling)
		{
			Vector3 vector = this.bodyForward;
			Vector3 vector2 = this.bodyUp;
			Vector3 vector3 = this.neckForward;
			if (this.isAimingSmooth > 0f)
			{
				float num = Mathf.SmoothStep(0f, 1f, this.isAimingSmooth);
				Transform t = MainCamera.t;
				Vector3 vector4 = t.forward;
				Vector3 vector5 = t.right;
				if (PlayerOrbitCamera.active.cameraMode == PlayerOrbitCamera.CameraMode.Backward)
				{
					Quaternion rotation = PlayerOrbitCamera.active.GetRotation();
					vector4 = rotation * Vector3.forward;
					vector5 = rotation * Vector3.right;
				}
				Vector3 vector6 = t.position + (10f * vector4.Flat().normalized - 2f * vector5) - this.neck.position;
				vector6.Normalize();
				Vector3 vector7 = Vector3.Slerp(this.bodyForward, vector6, num);
				vector7.y = 0f;
				vector = Vector3.RotateTowards(vector7, this.bodyForward, float.PositiveInfinity, 90f);
				vector = Vector3.RotateTowards(vector, vector7, float.PositiveInfinity, 0.5f * Time.deltaTime);
				this.bodyForward = Vector3.Slerp(this.bodyForward, vector, num);
				vector = this.bodyForward;
				if (PlayerOrbitCamera.active.cameraMode == PlayerOrbitCamera.CameraMode.Backward)
				{
					vector3 = this.ClampNeckDirection(Vector3.Slerp(this.neckForward, (t.position - (this.neck.position + new Vector3(0f, 0.2f, 0f))).normalized, 0.75f * num));
				}
				else
				{
					vector3 = this.ClampNeckDirection(Vector3.Slerp(this.neckForward, vector4, 0.75f * num));
				}
			}
			this.body.rotation = Quaternion.LookRotation(vector, vector2);
			bool flag = !this.movement.isSledding && !this.movement.modCustomMovement;
			this.rotationOffsetSmooth = Mathf.MoveTowards(this.rotationOffsetSmooth, flag ? 1f : 0f, 5f * Time.deltaTime);
			bool flag2 = !this.movement.isSledding;
			this.positionOffsetSmooth = Mathf.MoveTowards(this.positionOffsetSmooth, flag2 ? 1f : 0f, 5f * Time.deltaTime);
			Vector3 vector8 = Vector3.zero;
			if (this.rotationOffsetSmooth > 0f)
			{
				Vector3 vector9 = this.bodyRotationCenter + this.body.localRotation * this.bodyDisplacement;
				if (this.rotationOffsetSmooth < 1f)
				{
					vector9 *= Mathf.SmoothStep(0f, 1f, this.rotationOffsetSmooth);
				}
				vector8 = vector9;
			}
			if (this.positionOffsetSmooth > 0f)
			{
				if (this.footIK.enabled && this.footIK.smoothDeltaY != 0f)
				{
					vector8.y += (1f - this.movement.isClimbingSmooth) * this.footIK.smoothDeltaY;
				}
				if (this.positionOffsetSmooth < 1f)
				{
					vector8 *= Mathf.SmoothStep(0f, 1f, this.positionOffsetSmooth);
				}
			}
			this.body.localPosition = vector8;
			float @float = this.animator.GetFloat("UpperBodyRotation");
			this.forceUpperBodyRotation = Mathf.MoveTowards(this.forceUpperBodyRotation, (@float == 0f) ? 0f : 1f, 10f * Time.deltaTime);
			if (this.forceUpperBodyRotation > 0f)
			{
				Quaternion quaternion = this.body.rotation * Quaternion.AngleAxis(@float, Vector3.up);
				for (int i = 0; i < this.spine.Length; i++)
				{
					float num2 = (float)((i + 1) / this.spine.Length);
					this.spine[i].rotation = Quaternion.Slerp(this.spine[i].rotation, quaternion, num2 * this.isAimingSmooth);
				}
			}
			this.smoothAnimatorHeadTracking = Mathf.MoveTowards(this.smoothAnimatorHeadTracking, this.animator.GetFloat(this.headTrackingID), 4f * Time.deltaTime);
			float num3 = this.smoothAnimatorHeadTracking * (1f - Player.actor.globalLookAtWeight * Player.actor.headLookAtWeight);
			if (!this.movement.IsSwimming && num3 > 0f)
			{
				Quaternion quaternion2 = Quaternion.LookRotation(vector3, Vector3.Lerp(this.neck.parent.up, Vector3.up, this.neckUprightLerp));
				this.neck.rotation = Quaternion.Slerp(this.neck.rotation, quaternion2, num3);
			}
		}
	}

	// Token: 0x06000A74 RID: 2676 RVA: 0x0003D1A0 File Offset: 0x0003B3A0
	public void SetForward(Vector3 forward)
	{
		this.bodyForward = forward;
		this.nextBodyForward = forward;
		this.neckForward = forward;
		this.nextNeckForward = forward;
		this.UpdateRotations();
	}

	// Token: 0x04000D02 RID: 3330
	public static CharacterReactionController c;

	// Token: 0x04000D03 RID: 3331
	private Animator animator;

	// Token: 0x04000D04 RID: 3332
	private int headTrackingID = Animator.StringToHash("HeadTracking");

	// Token: 0x04000D05 RID: 3333
	private float smoothAnimatorHeadTracking;

	// Token: 0x04000D06 RID: 3334
	private PlayerInput playerInput;

	// Token: 0x04000D07 RID: 3335
	private PlayerMovement movement;

	// Token: 0x04000D08 RID: 3336
	public Transform body;

	// Token: 0x04000D09 RID: 3337
	public Transform neck;

	// Token: 0x04000D0A RID: 3338
	public Transform[] spine;

	// Token: 0x04000D0B RID: 3339
	public Rigidbody rigidbody;

	// Token: 0x04000D0C RID: 3340
	public float rotationSpeed = 2f;

	// Token: 0x04000D0D RID: 3341
	public float neckRotationSpeed = 2f;

	// Token: 0x04000D0E RID: 3342
	public float bodyRotationSpeed = 5f;

	// Token: 0x04000D0F RID: 3343
	public float glidingRotationSpeed = 1f;

	// Token: 0x04000D10 RID: 3344
	public float sleddingBodySpeed;

	// Token: 0x04000D11 RID: 3345
	public float sleddingBodyAngling;

	// Token: 0x04000D12 RID: 3346
	public float sleddingNeckSpeed;

	// Token: 0x04000D13 RID: 3347
	public float swimmingBodySpeed;

	// Token: 0x04000D14 RID: 3348
	public float neckUprightLerp = 0.5f;

	// Token: 0x04000D15 RID: 3349
	public Vector3 localRotationCenter;

	// Token: 0x04000D16 RID: 3350
	private Vector3 bodyRotationCenter;

	// Token: 0x04000D17 RID: 3351
	private Vector3 bodyDisplacement;

	// Token: 0x04000D18 RID: 3352
	public Vector3 climbingDisplacement;

	// Token: 0x04000D19 RID: 3353
	private Vector3 neckForward;

	// Token: 0x04000D1A RID: 3354
	private Vector3 lastNeckForward;

	// Token: 0x04000D1B RID: 3355
	private Vector3 nextNeckForward;

	// Token: 0x04000D1C RID: 3356
	private Vector3 bodyForward;

	// Token: 0x04000D1D RID: 3357
	private Vector3 lastBodyForward;

	// Token: 0x04000D1E RID: 3358
	private Vector3 nextBodyForward;

	// Token: 0x04000D1F RID: 3359
	private Vector3 bodyUp;

	// Token: 0x04000D20 RID: 3360
	private Vector3 lastBodyUp;

	// Token: 0x04000D21 RID: 3361
	private Vector3 nextBodyUp;

	// Token: 0x04000D22 RID: 3362
	private Vector3 upperBodyForward;

	// Token: 0x04000D23 RID: 3363
	private Vector3 upperBodyUp;

	// Token: 0x04000D24 RID: 3364
	private float climbingMulti;

	// Token: 0x04000D25 RID: 3365
	public FootIKSmooth footIK;

	// Token: 0x04000D26 RID: 3366
	public Transform hips;

	// Token: 0x04000D27 RID: 3367
	private Vector3 lastPosition;

	// Token: 0x04000D28 RID: 3368
	public float leanAmount = 0.5f;

	// Token: 0x04000D29 RID: 3369
	public float maxLean = 30f;

	// Token: 0x04000D2A RID: 3370
	private float lean;

	// Token: 0x04000D2B RID: 3371
	public bool isAiming;

	// Token: 0x04000D2C RID: 3372
	private float isAimingSmooth;

	// Token: 0x04000D2D RID: 3373
	private float aimEndTime = -1f;

	// Token: 0x04000D2E RID: 3374
	private const float aimEndDelay = 0.5f;

	// Token: 0x04000D2F RID: 3375
	private float forceUpperBodyRotation;

	// Token: 0x04000D30 RID: 3376
	private float positionOffsetSmooth;

	// Token: 0x04000D31 RID: 3377
	private float rotationOffsetSmooth;
}
