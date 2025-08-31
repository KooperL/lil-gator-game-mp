using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueOption : MonoBehaviour, ISelectHandler, IEventSystemHandler, ISubmitHandler, IPointerClickHandler
{
	// Token: 0x060003D0 RID: 976 RVA: 0x00016950 File Offset: 0x00014B50
	public void OnValidate()
	{
		if (this.options == null)
		{
			this.options = base.GetComponentInParent<DialogueOptions>();
		}
	}

	// Token: 0x060003D1 RID: 977 RVA: 0x0001696C File Offset: 0x00014B6C
	public void OnPointerClick(PointerEventData eventData)
	{
		this.options.Submit();
	}

	// Token: 0x060003D2 RID: 978 RVA: 0x00016979 File Offset: 0x00014B79
	public void OnSelect(BaseEventData eventData)
	{
		this.options.SetSelected(this.index);
	}

	// Token: 0x060003D3 RID: 979 RVA: 0x0001698C File Offset: 0x00014B8C
	public void OnSubmit(BaseEventData eventData)
	{
		this.options.Submit();
	}

	public int index;

	public DialogueOptions options;
}
