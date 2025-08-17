using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Selectable))]
public class SelectOnHighlight : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler
{
	// Token: 0x060011AB RID: 4523 RVA: 0x0000F116 File Offset: 0x0000D316
	private void Awake()
	{
		this.selectable = base.GetComponent<Selectable>();
	}

	// Token: 0x060011AC RID: 4524 RVA: 0x00058AB4 File Offset: 0x00056CB4
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
