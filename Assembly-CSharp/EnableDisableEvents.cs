using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000EB RID: 235
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

	// Token: 0x040006B2 RID: 1714
	public UnityEvent onEnable;

	// Token: 0x040006B3 RID: 1715
	public UnityEvent onDisable;
}
