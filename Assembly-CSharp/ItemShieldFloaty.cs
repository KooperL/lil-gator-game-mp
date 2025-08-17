using System;
using UnityEngine;
using UnityEngine.Events;

public class ItemShieldFloaty : MonoBehaviour
{
	// Token: 0x06000BB8 RID: 3000 RVA: 0x0000AF30 File Offset: 0x00009130
	private void Start()
	{
		Player.movement.sledAlwaysFloats = true;
		base.GetComponent<ItemShield>().onRemove.AddListener(new UnityAction(this.OnRemove));
	}

	// Token: 0x06000BB9 RID: 3001 RVA: 0x0000AF59 File Offset: 0x00009159
	private void OnRemove()
	{
		Player.movement.sledAlwaysFloats = false;
	}
}
