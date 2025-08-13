using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000320 RID: 800
public class InteractEvent : MonoBehaviour, Interaction
{
	// Token: 0x06000FA6 RID: 4006 RVA: 0x0000D99E File Offset: 0x0000BB9E
	public void Interact()
	{
		if (this.unityEvent != null)
		{
			this.unityEvent.Invoke();
		}
	}

	// Token: 0x0400142B RID: 5163
	public UnityEvent unityEvent;
}
