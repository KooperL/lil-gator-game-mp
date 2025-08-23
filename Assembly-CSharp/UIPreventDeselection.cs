using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPreventDeselection : MonoBehaviour
{
	// Token: 0x060011B5 RID: 4533 RVA: 0x0000F18D File Offset: 0x0000D38D
	public void OnEnable()
	{
		this.eventSystem = EventSystem.current;
		this.fallback = this.GetFallbackSelection();
	}

	// Token: 0x060011B6 RID: 4534 RVA: 0x00002229 File Offset: 0x00000429
	private void OnDisable()
	{
	}

	// Token: 0x060011B7 RID: 4535 RVA: 0x00058F38 File Offset: 0x00057138
	private void Update()
	{
		if (this.fallback == null || !this.fallback.activeSelf)
		{
			this.fallback = this.GetFallbackSelection();
		}
		if (this.fallback == null)
		{
			return;
		}
		if (this.eventSystem.currentSelectedGameObject != null && this.eventSystem.currentSelectedGameObject.activeInHierarchy)
		{
			this.selection = this.eventSystem.currentSelectedGameObject;
			return;
		}
		if (this.selection != null && this.selection.activeInHierarchy)
		{
			this.SetSelection(this.selection);
			return;
		}
		this.SetSelection(this.fallback);
		this.selection = this.fallback;
	}

	// Token: 0x060011B8 RID: 4536 RVA: 0x00058FF4 File Offset: 0x000571F4
	private void SetSelection(GameObject newSelection)
	{
		if (newSelection == null)
		{
			return;
		}
		Selectable selectable;
		if (newSelection.TryGetComponent<Selectable>(out selectable))
		{
			selectable.Select();
		}
		this.eventSystem.SetSelectedGameObject(newSelection);
	}

	// Token: 0x060011B9 RID: 4537 RVA: 0x00059028 File Offset: 0x00057228
	private GameObject GetFallbackSelection()
	{
		if (this.defaultSelection != null && this.defaultSelection.activeSelf)
		{
			return this.defaultSelection;
		}
		if (this.secondarySelection != null && this.secondarySelection.activeSelf)
		{
			return this.secondarySelection;
		}
		Selectable componentInChildren = base.GetComponentInChildren<Selectable>(false);
		if (componentInChildren != null)
		{
			return componentInChildren.gameObject;
		}
		return null;
	}

	public GameObject defaultSelection;

	public GameObject secondarySelection;

	private GameObject selection;

	private GameObject fallback;

	private EventSystem eventSystem;
}
