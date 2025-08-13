using System;
using UnityEngine;

// Token: 0x02000250 RID: 592
public class ItemRagdoll : MonoBehaviour, IItemBehaviour
{
	// Token: 0x17000108 RID: 264
	// (get) Token: 0x06000B22 RID: 2850 RVA: 0x0000A8AF File Offset: 0x00008AAF
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

	// Token: 0x06000B23 RID: 2851 RVA: 0x0000A8BC File Offset: 0x00008ABC
	private void Awake()
	{
		this.itemManager = Player.itemManager;
		this.movement = Player.movement;
	}

	// Token: 0x06000B24 RID: 2852 RVA: 0x0000A8D4 File Offset: 0x00008AD4
	public void Input(bool isDown, bool isHeld)
	{
		if (isDown)
		{
			this.SetState(!this.isActivated);
		}
	}

	// Token: 0x06000B25 RID: 2853 RVA: 0x0000A8E8 File Offset: 0x00008AE8
	private void LateUpdate()
	{
		if (this.isActivated && (!this.movement.isRagdolling || Player.itemManager.itemInUse != this))
		{
			this.SetState(false);
		}
	}

	// Token: 0x06000B26 RID: 2854 RVA: 0x0000A913 File Offset: 0x00008B13
	public void Cancel()
	{
		this.SetState(false);
	}

	// Token: 0x06000B27 RID: 2855 RVA: 0x0003ECEC File Offset: 0x0003CEEC
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

	// Token: 0x06000B28 RID: 2856 RVA: 0x0003ED4C File Offset: 0x0003CF4C
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

	// Token: 0x06000B29 RID: 2857 RVA: 0x00002229 File Offset: 0x00000429
	public void OnRemove()
	{
	}

	// Token: 0x06000B2A RID: 2858 RVA: 0x0000A91C File Offset: 0x00008B1C
	public void SetIndex(int index)
	{
		if (index == 1)
		{
			this.isOnRight = true;
		}
	}

	// Token: 0x04000E10 RID: 3600
	private PlayerItemManager itemManager;

	// Token: 0x04000E11 RID: 3601
	private PlayerMovement movement;

	// Token: 0x04000E12 RID: 3602
	private bool isActivated;

	// Token: 0x04000E13 RID: 3603
	private bool isOnRight;
}
