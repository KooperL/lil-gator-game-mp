using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200013C RID: 316
public class EnableDisableEvents : MonoBehaviour
{
	// Token: 0x060005EC RID: 1516 RVA: 0x00006369 File Offset: 0x00004569
	private void OnEnable()
	{
		this.onEnable.Invoke();
	}

	// Token: 0x060005ED RID: 1517 RVA: 0x00006376 File Offset: 0x00004576
	private void OnDisable()
	{
		this.onDisable.Invoke();
	}

	// Token: 0x040007F4 RID: 2036
	public UnityEvent onEnable;

	// Token: 0x040007F5 RID: 2037
	public UnityEvent onDisable;
}
