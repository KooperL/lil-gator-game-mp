using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000258 RID: 600
public class ItemShield : MonoBehaviour, IItemBehaviour
{
	// Token: 0x06000B5E RID: 2910 RVA: 0x0000AB2B File Offset: 0x00008D2B
	private void Awake()
	{
		this.itemManager = Player.itemManager;
		this.movement = Player.movement;
		this.movement.shield = this;
	}

	// Token: 0x06000B5F RID: 2911 RVA: 0x0000AB4F File Offset: 0x00008D4F
	private void OnDestroy()
	{
		if (this.movement != null && this.movement.shield == this)
		{
			this.movement.shield = null;
		}
	}

	// Token: 0x06000B60 RID: 2912 RVA: 0x0000AB7E File Offset: 0x00008D7E
	public void Input(bool isDown, bool isHeld)
	{
		if (isDown)
		{
			this.isSledding = this.movement.Sled();
			this.itemManager.SetEquippedState(this.isSledding ? PlayerItemManager.EquippedState.ShieldSled : PlayerItemManager.EquippedState.SwordAndShield, false);
			this.SetState(this.isSledding);
		}
	}

	// Token: 0x06000B61 RID: 2913 RVA: 0x0003F48C File Offset: 0x0003D68C
	private void LateUpdate()
	{
		if (this.isSledding)
		{
			if (!this.movement.isSledding)
			{
				this.SetState(false);
			}
			else
			{
				this.WhileSledding();
			}
		}
		if (base.transform.localPosition != Vector3.zero)
		{
			base.transform.localPosition = Vector3.MoveTowards(base.transform.localPosition, Vector3.zero, 3f * Time.deltaTime);
		}
		if (base.transform.localRotation != Quaternion.identity)
		{
			base.transform.localRotation = Quaternion.RotateTowards(base.transform.localRotation, Quaternion.identity, 520f * Time.deltaTime);
		}
	}

	// Token: 0x06000B62 RID: 2914 RVA: 0x00002229 File Offset: 0x00000429
	public virtual void WhileSledding()
	{
	}

	// Token: 0x06000B63 RID: 2915 RVA: 0x0000ABB8 File Offset: 0x00008DB8
	public void Cancel()
	{
		this.SetState(false);
	}

	// Token: 0x06000B64 RID: 2916 RVA: 0x0000ABC1 File Offset: 0x00008DC1
	private void SetState(bool isActive)
	{
		this.isSledding = isActive;
		if (!this.isSledding && this.movement.isSledding)
		{
			this.movement.Sled();
		}
		this.itemManager.SetItemInUse(this, isActive);
	}

	// Token: 0x06000B65 RID: 2917 RVA: 0x0003F544 File Offset: 0x0003D744
	public void SetEquipped(bool isEquipped)
	{
		if (this.itemManager == null)
		{
			this.itemManager = Player.itemManager;
		}
		Transform parent = this.GetParent(isEquipped, this.isSledding);
		if (base.transform.parent != parent)
		{
			base.transform.ApplyParent(parent);
		}
	}

	// Token: 0x06000B66 RID: 2918 RVA: 0x0003F598 File Offset: 0x0003D798
	public virtual Transform GetParent(bool isEquipped, bool isSledding)
	{
		Transform transform = null;
		switch (this.anchor)
		{
		case ItemShield.SledAnchor.Standard:
			transform = this.itemManager.shieldSledAnchor;
			break;
		case ItemShield.SledAnchor.Belly:
			transform = this.itemManager.shieldSledBellyAnchor;
			break;
		case ItemShield.SledAnchor.Skate:
			transform = this.itemManager.shieldSkateAnchor;
			break;
		}
		if (!isEquipped)
		{
			return this.itemManager.shieldUnequippedAnchor;
		}
		if (!isSledding)
		{
			return this.itemManager.shieldArmAnchor;
		}
		return transform;
	}

	// Token: 0x06000B67 RID: 2919 RVA: 0x0000ABF8 File Offset: 0x00008DF8
	public void OnRemove()
	{
		if (this.onRemove != null)
		{
			this.onRemove.Invoke();
		}
	}

	// Token: 0x06000B68 RID: 2920 RVA: 0x00002229 File Offset: 0x00000429
	public void SetIndex(int index)
	{
	}

	// Token: 0x06000B69 RID: 2921 RVA: 0x0000AC0D File Offset: 0x00008E0D
	public virtual void OnJump()
	{
		if (this.jumpSound != null)
		{
			this.jumpSound.Play();
		}
	}

	// Token: 0x06000B6A RID: 2922 RVA: 0x0003F60C File Offset: 0x0003D80C
	public virtual void OnLand()
	{
		if (this.landSound != null)
		{
			float num = Mathf.InverseLerp(0f, 10f, Player.movement.velocity.magnitude);
			this.landSound.PlayModified(Mathf.Lerp(0.5f, 1f, num), Mathf.Lerp(0.8f, 1.25f, num));
		}
	}

	// Token: 0x04000E3B RID: 3643
	private PlayerItemManager itemManager;

	// Token: 0x04000E3C RID: 3644
	private PlayerMovement movement;

	// Token: 0x04000E3D RID: 3645
	private bool isSledding;

	// Token: 0x04000E3E RID: 3646
	public UnityEvent onRemove;

	// Token: 0x04000E3F RID: 3647
	public AudioSourceVariance jumpSound;

	// Token: 0x04000E40 RID: 3648
	public AudioSourceVariance landSound;

	// Token: 0x04000E41 RID: 3649
	public ItemShield.SledAnchor anchor;

	// Token: 0x02000259 RID: 601
	public enum SledAnchor
	{
		// Token: 0x04000E43 RID: 3651
		Standard,
		// Token: 0x04000E44 RID: 3652
		Belly,
		// Token: 0x04000E45 RID: 3653
		Skate
	}
}
