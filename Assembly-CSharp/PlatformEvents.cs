using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020001A9 RID: 425
public class PlatformEvents : MonoBehaviour
{
	// Token: 0x060008C4 RID: 2244 RVA: 0x0002969C File Offset: 0x0002789C
	private void Start()
	{
		if (this.pcEvent != null)
		{
			this.pcEvent.Invoke();
		}
	}

	// Token: 0x04000AD3 RID: 2771
	public UnityEvent debugEvent;

	// Token: 0x04000AD4 RID: 2772
	public UnityEvent nxEvent;

	// Token: 0x04000AD5 RID: 2773
	public UnityEvent pcEvent;
}
