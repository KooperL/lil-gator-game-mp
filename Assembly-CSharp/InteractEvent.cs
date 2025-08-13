using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200025A RID: 602
public class InteractEvent : MonoBehaviour, Interaction
{
	// Token: 0x06000CF9 RID: 3321 RVA: 0x0003EA8C File Offset: 0x0003CC8C
	public void Interact()
	{
		if (this.unityEvent != null)
		{
			this.unityEvent.Invoke();
		}
	}

	// Token: 0x0400110F RID: 4367
	public UnityEvent unityEvent;
}
