using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDescription : MonoBehaviour, ISelectHandler, IEventSystemHandler, IDeselectHandler
{
	// Token: 0x06001121 RID: 4385 RVA: 0x0000E99A File Offset: 0x0000CB9A
	private void OnEnable()
	{
		if (EventSystem.current.currentSelectedGameObject == base.gameObject)
		{
			this.CreateDescription();
		}
	}

	// Token: 0x06001122 RID: 4386 RVA: 0x0000E9B9 File Offset: 0x0000CBB9
	private void OnDisable()
	{
		if (this.descriptionDisplay != null)
		{
			global::UnityEngine.Object.Destroy(this.descriptionDisplay.gameObject);
		}
	}

	// Token: 0x06001123 RID: 4387 RVA: 0x0000E9D9 File Offset: 0x0000CBD9
	public void OnSelect(BaseEventData eventData)
	{
		this.CreateDescription();
	}

	// Token: 0x06001124 RID: 4388 RVA: 0x0000E9E1 File Offset: 0x0000CBE1
	public void OnDeselect(BaseEventData eventData)
	{
		this.ClearDescription();
	}

	// Token: 0x06001125 RID: 4389 RVA: 0x000579BC File Offset: 0x00055BBC
	private void CreateDescription()
	{
		if (this.descriptionDisplay != null)
		{
			this.descriptionDisplay.KeepOpen();
			return;
		}
		this.descriptionDisplay = global::UnityEngine.Object.Instantiate<GameObject>(this.prefab, base.transform).GetComponent<UIDescriptionDisplay>();
		if (this.document != null)
		{
			this.descriptionDisplay.Load(this.document.FetchString(this.descriptionID, Language.Auto), this);
			return;
		}
		this.descriptionDisplay.Load(this.descriptionText, this);
	}

	// Token: 0x06001126 RID: 4390 RVA: 0x0000E9E9 File Offset: 0x0000CBE9
	private void ClearDescription()
	{
		if (this.descriptionDisplay == null)
		{
			return;
		}
		this.descriptionDisplay.Clear();
	}

	// Token: 0x06001127 RID: 4391 RVA: 0x0000EA05 File Offset: 0x0000CC05
	public void ForgetDescription()
	{
		this.descriptionDisplay = null;
	}

	// Token: 0x06001128 RID: 4392 RVA: 0x0000EA0E File Offset: 0x0000CC0E
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
