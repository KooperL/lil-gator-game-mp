using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Selectable))]
public class SelectOnHighlight : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler
{
	// Token: 0x060011AC RID: 4524 RVA: 0x0000F120 File Offset: 0x0000D320
	private void Awake()
	{
		this.selectable = base.GetComponent<Selectable>();
	}

	// Token: 0x060011AD RID: 4525 RVA: 0x00058D7C File Offset: 0x00056F7C
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
