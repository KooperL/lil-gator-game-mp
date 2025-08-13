using System;
using UnityEngine;

// Token: 0x0200024F RID: 591
public class ItemNart : MonoBehaviour, IItemBehaviour
{
	// Token: 0x06000B19 RID: 2841 RVA: 0x0000A84D File Offset: 0x00008A4D
	private void Awake()
	{
		this.movement = Player.movement;
		this.itemManager = Player.itemManager;
	}

	// Token: 0x06000B1A RID: 2842 RVA: 0x0000A865 File Offset: 0x00008A65
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

	// Token: 0x06000B1B RID: 2843 RVA: 0x0000A888 File Offset: 0x00008A88
	private void LateUpdate()
	{
		if (this.activated && !this.movement.isModified)
		{
			this.SetState(false);
		}
	}

	// Token: 0x06000B1C RID: 2844 RVA: 0x0000A8A6 File Offset: 0x00008AA6
	public void Cancel()
	{
		this.SetState(false);
	}

	// Token: 0x06000B1D RID: 2845 RVA: 0x0003EBDC File Offset: 0x0003CDDC
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

	// Token: 0x06000B1E RID: 2846 RVA: 0x0003EC74 File Offset: 0x0003CE74
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

	// Token: 0x06000B1F RID: 2847 RVA: 0x00002229 File Offset: 0x00000429
	public void OnRemove()
	{
	}

	// Token: 0x06000B20 RID: 2848 RVA: 0x00002229 File Offset: 0x00000429
	public void SetIndex(int index)
	{
	}

	// Token: 0x04000E09 RID: 3593
	private bool activated;

	// Token: 0x04000E0A RID: 3594
	private PlayerMovement movement;

	// Token: 0x04000E0B RID: 3595
	private PlayerItemManager itemManager;

	// Token: 0x04000E0C RID: 3596
	public bool restrictMovement;

	// Token: 0x04000E0D RID: 3597
	public bool restrictSpeed;

	// Token: 0x04000E0E RID: 3598
	public bool restrictClimbing;

	// Token: 0x04000E0F RID: 3599
	public bool restrictJumping;
}
