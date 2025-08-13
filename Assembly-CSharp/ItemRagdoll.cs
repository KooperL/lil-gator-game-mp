using System;
using UnityEngine;

// Token: 0x020001CD RID: 461
public class ItemRagdoll : MonoBehaviour, IItemBehaviour
{
	// Token: 0x17000080 RID: 128
	// (get) Token: 0x0600098B RID: 2443 RVA: 0x0002CEA0 File Offset: 0x0002B0A0
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

	// Token: 0x0600098C RID: 2444 RVA: 0x0002CEAD File Offset: 0x0002B0AD
	private void Awake()
	{
		this.itemManager = Player.itemManager;
		this.movement = Player.movement;
	}

	// Token: 0x0600098D RID: 2445 RVA: 0x0002CEC5 File Offset: 0x0002B0C5
	public void Input(bool isDown, bool isHeld)
	{
		if (isDown)
		{
			this.SetState(!this.isActivated);
		}
	}

	// Token: 0x0600098E RID: 2446 RVA: 0x0002CED9 File Offset: 0x0002B0D9
	private void LateUpdate()
	{
		if (this.isActivated && (!this.movement.isRagdolling || Player.itemManager.itemInUse != this))
		{
			this.SetState(false);
		}
	}

	// Token: 0x0600098F RID: 2447 RVA: 0x0002CF04 File Offset: 0x0002B104
	public void Cancel()
	{
		this.SetState(false);
	}

	// Token: 0x06000990 RID: 2448 RVA: 0x0002CF10 File Offset: 0x0002B110
	private void SetState(bool isActive)
	{
		this.isActivated = isActive;
		bool flag = Player.itemManager.itemInUse == this;
		this.itemManager.SetItemInUse(this, isActive);
		if (isActive)
		{
			Player.itemManager.SetEquippedState(this.EquippedState, false);
			this.movement.Ragdoll();
			return;
		}
		if (flag)
		{
			this.movement.ClearMods();
		}
	}

	// Token: 0x06000991 RID: 2449 RVA: 0x0002CF70 File Offset: 0x0002B170
	public void SetEquipped(bool isEquipped)
	{
		Transform transform = (this.isOnRight ? this.itemManager.hipAnchor_r : this.itemManager.hipAnchor);
		if (base.transform.parent != transform)
		{
			base.transform.parent = transform;
			base.transform.localPosition = Vector3.zero;
			base.transform.localRotation = Quaternion.identity;
			base.transform.localScale = Vector3.one;
		}
	}

	// Token: 0x06000992 RID: 2450 RVA: 0x0002CFED File Offset: 0x0002B1ED
	public void OnRemove()
	{
	}

	// Token: 0x06000993 RID: 2451 RVA: 0x0002CFEF File Offset: 0x0002B1EF
	public void SetIndex(int index)
	{
		if (index == 1)
		{
			this.isOnRight = true;
		}
	}

	// Token: 0x04000BEA RID: 3050
	private PlayerItemManager itemManager;

	// Token: 0x04000BEB RID: 3051
	private PlayerMovement movement;

	// Token: 0x04000BEC RID: 3052
	private bool isActivated;

	// Token: 0x04000BED RID: 3053
	private bool isOnRight;
}
