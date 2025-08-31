using System;
using UnityEngine;

public class ItemThrowable : MonoBehaviour, IItemBehaviour
{
	// Token: 0x060009E0 RID: 2528 RVA: 0x0002DF7A File Offset: 0x0002C17A
	public static bool AimSolver(float speed, Vector3 direction, out Vector3 velocity, float maxSolveDistance = 20f, float failedSolveDistance = 15f, float gravityFactor = 1f)
	{
		return ItemThrowable.AimSolver(speed, direction, out velocity, Player.itemManager.thrownSpawnPoint.position, maxSolveDistance, failedSolveDistance, gravityFactor);
	}

	// Token: 0x060009E1 RID: 2529 RVA: 0x0002DF98 File Offset: 0x0002C198
	public static bool AimSolver(float speed, Vector3 direction, out Vector3 velocity, Vector3 spawnPoint, float maxSolveDistance = 20f, float failedSolveDistance = 15f, float gravityFactor = 1f)
	{
		bool flag = false;
		RaycastHit raycastHit;
		Vector3 vector;
		if (Physics.SphereCast(MainCamera.c.ViewportPointToRay(new Vector3(0.5f, 0.5f, 1f)), 0.1f, out raycastHit, maxSolveDistance, LayerUtil.HitLayers, QueryTriggerInteraction.Collide) && PhysUtil.SolveProjectileVelocity(raycastHit.point - spawnPoint, speed, out vector, gravityFactor))
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

	// (get) Token: 0x060009E2 RID: 2530 RVA: 0x0002E043 File Offset: 0x0002C243
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

	// Token: 0x060009E3 RID: 2531 RVA: 0x0002E050 File Offset: 0x0002C250
	private void Awake()
	{
		this.itemManager = Player.itemManager;
		this.movement = Player.movement;
		this.animator = this.itemManager.animator;
		this.reaction = Player.reaction;
	}

	// Token: 0x060009E4 RID: 2532 RVA: 0x0002E084 File Offset: 0x0002C284
	protected virtual bool CanStartThrow(bool isDown, bool isHeld)
	{
		return Time.time - this.lastHeldTime > 0.5f || isDown;
	}

	// Token: 0x060009E5 RID: 2533 RVA: 0x0002E09C File Offset: 0x0002C29C
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

	// Token: 0x060009E6 RID: 2534 RVA: 0x0002E29E File Offset: 0x0002C49E
	public virtual float GetSolveSpeed(float charge = 1f)
	{
		return 30f;
	}

	// Token: 0x060009E7 RID: 2535 RVA: 0x0002E2A8 File Offset: 0x0002C4A8
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

	// Token: 0x060009E8 RID: 2536 RVA: 0x0002E2F5 File Offset: 0x0002C4F5
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

	// Token: 0x060009E9 RID: 2537 RVA: 0x0002E32D File Offset: 0x0002C52D
	public virtual void Cancel()
	{
		this.SetCharging(false);
		Player.itemManager.IsAiming = false;
	}

	// Token: 0x060009EA RID: 2538 RVA: 0x0002E344 File Offset: 0x0002C544
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

	// Token: 0x060009EB RID: 2539 RVA: 0x0002E3B0 File Offset: 0x0002C5B0
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

	// Token: 0x060009EC RID: 2540 RVA: 0x0002E417 File Offset: 0x0002C617
	public virtual void OnRemove()
	{
		Player.itemManager.IsAiming = false;
	}

	// Token: 0x060009ED RID: 2541 RVA: 0x0002E424 File Offset: 0x0002C624
	public void SetIndex(int index)
	{
		if (index == 1)
		{
			this.isOnRight = true;
		}
		SpawnExtraItemObject spawnExtraItemObject;
		if (base.TryGetComponent<SpawnExtraItemObject>(out spawnExtraItemObject))
		{
			spawnExtraItemObject.isOnRight = this.isOnRight;
		}
	}

	protected PlayerItemManager itemManager;

	protected PlayerMovement movement;

	protected Transform mainCamera;

	protected Animator animator;

	protected CharacterReactionController reaction;

	public float chargeTime = 0.5f;

	protected bool charging;

	private float chargeStartTime = -1f;

	public AudioSourceVariance chargeSound;

	public AudioSourceVariance throwSound;

	[Space]
	public bool aimSolver;

	[ConditionalHide("aimSolver", true)]
	public bool solveRaycast = true;

	[ConditionalHide("aimSolver", true, ConditionalSourceField2 = "solveRaycast")]
	public float maxSolveDistance = 40f;

	[ConditionalHide("aimSolver", true)]
	public float failedSolveDistance = 20f;

	public float gravityFactor = 1f;

	private static readonly int _Throwing = Animator.StringToHash("Throwing");

	private static readonly int _Throw = Animator.StringToHash("Throw");

	private static readonly int _ThrowBody = Animator.StringToHash("ThrowBody");

	public bool allowStartOnHold = true;

	public float startOnHoldDelay;

	private float startOnHoldCounter;

	private float lastHeldTime = -10f;

	private bool ensureThrowingState;

	protected bool isOnRight;
}
