using System;
using UnityEngine;

public class ItemHatBare : MonoBehaviour, IItemBehaviour
{
	// Token: 0x06000974 RID: 2420 RVA: 0x0002CBED File Offset: 0x0002ADED
	private void Start()
	{
		this.itemManager = Player.itemManager;
		this.itemManager.bareHead.SetActive(true);
	}

	// Token: 0x06000975 RID: 2421 RVA: 0x0002CC0B File Offset: 0x0002AE0B
	public void OnRemove()
	{
		this.itemManager.bareHead.SetActive(false);
	}

	// Token: 0x06000976 RID: 2422 RVA: 0x0002CC1E File Offset: 0x0002AE1E
	public void Cancel()
	{
	}

	// Token: 0x06000977 RID: 2423 RVA: 0x0002CC20 File Offset: 0x0002AE20
	public void Input(bool isDown, bool isHeld)
	{
	}

	// Token: 0x06000978 RID: 2424 RVA: 0x0002CC22 File Offset: 0x0002AE22
	public void SetEquipped(bool isEquipped)
	{
	}

	// Token: 0x06000979 RID: 2425 RVA: 0x0002CC24 File Offset: 0x0002AE24
	public void SetIndex(int index)
	{
	}

	private PlayerItemManager itemManager;
}
