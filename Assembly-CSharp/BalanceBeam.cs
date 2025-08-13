using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000B6 RID: 182
public class BalanceBeam : GenericPath, ICustomPlayerMovement, ICustomFootIKPositions
{
	// Token: 0x060002BB RID: 699 RVA: 0x00004357 File Offset: 0x00002557
	private void OnValidate()
	{
		if (!Application.isPlaying)
		{
			base.enabled = false;
		}
	}

	// Token: 0x060002BC RID: 700 RVA: 0x00020CC0 File Offset: 0x0001EEC0
	[ContextMenu("Fit Capsule To Beam")]
	public void FitCapsule()
	{
		Vector3 vector = this.positions[this.positions.Length - 1];
		Vector3 vector2 = this.positions[0];
		CapsuleCollider component = base.GetComponent<CapsuleCollider>();
		component.center = 0.5f * (vector + vector2);
		component.height = Mathf.Abs(vector.z - vector2.z) + 1f;
	}

	// Token: 0x060002BD RID: 701 RVA: 0x00002229 File Offset: 0x00000429
	private void Awake()
	{
	}

	// Token: 0x060002BE RID: 702 RVA: 0x00020D2C File Offset: 0x0001EF2C
	private void Initialize()
	{
		this.negativeClamp = 0f;
		this.positiveClamp = (float)(this.positions.Length - 1);
		if (this.clampWithCollision)
		{
			RaycastHit raycastHit;
			for (int i = Mathf.CeilToInt((float)this.positions.Length / 2f); i > 0; i--)
			{
				Vector3 vector = base.GetPosition(i - 1) - base.GetPosition(i);
				float magnitude = vector.magnitude;
				if (Physics.SphereCast(base.GetPosition(i), 0.1f, vector, ref raycastHit, magnitude, LayerUtil.BalanceBeamAnchorLayers))
				{
					this.negativeClamp = (float)i - raycastHit.distance / magnitude;
					break;
				}
			}
			for (int j = Mathf.FloorToInt((float)this.positions.Length / 2f); j < this.positions.Length - 1; j++)
			{
				Vector3 vector2 = base.GetPosition(j + 1) - base.GetPosition(j);
				float magnitude2 = vector2.magnitude;
				if (Physics.SphereCast(base.GetPosition(j), 0.1f, vector2, ref raycastHit, magnitude2, LayerUtil.BalanceBeamAnchorLayers))
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

	// Token: 0x060002BF RID: 703 RVA: 0x00020EA4 File Offset: 0x0001F0A4
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

	// Token: 0x060002C0 RID: 704 RVA: 0x00020EEC File Offset: 0x0001F0EC
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

	// Token: 0x060002C1 RID: 705 RVA: 0x00020FD8 File Offset: 0x0001F1D8
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

	// Token: 0x060002C2 RID: 706 RVA: 0x000210F0 File Offset: 0x0001F2F0
	private BalanceBeam[] FindBeamsAtPosition(Vector3 position)
	{
		if (BalanceBeam.playerTriggerMask == 0)
		{
			BalanceBeam.playerTriggerMask = LayerMask.GetMask(new string[] { "Player Trigger" });
		}
		int num = Physics.OverlapSphereNonAlloc(position, 0.2f, BalanceBeam.sphereTestResults, BalanceBeam.playerTriggerMask, 2);
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

	// Token: 0x060002C3 RID: 707 RVA: 0x000211C8 File Offset: 0x0001F3C8
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

	// Token: 0x060002C4 RID: 708 RVA: 0x00021230 File Offset: 0x0001F430
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

	// Token: 0x060002C5 RID: 709 RVA: 0x000212FC File Offset: 0x0001F4FC
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

	// Token: 0x060002C6 RID: 710 RVA: 0x000214A0 File Offset: 0x0001F6A0
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

	// Token: 0x060002C7 RID: 711 RVA: 0x000215C4 File Offset: 0x0001F7C4
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

	// Token: 0x060002C8 RID: 712 RVA: 0x000219D0 File Offset: 0x0001FBD0
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

	// Token: 0x060002C9 RID: 713 RVA: 0x00004367 File Offset: 0x00002567
	public void Cancel()
	{
		if (this != null)
		{
			base.enabled = false;
		}
	}

	// Token: 0x060002CA RID: 714 RVA: 0x00004379 File Offset: 0x00002579
	public void GetStatsForJoinedBeam(out float speed)
	{
		speed = this.speed;
	}

	// Token: 0x060002CB RID: 715 RVA: 0x00002229 File Offset: 0x00000429
	private void CopyFromJoinedBeam(BalanceBeam joinedBeam)
	{
	}

	// Token: 0x060002CC RID: 716 RVA: 0x00004383 File Offset: 0x00002583
	public Vector3 GetLeftFootTarget(Vector3 currentPosition)
	{
		return this.GetFootTarget(currentPosition);
	}

	// Token: 0x060002CD RID: 717 RVA: 0x00004383 File Offset: 0x00002583
	public Vector3 GetRightFootTarget(Vector3 currentPosition)
	{
		return this.GetFootTarget(currentPosition);
	}

	// Token: 0x060002CE RID: 718 RVA: 0x0000438C File Offset: 0x0000258C
	private float GetInterpolatedT()
	{
		return Mathf.Lerp(this.prevT, this.t, (Time.time - Time.fixedTime) / Time.fixedDeltaTime);
	}

	// Token: 0x060002CF RID: 719 RVA: 0x00021A14 File Offset: 0x0001FC14
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

	// Token: 0x040003B8 RID: 952
	private static LayerMask playerTriggerMask = 0;

	// Token: 0x040003B9 RID: 953
	private static Collider[] sphereTestResults = new Collider[10];

	// Token: 0x040003BA RID: 954
	private const float movementSpeed = 3f;

	// Token: 0x040003BB RID: 955
	private const float backwardsSpeed = 2.5f;

	// Token: 0x040003BC RID: 956
	private const float acceleration = 20f;

	// Token: 0x040003BD RID: 957
	public bool exitOnPositiveEnd = true;

	// Token: 0x040003BE RID: 958
	public bool exitOnNegativeEnd = true;

	// Token: 0x040003BF RID: 959
	public bool twoSidedAnchor = true;

	// Token: 0x040003C0 RID: 960
	private float positiveClamp;

	// Token: 0x040003C1 RID: 961
	private float negativeClamp;

	// Token: 0x040003C2 RID: 962
	public bool clampWithCollision;

	// Token: 0x040003C3 RID: 963
	public float positiveClampDistance = 0.25f;

	// Token: 0x040003C4 RID: 964
	public float negativeClampDistance = 0.25f;

	// Token: 0x040003C5 RID: 965
	private bool isInitialized;

	// Token: 0x040003C6 RID: 966
	private Rigidbody rigidbody;

	// Token: 0x040003C7 RID: 967
	private int forward = 1;

	// Token: 0x040003C8 RID: 968
	private float t;

	// Token: 0x040003C9 RID: 969
	private float prevT;

	// Token: 0x040003CA RID: 970
	private Vector3 velocity;

	// Token: 0x040003CB RID: 971
	private float speed;

	// Token: 0x040003CC RID: 972
	private float lastEnabled = -10f;

	// Token: 0x040003CD RID: 973
	public float distanceAllowance = 0.5f;

	// Token: 0x040003CE RID: 974
	[HideInInspector]
	public float lerpToPosition;

	// Token: 0x040003CF RID: 975
	public float lerpSpeed = 5f;

	// Token: 0x040003D0 RID: 976
	public bool isStiff;

	// Token: 0x040003D1 RID: 977
	private float initialHeight;

	// Token: 0x040003D2 RID: 978
	public AnimationSet animationSet;

	// Token: 0x040003D3 RID: 979
	public UnityEvent onEnable;

	// Token: 0x040003D4 RID: 980
	private const float walkOffTime = 0.25f;

	// Token: 0x040003D5 RID: 981
	private float walkOffCounter;

	// Token: 0x040003D6 RID: 982
	private bool checkedForConnectedBeams;

	// Token: 0x040003D7 RID: 983
	private BalanceBeam[] backBeams;

	// Token: 0x040003D8 RID: 984
	private BalanceBeam[] forwardBeams;

	// Token: 0x040003D9 RID: 985
	private Vector3[] backBeamDirections;

	// Token: 0x040003DA RID: 986
	private Vector3[] forwardBeamDirections;

	// Token: 0x040003DB RID: 987
	public SurfaceMaterial beamMaterial;

	// Token: 0x040003DC RID: 988
	public bool playJumpSound = true;

	// Token: 0x040003DD RID: 989
	[ConditionalHide("playJumpSound", true)]
	public AudioSourceVariance jumpSoundEffect;

	// Token: 0x040003DE RID: 990
	public bool playLandSound = true;

	// Token: 0x040003DF RID: 991
	[ConditionalHide("playLandSound", true)]
	public AudioSourceVariance landSoundEffect;
}
