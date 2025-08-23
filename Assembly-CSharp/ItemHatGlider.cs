using System;
using UnityEngine;

public class ItemHatGlider : MonoBehaviour, IItemBehaviour
{
	// Token: 0x06000B5F RID: 2911 RVA: 0x0000AB1F File Offset: 0x00008D1F
	private void Start()
	{
		this.movement = Player.movement;
		this.itemManager = Player.itemManager;
		this.glider = global::UnityEngine.Object.Instantiate<GameObject>(this.gliderPrefab, this.itemManager.gliderAnchor);
		this.glider.SetActive(false);
	}

	// Token: 0x06000B60 RID: 2912 RVA: 0x00002229 File Offset: 0x00000429
	public void Input(bool isDown, bool isHeld)
	{
	}

	// Token: 0x06000B61 RID: 2913 RVA: 0x00040930 File Offset: 0x0003EB30
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

	// Token: 0x06000B62 RID: 2914 RVA: 0x0000AB5F File Offset: 0x00008D5F
	public void OnRemove()
	{
		if (this.glider != null)
		{
			global::UnityEngine.Object.Destroy(this.glider);
		}
		this.itemManager.bareHead.SetActive(false);
	}

	// Token: 0x06000B63 RID: 2915 RVA: 0x00002229 File Offset: 0x00000429
	public void Cancel()
	{
	}

	// Token: 0x06000B64 RID: 2916 RVA: 0x00002229 File Offset: 0x00000429
	public void SetIndex(int index)
	{
	}

	public GameObject gliderPrefab;

	private GameObject glider;

	private PlayerMovement movement;

	private PlayerItemManager itemManager;
}
