using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000B7 RID: 183
public class BalancePoint : MonoBehaviour, ICustomPlayerMovement, ICustomFootIKPositions
{
	// Token: 0x060002D2 RID: 722 RVA: 0x00004357 File Offset: 0x00002557
	private void OnValidate()
	{
		if (!Application.isPlaying)
		{
			base.enabled = false;
		}
	}

	// Token: 0x060002D3 RID: 723 RVA: 0x00021AF0 File Offset: 0x0001FCF0
	private bool IsEligible()
	{
		Vector3 rawPosition = Player.RawPosition;
		return Vector3.SqrMagnitude(base.transform.position - rawPosition) <= this.distanceAllowance * this.distanceAllowance && Player.rigidbody.velocity.y <= 0f;
	}

	// Token: 0x060002D4 RID: 724 RVA: 0x00021B44 File Offset: 0x0001FD44
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

	// Token: 0x060002D5 RID: 725 RVA: 0x00021C38 File Offset: 0x0001FE38
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

	// Token: 0x060002D6 RID: 726 RVA: 0x00021D38 File Offset: 0x0001FF38
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

	// Token: 0x060002D7 RID: 727 RVA: 0x00021DE0 File Offset: 0x0001FFE0
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

	// Token: 0x060002D8 RID: 728 RVA: 0x000043C9 File Offset: 0x000025C9
	public void Cancel()
	{
		base.enabled = false;
	}

	// Token: 0x060002D9 RID: 729 RVA: 0x000043D2 File Offset: 0x000025D2
	public Vector3 GetLeftFootTarget(Vector3 currentPosition)
	{
		return Vector3.Lerp(currentPosition, base.transform.position, this.lerpToPosition);
	}

	// Token: 0x060002DA RID: 730 RVA: 0x000043D2 File Offset: 0x000025D2
	public Vector3 GetRightFootTarget(Vector3 currentPosition)
	{
		return Vector3.Lerp(currentPosition, base.transform.position, this.lerpToPosition);
	}

	// Token: 0x040003E0 RID: 992
	private float lastEnabled = -1f;

	// Token: 0x040003E1 RID: 993
	public float distanceAllowance = 0.5f;

	// Token: 0x040003E2 RID: 994
	[HideInInspector]
	public float lerpToPosition;

	// Token: 0x040003E3 RID: 995
	public float lerpSpeed = 5f;

	// Token: 0x040003E4 RID: 996
	public AnimationSet animationSet;

	// Token: 0x040003E5 RID: 997
	public UnityEvent onEnable;

	// Token: 0x040003E6 RID: 998
	private const float walkOffTime = 0.25f;

	// Token: 0x040003E7 RID: 999
	private float walkOffCounter;

	// Token: 0x040003E8 RID: 1000
	public float rotationSpeed = 30f;

	// Token: 0x040003E9 RID: 1001
	public SurfaceMaterial beamMaterial;

	// Token: 0x040003EA RID: 1002
	public bool playJumpSound = true;

	// Token: 0x040003EB RID: 1003
	[ConditionalHide("playJumpSound", true)]
	public AudioSourceVariance jumpSoundEffect;

	// Token: 0x040003EC RID: 1004
	public bool playLandSound = true;

	// Token: 0x040003ED RID: 1005
	[ConditionalHide("playLandSound", true)]
	public AudioSourceVariance landSoundEffect;
}
