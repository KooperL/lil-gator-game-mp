using System;
using UnityEngine;

// Token: 0x0200024E RID: 590
public class ItemHatGlider : MonoBehaviour, IItemBehaviour
{
	// Token: 0x06000B12 RID: 2834 RVA: 0x0000A7E1 File Offset: 0x000089E1
	private void Start()
	{
		this.movement = Player.movement;
		this.itemManager = Player.itemManager;
		this.glider = Object.Instantiate<GameObject>(this.gliderPrefab, this.itemManager.gliderAnchor);
		this.glider.SetActive(false);
	}

	// Token: 0x06000B13 RID: 2835 RVA: 0x00002229 File Offset: 0x00000429
	public void Input(bool isDown, bool isHeld)
	{
	}

	// Token: 0x06000B14 RID: 2836 RVA: 0x0003EB60 File Offset: 0x0003CD60
	public void SetEquipped(bool isEquipped)
	{
		if (this.itemManager == null)
		{
			this.itemManager = Player.itemManager;
		}
		if (this.movement == null)
		{
			this.movement = Player.movement;
		}
		if (this.glider != null)
		{
			this.glider.SetActive(isEquipped);
		}
		base.gameObject.SetActive(!isEquipped);
		this.itemManager.bareHead.SetActive(isEquipped);
	}

	// Token: 0x06000B15 RID: 2837 RVA: 0x0000A821 File Offset: 0x00008A21
	public void OnRemove()
	{
		if (this.glider != null)
		{
			Object.Destroy(this.glider);
		}
		this.itemManager.bareHead.SetActive(false);
	}

	// Token: 0x06000B16 RID: 2838 RVA: 0x00002229 File Offset: 0x00000429
	public void Cancel()
	{
	}

	// Token: 0x06000B17 RID: 2839 RVA: 0x00002229 File Offset: 0x00000429
	public void SetIndex(int index)
	{
	}

	// Token: 0x04000E05 RID: 3589
	public GameObject gliderPrefab;

	// Token: 0x04000E06 RID: 3590
	private GameObject glider;

	// Token: 0x04000E07 RID: 3591
	private PlayerMovement movement;

	// Token: 0x04000E08 RID: 3592
	private PlayerItemManager itemManager;
}
