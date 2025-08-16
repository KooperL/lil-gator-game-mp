using System;
using UnityEngine;
using UnityEngine.Events;

public class ItemShieldFloaty : MonoBehaviour
{
	// Token: 0x06000BB8 RID: 3000 RVA: 0x0000AF1B File Offset: 0x0000911B
	private void Start()
	{
		Player.movement.sledAlwaysFloats = true;
		base.GetComponent<ItemShield>().onRemove.AddListener(new UnityAction(this.OnRemove));
	}

	// Token: 0x06000BB9 RID: 3001 RVA: 0x0000AF44 File Offset: 0x00009144
	private void OnRemove()
	{
		Player.movement.sledAlwaysFloats = false;
	}
}
