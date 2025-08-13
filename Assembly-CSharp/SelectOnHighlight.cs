using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200038C RID: 908
[RequireComponent(typeof(Selectable))]
public class SelectOnHighlight : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler
{
	// Token: 0x0600114B RID: 4427 RVA: 0x0000ED2D File Offset: 0x0000CF2D
	private void Awake()
	{
		this.selectable = base.GetComponent<Selectable>();
	}

	// Token: 0x0600114C RID: 4428 RVA: 0x00056AF4 File Offset: 0x00054CF4
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (EventSystem.current.alreadySelecting)
		{
			return;
		}
		if (eventData.delta == Vector2.zero)
		{
			return;
		}
		if (this.selectable != null && !this.selectable.IsInteractable())
		{
			return;
		}
		EventSystem.current.SetSelectedGameObject(base.gameObject);
	}

	// Token: 0x04001639 RID: 5689
	private Selectable selectable;
}
