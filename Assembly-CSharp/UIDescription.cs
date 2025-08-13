using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200036B RID: 875
public class UIDescription : MonoBehaviour, ISelectHandler, IEventSystemHandler, IDeselectHandler
{
	// Token: 0x060010C6 RID: 4294 RVA: 0x0000E646 File Offset: 0x0000C846
	private void OnEnable()
	{
		if (EventSystem.current.currentSelectedGameObject == base.gameObject)
		{
			this.CreateDescription();
		}
	}

	// Token: 0x060010C7 RID: 4295 RVA: 0x0000E665 File Offset: 0x0000C865
	private void OnDisable()
	{
		if (this.descriptionDisplay != null)
		{
			Object.Destroy(this.descriptionDisplay.gameObject);
		}
	}

	// Token: 0x060010C8 RID: 4296 RVA: 0x0000E685 File Offset: 0x0000C885
	public void OnSelect(BaseEventData eventData)
	{
		this.CreateDescription();
	}

	// Token: 0x060010C9 RID: 4297 RVA: 0x0000E68D File Offset: 0x0000C88D
	public void OnDeselect(BaseEventData eventData)
	{
		this.ClearDescription();
	}

	// Token: 0x060010CA RID: 4298 RVA: 0x00055BDC File Offset: 0x00053DDC
	private void CreateDescription()
	{
		if (this.descriptionDisplay != null)
		{
			this.descriptionDisplay.KeepOpen();
			return;
		}
		this.descriptionDisplay = Object.Instantiate<GameObject>(this.prefab, base.transform).GetComponent<UIDescriptionDisplay>();
		this.descriptionDisplay.Load(this.descriptionText, this);
	}

	// Token: 0x060010CB RID: 4299 RVA: 0x0000E695 File Offset: 0x0000C895
	private void ClearDescription()
	{
		if (this.descriptionDisplay == null)
		{
			return;
		}
		this.descriptionDisplay.Clear();
	}

	// Token: 0x060010CC RID: 4300 RVA: 0x0000E6B1 File Offset: 0x0000C8B1
	public void ForgetDescription()
	{
		this.descriptionDisplay = null;
	}

	// Token: 0x040015D4 RID: 5588
	public GameObject prefab;

	// Token: 0x040015D5 RID: 5589
	private UIDescriptionDisplay descriptionDisplay;

	// Token: 0x040015D6 RID: 5590
	[TextArea]
	public string descriptionText;
}
