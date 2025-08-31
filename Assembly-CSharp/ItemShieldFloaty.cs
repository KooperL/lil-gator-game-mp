using System;
using UnityEngine;
using UnityEngine.Events;

public class ItemShieldFloaty : MonoBehaviour
{
	// Token: 0x060009C3 RID: 2499 RVA: 0x0002D705 File Offset: 0x0002B905
	private void Start()
	{
		Player.movement.sledAlwaysFloats = true;
		base.GetComponent<ItemShield>().onRemove.AddListener(new UnityAction(this.OnRemove));
	}

	// Token: 0x060009C4 RID: 2500 RVA: 0x0002D72E File Offset: 0x0002B92E
	private void OnRemove()
	{
		Player.movement.sledAlwaysFloats = false;
	}
}
