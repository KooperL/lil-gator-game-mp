using System;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
	// Token: 0x06001007 RID: 4103 RVA: 0x0000DD89 File Offset: 0x0000BF89
	public void OnTriggerEnter(Collider other)
	{
		if (this.onEnter && this.unityEvent != null)
		{
			this.unityEvent.Invoke();
		}
	}

	// Token: 0x06001008 RID: 4104 RVA: 0x0000DDA6 File Offset: 0x0000BFA6
	public void OnTriggerStay(Collider other)
	{
		if (this.onStay && this.unityEvent != null)
		{
			this.unityEvent.Invoke();
		}
	}

	// Token: 0x06001009 RID: 4105 RVA: 0x0000DDC3 File Offset: 0x0000BFC3
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
