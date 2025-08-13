using System;
using UnityEngine;

// Token: 0x020001CC RID: 460
public class ItemNart : MonoBehaviour, IItemBehaviour
{
	// Token: 0x06000982 RID: 2434 RVA: 0x0002CD21 File Offset: 0x0002AF21
	private void Awake()
	{
		this.movement = Player.movement;
		this.itemManager = Player.itemManager;
	}

	// Token: 0x06000983 RID: 2435 RVA: 0x0002CD39 File Offset: 0x0002AF39
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

	// Token: 0x06000984 RID: 2436 RVA: 0x0002CD5C File Offset: 0x0002AF5C
	private void LateUpdate()
	{
		if (this.activated && !this.movement.isModified)
		{
			this.SetState(false);
		}
	}

	// Token: 0x06000985 RID: 2437 RVA: 0x0002CD7A File Offset: 0x0002AF7A
	public void Cancel()
	{
		this.SetState(false);
	}

	// Token: 0x06000986 RID: 2438 RVA: 0x0002CD84 File Offset: 0x0002AF84
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

	// Token: 0x06000987 RID: 2439 RVA: 0x0002CE1C File Offset: 0x0002B01C
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

	// Token: 0x06000988 RID: 2440 RVA: 0x0002CE94 File Offset: 0x0002B094
	public void OnRemove()
	{
	}

	// Token: 0x06000989 RID: 2441 RVA: 0x0002CE96 File Offset: 0x0002B096
	public void SetIndex(int index)
	{
	}

	// Token: 0x04000BE3 RID: 3043
	private bool activated;

	// Token: 0x04000BE4 RID: 3044
	private PlayerMovement movement;

	// Token: 0x04000BE5 RID: 3045
	private PlayerItemManager itemManager;

	// Token: 0x04000BE6 RID: 3046
	public bool restrictMovement;

	// Token: 0x04000BE7 RID: 3047
	public bool restrictSpeed;

	// Token: 0x04000BE8 RID: 3048
	public bool restrictClimbing;

	// Token: 0x04000BE9 RID: 3049
	public bool restrictJumping;
}
