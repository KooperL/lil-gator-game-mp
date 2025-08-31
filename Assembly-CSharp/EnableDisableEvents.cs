using System;
using UnityEngine;
using UnityEngine.Events;

public class EnableDisableEvents : MonoBehaviour
{
	// Token: 0x060004E0 RID: 1248 RVA: 0x0001A707 File Offset: 0x00018907
	private void OnEnable()
	{
		this.onEnable.Invoke();
	}

	// Token: 0x060004E1 RID: 1249 RVA: 0x0001A714 File Offset: 0x00018914
	private void OnDisable()
	{
		this.onDisable.Invoke();
	}

	public UnityEvent onEnable;

	public UnityEvent onDisable;
}
