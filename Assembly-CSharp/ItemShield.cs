using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020001D2 RID: 466
public class ItemShield : MonoBehaviour, IItemBehaviour
{
	// Token: 0x060009B5 RID: 2485 RVA: 0x0002D41B File Offset: 0x0002B61B
	private void Awake()
	{
		this.itemManager = Player.itemManager;
		this.movement = Player.movement;
		this.movement.shield = this;
	}

	// Token: 0x060009B6 RID: 2486 RVA: 0x0002D43F File Offset: 0x0002B63F
	private void OnDestroy()
	{
		if (this.movement != null && this.movement.shield == this)
		{
			this.movement.shield = null;
		}
	}

	// Token: 0x060009B7 RID: 2487 RVA: 0x0002D46E File Offset: 0x0002B66E
	public void Input(bool isDown, bool isHeld)
	{
		if (isDown)
		{
			this.isSledding = this.movement.Sled();
			this.itemManager.SetEquippedState(this.isSledding ? PlayerItemManager.EquippedState.ShieldSled : PlayerItemManager.EquippedState.SwordAndShield, false);
			this.SetState(this.isSledding);
		}
	}

	// Token: 0x060009B8 RID: 2488 RVA: 0x0002D4A8 File Offset: 0x0002B6A8
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

	// Token: 0x060009B9 RID: 2489 RVA: 0x0002D55D File Offset: 0x0002B75D
	public virtual void WhileSledding()
	{
	}

	// Token: 0x060009BA RID: 2490 RVA: 0x0002D55F File Offset: 0x0002B75F
	public void Cancel()
	{
		this.SetState(false);
	}

	// Token: 0x060009BB RID: 2491 RVA: 0x0002D568 File Offset: 0x0002B768
	private void SetState(bool isActive)
	{
		this.isSledding = isActive;
		if (!this.isSledding && this.movement.isSledding)
		{
			this.movement.Sled();
		}
		this.itemManager.SetItemInUse(this, isActive);
	}

	// Token: 0x060009BC RID: 2492 RVA: 0x0002D5A0 File Offset: 0x0002B7A0
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

	// Token: 0x060009BD RID: 2493 RVA: 0x0002D5F4 File Offset: 0x0002B7F4
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

	// Token: 0x060009BE RID: 2494 RVA: 0x0002D665 File Offset: 0x0002B865
	public void OnRemove()
	{
		if (this.onRemove != null)
		{
			this.onRemove.Invoke();
		}
	}

	// Token: 0x060009BF RID: 2495 RVA: 0x0002D67A File Offset: 0x0002B87A
	public void SetIndex(int index)
	{
	}

	// Token: 0x060009C0 RID: 2496 RVA: 0x0002D67C File Offset: 0x0002B87C
	public virtual void OnJump()
	{
		if (this.jumpSound != null)
		{
			this.jumpSound.Play();
		}
	}

	// Token: 0x060009C1 RID: 2497 RVA: 0x0002D698 File Offset: 0x0002B898
	public virtual void OnLand()
	{
		if (this.landSound != null)
		{
			float num = Mathf.InverseLerp(0f, 10f, Player.movement.velocity.magnitude);
			this.landSound.PlayModified(Mathf.Lerp(0.5f, 1f, num), Mathf.Lerp(0.8f, 1.25f, num));
		}
	}

	// Token: 0x04000C07 RID: 3079
	private PlayerItemManager itemManager;

	// Token: 0x04000C08 RID: 3080
	private PlayerMovement movement;

	// Token: 0x04000C09 RID: 3081
	private bool isSledding;

	// Token: 0x04000C0A RID: 3082
	public UnityEvent onRemove;

	// Token: 0x04000C0B RID: 3083
	public AudioSourceVariance jumpSound;

	// Token: 0x04000C0C RID: 3084
	public AudioSourceVariance landSound;

	// Token: 0x04000C0D RID: 3085
	public ItemShield.SledAnchor anchor;

	// Token: 0x020003E3 RID: 995
	public enum SledAnchor
	{
		// Token: 0x04001C61 RID: 7265
		Standard,
		// Token: 0x04001C62 RID: 7266
		Belly,
		// Token: 0x04001C63 RID: 7267
		Skate
	}
}
