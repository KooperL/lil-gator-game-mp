using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueOption : MonoBehaviour, ISelectHandler, IEventSystemHandler, ISubmitHandler, IPointerClickHandler
{
	// Token: 0x060004A4 RID: 1188 RVA: 0x00005644 File Offset: 0x00003844
	public void OnValidate()
	{
		if (this.options == null)
		{
			this.options = base.GetComponentInParent<DialogueOptions>();
		}
	}

	// Token: 0x060004A5 RID: 1189 RVA: 0x00005660 File Offset: 0x00003860
	public void OnPointerClick(PointerEventData eventData)
	{
		this.options.Submit();
	}

	// Token: 0x060004A6 RID: 1190 RVA: 0x0000566D File Offset: 0x0000386D
	public void OnSelect(BaseEventData eventData)
	{
		this.options.SetSelected(this.index);
	}

	// Token: 0x060004A7 RID: 1191 RVA: 0x00005660 File Offset: 0x00003860
	public void OnSubmit(BaseEventData eventData)
	{
		this.options.Submit();
	}

	public int index;

	public DialogueOptions options;
}
