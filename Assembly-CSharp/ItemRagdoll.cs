using System;
using UnityEngine;

public class ItemRagdoll : MonoBehaviour, IItemBehaviour
{
	// (get) Token: 0x06000B6F RID: 2927 RVA: 0x0000ABED File Offset: 0x00008DED
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

	// Token: 0x06000B70 RID: 2928 RVA: 0x0000ABFA File Offset: 0x00008DFA
	private void Awake()
	{
		this.itemManager = Player.itemManager;
		this.movement = Player.movement;
	}

	// Token: 0x06000B71 RID: 2929 RVA: 0x0000AC12 File Offset: 0x00008E12
	public void Input(bool isDown, bool isHeld)
	{
		if (isDown)
		{
			this.SetState(!this.isActivated);
		}
	}

	// Token: 0x06000B72 RID: 2930 RVA: 0x0000AC26 File Offset: 0x00008E26
	private void LateUpdate()
	{
		if (this.isActivated && (!this.movement.isRagdolling || Player.itemManager.itemInUse != this))
		{
			this.SetState(false);
		}
	}

	// Token: 0x06000B73 RID: 2931 RVA: 0x0000AC51 File Offset: 0x00008E51
	public void Cancel()
	{
		this.SetState(false);
	}

	// Token: 0x06000B74 RID: 2932 RVA: 0x00040ABC File Offset: 0x0003ECBC
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

	// Token: 0x06000B75 RID: 2933 RVA: 0x00040B1C File Offset: 0x0003ED1C
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

	// Token: 0x06000B76 RID: 2934 RVA: 0x00002229 File Offset: 0x00000429
	public void OnRemove()
	{
	}

	// Token: 0x06000B77 RID: 2935 RVA: 0x0000AC5A File Offset: 0x00008E5A
	public void SetIndex(int index)
	{
		if (index == 1)
		{
			this.isOnRight = true;
		}
	}

	private PlayerItemManager itemManager;

	private PlayerMovement movement;

	private bool isActivated;

	private bool isOnRight;
}
