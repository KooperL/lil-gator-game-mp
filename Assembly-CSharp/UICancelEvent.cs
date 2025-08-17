using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UICancelEvent : MonoBehaviour, ICancelHandler, IEventSystemHandler
{
	// Token: 0x060011F6 RID: 4598 RVA: 0x0000F464 File Offset: 0x0000D664
	public void OnCancel(BaseEventData eventData)
	{
		this.onCancel.Invoke();
	}

	public UnityEvent onCancel;
}
