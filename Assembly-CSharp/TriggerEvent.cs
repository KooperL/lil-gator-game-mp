using System;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
	// Token: 0x06001006 RID: 4102 RVA: 0x0000DD7F File Offset: 0x0000BF7F
	public void OnTriggerEnter(Collider other)
	{
		if (this.onEnter && this.unityEvent != null)
		{
			this.unityEvent.Invoke();
		}
	}

	// Token: 0x06001007 RID: 4103 RVA: 0x0000DD9C File Offset: 0x0000BF9C
	public void OnTriggerStay(Collider other)
	{
		if (this.onStay && this.unityEvent != null)
		{
			this.unityEvent.Invoke();
		}
	}

	// Token: 0x06001008 RID: 4104 RVA: 0x0000DDB9 File Offset: 0x0000BFB9
	public void OnTriggerExit(Collider other)
	{
		if (this.onExit && this.unityEvent != null)
		{
			this.unityEvent.Invoke();
		}
	}

	public UnityEvent unityEvent;

	public bool onEnter = true;

	public bool onStay;

	public bool onExit;
}
