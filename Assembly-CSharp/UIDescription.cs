using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDescription : MonoBehaviour, ISelectHandler, IEventSystemHandler, IDeselectHandler
{
	// Token: 0x06000DFD RID: 3581 RVA: 0x00043BF7 File Offset: 0x00041DF7
	private void OnEnable()
	{
		if (EventSystem.current.currentSelectedGameObject == base.gameObject)
		{
			this.CreateDescription();
		}
	}

	// Token: 0x06000DFE RID: 3582 RVA: 0x00043C16 File Offset: 0x00041E16
	private void OnDisable()
	{
		if (this.descriptionDisplay != null)
		{
			Object.Destroy(this.descriptionDisplay.gameObject);
		}
	}

	// Token: 0x06000DFF RID: 3583 RVA: 0x00043C36 File Offset: 0x00041E36
	public void OnSelect(BaseEventData eventData)
	{
		this.CreateDescription();
	}

	// Token: 0x06000E00 RID: 3584 RVA: 0x00043C3E File Offset: 0x00041E3E
	public void OnDeselect(BaseEventData eventData)
	{
		this.ClearDescription();
	}

	// Token: 0x06000E01 RID: 3585 RVA: 0x00043C48 File Offset: 0x00041E48
	private void CreateDescription()
	{
		if (this.descriptionDisplay != null)
		{
			this.descriptionDisplay.KeepOpen();
			return;
		}
		this.descriptionDisplay = Object.Instantiate<GameObject>(this.prefab, base.transform).GetComponent<UIDescriptionDisplay>();
		if (this.document != null)
		{
			this.descriptionDisplay.Load(this.document.FetchString(this.descriptionID, Language.Auto), this);
			return;
		}
		this.descriptionDisplay.Load(this.descriptionText, this);
	}

	// Token: 0x06000E02 RID: 3586 RVA: 0x00043CCB File Offset: 0x00041ECB
	private void ClearDescription()
	{
		if (this.descriptionDisplay == null)
		{
			return;
		}
		this.descriptionDisplay.Clear();
	}

	// Token: 0x06000E03 RID: 3587 RVA: 0x00043CE7 File Offset: 0x00041EE7
	public void ForgetDescription()
	{
		this.descriptionDisplay = null;
	}

	// Token: 0x06000E04 RID: 3588 RVA: 0x00043CF0 File Offset: 0x00041EF0
	[ContextMenu("Add to document")]
	public void AddToDocument()
	{
		this.document.AddStringEntry(this.descriptionID, this.descriptionText);
	}

	public GameObject prefab;

	private UIDescriptionDisplay descriptionDisplay;

	public MultilingualTextDocument document;

	[TextLookup("document")]
	public string descriptionID;

	[TextArea]
	public string descriptionText;
}
