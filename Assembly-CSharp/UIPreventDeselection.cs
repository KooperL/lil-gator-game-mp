using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020002B1 RID: 689
public class UIPreventDeselection : MonoBehaviour
{
	// Token: 0x06000E84 RID: 3716 RVA: 0x000454ED File Offset: 0x000436ED
	public void OnEnable()
	{
		this.eventSystem = EventSystem.current;
		this.fallback = this.GetFallbackSelection();
	}

	// Token: 0x06000E85 RID: 3717 RVA: 0x00045506 File Offset: 0x00043706
	private void OnDisable()
	{
	}

	// Token: 0x06000E86 RID: 3718 RVA: 0x00045508 File Offset: 0x00043708
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

	// Token: 0x06000E87 RID: 3719 RVA: 0x000455C4 File Offset: 0x000437C4
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

	// Token: 0x06000E88 RID: 3720 RVA: 0x000455F8 File Offset: 0x000437F8
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

	// Token: 0x040012E6 RID: 4838
	public GameObject defaultSelection;

	// Token: 0x040012E7 RID: 4839
	public GameObject secondarySelection;

	// Token: 0x040012E8 RID: 4840
	private GameObject selection;

	// Token: 0x040012E9 RID: 4841
	private GameObject fallback;

	// Token: 0x040012EA RID: 4842
	private EventSystem eventSystem;
}
