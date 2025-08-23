using System;
using UnityEngine;

public class ItemThrowable : MonoBehaviour, IItemBehaviour
{
	// Token: 0x06000BD6 RID: 3030 RVA: 0x0000B0C7 File Offset: 0x000092C7
	public static bool AimSolver(float speed, Vector3 direction, out Vector3 velocity, float maxSolveDistance = 20f, float failedSolveDistance = 15f, float gravityFactor = 1f)
	{
		return ItemThrowable.AimSolver(speed, direction, out velocity, Player.itemManager.thrownSpawnPoint.position, maxSolveDistance, failedSolveDistance, gravityFactor);
	}

	// Token: 0x06000BD7 RID: 3031 RVA: 0x00041BA8 File Offset: 0x0003FDA8
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

	// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x0000B0E5 File Offset: 0x000092E5
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

	// Token: 0x06000BD9 RID: 3033 RVA: 0x0000B0F2 File Offset: 0x000092F2
	private void Awake()
	{
		this.itemManager = Player.itemManager;
		this.movement = Player.movement;
		this.animator = this.itemManager.animator;
		this.reaction = Player.reaction;
	}

	// Token: 0x06000BDA RID: 3034 RVA: 0x0000B126 File Offset: 0x00009326
	protected virtual bool CanStartThrow(bool isDown, bool isHeld)
	{
		return Time.time - this.lastHeldTime > 0.5f || isDown;
	}

	// Token: 0x06000BDB RID: 3035 RVA: 0x00041C54 File Offset: 0x0003FE54
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

	// Token: 0x06000BDC RID: 3036 RVA: 0x0000A714 File Offset: 0x00008914
	public virtual float GetSolveSpeed(float charge = 1f)
	{
		return 30f;
	}

	// Token: 0x06000BDD RID: 3037 RVA: 0x00041E58 File Offset: 0x00040058
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

	// Token: 0x06000BDE RID: 3038 RVA: 0x0000B13D File Offset: 0x0000933D
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

	// Token: 0x06000BDF RID: 3039 RVA: 0x0000B175 File Offset: 0x00009375
	public virtual void Cancel()
	{
		this.SetCharging(false);
		Player.itemManager.IsAiming = false;
	}

	// Token: 0x06000BE0 RID: 3040 RVA: 0x00041EA8 File Offset: 0x000400A8
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

	// Token: 0x06000BE1 RID: 3041 RVA: 0x00041F14 File Offset: 0x00040114
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

	// Token: 0x06000BE2 RID: 3042 RVA: 0x0000AA4C File Offset: 0x00008C4C
	public virtual void OnRemove()
	{
		Player.itemManager.IsAiming = false;
	}

	// Token: 0x06000BE3 RID: 3043 RVA: 0x00041F7C File Offset: 0x0004017C
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
