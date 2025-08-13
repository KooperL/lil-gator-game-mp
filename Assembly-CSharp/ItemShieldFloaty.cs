using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200025A RID: 602
public class ItemShieldFloaty : MonoBehaviour
{
	// Token: 0x06000B6C RID: 2924 RVA: 0x0000AC28 File Offset: 0x00008E28
	private void Start()
	{
		Player.movement.sledAlwaysFloats = true;
		base.GetComponent<ItemShield>().onRemove.AddListener(new UnityAction(this.OnRemove));
	}

	// Token: 0x06000B6D RID: 2925 RVA: 0x0000AC51 File Offset: 0x00008E51
	private void OnRemove()
	{
		Player.movement.sledAlwaysFloats = false;
	}
}
