using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000223 RID: 547
public class PlatformEvents : MonoBehaviour
{
	// Token: 0x06000A45 RID: 2629 RVA: 0x00009DF9 File Offset: 0x00007FF9
	private void Start()
	{
		if (this.pcEvent != null)
		{
			this.pcEvent.Invoke();
		}
	}

	// Token: 0x04000CD3 RID: 3283
	public UnityEvent debugEvent;

	// Token: 0x04000CD4 RID: 3284
	public UnityEvent nxEvent;

	// Token: 0x04000CD5 RID: 3285
	public UnityEvent pcEvent;
}
