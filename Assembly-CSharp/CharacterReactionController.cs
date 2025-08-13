using System;
using UnityEngine;

// Token: 0x020001AF RID: 431
public class CharacterReactionController : MonoBehaviour
{
	// Token: 0x060008E4 RID: 2276 RVA: 0x00029C68 File Offset: 0x00027E68
	private void Awake()
	{
		CharacterReactionController.c = this;
		this.playerInput = base.GetComponent<PlayerInput>();
		this.movement = base.GetComponent<PlayerMovement>();
		this.animator = this.movement.animator;
	}

	// Token: 0x060008E5 RID: 2277 RVA: 0x00029C99 File Offset: 0x00027E99
	private void OnEnable()
	{
		CharacterReactionController.c = this;
	}

	// Token: 0x060008E6 RID: 2278 RVA: 0x00029CA4 File Offset: 0x00027EA4
	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawRay(this.hips.position, this.bodyForward);
		Gizmos.DrawRay(this.hips.position, this.bodyUp);
		Gizmos.DrawRay(this.neck.position, this.neckForward);
	}

	// Token: 0x060008E7 RID: 2279 RVA: 0x00029CF4 File Offset: 0x00027EF4
	private void Start()
	{
		this.lastNeckForward = (this.nextNeckForward = (this.neckForward = this.neck.forward));
		this.lastBodyForward = (this.nextBodyForward = (this.bodyForward = this.body.forward));
		this.lastBodyUp = (this.nextBodyUp = (this.bodyUp = Vector3.up));
		this.bodyRotationCenter = this.body.parent.InverseTransformPoint(base.transform.TransformPoint(this.localRotationCenter));
		this.bodyDisplacement = this.body.localPosition - this.bodyRotationCenter;
	}

	// Token: 0x060008E8 RID: 2280 RVA: 0x00029DA8 File Offset: 0x00027FA8
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

	// Token: 0x060008E9 RID: 2281 RVA: 0x0002A556 File Offset: 0x00028756
	private Vector3 ClampNeckDirection(Vector3 direction)
	{
		return Vector3.RotateTowards(this.bodyForward, direction, 1.2217305f, 1f);
	}

	// Token: 0x060008EA RID: 2282 RVA: 0x0002A570 File Offset: 0x00028770
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

	// Token: 0x060008EB RID: 2283 RVA: 0x0002A61C File Offset: 0x0002881C
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

	// Token: 0x060008EC RID: 2284 RVA: 0x0002A6C0 File Offset: 0x000288C0
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

	// Token: 0x060008ED RID: 2285 RVA: 0x0002ABA4 File Offset: 0x00028DA4
	public void SetForward(Vector3 forward)
	{
		this.bodyForward = forward;
		this.nextBodyForward = forward;
		this.neckForward = forward;
		this.nextNeckForward = forward;
		this.UpdateRotations();
	}

	// Token: 0x04000AFA RID: 2810
	public static CharacterReactionController c;

	// Token: 0x04000AFB RID: 2811
	private Animator animator;

	// Token: 0x04000AFC RID: 2812
	private int headTrackingID = Animator.StringToHash("HeadTracking");

	// Token: 0x04000AFD RID: 2813
	private float smoothAnimatorHeadTracking;

	// Token: 0x04000AFE RID: 2814
	private PlayerInput playerInput;

	// Token: 0x04000AFF RID: 2815
	private PlayerMovement movement;

	// Token: 0x04000B00 RID: 2816
	public Transform body;

	// Token: 0x04000B01 RID: 2817
	public Transform neck;

	// Token: 0x04000B02 RID: 2818
	public Transform[] spine;

	// Token: 0x04000B03 RID: 2819
	public Rigidbody rigidbody;

	// Token: 0x04000B04 RID: 2820
	public float rotationSpeed = 2f;

	// Token: 0x04000B05 RID: 2821
	public float neckRotationSpeed = 2f;

	// Token: 0x04000B06 RID: 2822
	public float bodyRotationSpeed = 5f;

	// Token: 0x04000B07 RID: 2823
	public float glidingRotationSpeed = 1f;

	// Token: 0x04000B08 RID: 2824
	public float sleddingBodySpeed;

	// Token: 0x04000B09 RID: 2825
	public float sleddingBodyAngling;

	// Token: 0x04000B0A RID: 2826
	public float sleddingNeckSpeed;

	// Token: 0x04000B0B RID: 2827
	public float swimmingBodySpeed;

	// Token: 0x04000B0C RID: 2828
	public float neckUprightLerp = 0.5f;

	// Token: 0x04000B0D RID: 2829
	public Vector3 localRotationCenter;

	// Token: 0x04000B0E RID: 2830
	private Vector3 bodyRotationCenter;

	// Token: 0x04000B0F RID: 2831
	private Vector3 bodyDisplacement;

	// Token: 0x04000B10 RID: 2832
	public Vector3 climbingDisplacement;

	// Token: 0x04000B11 RID: 2833
	private Vector3 neckForward;

	// Token: 0x04000B12 RID: 2834
	private Vector3 lastNeckForward;

	// Token: 0x04000B13 RID: 2835
	private Vector3 nextNeckForward;

	// Token: 0x04000B14 RID: 2836
	private Vector3 bodyForward;

	// Token: 0x04000B15 RID: 2837
	private Vector3 lastBodyForward;

	// Token: 0x04000B16 RID: 2838
	private Vector3 nextBodyForward;

	// Token: 0x04000B17 RID: 2839
	private Vector3 bodyUp;

	// Token: 0x04000B18 RID: 2840
	private Vector3 lastBodyUp;

	// Token: 0x04000B19 RID: 2841
	private Vector3 nextBodyUp;

	// Token: 0x04000B1A RID: 2842
	private Vector3 upperBodyForward;

	// Token: 0x04000B1B RID: 2843
	private Vector3 upperBodyUp;

	// Token: 0x04000B1C RID: 2844
	private float climbingMulti;

	// Token: 0x04000B1D RID: 2845
	public FootIKSmooth footIK;

	// Token: 0x04000B1E RID: 2846
	public Transform hips;

	// Token: 0x04000B1F RID: 2847
	private Vector3 lastPosition;

	// Token: 0x04000B20 RID: 2848
	public float leanAmount = 0.5f;

	// Token: 0x04000B21 RID: 2849
	public float maxLean = 30f;

	// Token: 0x04000B22 RID: 2850
	private float lean;

	// Token: 0x04000B23 RID: 2851
	public bool isAiming;

	// Token: 0x04000B24 RID: 2852
	private float isAimingSmooth;

	// Token: 0x04000B25 RID: 2853
	private float aimEndTime = -1f;

	// Token: 0x04000B26 RID: 2854
	private const float aimEndDelay = 0.5f;

	// Token: 0x04000B27 RID: 2855
	private float forceUpperBodyRotation;

	// Token: 0x04000B28 RID: 2856
	private float positionOffsetSmooth;

	// Token: 0x04000B29 RID: 2857
	private float rotationOffsetSmooth;
}
