using System;
using Rewired;
using UnityEngine;

// Token: 0x0200026E RID: 622
public class PlayerInput : MonoBehaviour
{
	// Token: 0x17000119 RID: 281
	// (get) Token: 0x06000BEB RID: 3051 RVA: 0x0000B309 File Offset: 0x00009509
	public static Vector2 RawInput
	{
		get
		{
			return new Vector2(PlayerInput.p.inputDirectionRaw.x, PlayerInput.p.inputDirectionRaw.z);
		}
	}

	// Token: 0x06000BEC RID: 3052 RVA: 0x00041778 File Offset: 0x0003F978
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

	// Token: 0x06000BED RID: 3053 RVA: 0x00002229 File Offset: 0x00000429
	private void OnDisable()
	{
	}

	// Token: 0x06000BEE RID: 3054 RVA: 0x000418A4 File Offset: 0x0003FAA4
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
				else if (PlayerInput.secondaryMapping == PlayerInput.SecondaryMapping.Primary && !Player.movement.IsGrounded)
				{
					this.primaryMapping = 2;
				}
				else
				{
					this.primaryMapping = 0;
				}
				if (!Player.movement.IsGrounded)
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
				flag3 = lastActiveController.type != 2;
			}
			if ((UpdateCursor.isCurrentlyLocked || !flag3) && Game.State != GameState.Menu)
			{
				this.lookAxisRaw = this.rePlayer.GetAxis2DRaw(this.lookHorizontal, this.lookVertical);
				if (this.rePlayer.GetAxisCoordinateMode(this.lookHorizontal) == null)
				{
					this.lookAxisRaw *= 120f * Time.unscaledDeltaTime;
				}
			}
			else
			{
				this.lookAxisRaw = Vector2.zero;
			}
		}
		if (PlayerInput.useMovementToAim && Player.itemManager.IsAiming)
		{
			if (this.rePlayer.GetAxisCoordinateMode(this.moveHorizontal) == null)
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

	// Token: 0x06000BEF RID: 3055 RVA: 0x0000B32E File Offset: 0x0000952E
	private void LateUpdate()
	{
		this.jumpDesired = false;
		this.cancelAction = false;
	}

	// Token: 0x06000BF0 RID: 3056 RVA: 0x00041E5C File Offset: 0x0004005C
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

	// Token: 0x06000BF1 RID: 3057 RVA: 0x00041F8C File Offset: 0x0004018C
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

	// Token: 0x06000BF2 RID: 3058 RVA: 0x00042048 File Offset: 0x00040248
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

	// Token: 0x06000BF3 RID: 3059 RVA: 0x000420C4 File Offset: 0x000402C4
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

	// Token: 0x04000F15 RID: 3861
	public static bool shouldReCenterCamera = false;

	// Token: 0x04000F16 RID: 3862
	private const float absoluteToRelativeMul = 120f;

	// Token: 0x04000F17 RID: 3863
	public static bool autoSword = true;

	// Token: 0x04000F18 RID: 3864
	public static bool useMovementToAim = false;

	// Token: 0x04000F19 RID: 3865
	public static PlayerInput.InteractMapping interactMapping;

	// Token: 0x04000F1A RID: 3866
	public static PlayerInput.SecondaryMapping secondaryMapping;

	// Token: 0x04000F1B RID: 3867
	public static PlayerInput p;

	// Token: 0x04000F1C RID: 3868
	private PlayerMovement movement;

	// Token: 0x04000F1D RID: 3869
	private PlayerItemManager itemManager;

	// Token: 0x04000F1E RID: 3870
	public RagdollController ragdollController;

	// Token: 0x04000F1F RID: 3871
	public PlayerInteract playerInteract;

	// Token: 0x04000F20 RID: 3872
	public Transform movementAxis;

	// Token: 0x04000F21 RID: 3873
	protected Vector3 inputDirectionRaw;

	// Token: 0x04000F22 RID: 3874
	public Vector3 inputDirection;

	// Token: 0x04000F23 RID: 3875
	public Vector3 smoothedInputDirection;

	// Token: 0x04000F24 RID: 3876
	public Vector3 smoothedInputDirectionVelocity;

	// Token: 0x04000F25 RID: 3877
	private float isClimbingSmooth;

	// Token: 0x04000F26 RID: 3878
	public bool jumpDesired;

	// Token: 0x04000F27 RID: 3879
	public bool cancelAction;

	// Token: 0x04000F28 RID: 3880
	private Vector2 lookAxisRaw;

	// Token: 0x04000F29 RID: 3881
	public Vector2 lookAxis;

	// Token: 0x04000F2A RID: 3882
	private Player rePlayer;

	// Token: 0x04000F2B RID: 3883
	private int moveHorizontal;

	// Token: 0x04000F2C RID: 3884
	private int moveVertical;

	// Token: 0x04000F2D RID: 3885
	private int jump;

	// Token: 0x04000F2E RID: 3886
	private int interact;

	// Token: 0x04000F2F RID: 3887
	private int lookHorizontal;

	// Token: 0x04000F30 RID: 3888
	private int lookVertical;

	// Token: 0x04000F31 RID: 3889
	private int primary;

	// Token: 0x04000F32 RID: 3890
	private int secondary;

	// Token: 0x04000F33 RID: 3891
	private int useItem;

	// Token: 0x04000F34 RID: 3892
	private int useItemR;

	// Token: 0x04000F35 RID: 3893
	private int interactSword;

	// Token: 0x04000F36 RID: 3894
	private int interactSwordShield;

	// Token: 0x04000F37 RID: 3895
	private int reCenterCamera;

	// Token: 0x04000F38 RID: 3896
	private int primaryMapping;

	// Token: 0x04000F39 RID: 3897
	private int jumpMapping;

	// Token: 0x04000F3A RID: 3898
	private float holdPrimaryCounter;

	// Token: 0x04000F3B RID: 3899
	private float holdPrimaryCounterTotal;

	// Token: 0x04000F3C RID: 3900
	private const float primaryAutoTime = 0.5f;

	// Token: 0x04000F3D RID: 3901
	private const float primaryAutoTimeFast = 0.3f;

	// Token: 0x0200026F RID: 623
	public enum InteractMapping
	{
		// Token: 0x04000F3F RID: 3903
		Self,
		// Token: 0x04000F40 RID: 3904
		Primary,
		// Token: 0x04000F41 RID: 3905
		Jump
	}

	// Token: 0x02000270 RID: 624
	public enum SecondaryMapping
	{
		// Token: 0x04000F43 RID: 3907
		Self,
		// Token: 0x04000F44 RID: 3908
		Primary
	}
}
