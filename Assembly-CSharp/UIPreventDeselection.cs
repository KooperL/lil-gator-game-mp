using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200038E RID: 910
public class UIPreventDeselection : MonoBehaviour
{
	// Token: 0x06001154 RID: 4436 RVA: 0x0000ED9A File Offset: 0x0000CF9A
	public void OnEnable()
	{
		this.eventSystem = EventSystem.current;
		this.fallback = this.GetFallbackSelection();
	}

	// Token: 0x06001155 RID: 4437 RVA: 0x00002229 File Offset: 0x00000429
	private void OnDisable()
	{
	}

	// Token: 0x06001156 RID: 4438 RVA: 0x00056CB0 File Offset: 0x00054EB0
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

	// Token: 0x06001157 RID: 4439 RVA: 0x00056D6C File Offset: 0x00054F6C
	private void SetSelection(GameObject newSelection)
	{
		if (newSelection == null)
		{
			return;
		}
		Selectable selectable;
		if (newSelection.TryGetComponent<Selectable>(ref selectable))
		{
			selectable.Select();
		}
		this.eventSystem.SetSelectedGameObject(newSelection);
	}

	// Token: 0x06001158 RID: 4440 RVA: 0x00056DA0 File Offset: 0x00054FA0
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

	// Token: 0x0400164E RID: 5710
	public GameObject defaultSelection;

	// Token: 0x0400164F RID: 5711
	public GameObject secondarySelection;

	// Token: 0x04001650 RID: 5712
	private GameObject selection;

	// Token: 0x04001651 RID: 5713
	private GameObject fallback;

	// Token: 0x04001652 RID: 5714
	private EventSystem eventSystem;
}
