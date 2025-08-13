using System;
using UnityEngine;

// Token: 0x0200027B RID: 635
public class PlayerMovement : MonoBehaviour
{
	// Token: 0x17000124 RID: 292
	// (get) Token: 0x06000C37 RID: 3127 RVA: 0x0000B70E File Offset: 0x0000990E
	private Vector3 position
	{
		get
		{
			return base.transform.position + Vector3.up * 0.5f;
		}
	}

	// Token: 0x17000125 RID: 293
	// (get) Token: 0x06000C38 RID: 3128 RVA: 0x0000B72F File Offset: 0x0000992F
	public float Speed
	{
		get
		{
			if (!this.overrideSpeed)
			{
				return this.speed;
			}
			return this.overriddenSpeed;
		}
	}

	// Token: 0x17000126 RID: 294
	// (get) Token: 0x06000C39 RID: 3129 RVA: 0x0000B746 File Offset: 0x00009946
	// (set) Token: 0x06000C3A RID: 3130 RVA: 0x00042EFC File Offset: 0x000410FC
	public float Stamina
	{
		get
		{
			if (ItemManager.HasInfiniteStamina)
			{
				return 100f;
			}
			return this.stamina;
		}
		set
		{
			if (value > this.stamina || value < 1f)
			{
				this.stamina = value;
				return;
			}
			float num = this.stamina - value;
			if (value < 2f)
			{
				num *= 1.25f;
			}
			else if (value < 3f)
			{
				num *= 1.5f;
			}
			this.stamina = Mathf.Max(this.stamina - num, Mathf.Floor(value));
		}
	}

	// Token: 0x17000127 RID: 295
	// (get) Token: 0x06000C3B RID: 3131 RVA: 0x00042F68 File Offset: 0x00041168
	private float DepthSubmerged
	{
		get
		{
			if (!this.isSledding)
			{
				return this.depthSubmerged;
			}
			if (this.sledAlwaysFloats)
			{
				return this.sledDepthSubmerged;
			}
			return Mathf.Lerp(this.depthSubmerged, this.sledDepthSubmerged, Mathf.InverseLerp(this.sledBouyancyMinSpeed, this.sledBouyancyMaxSpeed, this.velocity.Flat().magnitude));
		}
	}

	// Token: 0x17000128 RID: 296
	// (get) Token: 0x06000C3C RID: 3132 RVA: 0x0000B75B File Offset: 0x0000995B
	private bool CanUseAnything
	{
		get
		{
			return this.transitionCooldown <= 0f || !base.enabled;
		}
	}

	// Token: 0x17000129 RID: 297
	// (get) Token: 0x06000C3D RID: 3133 RVA: 0x0000B775 File Offset: 0x00009975
	public bool CanUsePrimary
	{
		get
		{
			return this.modPrimaryRule != PlayerMovement.ModRule.Locked && this.CanUseAnything;
		}
	}

	// Token: 0x1700012A RID: 298
	// (get) Token: 0x06000C3E RID: 3134 RVA: 0x0000B788 File Offset: 0x00009988
	public bool CanUseSecondary
	{
		get
		{
			return this.modSecondaryRule != PlayerMovement.ModRule.Locked && this.CanUseAnything;
		}
	}

	// Token: 0x1700012B RID: 299
	// (get) Token: 0x06000C3F RID: 3135 RVA: 0x0000B79B File Offset: 0x0000999B
	public bool CanUseItem
	{
		get
		{
			return this.modItemRule != PlayerMovement.ModRule.Locked && this.CanUseAnything;
		}
	}

	// Token: 0x1700012C RID: 300
	// (get) Token: 0x06000C40 RID: 3136 RVA: 0x0000B7AE File Offset: 0x000099AE
	public bool HasGroundContact
	{
		get
		{
			return this.groundContactCount > 0 || this.avgContactNormal != Vector3.zero;
		}
	}

	// Token: 0x1700012D RID: 301
	// (get) Token: 0x06000C41 RID: 3137 RVA: 0x0000B7CB File Offset: 0x000099CB
	private bool HasSteepContact
	{
		get
		{
			return this.steepContactCount > 0 || this.avgSteepNormal != Vector3.zero;
		}
	}

	// Token: 0x1700012E RID: 302
	// (get) Token: 0x06000C42 RID: 3138 RVA: 0x0000B7E8 File Offset: 0x000099E8
	private bool HasWallContact
	{
		get
		{
			return this.maxStamina > 0f && (this.wallContactCount > 0 || this.avgWallNormal != Vector3.zero);
		}
	}

	// Token: 0x1700012F RID: 303
	// (get) Token: 0x06000C43 RID: 3139 RVA: 0x0000B814 File Offset: 0x00009A14
	private bool HasStuckContact
	{
		get
		{
			return this.stuckCollider != null;
		}
	}

	// Token: 0x17000130 RID: 304
	// (get) Token: 0x06000C44 RID: 3140 RVA: 0x0000B822 File Offset: 0x00009A22
	public bool IsInWater
	{
		get
		{
			return this.stepsSinceInWater < 5;
		}
	}

	// Token: 0x17000131 RID: 305
	// (get) Token: 0x06000C45 RID: 3141 RVA: 0x00042FC8 File Offset: 0x000411C8
	public bool IsSubmerged
	{
		get
		{
			return this.IsInWater && base.transform.position.y + this.DepthSubmerged < (this.IsClimbing ? (this.waterHeight - 0.5f) : (this.waterHeight + 0.1f));
		}
	}

	// Token: 0x17000132 RID: 306
	// (get) Token: 0x06000C46 RID: 3142 RVA: 0x0000B82D File Offset: 0x00009A2D
	public bool IsSwimming
	{
		get
		{
			return this.IsSubmerged && !this.isSledding;
		}
	}

	// Token: 0x17000133 RID: 307
	// (get) Token: 0x06000C47 RID: 3143 RVA: 0x0000B842 File Offset: 0x00009A42
	public bool InAir
	{
		get
		{
			return this.stepsSinceLastGrounded > 2 && this.stepsSinceLastClimbing > 2 && !this.IsInWater;
		}
	}

	// Token: 0x17000134 RID: 308
	// (get) Token: 0x06000C48 RID: 3144 RVA: 0x0000B861 File Offset: 0x00009A61
	public bool IsClimbing
	{
		get
		{
			return this.maxStamina > 0f && this.stepsSinceLastClimbing < 2;
		}
	}

	// Token: 0x17000135 RID: 309
	// (get) Token: 0x06000C49 RID: 3145 RVA: 0x0000B87B File Offset: 0x00009A7B
	public bool JustJumped
	{
		get
		{
			return this.stepsSinceLastJump < 10;
		}
	}

	// Token: 0x17000136 RID: 310
	// (get) Token: 0x06000C4A RID: 3146 RVA: 0x0000B887 File Offset: 0x00009A87
	public bool JustCanceled
	{
		get
		{
			return this.stepsSinceLastCancel < 15;
		}
	}

	// Token: 0x17000137 RID: 311
	// (get) Token: 0x06000C4B RID: 3147 RVA: 0x0000B893 File Offset: 0x00009A93
	private bool IsSettled
	{
		get
		{
			return Time.time - this.unsettledTime > 0.5f;
		}
	}

	// Token: 0x17000138 RID: 312
	// (get) Token: 0x06000C4C RID: 3148 RVA: 0x0000B8A8 File Offset: 0x00009AA8
	public bool IsGrounded
	{
		get
		{
			return base.enabled && this.stepsSinceLastGrounded < 2 && !this.isSledding && this.stepsSinceLastJump > 5 && !this.IsClimbing;
		}
	}

	// Token: 0x17000139 RID: 313
	// (get) Token: 0x06000C4D RID: 3149 RVA: 0x0000B8D7 File Offset: 0x00009AD7
	public bool IsMoving
	{
		get
		{
			return !Mathf.Approximately(this.velocity.x, 0f) || !Mathf.Approximately(this.velocity.z, 0f);
		}
	}

	// Token: 0x06000C4E RID: 3150 RVA: 0x0004301C File Offset: 0x0004121C
	private void Start()
	{
		this.unsettledTime = Time.time;
		this.contextualStates = new int[8];
		this.contextualStates[0] = Animator.StringToHash("ContextualBlend");
		for (int i = -1; i < 6; i++)
		{
			this.contextualStates[i + 2] = Animator.StringToHash("Contextual" + i.ToString("0"));
		}
		if (this.persistentTransform)
		{
			this.SetPosition(GameData.g.ReadVector3(this.positionKey, base.transform.position));
			this.SetRotation(Quaternion.Euler(GameData.g.ReadFloat(this.rotationKey, base.transform.rotation.eulerAngles.y) * Vector3.up));
		}
		this.animatorController = this.animator.runtimeAnimatorController;
		this.Stamina = this.maxStamina;
	}

	// Token: 0x06000C4F RID: 3151 RVA: 0x0000B90A File Offset: 0x00009B0A
	internal void ClampSpeedForABit()
	{
		this.clampingSpeedUntil = Time.time + 0.25f;
	}

	// Token: 0x06000C50 RID: 3152 RVA: 0x00043108 File Offset: 0x00041308
	private void OnEnable()
	{
		PlayerMovement.mostRecentEnable = Time.time;
		this.unsettledTime = Time.time;
		this.recoveringControl = 0f;
		this.ClearState();
		this.stepsSinceLastGrounded = 30;
		this.stepsSinceLastClimbing = 30;
		this.stepsSinceLastJump = 30;
		this.stepsSinceInWater = 30;
		this.stepsSinceLastCancel = 30;
		if (this.specialAction)
		{
			this.transitionCooldown = this.sledCooldown;
			this.isSledding = true;
			this.animator.SetBool("Sledding", true);
		}
		else
		{
			this.transitionCooldown = 0.1f;
		}
		if (this.persistentTransform && PlayerMovement.isBabyPlayer != this.isBaby)
		{
			this.SetPosition(GameData.g.ReadVector3(this.positionKey, base.transform.position));
			this.SetRotation(Quaternion.Euler(GameData.g.ReadFloat(this.rotationKey, base.transform.rotation.eulerAngles.y) * Vector3.up));
		}
		PlayerMovement.isBabyPlayer = this.isBaby;
	}

	// Token: 0x06000C51 RID: 3153 RVA: 0x0004321C File Offset: 0x0004141C
	private void OnDisable()
	{
		this.recoveringControl = 0f;
		if (!this.isRagdolling)
		{
			this.ClearMods();
		}
		if (this.persistentTransform && this.rigidbody != null && GameData.g != null)
		{
			GameData.g.Write(this.positionKey, base.transform.position);
			GameData.g.Write(this.rotationKey, this.animator.transform.rotation.eulerAngles.y);
		}
	}

	// Token: 0x06000C52 RID: 3154 RVA: 0x000432B0 File Offset: 0x000414B0
	private void FixedUpdate()
	{
		this.UpdateState();
		if (!base.enabled)
		{
			return;
		}
		if (this.modCustomMovement)
		{
			this.HandleCustomMovement();
		}
		else
		{
			this.UpdateVelocity();
		}
		if (this.desiredJump || this.isJumpingFromRagdoll)
		{
			this.desiredJump = false;
			if (this.modJumpRule != PlayerMovement.ModRule.Locked)
			{
				this.Jump(false);
			}
		}
		this.UpdateReactors();
		this.rigidbody.angularVelocity = Vector3.zero;
		this.rigidbody.velocity = this.velocity;
		this.ClearState();
	}

	// Token: 0x06000C53 RID: 3155 RVA: 0x00043338 File Offset: 0x00041538
	private void ClearState()
	{
		this.groundContactCount = (this.steepContactCount = (this.wallContactCount = (this.stuckContactCount = 0)));
		this.contactNormal = (this.steepNormal = (this.wallNormal = Vector3.zero));
		this.wallContact = Vector3.zero;
		this.stuckCollider = null;
	}

	// Token: 0x06000C54 RID: 3156 RVA: 0x00043398 File Offset: 0x00041598
	public void ClearMods()
	{
		this.isModified = false;
		this.modNoClimbing = (this.modNoMovement = (this.modForceMovement = false));
		this.modSpeed = (this.modJumpHeight = 0f);
		this.modLockAnimatorSpeed = false;
		this.modDisableCollision = false;
		this.modIgnoreReady = false;
		this.modNoInteractions = false;
		this.modNoStaminaRecovery = false;
		this.modJumpRule = (this.modItemRule = (this.modPrimaryRule = (this.modSecondaryRule = (this.modGlideRule = PlayerMovement.ModRule.Allowed))));
		this.modIgnoreGroundedness = false;
		this.modCustomMovement = false;
		if (this.modCustomMovementScript != null)
		{
			this.modCustomMovementScript.Cancel();
			this.animator.CrossFadeInFixedTime(this.contextualStates[0], 0.1f);
		}
		this.moddedForward = Vector3.zero;
		this.moddedUp = Vector3.up;
		this.modCustomMovementScript = null;
		this.modCustomMovementAnimationIndex = -1f;
		if (this.isRagdolling)
		{
			this.ragdollController.Deactivate();
		}
	}

	// Token: 0x06000C55 RID: 3157 RVA: 0x0004349C File Offset: 0x0004169C
	public bool TryCancelNow()
	{
		if (this.transitionCooldown < 0f)
		{
			if (this.isGliding)
			{
				this.isGliding = false;
				this.glider.SetActive(false);
			}
			this.isSledding = false;
			this.stepsSinceLastCancel = 0;
			this.ClearMods();
			return true;
		}
		return false;
	}

	// Token: 0x06000C56 RID: 3158 RVA: 0x000434E8 File Offset: 0x000416E8
	private void UpdateState()
	{
		this.lastVelocity = this.velocity;
		this.avgContactNormal = (this.avgSteepNormal = (this.avgWallNormal = Vector3.zero));
		this.stepsSinceLastGrounded++;
		this.stepsSinceLastClimbing++;
		this.stepsSinceLastJump++;
		this.stepsSinceInWater++;
		this.stepsSinceLastCancel++;
		this.stepsSinceRagdolling++;
		this.wasGliding = this.isGliding;
		this.transitionCooldown -= Time.deltaTime;
		this.velocity = this.rigidbody.velocity;
		if (this.jumpVelocity != Vector3.zero)
		{
			this.stepsSinceLastJump = 0;
			this.jumpVelocity = Vector3.zero;
		}
		if (this.stepsSinceInWater < 10 && this.water != null)
		{
			this.waterHeight = this.water.GetWaterPlaneHeight(this.rigidbody.position);
		}
		if (!Game.HasControl)
		{
			if (this.isSledding)
			{
				this.cancelAction = true;
			}
			if (this.isGliding)
			{
				this.cancelAction = true;
			}
			if (!this.moddedWithoutControl && this.isRagdolling)
			{
				this.cancelAction = true;
			}
		}
		else
		{
			this.moddedWithoutControl = false;
		}
		if (this.isSledding && Player.itemManager.equippedState != PlayerItemManager.EquippedState.ShieldSled)
		{
			this.cancelAction = true;
		}
		if (this.cancelAction || (DialogueManager.d.isWaitingForPlayer && !this.modIgnoreReady))
		{
			this.cancelAction = false;
			if (this.transitionCooldown < 0f)
			{
				this.isGliding = false;
				this.isSledding = false;
				this.stepsSinceLastCancel = 0;
				this.ClearMods();
			}
		}
		if (this.specialAction)
		{
			this.specialAction = false;
		}
		if (this.isRagdolling)
		{
			this.ragdollController.enabled = true;
			base.enabled = false;
			return;
		}
		if (this.isModified && (this.IsSwimming || this.isGliding || this.IsClimbing || this.isSledding || (!this.modIgnoreReady && Game.State == GameState.Dialogue && !this.moddedWithoutControl) || DialogueManager.d.isWaitingForPlayer))
		{
			this.ClearMods();
		}
		if (this.HasStuckContact)
		{
			this.stuckContactFrames++;
			if (this.stuckContactFrames > 10)
			{
				Vector3 vector;
				float num;
				RaycastHit raycastHit;
				if (Physics.ComputePenetration(this.capsuleCollider, base.transform.position, base.transform.rotation, this.stuckCollider, this.stuckCollider.transform.position, this.stuckCollider.transform.rotation, ref vector, ref num) && num > 0.25f && Physics.Raycast(this.rigidbody.position + 10f * Vector3.up, Vector3.down, ref raycastHit, 20f, this.probeMask))
				{
					this.rigidbody.position = raycastHit.point;
					this.stuckContactFrames = 0;
				}
				this.stuckContactFrames = 0;
			}
		}
		else
		{
			this.stuckContactFrames = 0;
		}
		if (this.groundContactCount > 1)
		{
			this.contactNormal.Normalize();
			this.avgContactNormal = this.contactNormal;
		}
		if (this.wallContactCount > 1)
		{
			this.avgWallNormal = this.wallNormal.normalized;
		}
		if (this.steepContactCount > 1)
		{
			this.avgSteepNormal = this.steepNormal.normalized;
		}
		if (this.modForceMovement)
		{
			if (this.desiredDirection == Vector3.zero)
			{
				this.desiredDirection = this.velocity;
				this.desiredDirection.y = 0f;
			}
			this.desiredDirection.Normalize();
		}
		bool flag = this.HasGroundContact || this.CheckSteepContacts();
		if (this.modIgnoreGroundedness)
		{
			this.stepsSinceLastGrounded = 0;
		}
		if (Time.time - Weapon.lastWeaponSwingTime > 0.25f && this.maxStamina > 0f && !this.modNoClimbing && !Player.itemManager.IsAiming && !this.isSledding && this.stepsSinceLastCancel > 10 && this.stepsSinceLastJump > 10 && (this.SnapToWall() || this.CheckWallContacts()) && (this.Stamina > 0f || !flag))
		{
			this.stepsSinceLastClimbing = 0;
			if (this.desiredDirection != Vector3.zero)
			{
				this.Stamina = Mathf.MoveTowards(this.Stamina, 0f, this.staminaDrain * Time.deltaTime);
			}
			this.staminaVelocity = this.staminaSpeed;
			if (this.Stamina > 0f && this.Stamina < 0.75f)
			{
				this.climbTutorial.Press();
			}
		}
		else if (this.modIgnoreGroundedness || flag || this.SnapToGround())
		{
			this.stepsSinceLastGrounded = 0;
			if (this.stepsSinceLastJump > 1)
			{
				this.jumpPhase = 0;
			}
		}
		else
		{
			this.contactNormal = Vector3.up;
		}
		if (this.IsSwimming || this.HasGroundContact || this.IsClimbing)
		{
			this.isGliding = false;
		}
		if (this.isSledding && (this.isGliding || this.IsClimbing || (this.IsSubmerged && !this.sledAlwaysFloats && this.velocity.Flat().magnitude < this.sledBouyancyMinSpeed)))
		{
			this.isSledding = false;
		}
		if ((this.IsSwimming || this.IsGrounded) && this.stepsSinceLastClimbing > 10 && Game.HasControl && !this.modNoStaminaRecovery && this.stepsSinceRagdolling > 20)
		{
			if (this.Stamina < this.maxStamina)
			{
				this.staminaVelocity += Time.deltaTime * this.staminaAccel;
				this.Stamina = Mathf.MoveTowards(this.Stamina, this.maxStamina, this.staminaVelocity * Time.deltaTime);
			}
			else
			{
				this.staminaVelocity = this.staminaSpeed;
			}
		}
		if (this.recoveringControl < 1f && !this.isModified)
		{
			this.recoveringControl = Mathf.MoveTowards(this.recoveringControl, 1f, 2f * Time.deltaTime);
		}
		if (this.wallContactCount == 0)
		{
			this.climbPushing = 0f;
		}
		if (this.lockJumpHeldForNow && this.velocity.y < -0.25f)
		{
			this.lockJumpHeldForNow = false;
		}
		this.rigidbody.useGravity = !this.modCustomMovement;
		this.capsuleCollider.enabled = !this.isSledding && !this.modDisableCollision;
		this.sledCollider.enabled = this.isSledding && !this.modDisableCollision;
		if (this.sledHitTrigger.activeSelf != this.isSledding)
		{
			this.sledHitTrigger.SetActive(this.isSledding);
		}
	}

	// Token: 0x06000C57 RID: 3159 RVA: 0x0000B91D File Offset: 0x00009B1D
	public void ForceModdedState()
	{
		this.ClearMods();
		this.isSledding = false;
		this.isGliding = false;
		this.stepsSinceLastClimbing = 100;
	}

	// Token: 0x06000C58 RID: 3160 RVA: 0x00043BB8 File Offset: 0x00041DB8
	private bool SnapToGround()
	{
		if (this.stepsSinceLastGrounded > 1 || this.stepsSinceLastJump <= 2 || this.IsSubmerged)
		{
			return false;
		}
		float magnitude = this.velocity.magnitude;
		RaycastHit raycastHit;
		if (!Physics.SphereCast(base.transform.TransformPoint(Vector3.up), 0.15f, Vector3.down, ref raycastHit, this.probeLength, this.probeMask))
		{
			return false;
		}
		if (raycastHit.normal.y < this.groundNormalThreshold)
		{
			return false;
		}
		if (this.isSledding && Mathf.Abs(Vector3.Dot(raycastHit.normal, this.velocity)) > this.sledBounceThreshold)
		{
			return false;
		}
		this.groundContactCount = 1;
		this.contactNormal = raycastHit.normal;
		float num = Vector3.Dot(this.velocity, raycastHit.normal);
		if (num > 0f)
		{
			Vector3 vector = (this.velocity - raycastHit.normal * num).normalized * magnitude;
			this.velocity.x = vector.x;
			this.velocity.z = vector.z;
			if (vector.y > 0f)
			{
				this.velocity.y = vector.y;
			}
		}
		return true;
	}

	// Token: 0x06000C59 RID: 3161 RVA: 0x00043CFC File Offset: 0x00041EFC
	private bool SnapToWall()
	{
		if (this.stepsSinceLastClimbing > 1 || this.stepsSinceLastJump <= 2 || this.IsSubmerged)
		{
			return false;
		}
		Vector3 normalized = this.climbingDirection.normalized;
		if (normalized.y > 0f)
		{
			normalized.y = 0f;
			normalized.Normalize();
		}
		float magnitude = this.velocity.magnitude;
		int num = 0;
		int num2 = 0;
		Vector3 vector = Vector3.zero;
		Transform transform = this.animator.transform;
		Vector3 vector2 = base.transform.TransformPoint(Vector3.up * 0.75f);
		Vector3 vector3 = Quaternion.LookRotation(normalized) * Vector3.up;
		for (int i = -1; i < 2; i++)
		{
			Vector3 vector4 = vector2 + 0.5f * (float)i * vector3;
			Vector3 normalized2 = (normalized - 0.5f * (float)i * vector3).normalized;
			Debug.DrawRay(vector4, normalized2);
			RaycastHit raycastHit;
			if (Physics.SphereCast(vector4, 0.25f, normalized2, ref raycastHit, 1.5f, this.climbableLayers))
			{
				num++;
				this.wallContactCount++;
				this.wallNormal += raycastHit.normal;
				vector += raycastHit.point;
				if (raycastHit.normal.y >= this.groundNormalThreshold)
				{
					num2++;
					this.contactNormal += raycastHit.normal;
					this.groundContactCount++;
				}
			}
		}
		if (this.wallContactCount == 0)
		{
			return false;
		}
		if (this.wallContactCount > 1)
		{
			this.wallNormal.Normalize();
		}
		if (Vector3.Dot(new Vector3(normalized.x, 0f, normalized.z), this.desiredDirection) < -0.1f && (this.wallNormal.y > this.groundNormalThreshold || this.HasGroundContact || this.groundContactCount > 0))
		{
			return false;
		}
		if (num == num2)
		{
			return false;
		}
		this.contactNormal = this.wallNormal;
		Vector3 vector5 = Vector3.ProjectOnPlane(this.velocity, this.wallNormal);
		vector5 += -0.5f * this.wallNormal;
		if (this.Stamina > 0f)
		{
			this.velocity = vector5;
		}
		else
		{
			this.velocity = Vector3.Lerp(this.velocity, vector5, 0.25f);
		}
		return true;
	}

	// Token: 0x06000C5A RID: 3162 RVA: 0x00043F6C File Offset: 0x0004216C
	private bool CheckSteepContacts()
	{
		if (this.steepContactCount > 1)
		{
			this.steepNormal.Normalize();
			if (this.steepNormal.y >= this.groundNormalThreshold)
			{
				this.steepContactCount = 0;
				this.groundContactCount = 1;
				this.contactNormal = this.steepNormal;
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000C5B RID: 3163 RVA: 0x00043FC0 File Offset: 0x000421C0
	private bool CheckWallContacts()
	{
		if (this.stepsSinceLastJump < 5 || this.wallContactCount == 0 || (this.IsSubmerged && this.Stamina == 0f))
		{
			return false;
		}
		if (this.Stamina == 0f && !this.IsClimbing)
		{
			this.climbPushing = 0f;
		}
		if (this.wallContactCount > 1)
		{
			this.wallNormal.Normalize();
			this.wallContactCount = 1;
		}
		float num = Vector3.Dot(new Vector3(this.wallNormal.x, 0f, this.wallNormal.z), this.desiredDirection);
		if (0.5f * (this.wallNormal.y + this.contactNormal.y) < this.groundNormalThreshold && num <= 0f)
		{
			if (this.stepsSinceLastGrounded < 3 && !this.modForceMovement)
			{
				if (num < -0.6f)
				{
					this.climbPushing += Time.deltaTime;
				}
				else
				{
					this.climbPushing = 0f;
				}
				if (this.climbPushing < this.climbPushThreshold || this.Stamina < 0.5f)
				{
					return false;
				}
			}
			this.groundContactCount = 1;
			this.contactNormal = this.wallNormal;
			return true;
		}
		this.climbPushing = 0f;
		return false;
	}

	// Token: 0x06000C5C RID: 3164 RVA: 0x00044100 File Offset: 0x00042300
	private void UpdateVelocity()
	{
		Vector3 vector = this.velocity;
		Vector3 vector2 = this.desiredDirection;
		if (Time.time < this.clampingSpeedUntil)
		{
			Vector2 vector3;
			vector3..ctor(this.velocity.x, this.velocity.z);
			vector3 = Vector2.ClampMagnitude(vector3, 7f);
			this.velocity.x = vector3.x;
			this.velocity.z = vector3.y;
		}
		if (this.stepsSinceLastJump < 5)
		{
			this.velocity += -Physics.gravity * Time.deltaTime;
			return;
		}
		if (!this.IsSwimming && !this.isSledding && ((this.IsClimbing && this.Stamina > 0f) || (this.HasGroundContact && !this.IsClimbing)) && this.desiredDirection == Vector3.zero && this.stepsSinceLastJump > 5 && this.recoveringControl == 1f)
		{
			this.velocity = -Physics.gravity * Time.deltaTime;
			return;
		}
		if (!this.HasGroundContact && this.velocity.y > 0f && !this.holdingJump && !this.lockJumpHeldForNow)
		{
			this.velocity += this.jumpUnheldFactor * Time.deltaTime * Physics.gravity;
		}
		float num = (this.IsClimbing ? Mathf.Lerp(this.climbingSpeed, this.Speed, 0.25f * this.wallNormal.y) : (this.IsSwimming ? this.swimmingSpeed : (this.isGliding ? this.glidingSpeed : this.Speed)));
		if (this.modSpeed != 0f)
		{
			num = this.modSpeed;
		}
		if (this.modNoMovement)
		{
			num = 0f;
		}
		vector2 *= num;
		float num2 = 0f;
		if (this.IsSubmerged)
		{
			this.velocity += -Physics.gravity * Time.deltaTime;
			if (this.isSledding)
			{
				if (this.sledAlwaysFloats)
				{
					num2 = 1f;
				}
				else
				{
					num2 = Mathf.InverseLerp(this.sledBouyancyMinSpeed, this.sledBouyancyMaxSpeed, this.velocity.Flat().magnitude);
				}
			}
			this.velocity.y = Mathf.Max(Mathf.Lerp(-5f, -1f, num2), this.velocity.y);
			float num3 = this.waterHeight - (base.transform.position.y + this.DepthSubmerged);
			float num4 = Mathf.MoveTowards(this.velocity.y, Mathf.Lerp(this.buoyantSpeed, 5f, num2) * Mathf.InverseLerp(0f, this.buoyantDepth, num3), this.swimmingDamp * Time.deltaTime);
			if (this.isSledding)
			{
				this.velocity.y = Mathf.Lerp(this.velocity.y, num4, num2);
			}
			else
			{
				this.velocity.y = num4;
			}
		}
		if (this.IsSwimming)
		{
			float num5 = this.swimmingAcc * Time.deltaTime;
			Vector2 vector4 = vector2.xz() - this.velocity.xz();
			vector4 = Vector3.ClampMagnitude(vector4, num5);
			this.velocity.x = this.velocity.x + vector4.x;
			this.velocity.z = this.velocity.z + vector4.y;
			return;
		}
		if (this.HasGroundContact && !this.IsClimbing && !this.isSledding)
		{
			float num6 = Vector3.Dot(vector2.normalized, this.contactNormal);
			vector2 *= 1f + this.slopeFactor * num6;
		}
		if (this.HasSteepContact && !this.HasGroundContact && !this.IsClimbing && !this.isSledding)
		{
			float num7 = Vector3.Dot(vector2, this.steepNormal.normalized);
			if (num7 < 0f)
			{
				vector2 += 1.5f * vector2.normalized * num7;
			}
		}
		Vector3 vector5;
		Vector3 vector6;
		if (this.IsClimbing)
		{
			vector5 = this.ProjectOnWallPlane(Vector3.right).normalized;
			vector6 = this.ProjectOnWallPlane(Vector3.forward).normalized;
		}
		else
		{
			vector5 = this.ProjectOnContactPlane(Vector3.right).normalized;
			vector6 = this.ProjectOnContactPlane(Vector3.forward).normalized;
		}
		float num8 = Vector3.Dot(this.velocity, vector5);
		float num9 = Vector3.Dot(this.velocity, vector6);
		Vector2 vector7;
		vector7..ctor(num8, num9);
		float num10 = num8;
		float num11 = num9;
		if (this.isGliding)
		{
			float num12 = this.velocity.y;
			this.velocity.y = 0f;
			float num13 = Vector3.Dot(this.velocity.normalized, vector2.normalized);
			this.velocity = Vector3.MoveTowards(this.velocity, vector2, Mathf.Lerp(this.glidingDeccel, this.glidingAccel, 0.5f * (num13 + 1f)) * Time.deltaTime);
			float num14 = Mathf.Lerp(this.glidingVerticalMinSpeed, this.glidingVerticalSpeed, Mathf.InverseLerp(0.5f, 2f, this.velocity.magnitude));
			num12 = Mathf.MoveTowards(num12, num14, this.glidingVerticalAccel * Time.deltaTime);
			this.velocity.y = num12;
			return;
		}
		if (this.isSledding)
		{
			Vector3.Dot(this.contactNormal, vector5);
			Vector3.Dot(this.contactNormal, vector6);
			if (this.InAir)
			{
				num10 += this.desiredDirection.x * this.sledAirAccel * Time.deltaTime;
				num11 += this.desiredDirection.z * this.sledAirAccel * Time.deltaTime;
			}
			else if (this.desiredDirection != Vector3.zero)
			{
				bool flag = vector7.sqrMagnitude < this.sledMaxAccelSpeed * this.sledMaxAccelSpeed;
				Vector3 vector8 = Vector3.RotateTowards(new Vector3(num10, 0f, num11), this.sledMaxSpeed * this.desiredDirection, this.sledRotation * Time.deltaTime, flag ? (this.sledAccel * Time.deltaTime) : 0f);
				num10 = vector8.x;
				num11 = vector8.z;
			}
			Vector2 vector9;
			vector9..ctor(num10, num11);
			vector9 = Vector2.ClampMagnitude(vector9, this.sledMaxSpeed);
			if (this.IsSubmerged)
			{
				vector9 *= 1f - Time.deltaTime * Mathf.Lerp(this.sledWaterFriction, 0.25f * this.sledWaterFriction, num2);
			}
			num10 = vector9.x;
			num11 = vector9.y;
		}
		else
		{
			float num15 = (this.IsClimbing ? ((this.Stamina > 0f) ? 10000f : 100f) : ((this.HasGroundContact || this.HasSteepContact) ? ((vector2 == Vector3.zero && this.recoveringControl == 1f) ? 500f : this.maxAcceleration) : (this.HasSteepContact ? 500f : ((vector2 == Vector3.zero) ? 0f : this.maxAirAcceleration)))) * this.recoveringControl * Time.deltaTime;
			Vector3 vector10 = Vector3.zero;
			vector10.x = vector2.x - num8;
			vector10.z = vector2.z - num9;
			vector10 = Vector3.ClampMagnitude(vector10, num15);
			num10 = num8 + vector10.x;
			num11 = num9 + vector10.z;
		}
		this.velocity += vector5 * (num10 - num8) + vector6 * (num11 - num9);
		if (this.IsClimbing && this.Stamina == 0f)
		{
			this.velocity.y = Mathf.MoveTowards(vector.y - Time.deltaTime * Physics.gravity.y, -this.climbingSlideSpeed, 15f * Time.deltaTime);
		}
		if (this.IsGrounded && !this.isSledding)
		{
			this.velocity += Time.deltaTime * Physics.gravity;
		}
		this.velocity = Vector3.ClampMagnitude(this.velocity, this.maxSpeed);
	}

	// Token: 0x06000C5D RID: 3165 RVA: 0x00044984 File Offset: 0x00042B84
	private void HandleCustomMovement()
	{
		Vector3 position = this.rigidbody.position;
		if (this.moddedForward == Vector3.zero)
		{
			this.moddedForward = this.animator.transform.forward;
		}
		this.moddedUp = Vector3.up;
		AnimatorStateInfo currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(this.contextualAnimationsLayer);
		AnimatorStateInfo nextAnimatorStateInfo = this.animator.GetNextAnimatorStateInfo(this.contextualAnimationsLayer);
		Vector3 vector = position;
		float deltaTime = Time.deltaTime;
		Vector3 vector2 = this.velocity;
		float deltaTime2 = Time.deltaTime;
		Vector3 gravity = Physics.gravity;
		this.modCustomMovementScript.MovementUpdate(this.desiredDirection, ref position, ref this.velocity, ref this.moddedForward, ref this.moddedUp, ref this.modCustomMovementAnimationIndex);
		this.velocity = (position - vector) / Time.fixedDeltaTime;
		Vector2 vector3 = this.velocity.xz();
		vector3 = Vector2.ClampMagnitude(vector3, this.sledMaxSpeed);
		this.velocity.x = vector3.x;
		this.velocity.z = vector3.y;
		this.velocity.y = Mathf.Clamp(this.velocity.y, -15f, 15f);
		int num = Mathf.RoundToInt(Mathf.Max(0f, this.modCustomMovementAnimationIndex + 2f));
		if (currentAnimatorStateInfo.shortNameHash != this.contextualStates[num] && nextAnimatorStateInfo.shortNameHash != this.contextualStates[num])
		{
			this.animator.CrossFadeInFixedTime(this.contextualStates[num], 0.1f);
		}
		if (this.modCustomMovementAnimationIndex < -5f)
		{
			float num2 = this.modCustomMovementAnimationIndex + 10f;
			this.animator.SetFloat(PlayerMovement.ContextualBlendID, num2);
			return;
		}
		this.animator.SetFloat(PlayerMovement.ContextualBlendID, 0f);
	}

	// Token: 0x06000C5E RID: 3166 RVA: 0x0000B93B File Offset: 0x00009B3B
	public void TryJump()
	{
		if (this.modJumpRule != PlayerMovement.ModRule.Locked)
		{
			this.Jump(false);
		}
	}

	// Token: 0x06000C5F RID: 3167 RVA: 0x00044B54 File Offset: 0x00042D54
	public void Jump(bool fromRagdoll = false)
	{
		if (this.stepsSinceLastJump < 20 && !fromRagdoll)
		{
			return;
		}
		if (this.isRagdolling)
		{
			this.ragdollController.Deactivate();
			this.desiredJump = true;
			return;
		}
		if (base.transform.position.y > 140f)
		{
			return;
		}
		if (fromRagdoll && this.Stamina < 0.1f)
		{
			return;
		}
		if (fromRagdoll)
		{
			this.Stamina -= this.climbJumpStaminaCost;
		}
		bool flag = false;
		RaycastHit raycastHit;
		if (!this.isGliding && !this.IsClimbing && this.stepsSinceLastGrounded >= 10 && Physics.SphereCast(base.transform.TransformPoint(Vector3.up), 0.15f, Vector3.down, ref raycastHit, 1.35f, this.probeMask) && raycastHit.normal.y > 0.7f)
		{
			flag = true;
		}
		Vector3 vector;
		if (this.IsClimbing && this.stepsSinceLastGrounded > ((this.Stamina == 0f) ? 10 : 5))
		{
			if (this.Stamina > 0f)
			{
				vector = this.wallNormal + 3f * Vector3.up;
				this.Stamina = Mathf.MoveTowards(this.Stamina, 0f, this.climbJumpStaminaCost);
			}
			else
			{
				vector = this.wallNormal;
				vector.y = 0f;
			}
		}
		else if (this.IsGrounded || this.IsInWater || fromRagdoll || this.stepsSinceLastGrounded < 10 || flag)
		{
			vector = Vector3.up;
		}
		else if (this.HasSteepContact)
		{
			if (this.avgSteepNormal.y < this.groundNormalThreshold)
			{
				return;
			}
			vector = Vector3.up;
		}
		else
		{
			if (!this.isSledding || (this.stepsSinceLastGrounded >= 5 && this.stepsSinceInWater >= 10))
			{
				if (this.modGlideRule != PlayerMovement.ModRule.Locked && this.glidingUnlocked && !this.HasSteepContact && this.velocity.y < this.glidingMinFallSpeed && this.transitionCooldown < 0f)
				{
					if (this.modGlideRule == PlayerMovement.ModRule.Cancels)
					{
						this.ClearMods();
					}
					this.isGliding = !this.isGliding;
					this.transitionCooldown = this.glidingCooldown;
					this.glideTutorial.Press();
				}
				return;
			}
			vector = Vector3.up;
		}
		if (this.isSledding && this.stepsSinceLastGrounded > 5 && this.stepsSinceInWater < 10)
		{
			PlayerMovement.didShieldSkip = true;
		}
		this.stepsSinceLastJump = 0;
		if (this.modJumpRule == PlayerMovement.ModRule.Cancels)
		{
			if (this.stamina == 0f && this.modNoStaminaRecovery)
			{
				vector.y = 0f;
			}
			this.ClearMods();
		}
		this.jumpPhase++;
		float num = (this.isSledding ? this.sledJumpHeight : this.jumpHeight);
		if (this.modJumpHeight != 0f)
		{
			num = this.modJumpHeight;
		}
		float num2 = Mathf.Sqrt(-2f * Physics.gravity.y * num);
		vector = vector.normalized;
		if (this.isSledding)
		{
			vector = (vector + 0.4f * this.desiredDirection).normalized;
		}
		float num3 = Vector3.Dot(this.velocity, vector);
		if (num3 > 0f)
		{
			num2 = Mathf.Lerp(num2, Mathf.Max(num2 - num3, 0f), 0.5f);
		}
		Vector3 vector2 = Vector3.ProjectOnPlane(this.velocity, vector) + vector * (num2 + Mathf.Max(num3, 0f));
		this.jumpVelocity = vector2 - this.velocity;
		this.rigidbody.velocity = vector2;
		this.animator.SetBool("Swimming", false);
		this.animator.SetBool("IsSubmerged", false);
		this.animator.SetTrigger("Jump");
		if (this.IsSubmerged)
		{
			EffectsManager.e.Splash(new Vector3(base.transform.position.x, this.waterHeight, base.transform.position.z), 0.8f);
		}
		else
		{
			this.effects.Jump();
			if (this.isSledding && this.shield != null)
			{
				this.shield.OnJump();
			}
		}
		this.isJumpingFromRagdoll = false;
		this.jumpTutorial.Press();
	}

	// Token: 0x06000C60 RID: 3168 RVA: 0x0000B94D File Offset: 0x00009B4D
	public void ResetGrounded()
	{
		this.stepsSinceLastGrounded = 1;
		this.stepsSinceLastJump = 0;
	}

	// Token: 0x06000C61 RID: 3169 RVA: 0x00044FA0 File Offset: 0x000431A0
	public bool Sled()
	{
		if (this.isSledding)
		{
			if (this.transitionCooldown < 0f)
			{
				this.isSledding = false;
				this.transitionCooldown = this.sledCooldown;
				this.stepsSinceLastCancel = 0;
			}
		}
		else if (this.transitionCooldown < 0f || this.recoveringControl < 1f)
		{
			this.transitionCooldown = this.sledCooldown;
			this.isSledding = true;
			if (this.rigidbody.velocity.y > -5f)
			{
				this.effects.SledTransition();
				Vector3 vector = (this.InAir ? this.sledAirBoost : this.sledStandingBoost);
				if (this.isGliding || this.wasGliding)
				{
					vector.y = 0f;
				}
				Vector3 vector2 = this.animator.transform.InverseTransformVector(this.rigidbody.velocity);
				vector2.y = Mathf.Max(vector2.y, vector.y);
				vector2.z = Mathf.Max(vector2.z, vector.z);
				this.rigidbody.velocity = this.animator.transform.TransformVector(vector2);
			}
		}
		return this.isSledding;
	}

	// Token: 0x06000C62 RID: 3170 RVA: 0x0000B95D File Offset: 0x00009B5D
	public void Ragdoll(float forwardSpeed)
	{
		this.Ragdoll(this.animator.transform.forward * forwardSpeed);
	}

	// Token: 0x06000C63 RID: 3171 RVA: 0x0000B97B File Offset: 0x00009B7B
	public void Ragdoll()
	{
		this.Ragdoll(Vector3.zero);
	}

	// Token: 0x06000C64 RID: 3172 RVA: 0x000450DC File Offset: 0x000432DC
	public void Ragdoll(Vector3 velocity)
	{
		if (this.isRagdolling)
		{
			this.ragdollController.isDeactivating = false;
			return;
		}
		this.ClearMods();
		this.UpdateState();
		if (!Game.HasControl)
		{
			this.moddedWithoutControl = true;
		}
		this.rigidbody.velocity += velocity;
		this.isSledding = false;
		this.ragdollController.enabled = true;
		this.isModified = true;
		this.isRagdolling = true;
		base.enabled = false;
	}

	// Token: 0x06000C65 RID: 3173 RVA: 0x00045158 File Offset: 0x00043358
	private void UpdateReactors()
	{
		float num = (this.IsClimbing ? this.colliderClimbingHeight : (this.colliderStandingHeight + this.bonusHeight));
		this.capsuleCollider.height = Mathf.MoveTowards(this.capsuleCollider.height, num, ((num > this.capsuleCollider.height) ? 5f : 2f) * Time.deltaTime);
		this.animator.SetBool("Grounded", !this.JustJumped && (this.HasGroundContact || this.HasSteepContact || this.modIgnoreGroundedness || this.stepsSinceLastGrounded < 4));
		this.animator.SetBool("Climbing", this.IsClimbing);
		this.animator.SetBool("Swimming", this.IsSwimming && !this.JustJumped);
		this.animator.SetBool("Gliding", this.isGliding);
		this.animator.SetBool("Sledding", this.isSledding);
		if (!this.glider.activeSelf && this.isGliding)
		{
			EffectsManager.e.FloorDust(this.glider.transform.position, 10, Vector3.up);
		}
		if (this.glider.activeSelf != this.isGliding)
		{
			this.glider.SetActive(this.isGliding);
		}
		this.climbingDirection = -this.wallNormal;
		this.climbingContact = this.wallContact;
		this.animGroundNormal = this.contactNormal;
		if (this.stepsSinceInWater < 3)
		{
			this.rippleCounter += Time.deltaTime * Mathf.Lerp(this.idleRippleSpeed, this.movingRippleSpeed, Mathf.InverseLerp(0f, this.Speed, this.velocity.magnitude));
			if (this.rippleCounter > 1f)
			{
				int num2 = Mathf.FloorToInt(this.rippleCounter);
				this.rippleCounter -= (float)num2;
				Vector3 vector;
				vector..ctor(this.velocity.x, 0f, this.velocity.z);
				if (vector.magnitude > 2f)
				{
					EffectsManager.e.Ripple(new Vector3(base.transform.position.x, this.waterHeight, base.transform.position.z), num2, vector);
				}
				else
				{
					EffectsManager.e.Ripple(new Vector3(base.transform.position.x, this.waterHeight, base.transform.position.z), num2);
				}
			}
		}
		if (this.persistentTransform && Game.HasControl)
		{
			GameData.g.Write(this.positionKey, base.transform.position);
			GameData.g.Write(this.rotationKey, this.animator.transform.rotation.eulerAngles.y);
		}
	}

	// Token: 0x06000C66 RID: 3174 RVA: 0x00045454 File Offset: 0x00043654
	private void Update()
	{
		Vector3 vector = Vector3.Lerp(this.lastVelocity, this.velocity, (Time.time - Time.fixedTime) / Time.fixedDeltaTime);
		float magnitude = vector.magnitude;
		float num = Vector3.SignedAngle(this.animator.transform.up, this.animGroundNormal, -this.animator.transform.right);
		this.animatorAngle = Mathf.SmoothDamp(this.animatorAngle, num, ref this.animatorAngleVelocity, 0.2f);
		this.animator.SetFloat("Angle", this.animatorAngle);
		this.isClimbingSmooth = Mathf.MoveTowards(this.isClimbingSmooth, this.IsClimbing ? Mathf.Lerp(1f, 0f, -this.climbingDirection.y) : 0f, 1f * Time.deltaTime);
		this.animator.SetFloat("ClimbingSmooth", this.isClimbingSmooth);
		float num2;
		if (this.modLockAnimatorSpeed)
		{
			num2 = (this.animatorSpeed = 0f);
		}
		else if (this.modCustomMovement)
		{
			num2 = (this.animatorSpeed = magnitude);
		}
		else if (this.IsClimbing)
		{
			float num3 = magnitude;
			if (this.Stamina == 0f)
			{
				num3 = 5f;
			}
			if (Mathf.Abs(num3) < 0.5f)
			{
				num3 = 0f;
			}
			this.animatorSpeed = Mathf.MoveTowards(this.animatorSpeed, 1.4f * num3, 5f * Time.deltaTime);
			num2 = this.animatorSpeed;
		}
		else
		{
			float num4 = Mathf.Min(2f * this.Speed, (vector - Vector3.up * vector.y).magnitude);
			num4 = Mathf.Max(num4, this.desiredDirection.magnitude * this.Speed * 0.5f);
			if (num4 > this.animatorSpeed)
			{
				this.animatorSpeed = Mathf.MoveTowards(this.animatorSpeed, num4, 10f * Time.deltaTime);
			}
			else if (this.animatorSpeed - num4 > 5f)
			{
				this.animatorSpeed = num4 + 5f;
			}
			else
			{
				this.animatorSpeed = Mathf.MoveTowards(this.animatorSpeed, num4, 10f * Time.deltaTime);
			}
			num2 = this.animatorSpeed;
		}
		this.animator.SetBool("IsSubmerged", this.IsSubmerged && !this.JustJumped);
		this.animator.SetFloat("Speed", num2);
		float num5 = ((num2 > 0.1f || !this.IsGrounded) ? (this.IsGrounded ? num2 : 1f) : 0f);
		num5 = Mathf.Clamp01(num5);
		this.baseLayerWeight = Mathf.MoveTowards(this.baseLayerWeight, num5, ((num5 > this.baseLayerWeight) ? 10f : 2.5f) * Time.deltaTime);
		this.animator.SetLayerWeight(2, this.baseLayerWeight);
		this.animator.SetFloat("VerticalSpeed", vector.y);
		this.hasContextualAnimations = Mathf.MoveTowards(this.hasContextualAnimations, this.modCustomMovement ? 1f : 0f, 10f * Time.deltaTime);
		if (this.contextualAnimationsLayer == -1)
		{
			this.contextualAnimationsLayer = this.animator.GetLayerIndex("Contextual");
		}
		this.animator.SetLayerWeight(this.contextualAnimationsLayer, this.hasContextualAnimations);
	}

	// Token: 0x06000C67 RID: 3175 RVA: 0x0000B988 File Offset: 0x00009B88
	private void OnCollisionEnter(Collision collision)
	{
		this.EvaluateCollision(collision);
	}

	// Token: 0x06000C68 RID: 3176 RVA: 0x0000B988 File Offset: 0x00009B88
	private void OnCollisionStay(Collision collision)
	{
		this.EvaluateCollision(collision);
	}

	// Token: 0x06000C69 RID: 3177 RVA: 0x000457D4 File Offset: 0x000439D4
	private void EvaluateCollision(Collision collision)
	{
		bool flag = false;
		bool flag2 = false;
		Vector3 vector = Vector3.zero;
		Vector3 vector2 = Vector3.zero;
		for (int i = 0; i < collision.contactCount; i++)
		{
			int num = this.groundContactCount;
			int num2 = this.wallContactCount;
			int num3 = this.steepContactCount;
			ContactPoint contact = collision.GetContact(i);
			Vector3 normal = contact.normal;
			if (normal.y >= this.groundNormalThreshold)
			{
				this.groundContactCount++;
				this.contactNormal += normal;
				if (this.groundContactCount == 1 && this.stepsSinceLastGrounded > 5 && this.InAir && !this.JustJumped && this.IsSettled)
				{
					flag2 = true;
					vector = normal;
					vector2 = contact.point;
				}
			}
			else
			{
				if (!flag2 && this.isSledding && !this.IsGrounded && this.InAir && !this.JustJumped && this.IsSettled && Vector3.Dot(this.velocity, normal) < -1f)
				{
					flag2 = true;
					vector = normal;
					vector2 = contact.point;
				}
				if (normal.y > -this.groundNormalThreshold && this.climbableLayers.Contains(collision.collider.gameObject.layer))
				{
					this.wallContactCount++;
					this.wallNormal += normal;
				}
				if (normal.y > -0.01f)
				{
					this.steepContactCount++;
					this.steepNormal += normal;
				}
				if (normal.y < -this.groundNormalThreshold && !this.IsClimbing)
				{
					Vector3 vector3 = this.capsuleCollider.center + 0.5f * this.capsuleCollider.height * Vector3.up;
					vector3 += -this.capsuleCollider.radius * normal;
					vector3 = base.transform.TransformPoint(vector3);
					if (contact.point.y < vector3.y - 0.1f)
					{
						flag = true;
					}
				}
			}
		}
		if (flag2)
		{
			this.effects.Land(MaterialManager.m.SampleSurfaceMaterial(vector2, -vector), vector);
			if (this.isSledding && this.shield != null)
			{
				this.shield.OnLand();
			}
		}
		if (flag)
		{
			this.stuckContactCount++;
			this.stuckCollider = collision.collider;
		}
	}

	// Token: 0x06000C6A RID: 3178 RVA: 0x00045A64 File Offset: 0x00043C64
	public void WaterTrigger(Collider waterCollider, Water water)
	{
		float waterPlaneHeight = water.GetWaterPlaneHeight(this.rigidbody.position);
		Vector3 position = this.rigidbody.position;
		if (this.modCustomMovement)
		{
			return;
		}
		if (this.stepsSinceInWater < 2 && waterPlaneHeight < this.waterHeight)
		{
			return;
		}
		RaycastHit raycastHit;
		if (!this.IsSubmerged && Physics.Raycast(position, Vector3.down, ref raycastHit, 0.1f + position.y - waterPlaneHeight) && raycastHit.collider != waterCollider)
		{
			return;
		}
		if (this.isRagdolling && position.y > waterPlaneHeight - this.DepthSubmerged)
		{
			return;
		}
		if (position.y < waterPlaneHeight)
		{
			if (this.stepsSinceLastGrounded > 5 && this.stepsSinceInWater > 15)
			{
				EffectsManager.e.Splash(new Vector3(base.transform.position.x, waterPlaneHeight, base.transform.position.z), 0.8f);
			}
			if (this.isRagdolling)
			{
				this.ragdollController.Deactivate();
			}
			if (this.isGliding)
			{
				this.isGliding = false;
			}
			this.water = water;
			this.waterHeight = waterPlaneHeight;
			this.stepsSinceInWater = 0;
		}
	}

	// Token: 0x06000C6B RID: 3179 RVA: 0x0000B991 File Offset: 0x00009B91
	private Vector3 ProjectOnContactPlane(Vector3 vector)
	{
		return vector - this.contactNormal * Vector3.Dot(vector, this.contactNormal);
	}

	// Token: 0x06000C6C RID: 3180 RVA: 0x0000B9B0 File Offset: 0x00009BB0
	private Vector3 ProjectOnWallPlane(Vector3 vector)
	{
		return this.ProjectOnContactPlane(vector) + Vector3.down * Vector3.Dot(vector, this.wallNormal);
	}

	// Token: 0x06000C6D RID: 3181 RVA: 0x0000B9D4 File Offset: 0x00009BD4
	public void ApplyTransform(Transform newTransform)
	{
		this.SetPosition(newTransform.position);
		this.SetRotation(newTransform.rotation);
	}

	// Token: 0x06000C6E RID: 3182 RVA: 0x00045B84 File Offset: 0x00043D84
	public void SetPosition(Vector3 newPosition)
	{
		if (this.isModified)
		{
			this.ClearMods();
		}
		this.unsettledTime = Time.time;
		this.rigidbody.velocity = (this.velocity = Vector3.zero);
		base.transform.position = newPosition;
		this.animator.transform.position = newPosition;
		this.animator.GetComponent<FootIKSmooth>().ResetHeight();
		if (this.isRagdolling)
		{
			this.ragdollController.SetPosition(newPosition);
		}
	}

	// Token: 0x06000C6F RID: 3183 RVA: 0x0000B9EE File Offset: 0x00009BEE
	public void SetRotation(Quaternion newRotation)
	{
		this.animator.transform.rotation = newRotation;
		this.reactionController.SetForward(newRotation * Vector3.forward);
	}

	// Token: 0x04000FB0 RID: 4016
	public static float mostRecentEnable;

	// Token: 0x04000FB1 RID: 4017
	public const float maxHeight = 150f;

	// Token: 0x04000FB2 RID: 4018
	private static bool isBabyPlayer;

	// Token: 0x04000FB3 RID: 4019
	public static bool didShieldSkip = false;

	// Token: 0x04000FB4 RID: 4020
	public Animator animator;

	// Token: 0x04000FB5 RID: 4021
	private RuntimeAnimatorController animatorController;

	// Token: 0x04000FB6 RID: 4022
	public CharacterReactionController reactionController;

	// Token: 0x04000FB7 RID: 4023
	public PlayerEffects effects;

	// Token: 0x04000FB8 RID: 4024
	public bool isBaby;

	// Token: 0x04000FB9 RID: 4025
	public Rigidbody rigidbody;

	// Token: 0x04000FBA RID: 4026
	public Vector3 velocity = Vector3.zero;

	// Token: 0x04000FBB RID: 4027
	private Vector3 lastVelocity;

	// Token: 0x04000FBC RID: 4028
	public float maxSpeed = 40f;

	// Token: 0x04000FBD RID: 4029
	[HideInInspector]
	public Vector3 desiredDirection;

	// Token: 0x04000FBE RID: 4030
	[HideInInspector]
	public Vector3 desiredDirectionRaw;

	// Token: 0x04000FBF RID: 4031
	[HideInInspector]
	public bool desiredJump;

	// Token: 0x04000FC0 RID: 4032
	[HideInInspector]
	public bool holdingJump;

	// Token: 0x04000FC1 RID: 4033
	[HideInInspector]
	public bool cancelAction;

	// Token: 0x04000FC2 RID: 4034
	[HideInInspector]
	public bool specialAction;

	// Token: 0x04000FC3 RID: 4035
	[Space]
	public bool persistentTransform = true;

	// Token: 0x04000FC4 RID: 4036
	[ConditionalHide("persistentTransform", true)]
	public string positionKey = "PlayerPosition";

	// Token: 0x04000FC5 RID: 4037
	[ConditionalHide("persistentTransform", true)]
	public string rotationKey = "PlayerRotation";

	// Token: 0x04000FC6 RID: 4038
	[Header("Movement")]
	public float maxAcceleration = 5f;

	// Token: 0x04000FC7 RID: 4039
	public float maxAirAcceleration = 2f;

	// Token: 0x04000FC8 RID: 4040
	[ReadOnly]
	public float animatorSpeed;

	// Token: 0x04000FC9 RID: 4041
	[ReadOnly]
	public float animatorVerticalSpeed;

	// Token: 0x04000FCA RID: 4042
	private float animatorAngle = 1f;

	// Token: 0x04000FCB RID: 4043
	private float animatorAngleVelocity;

	// Token: 0x04000FCC RID: 4044
	public float speed = 5.5f;

	// Token: 0x04000FCD RID: 4045
	public float slopeFactor = 0.4f;

	// Token: 0x04000FCE RID: 4046
	public bool overrideSpeed;

	// Token: 0x04000FCF RID: 4047
	public float overriddenSpeed = 5.5f;

	// Token: 0x04000FD0 RID: 4048
	[Header("Stamina")]
	private float stamina;

	// Token: 0x04000FD1 RID: 4049
	public float maxStamina;

	// Token: 0x04000FD2 RID: 4050
	private float staminaVelocity;

	// Token: 0x04000FD3 RID: 4051
	public float staminaSpeed = 1f;

	// Token: 0x04000FD4 RID: 4052
	public float staminaAccel = 5f;

	// Token: 0x04000FD5 RID: 4053
	[Header("Climbing")]
	public float climbingSpeed = 1f;

	// Token: 0x04000FD6 RID: 4054
	public float climbingSlideSpeed = 3f;

	// Token: 0x04000FD7 RID: 4055
	public LayerMask climbableLayers;

	// Token: 0x04000FD8 RID: 4056
	public float staminaDrain = 1f;

	// Token: 0x04000FD9 RID: 4057
	public CapsuleCollider capsuleCollider;

	// Token: 0x04000FDA RID: 4058
	public float colliderStandingHeight;

	// Token: 0x04000FDB RID: 4059
	public float colliderClimbingHeight;

	// Token: 0x04000FDC RID: 4060
	[HideInInspector]
	public Vector3 climbingDirection;

	// Token: 0x04000FDD RID: 4061
	public Vector3 climbingContact;

	// Token: 0x04000FDE RID: 4062
	public float climbPushThreshold;

	// Token: 0x04000FDF RID: 4063
	private float climbPushing;

	// Token: 0x04000FE0 RID: 4064
	public float climbJumpStaminaCost = 0.5f;

	// Token: 0x04000FE1 RID: 4065
	[Header("Jumps")]
	public float jumpHeight = 2f;

	// Token: 0x04000FE2 RID: 4066
	public float jumpUnheldFactor = 0.5f;

	// Token: 0x04000FE3 RID: 4067
	public int maxAirJumps;

	// Token: 0x04000FE4 RID: 4068
	[HideInInspector]
	public bool lockJumpHeldForNow;

	// Token: 0x04000FE5 RID: 4069
	[Header("Gliding")]
	public bool glidingUnlocked = true;

	// Token: 0x04000FE6 RID: 4070
	public float glidingMinFallSpeed = -1f;

	// Token: 0x04000FE7 RID: 4071
	public float glidingSpeed = 1f;

	// Token: 0x04000FE8 RID: 4072
	public float glidingAccel = 0.5f;

	// Token: 0x04000FE9 RID: 4073
	public float glidingDeccel = 0.5f;

	// Token: 0x04000FEA RID: 4074
	public float glidingRotationSpeed = 90f;

	// Token: 0x04000FEB RID: 4075
	public float glidingVerticalMinSpeed = 0.5f;

	// Token: 0x04000FEC RID: 4076
	public float glidingVerticalSpeed = 0.5f;

	// Token: 0x04000FED RID: 4077
	public float glidingVerticalAccel = 5f;

	// Token: 0x04000FEE RID: 4078
	public bool isGliding;

	// Token: 0x04000FEF RID: 4079
	private bool wasGliding;

	// Token: 0x04000FF0 RID: 4080
	public GameObject glider;

	// Token: 0x04000FF1 RID: 4081
	public float glidingCooldown = 0.5f;

	// Token: 0x04000FF2 RID: 4082
	[Header("Sledding")]
	[HideInInspector]
	public ItemShield shield;

	// Token: 0x04000FF3 RID: 4083
	public bool sleddingUnlocked = true;

	// Token: 0x04000FF4 RID: 4084
	public bool isSledding;

	// Token: 0x04000FF5 RID: 4085
	public Vector3 sledAirBoost;

	// Token: 0x04000FF6 RID: 4086
	public Vector3 sledStandingBoost;

	// Token: 0x04000FF7 RID: 4087
	public Collider sledCollider;

	// Token: 0x04000FF8 RID: 4088
	public float sledBounceThreshold = 0.2f;

	// Token: 0x04000FF9 RID: 4089
	public float sledMaxSpeed = 15f;

	// Token: 0x04000FFA RID: 4090
	public float sledMaxAccelSpeed = 10f;

	// Token: 0x04000FFB RID: 4091
	public float sledAccel = 2f;

	// Token: 0x04000FFC RID: 4092
	public float sledRotation = 20f;

	// Token: 0x04000FFD RID: 4093
	public float sledAirAccel = 1f;

	// Token: 0x04000FFE RID: 4094
	public float sledSlopeAccel = 5f;

	// Token: 0x04000FFF RID: 4095
	public float sledJumpHeight;

	// Token: 0x04001000 RID: 4096
	public float sledJumpDirectionInfluence = 0.5f;

	// Token: 0x04001001 RID: 4097
	[HideInInspector]
	public Vector3 animGroundNormal;

	// Token: 0x04001002 RID: 4098
	public float sledCooldown = 0.7f;

	// Token: 0x04001003 RID: 4099
	public GameObject sledHitTrigger;

	// Token: 0x04001004 RID: 4100
	public float sledWaterFriction = 0.5f;

	// Token: 0x04001005 RID: 4101
	public float sledBouyancyMinSpeed = 1f;

	// Token: 0x04001006 RID: 4102
	public float sledBouyancyMaxSpeed = 3f;

	// Token: 0x04001007 RID: 4103
	public bool sledAlwaysFloats;

	// Token: 0x04001008 RID: 4104
	[Header("Swimming")]
	public LayerMask swimmingLayer;

	// Token: 0x04001009 RID: 4105
	public float swimmingSpeed = 2f;

	// Token: 0x0400100A RID: 4106
	public float swimmingAcc = 5f;

	// Token: 0x0400100B RID: 4107
	public float swimmingDamp = 10f;

	// Token: 0x0400100C RID: 4108
	[HideInInspector]
	public Water water;

	// Token: 0x0400100D RID: 4109
	[HideInInspector]
	public float waterHeight;

	// Token: 0x0400100E RID: 4110
	public float depthSubmerged = 0.25f;

	// Token: 0x0400100F RID: 4111
	public float sledDepthSubmerged = 0.05f;

	// Token: 0x04001010 RID: 4112
	public float buoyancy = 0.25f;

	// Token: 0x04001011 RID: 4113
	public float buoyantSpeed = 1f;

	// Token: 0x04001012 RID: 4114
	public float buoyantDepth = 1f;

	// Token: 0x04001013 RID: 4115
	public float idleRippleSpeed = 2f;

	// Token: 0x04001014 RID: 4116
	public float movingRippleSpeed = 5f;

	// Token: 0x04001015 RID: 4117
	private float rippleCounter;

	// Token: 0x04001016 RID: 4118
	[Header("Modifiers")]
	public bool isModified;

	// Token: 0x04001017 RID: 4119
	public PlayerMovement.ModRule modPrimaryRule;

	// Token: 0x04001018 RID: 4120
	public PlayerMovement.ModRule modSecondaryRule;

	// Token: 0x04001019 RID: 4121
	public PlayerMovement.ModRule modItemRule;

	// Token: 0x0400101A RID: 4122
	public PlayerMovement.ModRule modJumpRule;

	// Token: 0x0400101B RID: 4123
	public PlayerMovement.ModRule modGlideRule;

	// Token: 0x0400101C RID: 4124
	public bool modNoClimbing;

	// Token: 0x0400101D RID: 4125
	public bool modNoMovement;

	// Token: 0x0400101E RID: 4126
	public bool modForceMovement;

	// Token: 0x0400101F RID: 4127
	public bool moddedWithoutControl;

	// Token: 0x04001020 RID: 4128
	public bool modDisableCollision;

	// Token: 0x04001021 RID: 4129
	public bool modCustomMovement;

	// Token: 0x04001022 RID: 4130
	public bool modLockAnimatorSpeed;

	// Token: 0x04001023 RID: 4131
	public bool modIgnoreReady;

	// Token: 0x04001024 RID: 4132
	public bool modNoInteractions;

	// Token: 0x04001025 RID: 4133
	public bool modNoStaminaRecovery;

	// Token: 0x04001026 RID: 4134
	public ICustomPlayerMovement modCustomMovementScript;

	// Token: 0x04001027 RID: 4135
	[ReadOnly]
	public Vector3 moddedForward;

	// Token: 0x04001028 RID: 4136
	[ReadOnly]
	public Vector3 moddedUp;

	// Token: 0x04001029 RID: 4137
	private float modCustomMovementAnimationIndex = -1f;

	// Token: 0x0400102A RID: 4138
	private int[] contextualStates;

	// Token: 0x0400102B RID: 4139
	private static readonly int ContextualBlendID = Animator.StringToHash("ContextualBlend");

	// Token: 0x0400102C RID: 4140
	private const int contextualStateCount = 6;

	// Token: 0x0400102D RID: 4141
	public float modSpeed;

	// Token: 0x0400102E RID: 4142
	public float modJumpHeight;

	// Token: 0x0400102F RID: 4143
	public bool isRagdolling;

	// Token: 0x04001030 RID: 4144
	public RagdollController ragdollController;

	// Token: 0x04001031 RID: 4145
	public bool modIgnoreGroundedness;

	// Token: 0x04001032 RID: 4146
	[Header("Technical grounded stuff")]
	public float groundNormalThreshold = 0.75f;

	// Token: 0x04001033 RID: 4147
	public float probeLength = 0.5f;

	// Token: 0x04001034 RID: 4148
	public LayerMask probeMask;

	// Token: 0x04001035 RID: 4149
	[ReadOnly]
	public int stepsSinceLastGrounded;

	// Token: 0x04001036 RID: 4150
	[ReadOnly]
	public int stepsSinceLastClimbing;

	// Token: 0x04001037 RID: 4151
	[ReadOnly]
	public int stepsSinceLastJump;

	// Token: 0x04001038 RID: 4152
	[ReadOnly]
	public int stepsSinceInWater;

	// Token: 0x04001039 RID: 4153
	[ReadOnly]
	public int stepsSinceLastCancel;

	// Token: 0x0400103A RID: 4154
	[ReadOnly]
	public int stepsSinceRagdolling;

	// Token: 0x0400103B RID: 4155
	private int jumpPhase;

	// Token: 0x0400103C RID: 4156
	private Vector3 jumpVelocity;

	// Token: 0x0400103D RID: 4157
	public float recoveringControl = 1f;

	// Token: 0x0400103E RID: 4158
	private Vector3 contactNormal;

	// Token: 0x0400103F RID: 4159
	private Vector3 steepNormal;

	// Token: 0x04001040 RID: 4160
	private Vector3 wallNormal;

	// Token: 0x04001041 RID: 4161
	private Vector3 avgContactNormal;

	// Token: 0x04001042 RID: 4162
	private Vector3 avgSteepNormal;

	// Token: 0x04001043 RID: 4163
	private Vector3 avgWallNormal;

	// Token: 0x04001044 RID: 4164
	private int groundContactCount;

	// Token: 0x04001045 RID: 4165
	private int steepContactCount;

	// Token: 0x04001046 RID: 4166
	private int wallContactCount;

	// Token: 0x04001047 RID: 4167
	private int stuckContactCount;

	// Token: 0x04001048 RID: 4168
	private Collider stuckCollider;

	// Token: 0x04001049 RID: 4169
	private Vector3 wallContact;

	// Token: 0x0400104A RID: 4170
	private int stuckContactFrames;

	// Token: 0x0400104B RID: 4171
	public float isClimbingSmooth;

	// Token: 0x0400104C RID: 4172
	private float unsettledTime = -1f;

	// Token: 0x0400104D RID: 4173
	public float bonusHeight;

	// Token: 0x0400104E RID: 4174
	private float transitionCooldown;

	// Token: 0x0400104F RID: 4175
	[Header("Tutorials")]
	public ButtonTutorial jumpTutorial;

	// Token: 0x04001050 RID: 4176
	public ButtonTutorial glideTutorial;

	// Token: 0x04001051 RID: 4177
	public ButtonTutorial climbTutorial;

	// Token: 0x04001052 RID: 4178
	private float clampingSpeedUntil = -10f;

	// Token: 0x04001053 RID: 4179
	private bool isJumpingFromRagdoll;

	// Token: 0x04001054 RID: 4180
	private float hasContextualAnimations;

	// Token: 0x04001055 RID: 4181
	private int contextualAnimationsLayer = -1;

	// Token: 0x04001056 RID: 4182
	private float baseLayerWeight = 1f;

	// Token: 0x0200027C RID: 636
	public enum ModRule
	{
		// Token: 0x04001058 RID: 4184
		Allowed,
		// Token: 0x04001059 RID: 4185
		Locked,
		// Token: 0x0400105A RID: 4186
		Cancels
	}
}
