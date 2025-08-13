using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000091 RID: 145
public class BalancePoint : MonoBehaviour, ICustomPlayerMovement, ICustomFootIKPositions
{
	// Token: 0x06000288 RID: 648 RVA: 0x0000DEB5 File Offset: 0x0000C0B5
	private void OnValidate()
	{
		if (!Application.isPlaying)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06000289 RID: 649 RVA: 0x0000DEC8 File Offset: 0x0000C0C8
	private bool IsEligible()
	{
		Vector3 rawPosition = Player.RawPosition;
		return Vector3.SqrMagnitude(base.transform.position - rawPosition) <= this.distanceAllowance * this.distanceAllowance && Player.rigidbody.velocity.y <= 0f;
	}

	// Token: 0x0600028A RID: 650 RVA: 0x0000DF1C File Offset: 0x0000C11C
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

	// Token: 0x0600028B RID: 651 RVA: 0x0000E010 File Offset: 0x0000C210
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

	// Token: 0x0600028C RID: 652 RVA: 0x0000E110 File Offset: 0x0000C310
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

	// Token: 0x0600028D RID: 653 RVA: 0x0000E1B8 File Offset: 0x0000C3B8
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

	// Token: 0x0600028E RID: 654 RVA: 0x0000E2B6 File Offset: 0x0000C4B6
	public void Cancel()
	{
		base.enabled = false;
	}

	// Token: 0x0600028F RID: 655 RVA: 0x0000E2BF File Offset: 0x0000C4BF
	public Vector3 GetLeftFootTarget(Vector3 currentPosition)
	{
		return Vector3.Lerp(currentPosition, base.transform.position, this.lerpToPosition);
	}

	// Token: 0x06000290 RID: 656 RVA: 0x0000E2D8 File Offset: 0x0000C4D8
	public Vector3 GetRightFootTarget(Vector3 currentPosition)
	{
		return Vector3.Lerp(currentPosition, base.transform.position, this.lerpToPosition);
	}

	// Token: 0x04000347 RID: 839
	private float lastEnabled = -1f;

	// Token: 0x04000348 RID: 840
	public float distanceAllowance = 0.5f;

	// Token: 0x04000349 RID: 841
	[HideInInspector]
	public float lerpToPosition;

	// Token: 0x0400034A RID: 842
	public float lerpSpeed = 5f;

	// Token: 0x0400034B RID: 843
	public AnimationSet animationSet;

	// Token: 0x0400034C RID: 844
	public UnityEvent onEnable;

	// Token: 0x0400034D RID: 845
	private const float walkOffTime = 0.25f;

	// Token: 0x0400034E RID: 846
	private float walkOffCounter;

	// Token: 0x0400034F RID: 847
	public float rotationSpeed = 30f;

	// Token: 0x04000350 RID: 848
	public SurfaceMaterial beamMaterial;

	// Token: 0x04000351 RID: 849
	public bool playJumpSound = true;

	// Token: 0x04000352 RID: 850
	[ConditionalHide("playJumpSound", true)]
	public AudioSourceVariance jumpSoundEffect;

	// Token: 0x04000353 RID: 851
	public bool playLandSound = true;

	// Token: 0x04000354 RID: 852
	[ConditionalHide("playLandSound", true)]
	public AudioSourceVariance landSoundEffect;
}
