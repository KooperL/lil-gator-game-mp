using System;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Contextual/Climbing Pole")]
public class ClimbingPole : GenericPath, ICustomPlayerMovement, ICustomFootIKPositions, ICustomHandIKPositions
{
	// Token: 0x060002A2 RID: 674 RVA: 0x0000ED3E File Offset: 0x0000CF3E
	private void OnValidate()
	{
		if (!Application.isPlaying && base.enabled)
		{
			base.enabled = false;
		}
	}

	// Token: 0x060002A3 RID: 675 RVA: 0x0000ED58 File Offset: 0x0000CF58
	private void OnDrawGizmosSelected()
	{
		for (int i = 0; i < this.positions.Length; i++)
		{
			float num = (float)i / (float)(this.positions.Length - 1);
			Gizmos.DrawWireSphere(base.transform.TransformPoint(this.positions[i]), base.transform.lossyScale.x * this.thickness.Evaluate(num));
		}
	}

	// Token: 0x060002A4 RID: 676 RVA: 0x0000EDC0 File Offset: 0x0000CFC0
	[ContextMenu("Fit Capsule To Beam")]
	public void FitCapsule()
	{
		Vector3 vector = this.positions[this.positions.Length - 1];
		Vector3 vector2 = this.positions[0];
		CapsuleCollider component = base.GetComponent<CapsuleCollider>();
		component.center = 0.5f * (vector + vector2);
		component.height = Mathf.Abs(vector.y - vector2.y) + 1f;
	}

	// Token: 0x060002A5 RID: 677 RVA: 0x0000EE2C File Offset: 0x0000D02C
	private void Initialize()
	{
		this.topHeldT = base.MoveAlongPath((float)(this.positions.Length - 1), -0.75f);
		if (this.raycastForGround)
		{
			for (int i = this.positions.Length - 1; i > 0; i--)
			{
				Vector3 vector = base.GetPosition(i - 1) - base.GetPosition(i);
				float magnitude = vector.magnitude;
				RaycastHit raycastHit;
				if (Physics.SphereCast(base.GetPosition(i), 0.1f, vector, out raycastHit, magnitude, LayerUtil.GroundLayersMinusPlayer))
				{
					this.groundT = (float)i - raycastHit.distance / magnitude;
					return;
				}
			}
		}
		this.groundT = 0f;
	}

	// Token: 0x060002A6 RID: 678 RVA: 0x0000EED0 File Offset: 0x0000D0D0
	private bool IsEligible()
	{
		this.t = base.GetClosestInterpolated(Player.RawPosition);
		Vector3 vector = base.GetDirection(this.t);
		return this.IsEligible(this.t, vector);
	}

	// Token: 0x060002A7 RID: 679 RVA: 0x0000EF08 File Offset: 0x0000D108
	private bool IsEligible(float t, Vector3 direction)
	{
		if (PlayerInput.RawInput.y < 0f)
		{
			return false;
		}
		Vector3 rawPosition = Player.RawPosition;
		float num = this.distanceAllowance;
		if (Player.movement.isSledding)
		{
			return false;
		}
		if (!this.isAggressive && Player.movement.IsGrounded)
		{
			return false;
		}
		if (this.isAggressive)
		{
			if (t < this.groundT - 0.25f)
			{
				return false;
			}
		}
		else if (Player.movement.IsGrounded)
		{
			num *= 0.5f;
			if (t < this.groundT - 0.1f)
			{
				return false;
			}
		}
		else if (t < this.groundT)
		{
			return false;
		}
		if (t > this.topHeldT)
		{
			Vector3 position = base.GetPosition(this.positions.Length - 1);
			if (Player.rigidbody.velocity.y > 0f)
			{
				return false;
			}
			if (Vector3.SqrMagnitude(position - rawPosition) > num * num)
			{
				return false;
			}
		}
		else
		{
			Vector3 vector = base.GetPosition(t);
			float num2 = this.GetOffset(t);
			Vector3 vector2;
			if (Player.movement.IsClimbing)
			{
				vector2 = Player.Forward;
			}
			else if (PlayerInput.p.inputDirection.sqrMagnitude > 0.5f)
			{
				vector2 = Player.input.inputDirection;
			}
			else if (Player.rigidbody.velocity.sqrMagnitude > 0.5f)
			{
				vector2 = Player.rigidbody.velocity;
			}
			else
			{
				vector2 = Player.Forward;
			}
			vector2.y = 0f;
			vector2.Normalize();
			Vector3 vector3 = vector - rawPosition;
			vector3.y = 0f;
			vector3.Normalize();
			if (!this.isAggressive && Player.movement.IsGrounded && Vector3.Dot(vector3, vector2) < 0.6f)
			{
				return false;
			}
			vector += num2 * vector3;
			if (Vector3.SqrMagnitude(vector - rawPosition) > num * num)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060002A8 RID: 680 RVA: 0x0000F0DC File Offset: 0x0000D2DC
	private void OnTriggerStay(Collider other)
	{
		if (this.groundT < -0.5f)
		{
			this.Initialize();
		}
		if (Time.time - this.lastEnabled < 0.5f && Time.time - PlayerMovement.mostRecentEnable > 0.5f)
		{
			return;
		}
		if (base.enabled || Player.movement.JustCanceled)
		{
			return;
		}
		if (Player.movement.isRagdolling)
		{
			return;
		}
		if (Player.movement.modCustomMovement)
		{
			return;
		}
		if (!this.IsEligible())
		{
			return;
		}
		Player.movement.ForceModdedState();
		base.enabled = true;
		if (this.playLandSound)
		{
			if (this.landSoundEffect != null)
			{
				this.landSoundEffect.Play();
				return;
			}
			if (this.material != null)
			{
				this.material.PlayImpact(Player.Position, 1f, 1f);
			}
		}
	}

	// Token: 0x060002A9 RID: 681 RVA: 0x0000F1B8 File Offset: 0x0000D3B8
	private void OnEnable()
	{
		Player.movement.isModified = true;
		Player.movement.modGlideRule = PlayerMovement.ModRule.Locked;
		Player.movement.modIgnoreReady = true;
		Player.movement.modCustomMovement = true;
		Player.movement.modCustomMovementScript = this;
		Player.movement.modNoClimbing = true;
		Player.movement.modDisableCollision = true;
		Player.movement.modIgnoreGroundedness = true;
		Player.overrideAnimations.SetContextualAnimations(this.animationSet);
		Player.footIK.overrideIK = true;
		Player.footsteps.overrideSettings = true;
		Player.footsteps.overrideFootstepMaterial = this.material;
		Player.footsteps.overrideHasVisualFootsteps = false;
		Player.footsteps.overrideHasFootstepDust = false;
		Player.itemManager.SetEquippedState(PlayerItemManager.EquippedState.None, false);
		this.t = base.GetClosestInterpolated(Player.RawPosition);
		this.t = Mathf.Max(this.t, this.groundT + 0.01f);
		this.direction = base.GetPosition(this.t) - Player.RawPosition;
		this.direction.y = 0f;
		if (this.direction.sqrMagnitude > 0f)
		{
			this.direction.Normalize();
		}
		else
		{
			this.direction = Vector3.forward;
		}
		this.isOnTop = this.t > this.topHeldT;
		if (this.isOnTop)
		{
			this.t = (float)(this.positions.Length - 1);
			this.lerpToTop = 1f;
		}
		Vector3 vector = base.GetDirection(this.t);
		this.speed = Vector3.Dot(Player.rigidbody.velocity, vector);
		this.lerpToPosition = 0f;
		this.onEnable.Invoke();
		if (this.isOnTop)
		{
			Player.footIK.customIKPositions = this;
			Player.movement.modJumpRule = PlayerMovement.ModRule.Cancels;
			Player.movement.modPrimaryRule = PlayerMovement.ModRule.Allowed;
			Player.movement.modSecondaryRule = PlayerMovement.ModRule.Locked;
			Player.movement.modItemRule = PlayerMovement.ModRule.Allowed;
			return;
		}
		Player.movement.modJumpRule = PlayerMovement.ModRule.Locked;
		Player.movement.modPrimaryRule = PlayerMovement.ModRule.Locked;
		Player.movement.modSecondaryRule = PlayerMovement.ModRule.Locked;
		Player.movement.modItemRule = PlayerMovement.ModRule.Locked;
		Player.movement.modNoStaminaRecovery = true;
	}

	// Token: 0x060002AA RID: 682 RVA: 0x0000F3E4 File Offset: 0x0000D5E4
	private void OnDisable()
	{
		Player.footIK.ClearOverrides();
		Player.footsteps.ClearOverride();
		if (Player.movement.JustJumped || Player.movement.desiredJump)
		{
			if (!this.isOnTop)
			{
				Player.movement.velocity += -2.5f * this.direction;
				if (Player.movement.Stamina > 0f && this.requiresStamina)
				{
					Player.movement.Stamina = Mathf.Max(Player.movement.Stamina - Player.movement.climbJumpStaminaCost, 0f);
				}
			}
			if (this.playJumpSound)
			{
				if (this.jumpSoundEffect != null)
				{
					this.jumpSoundEffect.Play();
				}
				else if (this.material != null)
				{
					this.material.PlayImpact(Player.Position, 1f, 1f);
				}
			}
		}
		Player.movement.ClampSpeedForABit();
		this.lerpToPosition = 0f;
		this.lastEnabled = Time.time;
	}

	// Token: 0x060002AB RID: 683 RVA: 0x0000F4FA File Offset: 0x0000D6FA
	public float GetOffset(float t)
	{
		return (this.thickness.Evaluate(t / (float)(this.positions.Length - 1)) + 0f) * -1f * base.transform.lossyScale.x;
	}

	// Token: 0x060002AC RID: 684 RVA: 0x0000F534 File Offset: 0x0000D734
	public void MovementUpdate(Vector3 input, ref Vector3 position, ref Vector3 velocity, ref Vector3 direction, ref Vector3 up, ref float animationIndex)
	{
		this.t = Mathf.Clamp(this.t, 0f, (float)(this.positions.Length - 1));
		Vector3 vector = base.GetPosition(this.t);
		base.GetClosestInterpolated(position);
		Vector3 vector2;
		if (this.isOnTop)
		{
			if (this.lerpToTop < 1f)
			{
				vector = Vector3.Lerp(base.GetPosition(this.topHeldT) + this.GetOffset(this.topHeldT) * direction, base.GetPosition(this.positions.Length - 1), this.lerpToTop);
				this.lerpToTop = Mathf.MoveTowards(this.lerpToTop, 1f, 2f * Time.deltaTime);
				this.t = Mathf.Lerp(this.topHeldT, (float)(this.positions.Length - 1), this.lerpToTop);
				up = Vector3.Slerp(base.GetDirection(this.topHeldT), Vector3.up, this.lerpToTop);
				direction = this.direction;
				vector2 = Vector3.Lerp(base.GetPosition(this.topHeldT) + this.GetOffset(this.topHeldT) * direction, base.GetPosition(this.positions.Length - 1), this.lerpToTop);
				animationIndex = 3f;
				if (this.lerpToTop == 1f)
				{
					Player.movement.modPrimaryRule = PlayerMovement.ModRule.Allowed;
					Player.movement.modSecondaryRule = PlayerMovement.ModRule.Locked;
					Player.movement.modItemRule = PlayerMovement.ModRule.Allowed;
					Player.movement.modNoStaminaRecovery = false;
					Player.footIK.customIKPositions = this;
				}
			}
			else
			{
				this.t = (float)(this.positions.Length - 1);
				up = Vector3.up;
				if (input != Vector3.zero)
				{
					direction = MathUtils.SlerpFlat(direction, input, Time.deltaTime * 5f, false);
				}
				direction.y = 0f;
				direction.Normalize();
				vector2 = base.GetPosition(this.t);
				animationIndex = 4f;
			}
		}
		else
		{
			vector += this.GetOffset(this.t) * direction;
			up = base.GetDirection(this.t);
			Vector2 rawInput = PlayerInput.RawInput;
			if (rawInput.x != 0f)
			{
				this.direction = Quaternion.Euler(0f, Time.deltaTime * -120f * rawInput.x, 0f) * this.direction;
			}
			this.direction.y = 0f;
			this.direction.Normalize();
			direction = Quaternion.FromToRotation(Vector3.up, up) * this.direction;
			float num = rawInput.y;
			if (Player.movement.Stamina <= 0f && this.requiresStamina)
			{
				num = -1f;
			}
			else if (num > 0f && this.requiresStamina)
			{
				float num2 = 0.5f;
				num2 *= num;
				num2 *= Vector3.Dot(Vector3.up, up);
				Player.movement.Stamina = Mathf.MoveTowards(Player.movement.Stamina, 0f, this.staminaMultiplier * num2 * Time.deltaTime);
			}
			if (num < 0f)
			{
				Player.effects.Scrape();
			}
			float num3 = ((num > 0f) ? 3f : 4f);
			float num4 = 20f;
			if (this.speed < 0f && num >= 0f)
			{
				num4 *= 5f;
			}
			this.speed = Mathf.MoveTowards(this.speed, num * num3, Time.deltaTime * num4);
			this.t = base.MoveAlongPath(this.t, this.speed * Time.deltaTime);
			vector2 = base.GetPosition(this.t) + this.GetOffset(this.t) * direction;
			if (num == 0f)
			{
				animationIndex = 0f;
			}
			else if (num > 0f)
			{
				animationIndex = 1f;
			}
			else
			{
				animationIndex = 2f;
			}
			if (this.t <= this.groundT && num <= 0f)
			{
				Player.movement.ClearMods();
			}
			if (this.t >= this.topHeldT && num > 0f)
			{
				this.lerpToTop = 0f;
				this.isOnTop = true;
			}
		}
		if (this.lerpToPosition < 1f)
		{
			float num5 = 1f - this.t / (float)(this.positions.Length - 1);
			num5 *= num5;
			float num6 = this.lerpSpeed * Mathf.Lerp(1f, 5f, num5);
			this.lerpToPosition = Mathf.MoveTowards(this.lerpToPosition, 1f, num6 * Time.deltaTime);
			if (this.lerpToPosition > 0.25f && Player.movement.modJumpRule != PlayerMovement.ModRule.Cancels)
			{
				Player.movement.modJumpRule = PlayerMovement.ModRule.Cancels;
			}
			position = Vector3.Lerp(position + Time.fixedDeltaTime * velocity, vector2, this.lerpToPosition);
			return;
		}
		position = vector2;
	}

	// Token: 0x060002AD RID: 685 RVA: 0x0000FA88 File Offset: 0x0000DC88
	public void Cancel()
	{
		if (this == null)
		{
			return;
		}
		base.enabled = false;
	}

	// Token: 0x060002AE RID: 686 RVA: 0x0000FA9B File Offset: 0x0000DC9B
	public Vector3 GetLeftFootTarget(Vector3 currentPosition)
	{
		return this.GetTargetPoint(currentPosition, this.isOnTop ? Vector3.left : new Vector3(-1f, 0f, -1f));
	}

	// Token: 0x060002AF RID: 687 RVA: 0x0000FAC7 File Offset: 0x0000DCC7
	public Vector3 GetRightFootTarget(Vector3 currentPosition)
	{
		return this.GetTargetPoint(currentPosition, this.isOnTop ? Vector3.right : new Vector3(1f, 0f, -1f));
	}

	// Token: 0x060002B0 RID: 688 RVA: 0x0000FAF3 File Offset: 0x0000DCF3
	public Vector3 GetLeftHandTarget(Vector3 currentPosition)
	{
		return this.GetTargetPoint(currentPosition, new Vector3(-1f, 0f, -1f));
	}

	// Token: 0x060002B1 RID: 689 RVA: 0x0000FB10 File Offset: 0x0000DD10
	public Vector3 GetRightHandTarget(Vector3 currentPosition)
	{
		return this.GetTargetPoint(currentPosition, new Vector3(1f, 0f, -1f));
	}

	// Token: 0x060002B2 RID: 690 RVA: 0x0000FB30 File Offset: 0x0000DD30
	private Vector3 GetTargetPoint(Vector3 currentPosition, Vector3 offsetDirection)
	{
		float closestInterpolated = base.GetClosestInterpolated(currentPosition);
		Vector3 vector = base.GetPosition(closestInterpolated);
		if (this.lerpToTop > 0f)
		{
			vector = Vector3.Lerp(vector, base.GetPosition(this.positions.Length - 1), this.lerpToTop);
		}
		Vector3 normalized = (Quaternion.FromToRotation(Vector3.forward, this.direction) * offsetDirection).normalized;
		return vector + -this.GetOffset(closestInterpolated) * normalized;
	}

	private static LayerMask playerTriggerMask = 0;

	private static Collider[] sphereTestResults = new Collider[10];

	private const float climbSpeed = 3f;

	private const float slideSpeed = 4f;

	private const float acceleration = 20f;

	private const float staminaDrainSpeed = 0.5f;

	private const float rotationSpeed = -120f;

	private const float offset = 0f;

	private const float handHeight = 0.75f;

	private const float lerpToTopSpeed = 2f;

	private Rigidbody rigidbody;

	public AnimationCurve thickness = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0.1f),
		new Keyframe(1f, 0.1f)
	});

	[HideInInspector]
	public float t;

	private Vector3 velocity;

	private float speed;

	private float lastEnabled = -1f;

	[HideInInspector]
	public Vector3 direction;

	private float rotation;

	public float distanceAllowance = 0.5f;

	public bool raycastForGround = true;

	private float groundT = -1f;

	private float topHeldT = -1f;

	[HideInInspector]
	public float lerpToPosition;

	public float lerpSpeed = 5f;

	private float lerpToTop;

	private bool isOnTop;

	public AnimationSet animationSet;

	public UnityEvent onEnable;

	private bool checkedForConnectedBeams;

	private BalanceBeam[] backBeams;

	private BalanceBeam[] forwardBeams;

	private Vector3[] backBeamDirections;

	private Vector3[] forwardBeamDirections;

	public SurfaceMaterial material;

	public bool playJumpSound = true;

	[ConditionalHide("playJumpSound", true)]
	public AudioSourceVariance jumpSoundEffect;

	public bool playLandSound = true;

	[ConditionalHide("playLandSound", true)]
	public AudioSourceVariance landSoundEffect;

	public bool requiresStamina = true;

	public float staminaMultiplier = 1f;

	public bool isAggressive;
}
