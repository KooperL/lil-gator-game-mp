using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200025C RID: 604
public class TriggerEvent : MonoBehaviour
{
	// Token: 0x06000CFE RID: 3326 RVA: 0x0003EB0C File Offset: 0x0003CD0C
	public void OnTriggerEnter(Collider other)
	{
		if (this.onEnter && this.unityEvent != null)
		{
			this.unityEvent.Invoke();
		}
	}

	// Token: 0x06000CFF RID: 3327 RVA: 0x0003EB29 File Offset: 0x0003CD29
	public void OnTriggerStay(Collider other)
	{
		if (this.onStay && this.unityEvent != null)
		{
			this.unityEvent.Invoke();
		}
	}

	// Token: 0x06000D00 RID: 3328 RVA: 0x0003EB46 File Offset: 0x0003CD46
	public void OnTriggerExit(Collider other)
	{
		if (this.onExit && this.unityEvent != null)
		{
			this.unityEvent.Invoke();
		}
	}

	// Token: 0x04001114 RID: 4372
	public UnityEvent unityEvent;

	// Token: 0x04001115 RID: 4373
	public bool onEnter = true;

	// Token: 0x04001116 RID: 4374
	public bool onStay;

	// Token: 0x04001117 RID: 4375
	public bool onExit;
}
