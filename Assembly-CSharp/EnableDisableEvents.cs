using System;
using UnityEngine;
using UnityEngine.Events;

public class EnableDisableEvents : MonoBehaviour
{
	// Token: 0x06000626 RID: 1574 RVA: 0x0000662F File Offset: 0x0000482F
	private void OnEnable()
	{
		this.onEnable.Invoke();
	}

	// Token: 0x06000627 RID: 1575 RVA: 0x0000663C File Offset: 0x0000483C
	private void OnDisable()
	{
		this.onDisable.Invoke();
	}

	public UnityEvent onEnable;

	public UnityEvent onDisable;
}
