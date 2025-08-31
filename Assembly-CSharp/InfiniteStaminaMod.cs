using System;
using UnityEngine;
using UnityEngine.Events;

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

	public UnityEvent ifInfiniteStamina;

	public UnityEvent ifFiniteStamina;
}
