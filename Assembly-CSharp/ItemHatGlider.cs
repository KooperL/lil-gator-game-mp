using System;
using UnityEngine;

// Token: 0x020001CB RID: 459
public class ItemHatGlider : MonoBehaviour, IItemBehaviour
{
	// Token: 0x0600097B RID: 2427 RVA: 0x0002CC2E File Offset: 0x0002AE2E
	private void Start()
	{
		this.movement = Player.movement;
		this.itemManager = Player.itemManager;
		this.glider = Object.Instantiate<GameObject>(this.gliderPrefab, this.itemManager.gliderAnchor);
		this.glider.SetActive(false);
	}

	// Token: 0x0600097C RID: 2428 RVA: 0x0002CC6E File Offset: 0x0002AE6E
	public void Input(bool isDown, bool isHeld)
	{
	}

	// Token: 0x0600097D RID: 2429 RVA: 0x0002CC70 File Offset: 0x0002AE70
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

	// Token: 0x0600097E RID: 2430 RVA: 0x0002CCE9 File Offset: 0x0002AEE9
	public void OnRemove()
	{
		if (this.glider != null)
		{
			Object.Destroy(this.glider);
		}
		this.itemManager.bareHead.SetActive(false);
	}

	// Token: 0x0600097F RID: 2431 RVA: 0x0002CD15 File Offset: 0x0002AF15
	public void Cancel()
	{
	}

	// Token: 0x06000980 RID: 2432 RVA: 0x0002CD17 File Offset: 0x0002AF17
	public void SetIndex(int index)
	{
	}

	// Token: 0x04000BDF RID: 3039
	public GameObject gliderPrefab;

	// Token: 0x04000BE0 RID: 3040
	private GameObject glider;

	// Token: 0x04000BE1 RID: 3041
	private PlayerMovement movement;

	// Token: 0x04000BE2 RID: 3042
	private PlayerItemManager itemManager;
}
