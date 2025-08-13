using System;
using UnityEngine;

// Token: 0x020001C7 RID: 455
public class ItemFireable : MonoBehaviour, IItemBehaviour
{
	// Token: 0x1700007F RID: 127
	// (get) Token: 0x0600095B RID: 2395 RVA: 0x0002C787 File Offset: 0x0002A987
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

	// Token: 0x0600095C RID: 2396 RVA: 0x0002C794 File Offset: 0x0002A994
	private void Awake()
	{
		this.itemManager = Player.itemManager;
		this.movement = Player.movement;
		this.animator = this.itemManager.animator;
		this.reaction = Player.reaction;
	}

	// Token: 0x0600095D RID: 2397 RVA: 0x0002C7C8 File Offset: 0x0002A9C8
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

	// Token: 0x0600095E RID: 2398 RVA: 0x0002C8CE File Offset: 0x0002AACE
	public virtual Vector3 GetSpawnPoint()
	{
		return Player.itemManager.thrownSpawnPoint.position;
	}

	// Token: 0x0600095F RID: 2399 RVA: 0x0002C8E0 File Offset: 0x0002AAE0
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

	// Token: 0x06000960 RID: 2400 RVA: 0x0002C955 File Offset: 0x0002AB55
	public virtual void Fire(Vector3 direction)
	{
		if (this.fireSound != null)
		{
			this.fireSound.Play();
		}
		this.animator.SetTrigger("Fire");
	}

	// Token: 0x06000961 RID: 2401 RVA: 0x0002C980 File Offset: 0x0002AB80
	public virtual float GetSpeed(float charge = 1f)
	{
		return 30f;
	}

	// Token: 0x06000962 RID: 2402 RVA: 0x0002C987 File Offset: 0x0002AB87
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

	// Token: 0x06000963 RID: 2403 RVA: 0x0002C9BF File Offset: 0x0002ABBF
	public virtual void Cancel()
	{
		this.SetAiming(false);
		Player.itemManager.IsAiming = false;
	}

	// Token: 0x06000964 RID: 2404 RVA: 0x0002C9D4 File Offset: 0x0002ABD4
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

	// Token: 0x06000965 RID: 2405 RVA: 0x0002CA3B File Offset: 0x0002AC3B
	public virtual void OnRemove()
	{
		Player.itemManager.IsAiming = false;
	}

	// Token: 0x06000966 RID: 2406 RVA: 0x0002CA48 File Offset: 0x0002AC48
	public void SetIndex(int index)
	{
		if (index == 1)
		{
			this.isOnRight = true;
		}
	}

	// Token: 0x04000BCA RID: 3018
	protected PlayerItemManager itemManager;

	// Token: 0x04000BCB RID: 3019
	protected PlayerMovement movement;

	// Token: 0x04000BCC RID: 3020
	protected Transform mainCamera;

	// Token: 0x04000BCD RID: 3021
	protected Animator animator;

	// Token: 0x04000BCE RID: 3022
	protected CharacterReactionController reaction;

	// Token: 0x04000BCF RID: 3023
	private float nextAllowedFireTime = -1f;

	// Token: 0x04000BD0 RID: 3024
	public float minFireTime = 0.1f;

	// Token: 0x04000BD1 RID: 3025
	private bool isAiming;

	// Token: 0x04000BD2 RID: 3026
	public bool hasAmmo;

	// Token: 0x04000BD3 RID: 3027
	[ConditionalHide("hasAmmo", true)]
	public int ammo = 6;

	// Token: 0x04000BD4 RID: 3028
	private int currentAmmo;

	// Token: 0x04000BD5 RID: 3029
	public AudioSourceVariance aimSound;

	// Token: 0x04000BD6 RID: 3030
	public AudioSourceVariance fireSound;

	// Token: 0x04000BD7 RID: 3031
	private static readonly int _Aiming = Animator.StringToHash("Aiming");

	// Token: 0x04000BD8 RID: 3032
	private bool isOnRight;
}
