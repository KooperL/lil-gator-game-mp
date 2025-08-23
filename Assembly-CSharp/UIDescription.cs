using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDescription : MonoBehaviour, ISelectHandler, IEventSystemHandler, IDeselectHandler
{
	// Token: 0x06001122 RID: 4386 RVA: 0x0000E9B9 File Offset: 0x0000CBB9
	private void OnEnable()
	{
		if (EventSystem.current.currentSelectedGameObject == base.gameObject)
		{
			this.CreateDescription();
		}
	}

	// Token: 0x06001123 RID: 4387 RVA: 0x0000E9D8 File Offset: 0x0000CBD8
	private void OnDisable()
	{
		if (this.descriptionDisplay != null)
		{
			global::UnityEngine.Object.Destroy(this.descriptionDisplay.gameObject);
		}
	}

	// Token: 0x06001124 RID: 4388 RVA: 0x0000E9F8 File Offset: 0x0000CBF8
	public void OnSelect(BaseEventData eventData)
	{
		this.CreateDescription();
	}

	// Token: 0x06001125 RID: 4389 RVA: 0x0000EA00 File Offset: 0x0000CC00
	public void OnDeselect(BaseEventData eventData)
	{
		this.ClearDescription();
	}

	// Token: 0x06001126 RID: 4390 RVA: 0x00057E18 File Offset: 0x00056018
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

	// Token: 0x06001127 RID: 4391 RVA: 0x0000EA08 File Offset: 0x0000CC08
	private void ClearDescription()
	{
		if (this.descriptionDisplay == null)
		{
			return;
		}
		this.descriptionDisplay.Clear();
	}

	// Token: 0x06001128 RID: 4392 RVA: 0x0000EA24 File Offset: 0x0000CC24
	public void ForgetDescription()
	{
		this.descriptionDisplay = null;
	}

	// Token: 0x06001129 RID: 4393 RVA: 0x0000EA2D File Offset: 0x0000CC2D
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
