using System;
using Rewired;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	// (get) Token: 0x06000C37 RID: 3127 RVA: 0x0000B5FC File Offset: 0x000097FC
	public static Vector2 RawInput
	{
		get
		{
			return new Vector2(PlayerInput.p.inputDirectionRaw.x, PlayerInput.p.inputDirectionRaw.z);
		}
	}

	// Token: 0x06000C38 RID: 3128 RVA: 0x0004316C File Offset: 0x0004136C
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

	// Token: 0x06000C39 RID: 3129 RVA: 0x00002229 File Offset: 0x00000429
	private void OnDisable()
	{
	}

	// Token: 0x06000C3A RID: 3130 RVA: 0x00043298 File Offset: 0x00041498
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

	// Token: 0x06000C3B RID: 3131 RVA: 0x0000B621 File Offset: 0x00009821
	private void LateUpdate()
	{
		this.jumpDesired = false;
		this.cancelAction = false;
	}

	// Token: 0x06000C3C RID: 3132 RVA: 0x00043850 File Offset: 0x00041A50
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

	// Token: 0x06000C3D RID: 3133 RVA: 0x00043980 File Offset: 0x00041B80
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

	// Token: 0x06000C3E RID: 3134 RVA: 0x00043A3C File Offset: 0x00041C3C
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

	// Token: 0x06000C3F RID: 3135 RVA: 0x00043AB8 File Offset: 0x00041CB8
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

	public static bool shouldReCenterCamera = false;

	private const float absoluteToRelativeMul = 120f;

	public static bool autoSword = true;

	public static bool useMovementToAim = false;

	public static PlayerInput.InteractMapping interactMapping;

	public static PlayerInput.SecondaryMapping secondaryMapping;

	public static PlayerInput p;

	private PlayerMovement movement;

	private PlayerItemManager itemManager;

	public RagdollController ragdollController;

	public PlayerInteract playerInteract;

	public Transform movementAxis;

	protected Vector3 inputDirectionRaw;

	public Vector3 inputDirection;

	public Vector3 smoothedInputDirection;

	public Vector3 smoothedInputDirectionVelocity;

	private float isClimbingSmooth;

	public bool jumpDesired;

	public bool cancelAction;

	private Vector2 lookAxisRaw;

	public Vector2 lookAxis;

	private global::Rewired.Player rePlayer;

	private int moveHorizontal;

	private int moveVertical;

	private int jump;

	private int interact;

	private int lookHorizontal;

	private int lookVertical;

	private int primary;

	private int secondary;

	private int useItem;

	private int useItemR;

	private int interactSword;

	private int interactSwordShield;

	private int reCenterCamera;

	private int primaryMapping;

	private int jumpMapping;

	private float holdPrimaryCounter;

	private float holdPrimaryCounterTotal;

	private const float primaryAutoTime = 0.5f;

	private const float primaryAutoTimeFast = 0.3f;

	public enum InteractMapping
	{
		Self,
		Primary,
		Jump
	}

	public enum SecondaryMapping
	{
		Self,
		Primary
	}
}
