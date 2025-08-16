using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	// (get) Token: 0x06000C83 RID: 3203 RVA: 0x0000BA01 File Offset: 0x00009C01
	private Vector3 position
	{
		get
		{
			return base.transform.position + Vector3.up * 0.5f;
		}
	}

	// (get) Token: 0x06000C84 RID: 3204 RVA: 0x0000BA22 File Offset: 0x00009C22
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

	// (get) Token: 0x06000C85 RID: 3205 RVA: 0x0000BA39 File Offset: 0x00009C39
	// (set) Token: 0x06000C86 RID: 3206 RVA: 0x000448F0 File Offset: 0x00042AF0
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

	// (get) Token: 0x06000C87 RID: 3207 RVA: 0x0004495C File Offset: 0x00042B5C
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

	// (get) Token: 0x06000C88 RID: 3208 RVA: 0x0000BA4E File Offset: 0x00009C4E
	private bool CanUseAnything
	{
		get
		{
			return this.transitionCooldown <= 0f || !base.enabled;
		}
	}

	// (get) Token: 0x06000C89 RID: 3209 RVA: 0x0000BA68 File Offset: 0x00009C68
	public bool CanUsePrimary
	{
		get
		{
			return this.modPrimaryRule != PlayerMovement.ModRule.Locked && this.CanUseAnything;
		}
	}

	// (get) Token: 0x06000C8A RID: 3210 RVA: 0x0000BA7B File Offset: 0x00009C7B
	public bool CanUseSecondary
	{
		get
		{
			return this.modSecondaryRule != PlayerMovement.ModRule.Locked && this.CanUseAnything;
		}
	}

	// (get) Token: 0x06000C8B RID: 3211 RVA: 0x0000BA8E File Offset: 0x00009C8E
	public bool CanUseItem
	{
		get
		{
			return this.modItemRule != PlayerMovement.ModRule.Locked && this.CanUseAnything;
		}
	}

	// (get) Token: 0x06000C8C RID: 3212 RVA: 0x0000BAA1 File Offset: 0x00009CA1
	public bool HasGroundContact
	{
		get
		{
			return this.groundContactCount > 0 || this.avgContactNormal != Vector3.zero;
		}
	}

	// (get) Token: 0x06000C8D RID: 3213 RVA: 0x0000BABE File Offset: 0x00009CBE
	private bool HasSteepContact
	{
		get
		{
			return this.steepContactCount > 0 || this.avgSteepNormal != Vector3.zero;
		}
	}

	// (get) Token: 0x06000C8E RID: 3214 RVA: 0x0000BADB File Offset: 0x00009CDB
	private bool HasWallContact
	{
		get
		{
			return this.maxStamina > 0f && (this.wallContactCount > 0 || this.avgWallNormal != Vector3.zero);
		}
	}

	// (get) Token: 0x06000C8F RID: 3215 RVA: 0x0000BB07 File Offset: 0x00009D07
	private bool HasStuckContact
	{
		get
		{
			return this.stuckCollider != null;
		}
	}

	// (get) Token: 0x06000C90 RID: 3216 RVA: 0x0000BB15 File Offset: 0x00009D15
	public bool IsInWater
	{
		get
		{
			return this.stepsSinceInWater < 5;
		}
	}

	// (get) Token: 0x06000C91 RID: 3217 RVA: 0x000449BC File Offset: 0x00042BBC
	public bool IsSubmerged
	{
		get
		{
			return this.IsInWater && base.transform.position.y + this.DepthSubmerged < (this.IsClimbing ? (this.waterHeight - 0.5f) : (this.waterHeight + 0.1f));
		}
	}

	// (get) Token: 0x06000C92 RID: 3218 RVA: 0x0000BB20 File Offset: 0x00009D20
	public bool IsSwimming
	{
		get
		{
			return this.IsSubmerged && !this.isSledding;
		}
	}

	// (get) Token: 0x06000C93 RID: 3219 RVA: 0x0000BB35 File Offset: 0x00009D35
	public bool InAir
	{
		get
		{
			return this.stepsSinceLastGrounded > 2 && this.stepsSinceLastClimbing > 2 && !this.IsInWater;
		}
	}

	// (get) Token: 0x06000C94 RID: 3220 RVA: 0x0000BB54 File Offset: 0x00009D54
	public bool IsClimbing
	{
		get
		{
			return this.maxStamina > 0f && this.stepsSinceLastClimbing < 2;
		}
	}

	// (get) Token: 0x06000C95 RID: 3221 RVA: 0x0000BB6E File Offset: 0x00009D6E
	public bool JustJumped
	{
		get
		{
			return this.stepsSinceLastJump < 10;
		}
	}

	// (get) Token: 0x06000C96 RID: 3222 RVA: 0x0000BB7A File Offset: 0x00009D7A
	public bool JustCanceled
	{
		get
		{
			return this.stepsSinceLastCancel < 15;
		}
	}

	// (get) Token: 0x06000C97 RID: 3223 RVA: 0x0000BB86 File Offset: 0x00009D86
	private bool IsSettled
	{
		get
		{
			return Time.time - this.unsettledTime > 0.5f;
		}
	}

	// (get) Token: 0x06000C98 RID: 3224 RVA: 0x0000BB9B File Offset: 0x00009D9B
	public bool IsGrounded
	{
		get
		{
			return base.enabled && this.stepsSinceLastGrounded < 2 && !this.isSledding && this.stepsSinceLastJump > 5 && !this.IsClimbing;
		}
	}

	// (get) Token: 0x06000C99 RID: 3225 RVA: 0x0000BBCA File Offset: 0x00009DCA
	public bool IsMoving
	{
		get
		{
			return !Mathf.Approximately(this.velocity.x, 0f) || !Mathf.Approximately(this.velocity.z, 0f);
		}
	}

	// Token: 0x06000C9A RID: 3226 RVA: 0x00044A10 File Offset: 0x00042C10
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

	// Token: 0x06000C9B RID: 3227 RVA: 0x0000BBFD File Offset: 0x00009DFD
	internal void ClampSpeedForABit()
	{
		this.clampingSpeedUntil = Time.time + 0.25f;
	}

	// Token: 0x06000C9C RID: 3228 RVA: 0x00044AFC File Offset: 0x00042CFC
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

	// Token: 0x06000C9D RID: 3229 RVA: 0x00044C10 File Offset: 0x00042E10
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

	// Token: 0x06000C9E RID: 3230 RVA: 0x00044CA4 File Offset: 0x00042EA4
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

	// Token: 0x06000C9F RID: 3231 RVA: 0x00044D2C File Offset: 0x00042F2C
	private void ClearState()
	{
		this.groundContactCount = (this.steepContactCount = (this.wallContactCount = (this.stuckContactCount = 0)));
		this.contactNormal = (this.steepNormal = (this.wallNormal = Vector3.zero));
		this.wallContact = Vector3.zero;
		this.stuckCollider = null;
	}

	// Token: 0x06000CA0 RID: 3232 RVA: 0x00044D8C File Offset: 0x00042F8C
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

	// Token: 0x06000CA1 RID: 3233 RVA: 0x00044E90 File Offset: 0x00043090
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

	// Token: 0x06000CA2 RID: 3234 RVA: 0x00044EDC File Offset: 0x000430DC
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
				if (Physics.ComputePenetration(this.capsuleCollider, base.transform.position, base.transform.rotation, this.stuckCollider, this.stuckCollider.transform.position, this.stuckCollider.transform.rotation, out vector, out num) && num > 0.25f && Physics.Raycast(this.rigidbody.position + 10f * Vector3.up, Vector3.down, out raycastHit, 20f, this.probeMask))
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

	// Token: 0x06000CA3 RID: 3235 RVA: 0x0000BC10 File Offset: 0x00009E10
	public void ForceModdedState()
	{
		this.ClearMods();
		this.isSledding = false;
		this.isGliding = false;
		this.stepsSinceLastClimbing = 100;
	}

	// Token: 0x06000CA4 RID: 3236 RVA: 0x000455AC File Offset: 0x000437AC
	private bool SnapToGround()
	{
		if (this.stepsSinceLastGrounded > 1 || this.stepsSinceLastJump <= 2 || this.IsSubmerged)
		{
			return false;
		}
		float magnitude = this.velocity.magnitude;
		RaycastHit raycastHit;
		if (!Physics.SphereCast(base.transform.TransformPoint(Vector3.up), 0.15f, Vector3.down, out raycastHit, this.probeLength, this.probeMask))
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

	// Token: 0x06000CA5 RID: 3237 RVA: 0x000456F0 File Offset: 0x000438F0
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
			if (Physics.SphereCast(vector4, 0.25f, normalized2, out raycastHit, 1.5f, this.climbableLayers))
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

	// Token: 0x06000CA6 RID: 3238 RVA: 0x00045960 File Offset: 0x00043B60
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

	// Token: 0x06000CA7 RID: 3239 RVA: 0x000459B4 File Offset: 0x00043BB4
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

	// Token: 0x06000CA8 RID: 3240 RVA: 0x00045AF4 File Offset: 0x00043CF4
	private void UpdateVelocity()
	{
		Vector3 vector = this.velocity;
		Vector3 vector2 = this.desiredDirection;
		if (Time.time < this.clampingSpeedUntil)
		{
			Vector2 vector3 = new Vector2(this.velocity.x, this.velocity.z);
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
		Vector2 vector7 = new Vector2(num8, num9);
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
			Vector2 vector9 = new Vector2(num10, num11);
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

	// Token: 0x06000CA9 RID: 3241 RVA: 0x00046378 File Offset: 0x00044578
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

	// Token: 0x06000CAA RID: 3242 RVA: 0x0000BC2E File Offset: 0x00009E2E
	public void TryJump()
	{
		if (this.modJumpRule != PlayerMovement.ModRule.Locked)
		{
			this.Jump(false);
		}
	}

	// Token: 0x06000CAB RID: 3243 RVA: 0x00046548 File Offset: 0x00044748
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
		if (!this.isGliding && !this.IsClimbing && this.stepsSinceLastGrounded >= 10 && Physics.SphereCast(base.transform.TransformPoint(Vector3.up), 0.15f, Vector3.down, out raycastHit, 1.35f, this.probeMask) && raycastHit.normal.y > 0.7f)
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

	// Token: 0x06000CAC RID: 3244 RVA: 0x0000BC40 File Offset: 0x00009E40
	public void ResetGrounded()
	{
		this.stepsSinceLastGrounded = 1;
		this.stepsSinceLastJump = 0;
	}

	// Token: 0x06000CAD RID: 3245 RVA: 0x00046994 File Offset: 0x00044B94
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

	// Token: 0x06000CAE RID: 3246 RVA: 0x0000BC50 File Offset: 0x00009E50
	public void Ragdoll(float forwardSpeed)
	{
		this.Ragdoll(this.animator.transform.forward * forwardSpeed);
	}

	// Token: 0x06000CAF RID: 3247 RVA: 0x0000BC6E File Offset: 0x00009E6E
	public void Ragdoll()
	{
		this.Ragdoll(Vector3.zero);
	}

	// Token: 0x06000CB0 RID: 3248 RVA: 0x00046AD0 File Offset: 0x00044CD0
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

	// Token: 0x06000CB1 RID: 3249 RVA: 0x00046B4C File Offset: 0x00044D4C
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
				Vector3 vector = new Vector3(this.velocity.x, 0f, this.velocity.z);
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

	// Token: 0x06000CB2 RID: 3250 RVA: 0x00046E48 File Offset: 0x00045048
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

	// Token: 0x06000CB3 RID: 3251 RVA: 0x0000BC7B File Offset: 0x00009E7B
	private void OnCollisionEnter(Collision collision)
	{
		this.EvaluateCollision(collision);
	}

	// Token: 0x06000CB4 RID: 3252 RVA: 0x0000BC7B File Offset: 0x00009E7B
	private void OnCollisionStay(Collision collision)
	{
		this.EvaluateCollision(collision);
	}

	// Token: 0x06000CB5 RID: 3253 RVA: 0x000471C8 File Offset: 0x000453C8
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

	// Token: 0x06000CB6 RID: 3254 RVA: 0x00047458 File Offset: 0x00045658
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
		if (!this.IsSubmerged && Physics.Raycast(position, Vector3.down, out raycastHit, 0.1f + position.y - waterPlaneHeight) && raycastHit.collider != waterCollider)
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

	// Token: 0x06000CB7 RID: 3255 RVA: 0x0000BC84 File Offset: 0x00009E84
	private Vector3 ProjectOnContactPlane(Vector3 vector)
	{
		return vector - this.contactNormal * Vector3.Dot(vector, this.contactNormal);
	}

	// Token: 0x06000CB8 RID: 3256 RVA: 0x0000BCA3 File Offset: 0x00009EA3
	private Vector3 ProjectOnWallPlane(Vector3 vector)
	{
		return this.ProjectOnContactPlane(vector) + Vector3.down * Vector3.Dot(vector, this.wallNormal);
	}

	// Token: 0x06000CB9 RID: 3257 RVA: 0x0000BCC7 File Offset: 0x00009EC7
	public void ApplyTransform(Transform newTransform)
	{
		this.SetPosition(newTransform.position);
		this.SetRotation(newTransform.rotation);
	}

	// Token: 0x06000CBA RID: 3258 RVA: 0x00047578 File Offset: 0x00045778
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

	// Token: 0x06000CBB RID: 3259 RVA: 0x0000BCE1 File Offset: 0x00009EE1
	public void SetRotation(Quaternion newRotation)
	{
		this.animator.transform.rotation = newRotation;
		this.reactionController.SetForward(newRotation * Vector3.forward);
	}

	public static float mostRecentEnable;

	public const float maxHeight = 150f;

	private static bool isBabyPlayer;

	public static bool didShieldSkip = false;

	public Animator animator;

	private RuntimeAnimatorController animatorController;

	public CharacterReactionController reactionController;

	public PlayerEffects effects;

	public bool isBaby;

	public Rigidbody rigidbody;

	public Vector3 velocity = Vector3.zero;

	private Vector3 lastVelocity;

	public float maxSpeed = 40f;

	[HideInInspector]
	public Vector3 desiredDirection;

	[HideInInspector]
	public Vector3 desiredDirectionRaw;

	[HideInInspector]
	public bool desiredJump;

	[HideInInspector]
	public bool holdingJump;

	[HideInInspector]
	public bool cancelAction;

	[HideInInspector]
	public bool specialAction;

	[Space]
	public bool persistentTransform = true;

	[ConditionalHide("persistentTransform", true)]
	public string positionKey = "PlayerPosition";

	[ConditionalHide("persistentTransform", true)]
	public string rotationKey = "PlayerRotation";

	[Header("Movement")]
	public float maxAcceleration = 5f;

	public float maxAirAcceleration = 2f;

	[ReadOnly]
	public float animatorSpeed;

	[ReadOnly]
	public float animatorVerticalSpeed;

	private float animatorAngle = 1f;

	private float animatorAngleVelocity;

	public float speed = 5.5f;

	public float slopeFactor = 0.4f;

	public bool overrideSpeed;

	public float overriddenSpeed = 5.5f;

	[Header("Stamina")]
	private float stamina;

	public float maxStamina;

	private float staminaVelocity;

	public float staminaSpeed = 1f;

	public float staminaAccel = 5f;

	[Header("Climbing")]
	public float climbingSpeed = 1f;

	public float climbingSlideSpeed = 3f;

	public LayerMask climbableLayers;

	public float staminaDrain = 1f;

	public CapsuleCollider capsuleCollider;

	public float colliderStandingHeight;

	public float colliderClimbingHeight;

	[HideInInspector]
	public Vector3 climbingDirection;

	public Vector3 climbingContact;

	public float climbPushThreshold;

	private float climbPushing;

	public float climbJumpStaminaCost = 0.5f;

	[Header("Jumps")]
	public float jumpHeight = 2f;

	public float jumpUnheldFactor = 0.5f;

	public int maxAirJumps;

	[HideInInspector]
	public bool lockJumpHeldForNow;

	[Header("Gliding")]
	public bool glidingUnlocked = true;

	public float glidingMinFallSpeed = -1f;

	public float glidingSpeed = 1f;

	public float glidingAccel = 0.5f;

	public float glidingDeccel = 0.5f;

	public float glidingRotationSpeed = 90f;

	public float glidingVerticalMinSpeed = 0.5f;

	public float glidingVerticalSpeed = 0.5f;

	public float glidingVerticalAccel = 5f;

	public bool isGliding;

	private bool wasGliding;

	public GameObject glider;

	public float glidingCooldown = 0.5f;

	[Header("Sledding")]
	[HideInInspector]
	public ItemShield shield;

	public bool sleddingUnlocked = true;

	public bool isSledding;

	public Vector3 sledAirBoost;

	public Vector3 sledStandingBoost;

	public Collider sledCollider;

	public float sledBounceThreshold = 0.2f;

	public float sledMaxSpeed = 15f;

	public float sledMaxAccelSpeed = 10f;

	public float sledAccel = 2f;

	public float sledRotation = 20f;

	public float sledAirAccel = 1f;

	public float sledSlopeAccel = 5f;

	public float sledJumpHeight;

	public float sledJumpDirectionInfluence = 0.5f;

	[HideInInspector]
	public Vector3 animGroundNormal;

	public float sledCooldown = 0.7f;

	public GameObject sledHitTrigger;

	public float sledWaterFriction = 0.5f;

	public float sledBouyancyMinSpeed = 1f;

	public float sledBouyancyMaxSpeed = 3f;

	public bool sledAlwaysFloats;

	[Header("Swimming")]
	public LayerMask swimmingLayer;

	public float swimmingSpeed = 2f;

	public float swimmingAcc = 5f;

	public float swimmingDamp = 10f;

	[HideInInspector]
	public Water water;

	[HideInInspector]
	public float waterHeight;

	public float depthSubmerged = 0.25f;

	public float sledDepthSubmerged = 0.05f;

	public float buoyancy = 0.25f;

	public float buoyantSpeed = 1f;

	public float buoyantDepth = 1f;

	public float idleRippleSpeed = 2f;

	public float movingRippleSpeed = 5f;

	private float rippleCounter;

	[Header("Modifiers")]
	public bool isModified;

	public PlayerMovement.ModRule modPrimaryRule;

	public PlayerMovement.ModRule modSecondaryRule;

	public PlayerMovement.ModRule modItemRule;

	public PlayerMovement.ModRule modJumpRule;

	public PlayerMovement.ModRule modGlideRule;

	public bool modNoClimbing;

	public bool modNoMovement;

	public bool modForceMovement;

	public bool moddedWithoutControl;

	public bool modDisableCollision;

	public bool modCustomMovement;

	public bool modLockAnimatorSpeed;

	public bool modIgnoreReady;

	public bool modNoInteractions;

	public bool modNoStaminaRecovery;

	public ICustomPlayerMovement modCustomMovementScript;

	[ReadOnly]
	public Vector3 moddedForward;

	[ReadOnly]
	public Vector3 moddedUp;

	private float modCustomMovementAnimationIndex = -1f;

	private int[] contextualStates;

	private static readonly int ContextualBlendID = Animator.StringToHash("ContextualBlend");

	private const int contextualStateCount = 6;

	public float modSpeed;

	public float modJumpHeight;

	public bool isRagdolling;

	public RagdollController ragdollController;

	public bool modIgnoreGroundedness;

	[Header("Technical grounded stuff")]
	public float groundNormalThreshold = 0.75f;

	public float probeLength = 0.5f;

	public LayerMask probeMask;

	[ReadOnly]
	public int stepsSinceLastGrounded;

	[ReadOnly]
	public int stepsSinceLastClimbing;

	[ReadOnly]
	public int stepsSinceLastJump;

	[ReadOnly]
	public int stepsSinceInWater;

	[ReadOnly]
	public int stepsSinceLastCancel;

	[ReadOnly]
	public int stepsSinceRagdolling;

	private int jumpPhase;

	private Vector3 jumpVelocity;

	public float recoveringControl = 1f;

	private Vector3 contactNormal;

	private Vector3 steepNormal;

	private Vector3 wallNormal;

	private Vector3 avgContactNormal;

	private Vector3 avgSteepNormal;

	private Vector3 avgWallNormal;

	private int groundContactCount;

	private int steepContactCount;

	private int wallContactCount;

	private int stuckContactCount;

	private Collider stuckCollider;

	private Vector3 wallContact;

	private int stuckContactFrames;

	public float isClimbingSmooth;

	private float unsettledTime = -1f;

	public float bonusHeight;

	private float transitionCooldown;

	[Header("Tutorials")]
	public ButtonTutorial jumpTutorial;

	public ButtonTutorial glideTutorial;

	public ButtonTutorial climbTutorial;

	private float clampingSpeedUntil = -10f;

	private bool isJumpingFromRagdoll;

	private float hasContextualAnimations;

	private int contextualAnimationsLayer = -1;

	private float baseLayerWeight = 1f;

	public enum ModRule
	{
		Allowed,
		Locked,
		Cancels
	}
}
