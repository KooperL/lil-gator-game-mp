using System;
using UnityEngine;

// Token: 0x0200024A RID: 586
public class ItemFireable : MonoBehaviour, IItemBehaviour
{
	// Token: 0x17000107 RID: 263
	// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x0000A656 File Offset: 0x00008856
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

	// Token: 0x06000AF3 RID: 2803 RVA: 0x0000A663 File Offset: 0x00008863
	private void Awake()
	{
		this.itemManager = Player.itemManager;
		this.movement = Player.movement;
		this.animator = this.itemManager.animator;
		this.reaction = Player.reaction;
	}

	// Token: 0x06000AF4 RID: 2804 RVA: 0x0003E878 File Offset: 0x0003CA78
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

	// Token: 0x06000AF5 RID: 2805 RVA: 0x0000A3C5 File Offset: 0x000085C5
	public virtual Vector3 GetSpawnPoint()
	{
		return Player.itemManager.thrownSpawnPoint.position;
	}

	// Token: 0x06000AF6 RID: 2806 RVA: 0x0003E980 File Offset: 0x0003CB80
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

	// Token: 0x06000AF7 RID: 2807 RVA: 0x0000A697 File Offset: 0x00008897
	public virtual void Fire(Vector3 direction)
	{
		if (this.fireSound != null)
		{
			this.fireSound.Play();
		}
		this.animator.SetTrigger("Fire");
	}

	// Token: 0x06000AF8 RID: 2808 RVA: 0x0000A3D6 File Offset: 0x000085D6
	public virtual float GetSpeed(float charge = 1f)
	{
		return 30f;
	}

	// Token: 0x06000AF9 RID: 2809 RVA: 0x0000A6C2 File Offset: 0x000088C2
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

	// Token: 0x06000AFA RID: 2810 RVA: 0x0000A6FA File Offset: 0x000088FA
	public virtual void Cancel()
	{
		this.SetAiming(false);
		Player.itemManager.IsAiming = false;
	}

	// Token: 0x06000AFB RID: 2811 RVA: 0x0003E9F8 File Offset: 0x0003CBF8
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

	// Token: 0x06000AFC RID: 2812 RVA: 0x0000A70E File Offset: 0x0000890E
	public virtual void OnRemove()
	{
		Player.itemManager.IsAiming = false;
	}

	// Token: 0x06000AFD RID: 2813 RVA: 0x0000A71B File Offset: 0x0000891B
	public void SetIndex(int index)
	{
		if (index == 1)
		{
			this.isOnRight = true;
		}
	}

	// Token: 0x04000DF0 RID: 3568
	protected PlayerItemManager itemManager;

	// Token: 0x04000DF1 RID: 3569
	protected PlayerMovement movement;

	// Token: 0x04000DF2 RID: 3570
	protected Transform mainCamera;

	// Token: 0x04000DF3 RID: 3571
	protected Animator animator;

	// Token: 0x04000DF4 RID: 3572
	protected CharacterReactionController reaction;

	// Token: 0x04000DF5 RID: 3573
	private float nextAllowedFireTime = -1f;

	// Token: 0x04000DF6 RID: 3574
	public float minFireTime = 0.1f;

	// Token: 0x04000DF7 RID: 3575
	private bool isAiming;

	// Token: 0x04000DF8 RID: 3576
	public bool hasAmmo;

	// Token: 0x04000DF9 RID: 3577
	[ConditionalHide("hasAmmo", true)]
	public int ammo = 6;

	// Token: 0x04000DFA RID: 3578
	private int currentAmmo;

	// Token: 0x04000DFB RID: 3579
	public AudioSourceVariance aimSound;

	// Token: 0x04000DFC RID: 3580
	public AudioSourceVariance fireSound;

	// Token: 0x04000DFD RID: 3581
	private static readonly int _Aiming = Animator.StringToHash("Aiming");

	// Token: 0x04000DFE RID: 3582
	private bool isOnRight;
}
