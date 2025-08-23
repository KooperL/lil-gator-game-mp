using System;
using UnityEngine;
using UnityEngine.Events;

public class BalancePoint : MonoBehaviour, ICustomPlayerMovement, ICustomFootIKPositions
{
	// Token: 0x060002DF RID: 735 RVA: 0x00004443 File Offset: 0x00002643
	private void OnValidate()
	{
		if (!Application.isPlaying)
		{
			base.enabled = false;
		}
	}

	// Token: 0x060002E0 RID: 736 RVA: 0x0002255C File Offset: 0x0002075C
	private bool IsEligible()
	{
		Vector3 rawPosition = Player.RawPosition;
		return Vector3.SqrMagnitude(base.transform.position - rawPosition) <= this.distanceAllowance * this.distanceAllowance && Player.rigidbody.velocity.y <= 0f;
	}

	// Token: 0x060002E1 RID: 737 RVA: 0x000225B0 File Offset: 0x000207B0
	private void OnTriggerStay(Collider other)
	{
		if (base.enabled || Time.time - this.lastEnabled < 0.5f || Player.movement.JustCanceled)
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
			if (!(Player.movement.modCustomMovementScript is ClimbingPole))
			{
				return;
			}
			if (Player.rigidbody.velocity.y < -0.1f)
			{
				return;
			}
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
			if (this.beamMaterial != null)
			{
				this.beamMaterial.PlayImpact(Player.Position, 1f, 1f);
			}
		}
	}

	// Token: 0x060002E2 RID: 738 RVA: 0x000226A4 File Offset: 0x000208A4
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
		Player.overrideAnimations.SetContextualAnimations(this.animationSet);
		Player.footIK.overrideIK = true;
		Player.footIK.customIKPositions = this;
		Player.footsteps.overrideSettings = true;
		Player.footsteps.overrideFootstepMaterial = this.beamMaterial;
		Player.footsteps.overrideHasVisualFootsteps = false;
		Player.footsteps.overrideHasFootstepDust = false;
		this.lerpToPosition = 0f;
		this.walkOffCounter = 0f;
		this.onEnable.Invoke();
	}

	// Token: 0x060002E3 RID: 739 RVA: 0x000227A4 File Offset: 0x000209A4
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
		}
		this.lerpToPosition = 0f;
		this.lastEnabled = Time.time;
	}

	// Token: 0x060002E4 RID: 740 RVA: 0x0002284C File Offset: 0x00020A4C
	public void MovementUpdate(Vector3 input, ref Vector3 position, ref Vector3 velocity, ref Vector3 direction, ref Vector3 up, ref float animationIndex)
	{
		Vector3 position2 = base.transform.position;
		up = Vector3.up;
		animationIndex = 0f;
		if (input.sqrMagnitude > 0.2f)
		{
			direction = Vector3.Slerp(direction, input, this.rotationSpeed * Time.deltaTime);
			this.walkOffCounter += input.magnitude * Time.deltaTime;
		}
		else
		{
			this.walkOffCounter = 0f;
		}
		if (this.lerpToPosition < 1f)
		{
			this.lerpToPosition = Mathf.MoveTowards(this.lerpToPosition, 1f, this.lerpSpeed * Time.deltaTime);
			position = Vector3.Lerp(position + Time.fixedDeltaTime * velocity, position2, this.lerpToPosition);
		}
		else
		{
			position = position2;
		}
		if (this.walkOffCounter > 0.25f)
		{
			Player.movement.ClearMods();
		}
	}

	// Token: 0x060002E5 RID: 741 RVA: 0x000044B5 File Offset: 0x000026B5
	public void Cancel()
	{
		base.enabled = false;
	}

	// Token: 0x060002E6 RID: 742 RVA: 0x000044BE File Offset: 0x000026BE
	public Vector3 GetLeftFootTarget(Vector3 currentPosition)
	{
		return Vector3.Lerp(currentPosition, base.transform.position, this.lerpToPosition);
	}

	// Token: 0x060002E7 RID: 743 RVA: 0x000044BE File Offset: 0x000026BE
	public Vector3 GetRightFootTarget(Vector3 currentPosition)
	{
		return Vector3.Lerp(currentPosition, base.transform.position, this.lerpToPosition);
	}

	private float lastEnabled = -1f;

	public float distanceAllowance = 0.5f;

	[HideInInspector]
	public float lerpToPosition;

	public float lerpSpeed = 5f;

	public AnimationSet animationSet;

	public UnityEvent onEnable;

	private const float walkOffTime = 0.25f;

	private float walkOffCounter;

	public float rotationSpeed = 30f;

	public SurfaceMaterial beamMaterial;

	public bool playJumpSound = true;

	[ConditionalHide("playJumpSound", true)]
	public AudioSourceVariance jumpSoundEffect;

	public bool playLandSound = true;

	[ConditionalHide("playLandSound", true)]
	public AudioSourceVariance landSoundEffect;
}
