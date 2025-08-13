using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020001BD RID: 445
public class InfiniteStaminaMod : MonoBehaviour
{
	// Token: 0x0600093C RID: 2364 RVA: 0x0002BE6F File Offset: 0x0002A06F
	public void Start()
	{
		if (ItemManager.HasInfiniteStamina)
		{
			this.ifInfiniteStamina.Invoke();
			return;
		}
		this.ifFiniteStamina.Invoke();
	}

	// Token: 0x04000B99 RID: 2969
	public UnityEvent ifInfiniteStamina;

	// Token: 0x04000B9A RID: 2970
	public UnityEvent ifFiniteStamina;
}
