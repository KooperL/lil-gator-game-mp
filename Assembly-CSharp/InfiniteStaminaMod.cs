using System;
using UnityEngine;
using UnityEngine.Events;

public class InfiniteStaminaMod : MonoBehaviour
{
	// Token: 0x06000B1F RID: 2847 RVA: 0x0000A81E File Offset: 0x00008A1E
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
