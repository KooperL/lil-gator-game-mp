using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x020000EF RID: 239
public class DialogueOption : MonoBehaviour, ISelectHandler, IEventSystemHandler, ISubmitHandler, IPointerClickHandler
{
	// Token: 0x0600047D RID: 1149 RVA: 0x00005411 File Offset: 0x00003611
	public void OnValidate()
	{
		if (this.options == null)
		{
			this.options = base.GetComponentInParent<DialogueOptions>();
		}
	}

	// Token: 0x0600047E RID: 1150 RVA: 0x0000542D File Offset: 0x0000362D
	public void OnPointerClick(PointerEventData eventData)
	{
		this.options.Submit();
	}

	// Token: 0x0600047F RID: 1151 RVA: 0x0000543A File Offset: 0x0000363A
	public void OnSelect(BaseEventData eventData)
	{
		this.options.SetSelected(this.index);
	}

	// Token: 0x06000480 RID: 1152 RVA: 0x0000542D File Offset: 0x0000362D
	public void OnSubmit(BaseEventData eventData)
	{
		this.options.Submit();
	}

	// Token: 0x0400065D RID: 1629
	public int index;

	// Token: 0x0400065E RID: 1630
	public DialogueOptions options;
}
