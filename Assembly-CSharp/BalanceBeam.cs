using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BalanceBeam : GenericPath, ICustomPlayerMovement, ICustomFootIKPositions
{
	// Token: 0x06000271 RID: 625 RVA: 0x0000D005 File Offset: 0x0000B205
	private void OnValidate()
	{
		if (!Application.isPlaying)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06000272 RID: 626 RVA: 0x0000D018 File Offset: 0x0000B218
	[ContextMenu("Fit Capsule To Beam")]
	public void FitCapsule()
	{
		Vector3 vector = this.positions[this.positions.Length - 1];
		Vector3 vector2 = this.positions[0];
		CapsuleCollider component = base.GetComponent<CapsuleCollider>();
		component.center = 0.5f * (vector + vector2);
		component.height = Mathf.Abs(vector.z - vector2.z) + 1f;
	}

	// Token: 0x06000273 RID: 627 RVA: 0x0000D082 File Offset: 0x0000B282
	private void Awake()
	{
	}

	// Token: 0x06000274 RID: 628 RVA: 0x0000D084 File Offset: 0x0000B284
	private void Initialize()
	{
		this.negativeClamp = 0f;
		this.positiveClamp = (float)(this.positions.Length - 1);
		if (this.clampWithCollision)
		{
			for (int i = Mathf.CeilToInt((float)this.positions.Length / 2f); i > 0; i--)
			{
				Vector3 vector = base.GetPosition(i - 1) - base.GetPosition(i);
				float magnitude = vector.magnitude;
				RaycastHit raycastHit;
				if (Physics.SphereCast(base.GetPosition(i), 0.1f, vector, out raycastHit, magnitude, LayerUtil.BalanceBeamAnchorLayers))
				{
					this.negativeClamp = (float)i - raycastHit.distance / magnitude;
					break;
				}
			}
			for (int j = Mathf.FloorToInt((float)this.positions.Length / 2f); j < this.positions.Length - 1; j++)
			{
				Vector3 vector2 = base.GetPosition(j + 1) - base.GetPosition(j);
				float magnitude2 = vector2.magnitude;
				RaycastHit raycastHit;
				if (Physics.SphereCast(base.GetPosition(j), 0.1f, vector2, out raycastHit, magnitude2, LayerUtil.BalanceBeamAnchorLayers))
				{
					this.positiveClamp = (float)j + raycastHit.distance / magnitude2;
					break;
				}
			}
		}
		if (!this.exitOnNegativeEnd)
		{
			this.negativeClamp = base.MoveAlongPath(this.negativeClamp, this.negativeClampDistance);
		}
		if (!this.exitOnPositiveEnd)
		{
			this.positiveClamp = base.MoveAlongPath(this.positiveClamp, -this.positiveClampDistance);
		}
		this.isInitialized = true;
	}

	// Token: 0x06000275 RID: 629 RVA: 0x0000D1FC File Offset: 0x0000B3FC
	private bool IsBeamEligible()
	{
		if (!this.isInitialized)
		{
			this.Initialize();
		}
		this.t = base.GetClosestInterpolated(Player.RawPosition);
		Vector3 direction = base.GetDirection(this.t);
		return this.IsBeamEligible(this.t, direction);
	}

	// Token: 0x06000276 RID: 630 RVA: 0x0000D244 File Offset: 0x0000B444
	private bool IsBeamEligible(float t, Vector3 direction)
	{
		if (!this.isInitialized)
		{
			this.Initialize();
		}
		Vector3 rawPosition = Player.RawPosition;
		if (t == 0f || t == (float)(this.positions.Length - 1))
		{
			return false;
		}
		if (Vector3.SqrMagnitude(base.GetPosition(t) - rawPosition) > this.distanceAllowance * this.distanceAllowance)
		{
			return false;
		}
		Vector3 vector = Player.rigidbody.velocity;
		vector.y = 0f;
		vector.Normalize();
		float num;
		if (PlayerInput.p.inputDirection.sqrMagnitude > 0.5f)
		{
			num = Vector3.Dot(PlayerInput.p.inputDirection, direction);
		}
		else
		{
			num = Vector3.Dot(vector, direction);
		}
		return (num >= 0f || base.MoveAlongPath(t, -0.5f) > 0f) && (num <= 0f || base.MoveAlongPath(t, 0.5f) < (float)(this.positions.Length - 1));
	}

	// Token: 0x06000277 RID: 631 RVA: 0x0000D330 File Offset: 0x0000B530
	private void OnTriggerStay(Collider other)
	{
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
		if (Player.rigidbody.velocity.y > 0f)
		{
			return;
		}
		if (Player.movement.modCustomMovement)
		{
			if (Player.movement.modCustomMovementScript is ClimbingPole)
			{
				if (Player.rigidbody.velocity.y < -0.1f)
				{
					return;
				}
			}
			else if (!(Player.movement.modCustomMovementScript is BalancePoint))
			{
				return;
			}
		}
		if (!this.IsBeamEligible())
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
			if (this.beamMaterial != null)
			{
				this.beamMaterial.PlayImpact(Player.Position, 1f, 1f);
			}
		}
	}

	// Token: 0x06000278 RID: 632 RVA: 0x0000D448 File Offset: 0x0000B648
	private BalanceBeam[] FindBeamsAtPosition(Vector3 position)
	{
		if (BalanceBeam.playerTriggerMask == 0)
		{
			BalanceBeam.playerTriggerMask = LayerMask.GetMask(new string[] { "Player Trigger" });
		}
		int num = Physics.OverlapSphereNonAlloc(position, 0.2f, BalanceBeam.sphereTestResults, BalanceBeam.playerTriggerMask, QueryTriggerInteraction.Collide);
		List<BalanceBeam> list = new List<BalanceBeam>();
		for (int i = 0; i < num; i++)
		{
			if (!(BalanceBeam.sphereTestResults[i].gameObject == base.gameObject))
			{
				BalanceBeam component = BalanceBeam.sphereTestResults[i].GetComponent<BalanceBeam>();
				if (!(component == null) && !(component == this) && (Vector3.Distance(position, component.GetPosition(0)) <= 0.25f || Vector3.Distance(position, component.GetPosition(component.positions.Length)) <= 0.25f))
				{
					list.Add(component);
				}
			}
		}
		return list.ToArray();
	}

	// Token: 0x06000279 RID: 633 RVA: 0x0000D520 File Offset: 0x0000B720
	private Vector3 GetBeamDirection(BalanceBeam beam, Vector3 referencePosition)
	{
		Vector3 position = beam.GetPosition(0);
		Vector3 position2 = beam.GetPosition(beam.positions.Length - 1);
		if ((position - referencePosition).sqrMagnitude > (position2 - referencePosition).sqrMagnitude)
		{
			return (position - position2).normalized;
		}
		return (position2 - position).normalized;
	}

	// Token: 0x0600027A RID: 634 RVA: 0x0000D588 File Offset: 0x0000B788
	private void FindConnectedBeams()
	{
		Vector3 position = base.GetPosition(0);
		Vector3 position2 = base.GetPosition(this.positions.Length - 1);
		this.backBeams = this.FindBeamsAtPosition(position);
		this.forwardBeams = this.FindBeamsAtPosition(position2);
		this.backBeamDirections = new Vector3[this.backBeams.Length];
		for (int i = 0; i < this.backBeams.Length; i++)
		{
			this.backBeamDirections[i] = this.GetBeamDirection(this.backBeams[i], position);
		}
		this.forwardBeamDirections = new Vector3[this.forwardBeams.Length];
		for (int j = 0; j < this.forwardBeams.Length; j++)
		{
			this.forwardBeamDirections[j] = this.GetBeamDirection(this.forwardBeams[j], position2);
		}
		this.checkedForConnectedBeams = true;
	}

	// Token: 0x0600027B RID: 635 RVA: 0x0000D654 File Offset: 0x0000B854
	private void OnEnable()
	{
		Player.movement.isModified = true;
		Player.movement.modJumpRule = PlayerMovement.ModRule.Cancels;
		Player.movement.modGlideRule = PlayerMovement.ModRule.Locked;
		Player.movement.modPrimaryRule = PlayerMovement.ModRule.Allowed;
		Player.movement.modSecondaryRule = PlayerMovement.ModRule.Locked;
		Player.movement.modItemRule = PlayerMovement.ModRule.Allowed;
		Player.movement.modCustomMovement = true;
		Player.movement.modCustomMovementScript = this;
		Player.movement.modNoClimbing = true;
		Player.movement.modDisableCollision = true;
		Player.movement.modIgnoreGroundedness = true;
		Player.movement.modIgnoreReady = true;
		Player.overrideAnimations.SetContextualAnimations(this.animationSet);
		Player.footIK.overrideIK = true;
		Player.footIK.customIKPositions = this;
		Player.footIK.overrideLock = true;
		Player.footsteps.overrideSettings = true;
		Player.footsteps.overrideFootstepMaterial = this.beamMaterial;
		Player.footsteps.overrideHasVisualFootsteps = false;
		Player.footsteps.overrideHasFootstepDust = false;
		if (!this.checkedForConnectedBeams)
		{
			this.FindConnectedBeams();
		}
		Vector3 rawPosition = Player.RawPosition;
		Vector3 vector = Player.movement.velocity;
		float num = base.DistanceToPath(rawPosition);
		this.t = base.GetClosestInterpolated(rawPosition + num * vector.normalized);
		Vector3 direction = base.GetDirection(this.t);
		this.speed = Vector3.Dot(Player.rigidbody.velocity, direction);
		this.forward = (int)Mathf.Sign(Vector3.Dot(Player.Forward, direction));
		this.lerpToPosition = 0f;
		this.walkOffCounter = 0f;
		this.initialHeight = Player.RawPosition.y;
		this.onEnable.Invoke();
	}

	// Token: 0x0600027C RID: 636 RVA: 0x0000D7F8 File Offset: 0x0000B9F8
	private void OnDisable()
	{
		if (Player.footIK.customIKPositions == this)
		{
			Player.footIK.ClearOverrides();
		}
		Player.footsteps.ClearOverride();
		if (this.playJumpSound && (Player.movement.JustJumped || Player.movement.desiredJump))
		{
			if (this.jumpSoundEffect != null)
			{
				this.jumpSoundEffect.Play();
			}
			else if (this.beamMaterial != null)
			{
				this.beamMaterial.PlayImpact(Player.Position, 1f, 1f);
			}
			Player.movement.velocity.y = Mathf.Max(Player.movement.velocity.y + 2f, 4f);
			if (Player.input.inputDirection == Vector3.zero)
			{
				Player.movement.velocity.x = 0f;
				Player.movement.velocity.z = 0f;
			}
		}
		Player.movement.ClampSpeedForABit();
		this.lerpToPosition = 0f;
		this.lastEnabled = Time.time;
	}

	// Token: 0x0600027D RID: 637 RVA: 0x0000D91C File Offset: 0x0000BB1C
	public void MovementUpdate(Vector3 input, ref Vector3 position, ref Vector3 velocity, ref Vector3 direction, ref Vector3 up, ref float animationIndex)
	{
		this.t = Mathf.Clamp(this.t, 0f, (float)(this.positions.Length - 1));
		this.prevT = this.t;
		base.GetPosition(this.t);
		direction = base.GetDirection(this.t);
		this.forward = (int)Mathf.Sign(Vector3.Dot(Player.Forward, direction));
		float num = 0f;
		if (input.sqrMagnitude > 0.2f)
		{
			num = Vector3.Dot(input, direction);
			if (Mathf.Abs(num) < 0.4f)
			{
				num = 0f;
				this.walkOffCounter += Time.deltaTime;
			}
			else
			{
				num = Mathf.Sign(num);
				this.walkOffCounter = 0f;
			}
		}
		else
		{
			this.walkOffCounter = 0f;
		}
		float num2 = ((num * (float)this.forward > 0f) ? 3f : 2.5f);
		this.speed = Mathf.MoveTowards(this.speed, num * num2, Time.deltaTime * 20f);
		this.t = base.MoveAlongPath(this.t, this.speed * Time.deltaTime);
		if (!this.exitOnPositiveEnd && this.t >= this.positiveClamp)
		{
			this.t = this.positiveClamp;
			num = 0f;
		}
		if (!this.exitOnNegativeEnd && this.t <= this.negativeClamp)
		{
			this.t = this.negativeClamp;
			num = 0f;
		}
		Vector3 position2 = base.GetPosition(this.t);
		if (this.lerpToPosition < 1f)
		{
			float num3;
			if (this.isStiff)
			{
				num3 = 1f;
			}
			else
			{
				if (this.twoSidedAnchor)
				{
					num3 = 2f * Mathf.Abs(this.t / (float)(this.positions.Length - 1) - 0.5f);
				}
				else
				{
					num3 = 1f - this.t / (float)(this.positions.Length - 1);
				}
				num3 *= num3;
			}
			float num4 = this.lerpSpeed * Mathf.Lerp(1f, 5f, num3);
			Vector3 vector2;
			if (this.lerpToPosition > 0f || position.y < position2.y)
			{
				this.lerpToPosition = Mathf.MoveTowards(this.lerpToPosition, 1f, num4 * Time.deltaTime);
				Vector3 vector = velocity;
				vector.x *= 0.5f;
				vector.z *= 0.5f;
				vector2 = Vector3.Lerp(position + Time.fixedDeltaTime * vector, position2, this.lerpToPosition);
			}
			else
			{
				velocity += Time.fixedDeltaTime * Physics.gravity;
				Vector3 vector3 = position + Time.fixedDeltaTime * velocity;
				float y = vector3.y;
				vector2 = Vector3.Lerp(vector3, position2, Mathf.InverseLerp(position2.y, this.initialHeight, y));
				vector2.y = y;
			}
			position = vector2;
		}
		else
		{
			position = position2;
		}
		direction *= (float)this.forward;
		num *= (float)this.forward;
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
		if (this.walkOffCounter <= 0.25f && (this.t >= (float)(this.positions.Length - 1) || this.t <= 0f))
		{
			BalanceBeam[] array = ((this.t == 0f) ? this.backBeams : this.forwardBeams);
			if (array != null && array.Length != 0)
			{
				BalanceBeam nextBeam = this.GetNextBeam(array, (this.t == 0f) ? this.backBeamDirections : this.forwardBeamDirections, input);
				if (nextBeam != null)
				{
					base.enabled = false;
					nextBeam.enabled = true;
					return;
				}
			}
			else
			{
				Player.movement.ClearMods();
			}
		}
	}

	// Token: 0x0600027E RID: 638 RVA: 0x0000DD28 File Offset: 0x0000BF28
	private BalanceBeam GetNextBeam(BalanceBeam[] beams, Vector3[] directions, Vector3 inputDirection)
	{
		BalanceBeam balanceBeam = null;
		float num = -1f;
		for (int i = 0; i < beams.Length; i++)
		{
			float num2 = Vector3.Dot(directions[i], inputDirection);
			if (num2 > 0.1f && num2 > num)
			{
				num = num2;
				balanceBeam = beams[i];
			}
		}
		return balanceBeam;
	}

	// Token: 0x0600027F RID: 639 RVA: 0x0000DD6C File Offset: 0x0000BF6C
	public void Cancel()
	{
		if (this != null)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06000280 RID: 640 RVA: 0x0000DD7E File Offset: 0x0000BF7E
	public void GetStatsForJoinedBeam(out float speed)
	{
		speed = this.speed;
	}

	// Token: 0x06000281 RID: 641 RVA: 0x0000DD88 File Offset: 0x0000BF88
	private void CopyFromJoinedBeam(BalanceBeam joinedBeam)
	{
	}

	// Token: 0x06000282 RID: 642 RVA: 0x0000DD8A File Offset: 0x0000BF8A
	public Vector3 GetLeftFootTarget(Vector3 currentPosition)
	{
		return this.GetFootTarget(currentPosition);
	}

	// Token: 0x06000283 RID: 643 RVA: 0x0000DD93 File Offset: 0x0000BF93
	public Vector3 GetRightFootTarget(Vector3 currentPosition)
	{
		return this.GetFootTarget(currentPosition);
	}

	// Token: 0x06000284 RID: 644 RVA: 0x0000DD9C File Offset: 0x0000BF9C
	private float GetInterpolatedT()
	{
		return Mathf.Lerp(this.prevT, this.t, (Time.time - Time.fixedTime) / Time.fixedDeltaTime);
	}

	// Token: 0x06000285 RID: 645 RVA: 0x0000DDC0 File Offset: 0x0000BFC0
	private Vector3 GetFootTarget(Vector3 footPosition)
	{
		float interpolatedT = this.GetInterpolatedT();
		Vector3 rawPosition = Player.RawPosition;
		footPosition - rawPosition;
		float closestInterpolated = base.GetClosestInterpolated(footPosition);
		Vector3 position = base.GetPosition((closestInterpolated > interpolatedT) ? (this.positions.Length - 1) : 0);
		float num = Mathf.InverseLerp(interpolatedT, (float)((closestInterpolated > interpolatedT) ? (this.positions.Length - 1) : 0), closestInterpolated);
		return Vector3.Lerp(rawPosition, position, num);
	}

	private static LayerMask playerTriggerMask = 0;

	private static Collider[] sphereTestResults = new Collider[10];

	private const float movementSpeed = 3f;

	private const float backwardsSpeed = 2.5f;

	private const float acceleration = 20f;

	public bool exitOnPositiveEnd = true;

	public bool exitOnNegativeEnd = true;

	public bool twoSidedAnchor = true;

	private float positiveClamp;

	private float negativeClamp;

	public bool clampWithCollision;

	public float positiveClampDistance = 0.25f;

	public float negativeClampDistance = 0.25f;

	private bool isInitialized;

	private Rigidbody rigidbody;

	private int forward = 1;

	private float t;

	private float prevT;

	private Vector3 velocity;

	private float speed;

	private float lastEnabled = -10f;

	public float distanceAllowance = 0.5f;

	[HideInInspector]
	public float lerpToPosition;

	public float lerpSpeed = 5f;

	public bool isStiff;

	private float initialHeight;

	public AnimationSet animationSet;

	public UnityEvent onEnable;

	private const float walkOffTime = 0.25f;

	private float walkOffCounter;

	private bool checkedForConnectedBeams;

	private BalanceBeam[] backBeams;

	private BalanceBeam[] forwardBeams;

	private Vector3[] backBeamDirections;

	private Vector3[] forwardBeamDirections;

	public SurfaceMaterial beamMaterial;

	public bool playJumpSound = true;

	[ConditionalHide("playJumpSound", true)]
	public AudioSourceVariance jumpSoundEffect;

	public bool playLandSound = true;

	[ConditionalHide("playLandSound", true)]
	public AudioSourceVariance landSoundEffect;
}
