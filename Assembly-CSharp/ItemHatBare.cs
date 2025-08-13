using System;
using UnityEngine;

// Token: 0x0200024D RID: 589
public class ItemHatBare : MonoBehaviour, IItemBehaviour
{
	// Token: 0x06000B0B RID: 2827 RVA: 0x0000A7B0 File Offset: 0x000089B0
	private void Start()
	{
		this.itemManager = Player.itemManager;
		this.itemManager.bareHead.SetActive(true);
	}

	// Token: 0x06000B0C RID: 2828 RVA: 0x0000A7CE File Offset: 0x000089CE
	public void OnRemove()
	{
		this.itemManager.bareHead.SetActive(false);
	}

	// Token: 0x06000B0D RID: 2829 RVA: 0x00002229 File Offset: 0x00000429
	public void Cancel()
	{
	}

	// Token: 0x06000B0E RID: 2830 RVA: 0x00002229 File Offset: 0x00000429
	public void Input(bool isDown, bool isHeld)
	{
	}

	// Token: 0x06000B0F RID: 2831 RVA: 0x00002229 File Offset: 0x00000429
	public void SetEquipped(bool isEquipped)
	{
	}

	// Token: 0x06000B10 RID: 2832 RVA: 0x00002229 File Offset: 0x00000429
	public void SetIndex(int index)
	{
	}

	// Token: 0x04000E04 RID: 3588
	private PlayerItemManager itemManager;
}
