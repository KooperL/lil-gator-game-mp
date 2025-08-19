using System;
using UnityEngine;
using UnityEngine.Events;

public class ItemShield : MonoBehaviour, IItemBehaviour
{
	// Token: 0x06000BAA RID: 2986 RVA: 0x0000AE3D File Offset: 0x0000903D
	private void Awake()
	{
		this.itemManager = Player.itemManager;
		this.movement = Player.movement;
		this.movement.shield = this;
	}

	// Token: 0x06000BAB RID: 2987 RVA: 0x0000AE61 File Offset: 0x00009061
	private void OnDestroy()
	{
		if (this.movement != null && this.movement.shield == this)
		{
			this.movement.shield = null;
		}
	}

	// Token: 0x06000BAC RID: 2988 RVA: 0x0000AE90 File Offset: 0x00009090
	public void Input(bool isDown, bool isHeld)
	{
		if (isDown)
		{
			this.isSledding = this.movement.Sled();
			this.itemManager.SetEquippedState(this.isSledding ? PlayerItemManager.EquippedState.ShieldSled : PlayerItemManager.EquippedState.SwordAndShield, false);
			this.SetState(this.isSledding);
		}
	}

	// Token: 0x06000BAD RID: 2989 RVA: 0x00040FF0 File Offset: 0x0003F1F0
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

	// Token: 0x06000BAE RID: 2990 RVA: 0x00002229 File Offset: 0x00000429
	public virtual void WhileSledding()
	{
	}

	// Token: 0x06000BAF RID: 2991 RVA: 0x0000AECA File Offset: 0x000090CA
	public void Cancel()
	{
		this.SetState(false);
	}

	// Token: 0x06000BB0 RID: 2992 RVA: 0x0000AED3 File Offset: 0x000090D3
	private void SetState(bool isActive)
	{
		this.isSledding = isActive;
		if (!this.isSledding && this.movement.isSledding)
		{
			this.movement.Sled();
		}
		this.itemManager.SetItemInUse(this, isActive);
	}

	// Token: 0x06000BB1 RID: 2993 RVA: 0x000410A8 File Offset: 0x0003F2A8
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

	// Token: 0x06000BB2 RID: 2994 RVA: 0x000410FC File Offset: 0x0003F2FC
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

	// Token: 0x06000BB3 RID: 2995 RVA: 0x0000AF0A File Offset: 0x0000910A
	public void OnRemove()
	{
		if (this.onRemove != null)
		{
			this.onRemove.Invoke();
		}
	}

	// Token: 0x06000BB4 RID: 2996 RVA: 0x00002229 File Offset: 0x00000429
	public void SetIndex(int index)
	{
	}

	// Token: 0x06000BB5 RID: 2997 RVA: 0x0000AF1F File Offset: 0x0000911F
	public virtual void OnJump()
	{
		if (this.jumpSound != null)
		{
			this.jumpSound.Play();
		}
	}

	// Token: 0x06000BB6 RID: 2998 RVA: 0x00041170 File Offset: 0x0003F370
	public virtual void OnLand()
	{
		if (this.landSound != null)
		{
			float num = Mathf.InverseLerp(0f, 10f, Player.movement.velocity.magnitude);
			this.landSound.PlayModified(Mathf.Lerp(0.5f, 1f, num), Mathf.Lerp(0.8f, 1.25f, num));
		}
	}

	private PlayerItemManager itemManager;

	private PlayerMovement movement;

	private bool isSledding;

	public UnityEvent onRemove;

	public AudioSourceVariance jumpSound;

	public AudioSourceVariance landSound;

	public ItemShield.SledAnchor anchor;

	public enum SledAnchor
	{
		Standard,
		Belly,
		Skate
	}
}
