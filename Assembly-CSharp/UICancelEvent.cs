using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// Token: 0x0200039E RID: 926
public class UICancelEvent : MonoBehaviour, ICancelHandler, IEventSystemHandler
{
	// Token: 0x06001196 RID: 4502 RVA: 0x0000F07B File Offset: 0x0000D27B
	public void OnCancel(BaseEventData eventData)
	{
		this.onCancel.Invoke();
	}

	// Token: 0x040016AD RID: 5805
	public UnityEvent onCancel;
}
