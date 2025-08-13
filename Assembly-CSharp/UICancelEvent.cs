using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// Token: 0x020002BC RID: 700
public class UICancelEvent : MonoBehaviour, ICancelHandler, IEventSystemHandler
{
	// Token: 0x06000EBE RID: 3774 RVA: 0x000468E0 File Offset: 0x00044AE0
	public void OnCancel(BaseEventData eventData)
	{
		this.onCancel.Invoke();
	}

	// Token: 0x04001337 RID: 4919
	public UnityEvent onCancel;
}
