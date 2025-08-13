using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000322 RID: 802
public class TriggerEvent : MonoBehaviour
{
	// Token: 0x06000FAB RID: 4011 RVA: 0x0000DA16 File Offset: 0x0000BC16
	public void OnTriggerEnter(Collider other)
	{
		if (this.onEnter && this.unityEvent != null)
		{
			this.unityEvent.Invoke();
		}
	}

	// Token: 0x06000FAC RID: 4012 RVA: 0x0000DA33 File Offset: 0x0000BC33
	public void OnTriggerStay(Collider other)
	{
		if (this.onStay && this.unityEvent != null)
		{
			this.unityEvent.Invoke();
		}
	}

	// Token: 0x06000FAD RID: 4013 RVA: 0x0000DA50 File Offset: 0x0000BC50
	public void OnTriggerExit(Collider other)
	{
		if (this.onExit && this.unityEvent != null)
		{
			this.unityEvent.Invoke();
		}
	}

	// Token: 0x04001430 RID: 5168
	public UnityEvent unityEvent;

	// Token: 0x04001431 RID: 5169
	public bool onEnter = true;

	// Token: 0x04001432 RID: 5170
	public bool onStay;

	// Token: 0x04001433 RID: 5171
	public bool onExit;
}
