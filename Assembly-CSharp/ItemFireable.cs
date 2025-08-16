using System;
using UnityEngine;

public class ItemFireable : MonoBehaviour, IItemBehaviour
{
	// (get) Token: 0x06000B3E RID: 2878 RVA: 0x0000A975 File Offset: 0x00008B75
	private PlayerItemManager.EquippedState EquippedState
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

	// Token: 0x06000B3F RID: 2879 RVA: 0x0000A982 File Offset: 0x00008B82
	private void Awake()
	{
		this.itemManager = Player.itemManager;
		this.movement = Player.movement;
		this.animator = this.itemManager.animator;
		this.reaction = Player.reaction;
	}

	// Token: 0x06000B40 RID: 2880 RVA: 0x000401EC File Offset: 0x0003E3EC
	public virtual void Input(bool isDown, bool isHeld)
	{
		if (Game.HasControl)
		{
			if (!this.isAiming)
			{
				if ((this.movement.IsGrounded || this.movement.InAir) && isDown)
				{
					this.itemManager.SetEquippedState(this.EquippedState, false);
					this.SetAiming(true);
					if (this.aimSound != null)
					{
						this.aimSound.Play();
						return;
					}
				}
			}
			else
			{
				this.SetAiming(true);
				this.itemManager.SetEquippedState(this.EquippedState, false);
				if (isDown && Time.time > this.nextAllowedFireTime)
				{
					Vector3 vector = MainCamera.t.forward;
					Vector3 vector2;
					if (ItemThrowable.AimSolver(this.GetSpeed(1f), vector, out vector2, this.GetSpawnPoint(), 20f, 15f, 1f))
					{
						vector = vector2.normalized;
					}
					this.Fire(vector);
					this.nextAllowedFireTime = Time.time + this.minFireTime;
					return;
				}
			}
		}
		else if (this.isAiming)
		{
			this.SetAiming(false);
		}
	}

	// Token: 0x06000B41 RID: 2881 RVA: 0x0000A6E4 File Offset: 0x000088E4
	public virtual Vector3 GetSpawnPoint()
	{
		return Player.itemManager.thrownSpawnPoint.position;
	}

	// Token: 0x06000B42 RID: 2882 RVA: 0x000402F4 File Offset: 0x0003E4F4
	private void SetAiming(bool isAiming)
	{
		this.isAiming = isAiming;
		if (UIMenus.reticle != null && UIMenus.reticle.isAiming != isAiming)
		{
			if (isAiming)
			{
				UIMenus.reticle.StartAiming(0.2f);
			}
			else
			{
				UIMenus.reticle.StopAiming();
			}
		}
		Player.itemManager.IsAiming = isAiming;
		this.itemManager.SetItemInUse(this, isAiming);
		this.animator.SetBool(ItemFireable._Aiming, isAiming);
	}

	// Token: 0x06000B43 RID: 2883 RVA: 0x0000A9B6 File Offset: 0x00008BB6
	public virtual void Fire(Vector3 direction)
	{
		if (this.fireSound != null)
		{
			this.fireSound.Play();
		}
		this.animator.SetTrigger("Fire");
	}

	// Token: 0x06000B44 RID: 2884 RVA: 0x0000A6F5 File Offset: 0x000088F5
	public virtual float GetSpeed(float charge = 1f)
	{
		return 30f;
	}

	// Token: 0x06000B45 RID: 2885 RVA: 0x0000A9E1 File Offset: 0x00008BE1
	public virtual void LateUpdate()
	{
		if (!this.isAiming)
		{
			return;
		}
		if (Player.itemManager.itemInUse != this || (Player.movement.isModified && Player.movement.modItemRule == PlayerMovement.ModRule.Locked))
		{
			this.SetAiming(false);
		}
	}

	// Token: 0x06000B46 RID: 2886 RVA: 0x0000AA19 File Offset: 0x00008C19
	public virtual void Cancel()
	{
		this.SetAiming(false);
		Player.itemManager.IsAiming = false;
	}

	// Token: 0x06000B47 RID: 2887 RVA: 0x0004036C File Offset: 0x0003E56C
	public virtual void SetEquipped(bool isEquipped)
	{
		Transform transform = (isEquipped ? this.itemManager.leftHandAnchor : (this.isOnRight ? this.itemManager.holsterAnchor_r : this.itemManager.holsterAnchor));
		if (base.transform.parent != transform)
		{
			base.transform.ApplyParent(transform);
		}
		if (!isEquipped)
		{
			this.Cancel();
		}
	}

	// Token: 0x06000B48 RID: 2888 RVA: 0x0000AA2D File Offset: 0x00008C2D
	public virtual void OnRemove()
	{
		Player.itemManager.IsAiming = false;
	}

	// Token: 0x06000B49 RID: 2889 RVA: 0x0000AA3A File Offset: 0x00008C3A
	public void SetIndex(int index)
	{
		if (index == 1)
		{
			this.isOnRight = true;
		}
	}

	protected PlayerItemManager itemManager;

	protected PlayerMovement movement;

	protected Transform mainCamera;

	protected Animator animator;

	protected CharacterReactionController reaction;

	private float nextAllowedFireTime = -1f;

	public float minFireTime = 0.1f;

	private bool isAiming;

	public bool hasAmmo;

	[ConditionalHide("hasAmmo", true)]
	public int ammo = 6;

	private int currentAmmo;

	public AudioSourceVariance aimSound;

	public AudioSourceVariance fireSound;

	private static readonly int _Aiming = Animator.StringToHash("Aiming");

	private bool isOnRight;
}
