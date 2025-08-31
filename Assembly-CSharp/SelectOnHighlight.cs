using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Selectable))]
public class SelectOnHighlight : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler
{
	// Token: 0x06000E7B RID: 3707 RVA: 0x000452BC File Offset: 0x000434BC
	private void Awake()
	{
		this.selectable = base.GetComponent<Selectable>();
	}

	// Token: 0x06000E7C RID: 3708 RVA: 0x000452CC File Offset: 0x000434CC
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

	private Selectable selectable;
}
