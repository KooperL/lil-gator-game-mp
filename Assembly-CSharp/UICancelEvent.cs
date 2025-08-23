using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UICancelEvent : MonoBehaviour, ICancelHandler, IEventSystemHandler
{
	// Token: 0x060011F7 RID: 4599 RVA: 0x0000F46E File Offset: 0x0000D66E
	public void OnCancel(BaseEventData eventData)
	{
		this.onCancel.Invoke();
	}

	public UnityEvent onCancel;
}
