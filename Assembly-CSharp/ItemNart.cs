using System;
using UnityEngine;

public class ItemNart : MonoBehaviour, IItemBehaviour
{
	// Token: 0x06000B65 RID: 2917 RVA: 0x0000AB6C File Offset: 0x00008D6C
	private void Awake()
	{
		this.movement = Player.movement;
		this.itemManager = Player.itemManager;
	}

	// Token: 0x06000B66 RID: 2918 RVA: 0x0000AB84 File Offset: 0x00008D84
	public void Input(bool isDown, bool isHeld)
	{
		if (Game.HasControl && isDown)
		{
			if (!this.activated)
			{
				this.SetState(true);
				return;
			}
			this.SetState(false);
		}
	}

	// Token: 0x06000B67 RID: 2919 RVA: 0x0000ABA7 File Offset: 0x00008DA7
	private void LateUpdate()
	{
		if (this.activated && !this.movement.isModified)
		{
			this.SetState(false);
		}
	}

	// Token: 0x06000B68 RID: 2920 RVA: 0x0000ABC5 File Offset: 0x00008DC5
	public void Cancel()
	{
		this.SetState(false);
	}

	// Token: 0x06000B69 RID: 2921 RVA: 0x00040550 File Offset: 0x0003E750
	private void SetState(bool isActive)
	{
		if (this.activated == isActive)
		{
			return;
		}
		this.activated = isActive;
		if (isActive)
		{
			this.itemManager.itemInUse = this;
		}
		else if (this.itemManager.itemInUse == this)
		{
			this.itemManager.itemInUse = null;
		}
		if (isActive)
		{
			this.movement.isModified = isActive;
			this.movement.modForceMovement = true;
			this.movement.modSpeed = 8f;
		}
		else
		{
			this.movement.ClearMods();
		}
		this.itemManager.animator.SetBool("Nart", isActive);
	}

	// Token: 0x06000B6A RID: 2922 RVA: 0x000405E8 File Offset: 0x0003E7E8
	public void SetEquipped(bool isEquipped)
	{
		Transform transform = (isEquipped ? this.itemManager.leftHandAnchor : this.itemManager.hipAnchor);
		if (base.transform.parent != transform)
		{
			base.transform.parent = transform;
			base.transform.localPosition = Vector3.zero;
			base.transform.localRotation = Quaternion.identity;
			base.transform.localScale = Vector3.one;
		}
	}

	// Token: 0x06000B6B RID: 2923 RVA: 0x00002229 File Offset: 0x00000429
	public void OnRemove()
	{
	}

	// Token: 0x06000B6C RID: 2924 RVA: 0x00002229 File Offset: 0x00000429
	public void SetIndex(int index)
	{
	}

	private bool activated;

	private PlayerMovement movement;

	private PlayerItemManager itemManager;

	public bool restrictMovement;

	public bool restrictSpeed;

	public bool restrictClimbing;

	public bool restrictJumping;
}
