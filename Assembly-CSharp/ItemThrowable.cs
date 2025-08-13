using System;
using UnityEngine;

// Token: 0x02000260 RID: 608
public class ItemThrowable : MonoBehaviour, IItemBehaviour
{
	// Token: 0x06000B89 RID: 2953 RVA: 0x0000ADB5 File Offset: 0x00008FB5
	public static bool AimSolver(float speed, Vector3 direction, out Vector3 velocity, float maxSolveDistance = 20f, float failedSolveDistance = 15f, float gravityFactor = 1f)
	{
		return ItemThrowable.AimSolver(speed, direction, out velocity, Player.itemManager.thrownSpawnPoint.position, maxSolveDistance, failedSolveDistance, gravityFactor);
	}

	// Token: 0x06000B8A RID: 2954 RVA: 0x0003FD58 File Offset: 0x0003DF58
	public static bool AimSolver(float speed, Vector3 direction, out Vector3 velocity, Vector3 spawnPoint, float maxSolveDistance = 20f, float failedSolveDistance = 15f, float gravityFactor = 1f)
	{
		bool flag = false;
		RaycastHit raycastHit;
		Vector3 vector;
		if (Physics.SphereCast(MainCamera.c.ViewportPointToRay(new Vector3(0.5f, 0.5f, 1f)), 0.1f, ref raycastHit, maxSolveDistance, LayerUtil.HitLayers, 2) && PhysUtil.SolveProjectileVelocity(raycastHit.point - spawnPoint, speed, out vector, gravityFactor))
		{
			velocity = vector;
			return true;
		}
		Vector3 vector2;
		if (!flag && PhysUtil.SolveProjectileVelocity(MainCamera.t.position + failedSolveDistance * direction - spawnPoint, speed, out vector2, gravityFactor))
		{
			velocity = vector2;
			return true;
		}
		velocity = Vector3.zero;
		return false;
	}

	// Token: 0x17000110 RID: 272
	// (get) Token: 0x06000B8B RID: 2955 RVA: 0x0000ADD3 File Offset: 0x00008FD3
	protected PlayerItemManager.EquippedState EquippedState
	{
		get
		{
			if (!this.isOnRight)
			{
				return PlayerItemManager.EquippedState.Item;
			}
			return PlayerItemManager.EquippedState.ItemR;
		}
	}

	// Token: 0x06000B8C RID: 2956 RVA: 0x0000ADE0 File Offset: 0x00008FE0
	private void Awake()
	{
		this.itemManager = Player.itemManager;
		this.movement = Player.movement;
		this.animator = this.itemManager.animator;
		this.reaction = Player.reaction;
	}

	// Token: 0x06000B8D RID: 2957 RVA: 0x0000AE14 File Offset: 0x00009014
	protected virtual bool CanStartThrow(bool isDown, bool isHeld)
	{
		return Time.time - this.lastHeldTime > 0.5f || isDown;
	}

	// Token: 0x06000B8E RID: 2958 RVA: 0x0003FE04 File Offset: 0x0003E004
	public virtual void Input(bool isDown, bool isHeld)
	{
		if (Game.HasControl)
		{
			if (this.movement.isRagdolling || this.movement.isGliding)
			{
				if (!isDown)
				{
					return;
				}
				this.movement.ClearMods();
			}
			if (!this.charging && this.allowStartOnHold && isHeld)
			{
				this.startOnHoldCounter += Time.deltaTime;
			}
			else
			{
				this.startOnHoldCounter = 0f;
			}
			if (!this.charging)
			{
				if ((this.movement.IsGrounded || this.movement.InAir) && (isDown || (isHeld && this.allowStartOnHold && this.startOnHoldCounter > this.startOnHoldDelay)) && this.CanStartThrow(isDown, isHeld))
				{
					this.itemManager.SetEquippedState(this.EquippedState, false);
					this.itemManager.SetItemInUse(this, true);
					this.SetCharging(true);
					if (this.chargeSound != null)
					{
						this.chargeSound.Play();
					}
					this.chargeStartTime = Time.time;
					return;
				}
			}
			else
			{
				this.animator.SetBool(ItemThrowable._Throwing, true);
				this.lastHeldTime = Time.time;
				float num = (Time.time - this.chargeStartTime) / this.chargeTime;
				this.itemManager.SetEquippedState(this.EquippedState, false);
				this.itemManager.SetItemInUse(this, true);
				if (num > 0.5f && !isHeld)
				{
					if (num >= 0.1f)
					{
						num = ((Mathf.Abs(1f - num) < 0.1f) ? 1f : 0.5f);
						Vector3 vector = MainCamera.t.forward;
						Vector3 vector2;
						if (this.aimSolver && ItemThrowable.AimSolver(this.GetSolveSpeed(num), vector, out vector2, 20f, 15f, this.gravityFactor))
						{
							vector = vector2.normalized;
						}
						this.Throw(1f, vector);
					}
					this.SetCharging(false);
					return;
				}
			}
		}
		else if (this.charging)
		{
			this.SetCharging(false);
		}
	}

	// Token: 0x06000B8F RID: 2959 RVA: 0x0000A3D6 File Offset: 0x000085D6
	public virtual float GetSolveSpeed(float charge = 1f)
	{
		return 30f;
	}

	// Token: 0x06000B90 RID: 2960 RVA: 0x00040008 File Offset: 0x0003E208
	public virtual void Throw(float charge, Vector3 direction)
	{
		if (this.throwSound != null)
		{
			this.throwSound.Play();
		}
		this.SetCharging(false);
		this.animator.SetTrigger(ItemThrowable._Throw);
		this.animator.SetTrigger(ItemThrowable._ThrowBody);
	}

	// Token: 0x06000B91 RID: 2961 RVA: 0x0000AE2B File Offset: 0x0000902B
	public virtual void LateUpdate()
	{
		if (!this.charging)
		{
			return;
		}
		if (Player.itemManager.itemInUse != this || (Player.movement.isModified && Player.movement.modItemRule == PlayerMovement.ModRule.Locked))
		{
			this.SetCharging(false);
		}
	}

	// Token: 0x06000B92 RID: 2962 RVA: 0x0000AE63 File Offset: 0x00009063
	public virtual void Cancel()
	{
		this.SetCharging(false);
		Player.itemManager.IsAiming = false;
	}

	// Token: 0x06000B93 RID: 2963 RVA: 0x00040058 File Offset: 0x0003E258
	public virtual void SetCharging(bool isCharging)
	{
		if (UIMenus.reticle != null)
		{
			if (isCharging)
			{
				UIMenus.reticle.StartAiming(this.chargeTime);
			}
			else
			{
				UIMenus.reticle.StopAiming();
			}
		}
		this.itemManager.SetItemInUse(this, isCharging);
		Player.itemManager.IsAiming = isCharging;
		this.animator.SetBool(ItemThrowable._Throwing, isCharging);
		this.charging = isCharging;
	}

	// Token: 0x06000B94 RID: 2964 RVA: 0x000400C4 File Offset: 0x0003E2C4
	public virtual void SetEquipped(bool isEquipped)
	{
		Transform transform = (isEquipped ? this.itemManager.leftHandAnchor : (this.isOnRight ? this.itemManager.hipAnchor_r : this.itemManager.hipAnchor));
		if (base.transform.parent != transform)
		{
			base.transform.ApplyParent(transform);
		}
		if (!isEquipped)
		{
			this.Cancel();
		}
	}

	// Token: 0x06000B95 RID: 2965 RVA: 0x0000A70E File Offset: 0x0000890E
	public virtual void OnRemove()
	{
		Player.itemManager.IsAiming = false;
	}

	// Token: 0x06000B96 RID: 2966 RVA: 0x0004012C File Offset: 0x0003E32C
	public void SetIndex(int index)
	{
		if (index == 1)
		{
			this.isOnRight = true;
		}
		SpawnExtraItemObject spawnExtraItemObject;
		if (base.TryGetComponent<SpawnExtraItemObject>(ref spawnExtraItemObject))
		{
			spawnExtraItemObject.isOnRight = this.isOnRight;
		}
	}

	// Token: 0x04000E66 RID: 3686
	protected PlayerItemManager itemManager;

	// Token: 0x04000E67 RID: 3687
	protected PlayerMovement movement;

	// Token: 0x04000E68 RID: 3688
	protected Transform mainCamera;

	// Token: 0x04000E69 RID: 3689
	protected Animator animator;

	// Token: 0x04000E6A RID: 3690
	protected CharacterReactionController reaction;

	// Token: 0x04000E6B RID: 3691
	public float chargeTime = 0.5f;

	// Token: 0x04000E6C RID: 3692
	protected bool charging;

	// Token: 0x04000E6D RID: 3693
	private float chargeStartTime = -1f;

	// Token: 0x04000E6E RID: 3694
	public AudioSourceVariance chargeSound;

	// Token: 0x04000E6F RID: 3695
	public AudioSourceVariance throwSound;

	// Token: 0x04000E70 RID: 3696
	[Space]
	public bool aimSolver;

	// Token: 0x04000E71 RID: 3697
	[ConditionalHide("aimSolver", true)]
	public bool solveRaycast = true;

	// Token: 0x04000E72 RID: 3698
	[ConditionalHide("aimSolver", true, ConditionalSourceField2 = "solveRaycast")]
	public float maxSolveDistance = 40f;

	// Token: 0x04000E73 RID: 3699
	[ConditionalHide("aimSolver", true)]
	public float failedSolveDistance = 20f;

	// Token: 0x04000E74 RID: 3700
	public float gravityFactor = 1f;

	// Token: 0x04000E75 RID: 3701
	private static readonly int _Throwing = Animator.StringToHash("Throwing");

	// Token: 0x04000E76 RID: 3702
	private static readonly int _Throw = Animator.StringToHash("Throw");

	// Token: 0x04000E77 RID: 3703
	private static readonly int _ThrowBody = Animator.StringToHash("ThrowBody");

	// Token: 0x04000E78 RID: 3704
	public bool allowStartOnHold = true;

	// Token: 0x04000E79 RID: 3705
	public float startOnHoldDelay;

	// Token: 0x04000E7A RID: 3706
	private float startOnHoldCounter;

	// Token: 0x04000E7B RID: 3707
	private float lastHeldTime = -10f;

	// Token: 0x04000E7C RID: 3708
	private bool ensureThrowingState;

	// Token: 0x04000E7D RID: 3709
	protected bool isOnRight;
}
