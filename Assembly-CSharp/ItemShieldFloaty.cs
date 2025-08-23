using System;
using UnityEngine;
using UnityEngine.Events;

public class ItemShieldFloaty : MonoBehaviour
{
	// Token: 0x06000BB9 RID: 3001 RVA: 0x0000AF3A File Offset: 0x0000913A
	private void Start()
	{
		Player.movement.sledAlwaysFloats = true;
		base.GetComponent<ItemShield>().onRemove.AddListener(new UnityAction(this.OnRemove));
	}

	// Token: 0x06000BBA RID: 3002 RVA: 0x0000AF63 File Offset: 0x00009163
	private void OnRemove()
	{
		Player.movement.sledAlwaysFloats = false;
	}
}
