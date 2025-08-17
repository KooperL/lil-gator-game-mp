using System;
using UnityEngine;

public class ItemHatBare : MonoBehaviour, IItemBehaviour
{
	// Token: 0x06000B57 RID: 2903 RVA: 0x0000AAE4 File Offset: 0x00008CE4
	private void Start()
	{
		this.itemManager = Player.itemManager;
		this.itemManager.bareHead.SetActive(true);
	}

	// Token: 0x06000B58 RID: 2904 RVA: 0x0000AB02 File Offset: 0x00008D02
	public void OnRemove()
	{
		this.itemManager.bareHead.SetActive(false);
	}

	// Token: 0x06000B59 RID: 2905 RVA: 0x00002229 File Offset: 0x00000429
	public void Cancel()
	{
	}

	// Token: 0x06000B5A RID: 2906 RVA: 0x00002229 File Offset: 0x00000429
	public void Input(bool isDown, bool isHeld)
	{
	}

	// Token: 0x06000B5B RID: 2907 RVA: 0x00002229 File Offset: 0x00000429
	public void SetEquipped(bool isEquipped)
	{
	}

	// Token: 0x06000B5C RID: 2908 RVA: 0x00002229 File Offset: 0x00000429
	public void SetIndex(int index)
	{
	}

	private PlayerItemManager itemManager;
}
