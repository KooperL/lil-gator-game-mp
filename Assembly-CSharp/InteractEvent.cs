using System;
using UnityEngine;
using UnityEngine.Events;

public class InteractEvent : MonoBehaviour, Interaction
{
	// Token: 0x06001001 RID: 4097 RVA: 0x0000DD11 File Offset: 0x0000BF11
	public void Interact()
	{
		if (this.unityEvent != null)
		{
			this.unityEvent.Invoke();
		}
	}

	public UnityEvent unityEvent;
}
