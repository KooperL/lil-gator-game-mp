using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200023E RID: 574
public class InfiniteStaminaMod : MonoBehaviour
{
	// Token: 0x06000AD3 RID: 2771 RVA: 0x0000A4EA File Offset: 0x000086EA
	public void Start()
	{
		if (ItemManager.HasInfiniteStamina)
		{
			this.ifInfiniteStamina.Invoke();
			return;
		}
		this.ifFiniteStamina.Invoke();
	}

	// Token: 0x04000DB5 RID: 3509
	public UnityEvent ifInfiniteStamina;

	// Token: 0x04000DB6 RID: 3510
	public UnityEvent ifFiniteStamina;
}
