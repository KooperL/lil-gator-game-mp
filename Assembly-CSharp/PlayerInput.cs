using System;
using Rewired;
using UnityEngine;

// Token: 0x020001E5 RID: 485
public class PlayerInput : MonoBehaviour
{
	// Token: 0x17000087 RID: 135
	// (get) Token: 0x06000A36 RID: 2614 RVA: 0x0002FDBD File Offset: 0x0002DFBD
	public static Vector2 RawInput
	{
		get
		{
			return new Vector2(PlayerInput.p.inputDirectionRaw.x, PlayerInput.p.inputDirectionRaw.z);
		}
	}

	// Token: 0x06000A37 RID: 2615 RVA: 0x0002FDE4 File Offset: 0x0002DFE4
	private void OnEnable()
	{
		PlayerInput.p = this;
		this.movement = base.GetComponent<PlayerMovement>();
		this.itemManager = base.GetComponent<PlayerItemManager>();
		if (this.rePlayer == null)
		{
			this.rePlayer = ReInput.players.GetPlayer(0);
		}
		this.moveHorizontal = ReInput.mapping.GetActionId("Move Horizontal");
		this.moveVertical = ReInput.mapping.GetActionId("Move Vertical");
		this.jump = ReInput.mapping.GetActionId("Jump");
		this.interact = ReInput.mapping.GetActionId("Interact");
		this.lookHorizontal = ReInput.mapping.GetActionId("Look Horizontal");
		this.lookVertical = ReInput.mapping.GetActionId("Look Vertical");
		this.primary = ReInput.mapping.GetActionId("Primary");
		this.secondary = ReInput.mapping.GetActionId("Secondary");
		this.useItem = ReInput.mapping.GetActionId("UseItem");
		this.useItemR = ReInput.mapping.GetActionId("UseItemR");
		this.reCenterCamera = ReInput.mapping.GetActionId("ReCenterCamera");
	}

	// Token: 0x06000A38 RID: 2616 RVA: 0x0002FF0F File Offset: 0x0002E10F
	private void OnDisable()
	{
	}

	// Token: 0x06000A39 RID: 2617 RVA: 0x0002FF14 File Offset: 0x0002E114
	private void Update()
	{
		if (!Game.HasControl)
		{
			this.inputDirectionRaw = Vector3.zero;
		}
		if (this.rePlayer != null)
		{
			if (Game.HasControl)
			{
				bool flag = PlayerInput.shouldReCenterCamera;
				if (Settings.reCenterCameraIsToggle)
				{
					if (this.rePlayer.GetButtonDown(this.reCenterCamera))
					{
						PlayerInput.shouldReCenterCamera = !PlayerInput.shouldReCenterCamera;
						if (!PlayerInput.shouldReCenterCamera)
						{
							PlayerOrbitCamera.active.CancelReCenterCamera();
						}
					}
				}
				else
				{
					PlayerInput.shouldReCenterCamera = this.rePlayer.GetButton(this.reCenterCamera);
				}
				if (PlayerInput.shouldReCenterCamera)
				{
					PlayerOrbitCamera.active.ReCenterCamera(!flag);
				}
				if (PlayerInput.autoSword && this.holdPrimaryCounterTotal > 0.001f)
				{
					this.primaryMapping = 0;
				}
				else if (PlayerInput.interactMapping == PlayerInput.InteractMapping.Primary && this.playerInteract.InteractionAvailable)
				{
					this.primaryMapping = 1;
				}
				else if (PlayerInput.secondaryMapping == PlayerInput.SecondaryMapping.Primary && !global::Player.movement.IsGrounded)
				{
					this.primaryMapping = 2;
				}
				else
				{
					this.primaryMapping = 0;
				}
				if (!global::Player.movement.IsGrounded)
				{
					this.jumpMapping = 0;
				}
				else if (PlayerInput.interactMapping == PlayerInput.InteractMapping.Jump && this.playerInteract.InteractionAvailable)
				{
					this.jumpMapping = 1;
				}
				else
				{
					this.jumpMapping = 0;
				}
				this.inputDirectionRaw = new Vector3(this.rePlayer.GetAxisRaw(this.moveHorizontal), 0f, this.rePlayer.GetAxisRaw(this.moveVertical));
				float magnitude = this.inputDirectionRaw.magnitude;
				if (magnitude > 0.2f)
				{
					float num;
					if (magnitude >= 0.9f)
					{
						num = 1f;
					}
					else
					{
						num = Mathf.Lerp(0.6f, 1f, Mathf.InverseLerp(0.2f, 0.9f, magnitude));
					}
					this.inputDirectionRaw *= num / magnitude;
				}
				else
				{
					this.inputDirectionRaw = Vector3.zero;
				}
				if (this.rePlayer.GetButtonDown(this.jump) && this.jumpMapping == 0)
				{
					if (this.movement.isRagdolling)
					{
						this.movement.ragdollController.Jump();
					}
					else
					{
						this.movement.TryJump();
					}
				}
				this.movement.holdingJump = this.rePlayer.GetButton(this.jump);
				if (ItemCamera.isActive)
				{
					if (this.rePlayer.GetButtonDown(this.interact))
					{
						ItemCamera.instance.InputHideUI();
					}
					if (this.rePlayer.GetButtonDown(ItemCamera.instance.isOnRight ? this.useItemR : this.useItem))
					{
						ItemCamera.instance.InputCancel();
					}
					if (ItemCamera.instance.cameraMode == ItemCamera.CameraMode.Static)
					{
						this.HandlePrimary();
						this.HandleSecondary();
						if (ItemCamera.instance.isOnRight)
						{
							this.HandleItem();
						}
						else
						{
							this.HandleItem_R();
						}
					}
					else
					{
						if (this.rePlayer.GetButtonDown(this.primary))
						{
							ItemCamera.instance.InputSwitch();
						}
						if (this.rePlayer.GetButtonDown(this.secondary))
						{
							ItemCamera.instance.InputLock();
						}
					}
				}
				else
				{
					bool flag2 = this.rePlayer.GetButtonDown(this.interact);
					if (PlayerInput.interactMapping == PlayerInput.InteractMapping.Primary && this.primaryMapping == 1)
					{
						flag2 |= this.rePlayer.GetButtonDown(this.primary);
					}
					if (PlayerInput.interactMapping == PlayerInput.InteractMapping.Jump && this.jumpMapping == 1)
					{
						flag2 |= this.rePlayer.GetButtonDown(this.jump);
					}
					if (this.playerInteract.InteractionAvailable && flag2)
					{
						this.playerInteract.Interact();
					}
					this.HandlePrimary();
					this.HandleSecondary();
					this.HandleItem();
					this.HandleItem_R();
				}
			}
			else
			{
				this.jumpDesired = this.rePlayer.GetButtonDown(this.jump);
			}
			bool flag3 = true;
			Controller lastActiveController = this.rePlayer.controllers.GetLastActiveController();
			if (lastActiveController != null)
			{
				flag3 = lastActiveController.type != ControllerType.Joystick;
			}
			if ((UpdateCursor.isCurrentlyLocked || !flag3) && Game.State != GameState.Menu)
			{
				this.lookAxisRaw = this.rePlayer.GetAxis2DRaw(this.lookHorizontal, this.lookVertical);
				if (this.rePlayer.GetAxisCoordinateMode(this.lookHorizontal) == AxisCoordinateMode.Absolute)
				{
					this.lookAxisRaw *= 120f * Time.unscaledDeltaTime;
				}
			}
			else
			{
				this.lookAxisRaw = Vector2.zero;
			}
		}
		if (PlayerInput.useMovementToAim && global::Player.itemManager.IsAiming)
		{
			if (this.rePlayer.GetAxisCoordinateMode(this.moveHorizontal) == AxisCoordinateMode.Absolute)
			{
				this.inputDirectionRaw *= 120f * Time.unscaledDeltaTime;
			}
			this.lookAxisRaw.x = this.lookAxisRaw.x + this.inputDirectionRaw.x;
			this.lookAxisRaw.y = this.lookAxisRaw.y + this.inputDirectionRaw.z;
			this.inputDirection = (this.inputDirectionRaw = Vector3.zero);
		}
		else
		{
			this.inputDirection = this.inputDirectionRaw;
			if (this.movementAxis != null)
			{
				Vector3 forward = this.movementAxis.forward;
				forward.y = 0f;
				forward.Normalize();
				Vector3 right = this.movementAxis.right;
				right.y = 0f;
				right.Normalize();
				this.inputDirection = forward * this.inputDirection.z + right * this.inputDirection.x;
			}
		}
		this.smoothedInputDirection = Vector3.SmoothDamp(this.smoothedInputDirection, this.inputDirection, ref this.smoothedInputDirectionVelocity, 0.2f);
		this.movement.desiredDirection = this.inputDirection;
		this.movement.desiredDirectionRaw = this.inputDirectionRaw;
		this.lookAxis = this.lookAxisRaw;
	}

	// Token: 0x06000A3A RID: 2618 RVA: 0x000304CB File Offset: 0x0002E6CB
	private void LateUpdate()
	{
		this.jumpDesired = false;
		this.cancelAction = false;
	}

	// Token: 0x06000A3B RID: 2619 RVA: 0x000304DC File Offset: 0x0002E6DC
	private void HandlePrimary()
	{
		bool flag = false;
		bool flag2 = false;
		if (this.primaryMapping == 0)
		{
			flag = this.rePlayer.GetButtonDown(this.primary);
			flag2 = this.rePlayer.GetButton(this.primary);
		}
		if (flag2 && PlayerInput.autoSword)
		{
			this.holdPrimaryCounter += Time.deltaTime;
			this.holdPrimaryCounterTotal += Time.deltaTime;
		}
		else
		{
			this.holdPrimaryCounter = 0f;
			this.holdPrimaryCounterTotal = 0f;
		}
		float num = (SpeedrunData.MaxSpeedAutoSword ? 0.01f : Mathf.Lerp(0.5f, 0.3f, this.holdPrimaryCounterTotal / 5f));
		if (this.holdPrimaryCounter > num)
		{
			this.holdPrimaryCounter -= num;
			flag = true;
		}
		bool flag3 = !this.movement.IsSwimming && !this.movement.IsClimbing;
		if ((this.movement.isGliding || this.movement.IsClimbing) && flag)
		{
			flag3 &= this.movement.TryCancelNow();
		}
		if (flag3)
		{
			this.itemManager.OnPrimary(flag, this.rePlayer.GetButton(this.primary));
		}
	}

	// Token: 0x06000A3C RID: 2620 RVA: 0x0003060C File Offset: 0x0002E80C
	private void HandleSecondary()
	{
		bool flag = this.rePlayer.GetButtonDown(this.secondary);
		bool flag2 = this.rePlayer.GetButton(this.secondary);
		if (this.primaryMapping == 2)
		{
			flag |= this.rePlayer.GetButtonDown(this.primary);
			flag2 |= this.rePlayer.GetButton(this.primary);
		}
		bool flag3 = !this.movement.IsSwimming && !this.movement.IsClimbing;
		if ((this.movement.isGliding || this.movement.IsClimbing) && flag)
		{
			flag3 &= this.movement.TryCancelNow();
		}
		if (flag3)
		{
			this.itemManager.OnSecondary(flag, flag2);
		}
	}

	// Token: 0x06000A3D RID: 2621 RVA: 0x000306C8 File Offset: 0x0002E8C8
	private void HandleItem()
	{
		bool buttonDown = this.rePlayer.GetButtonDown(this.useItem);
		bool flag = !this.movement.IsSwimming;
		if ((this.movement.isGliding || this.movement.IsClimbing) && buttonDown)
		{
			flag &= this.movement.TryCancelNow();
		}
		if (flag)
		{
			this.itemManager.OnUseItem(buttonDown, this.rePlayer.GetButton(this.useItem));
		}
	}

	// Token: 0x06000A3E RID: 2622 RVA: 0x00030744 File Offset: 0x0002E944
	private void HandleItem_R()
	{
		bool buttonDown = this.rePlayer.GetButtonDown(this.useItemR);
		bool flag = !this.movement.IsSwimming;
		if ((this.movement.isGliding || this.movement.IsClimbing) && buttonDown)
		{
			flag &= this.movement.TryCancelNow();
		}
		if (flag)
		{
			this.itemManager.OnUseItem_R(buttonDown, this.rePlayer.GetButton(this.useItemR));
		}
	}

	// Token: 0x04000CD7 RID: 3287
	public static bool shouldReCenterCamera = false;

	// Token: 0x04000CD8 RID: 3288
	private const float absoluteToRelativeMul = 120f;

	// Token: 0x04000CD9 RID: 3289
	public static bool autoSword = true;

	// Token: 0x04000CDA RID: 3290
	public static bool useMovementToAim = false;

	// Token: 0x04000CDB RID: 3291
	public static PlayerInput.InteractMapping interactMapping;

	// Token: 0x04000CDC RID: 3292
	public static PlayerInput.SecondaryMapping secondaryMapping;

	// Token: 0x04000CDD RID: 3293
	public static PlayerInput p;

	// Token: 0x04000CDE RID: 3294
	private PlayerMovement movement;

	// Token: 0x04000CDF RID: 3295
	private PlayerItemManager itemManager;

	// Token: 0x04000CE0 RID: 3296
	public RagdollController ragdollController;

	// Token: 0x04000CE1 RID: 3297
	public PlayerInteract playerInteract;

	// Token: 0x04000CE2 RID: 3298
	public Transform movementAxis;

	// Token: 0x04000CE3 RID: 3299
	protected Vector3 inputDirectionRaw;

	// Token: 0x04000CE4 RID: 3300
	public Vector3 inputDirection;

	// Token: 0x04000CE5 RID: 3301
	public Vector3 smoothedInputDirection;

	// Token: 0x04000CE6 RID: 3302
	public Vector3 smoothedInputDirectionVelocity;

	// Token: 0x04000CE7 RID: 3303
	private float isClimbingSmooth;

	// Token: 0x04000CE8 RID: 3304
	public bool jumpDesired;

	// Token: 0x04000CE9 RID: 3305
	public bool cancelAction;

	// Token: 0x04000CEA RID: 3306
	private Vector2 lookAxisRaw;

	// Token: 0x04000CEB RID: 3307
	public Vector2 lookAxis;

	// Token: 0x04000CEC RID: 3308
	private global::Rewired.Player rePlayer;

	// Token: 0x04000CED RID: 3309
	private int moveHorizontal;

	// Token: 0x04000CEE RID: 3310
	private int moveVertical;

	// Token: 0x04000CEF RID: 3311
	private int jump;

	// Token: 0x04000CF0 RID: 3312
	private int interact;

	// Token: 0x04000CF1 RID: 3313
	private int lookHorizontal;

	// Token: 0x04000CF2 RID: 3314
	private int lookVertical;

	// Token: 0x04000CF3 RID: 3315
	private int primary;

	// Token: 0x04000CF4 RID: 3316
	private int secondary;

	// Token: 0x04000CF5 RID: 3317
	private int useItem;

	// Token: 0x04000CF6 RID: 3318
	private int useItemR;

	// Token: 0x04000CF7 RID: 3319
	private int interactSword;

	// Token: 0x04000CF8 RID: 3320
	private int interactSwordShield;

	// Token: 0x04000CF9 RID: 3321
	private int reCenterCamera;

	// Token: 0x04000CFA RID: 3322
	private int primaryMapping;

	// Token: 0x04000CFB RID: 3323
	private int jumpMapping;

	// Token: 0x04000CFC RID: 3324
	private float holdPrimaryCounter;

	// Token: 0x04000CFD RID: 3325
	private float holdPrimaryCounterTotal;

	// Token: 0x04000CFE RID: 3326
	private const float primaryAutoTime = 0.5f;

	// Token: 0x04000CFF RID: 3327
	private const float primaryAutoTimeFast = 0.3f;

	// Token: 0x020003E6 RID: 998
	public enum InteractMapping
	{
		// Token: 0x04001C6B RID: 7275
		Self,
		// Token: 0x04001C6C RID: 7276
		Primary,
		// Token: 0x04001C6D RID: 7277
		Jump
	}

	// Token: 0x020003E7 RID: 999
	public enum SecondaryMapping
	{
		// Token: 0x04001C6F RID: 7279
		Self,
		// Token: 0x04001C70 RID: 7280
		Primary
	}
}
