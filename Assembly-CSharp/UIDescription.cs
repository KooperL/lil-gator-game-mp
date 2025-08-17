using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDescription : MonoBehaviour, ISelectHandler, IEventSystemHandler, IDeselectHandler
{
	// Token: 0x06001121 RID: 4385 RVA: 0x0000E9AF File Offset: 0x0000CBAF
	private void OnEnable()
	{
		if (EventSystem.current.currentSelectedGameObject == base.gameObject)
		{
			this.CreateDescription();
		}
	}

	// Token: 0x06001122 RID: 4386 RVA: 0x0000E9CE File Offset: 0x0000CBCE
	private void OnDisable()
	{
		if (this.descriptionDisplay != null)
		{
			global::UnityEngine.Object.Destroy(this.descriptionDisplay.gameObject);
		}
	}

	// Token: 0x06001123 RID: 4387 RVA: 0x0000E9EE File Offset: 0x0000CBEE
	public void OnSelect(BaseEventData eventData)
	{
		this.CreateDescription();
	}

	// Token: 0x06001124 RID: 4388 RVA: 0x0000E9F6 File Offset: 0x0000CBF6
	public void OnDeselect(BaseEventData eventData)
	{
		this.ClearDescription();
	}

	// Token: 0x06001125 RID: 4389 RVA: 0x00057B50 File Offset: 0x00055D50
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

	// Token: 0x06001126 RID: 4390 RVA: 0x0000E9FE File Offset: 0x0000CBFE
	private void ClearDescription()
	{
		if (this.descriptionDisplay == null)
		{
			return;
		}
		this.descriptionDisplay.Clear();
	}

	// Token: 0x06001127 RID: 4391 RVA: 0x0000EA1A File Offset: 0x0000CC1A
	public void ForgetDescription()
	{
		this.descriptionDisplay = null;
	}

	// Token: 0x06001128 RID: 4392 RVA: 0x0000EA23 File Offset: 0x0000CC23
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
