using System;
using UnityEngine;

public class ItemFireable : MonoBehaviour, IItemBehaviour
{
	// (get) Token: 0x06000B3F RID: 2879 RVA: 0x0000A994 File Offset: 0x00008B94
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

	// Token: 0x06000B40 RID: 2880 RVA: 0x0000A9A1 File Offset: 0x00008BA1
	private void Awake()
	{
		this.itemManager = Player.itemManager;
		this.movement = Player.movement;
		this.animator = this.itemManager.animator;
		this.reaction = Player.reaction;
	}

	// Token: 0x06000B41 RID: 2881 RVA: 0x00040648 File Offset: 0x0003E848
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

	// Token: 0x06000B42 RID: 2882 RVA: 0x0000A703 File Offset: 0x00008903
	public virtual Vector3 GetSpawnPoint()
	{
		return Player.itemManager.thrownSpawnPoint.position;
	}

	// Token: 0x06000B43 RID: 2883 RVA: 0x00040750 File Offset: 0x0003E950
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

	// Token: 0x06000B44 RID: 2884 RVA: 0x0000A9D5 File Offset: 0x00008BD5
	public virtual void Fire(Vector3 direction)
	{
		if (this.fireSound != null)
		{
			this.fireSound.Play();
		}
		this.animator.SetTrigger("Fire");
	}

	// Token: 0x06000B45 RID: 2885 RVA: 0x0000A714 File Offset: 0x00008914
	public virtual float GetSpeed(float charge = 1f)
	{
		return 30f;
	}

	// Token: 0x06000B46 RID: 2886 RVA: 0x0000AA00 File Offset: 0x00008C00
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

	// Token: 0x06000B47 RID: 2887 RVA: 0x0000AA38 File Offset: 0x00008C38
	public virtual void Cancel()
	{
		this.SetAiming(false);
		Player.itemManager.IsAiming = false;
	}

	// Token: 0x06000B48 RID: 2888 RVA: 0x000407C8 File Offset: 0x0003E9C8
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

	// Token: 0x06000B49 RID: 2889 RVA: 0x0000AA4C File Offset: 0x00008C4C
	public virtual void OnRemove()
	{
		Player.itemManager.IsAiming = false;
	}

	// Token: 0x06000B4A RID: 2890 RVA: 0x0000AA59 File Offset: 0x00008C59
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
