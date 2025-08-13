using System;
using Rewired;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x020003C9 RID: 969
public class UIScrollSelected : MonoBehaviour
{
	// Token: 0x0600128D RID: 4749 RVA: 0x0000FB84 File Offset: 0x0000DD84
	private void Awake()
	{
		this.scrollToSelected = base.GetComponentInParent<UIScrollToSelected>();
	}

	// Token: 0x0600128E RID: 4750 RVA: 0x0000FB92 File Offset: 0x0000DD92
	public void OnEnable()
	{
		this.rePlayer = ReInput.players.GetPlayer(0);
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.ScrollInput), 0, 38, ReInput.mapping.GetActionId("UIScroll"));
	}

	// Token: 0x0600128F RID: 4751 RVA: 0x0000FBCE File Offset: 0x0000DDCE
	private void OnDisable()
	{
		this.rePlayer.RemoveInputEventDelegate(new Action<InputActionEventData>(this.ScrollInput));
	}

	// Token: 0x06001290 RID: 4752 RVA: 0x0005B7A0 File Offset: 0x000599A0
	private void ScrollInput(InputActionEventData obj)
	{
		if (Time.frameCount - this.scrollFrame < 3)
		{
			return;
		}
		float axisRaw = obj.GetAxisRaw();
		this.scroll += axisRaw;
		if (Mathf.Abs(this.scroll) > 0.2f)
		{
			this.scrollFrame = Time.frameCount;
			this.Scroll(Mathf.RoundToInt(Mathf.Sign(this.scroll)));
			this.scroll = 0f;
		}
	}

	// Token: 0x06001291 RID: 4753 RVA: 0x0005B814 File Offset: 0x00059A14
	private void Scroll(int direction)
	{
		EventSystem current = EventSystem.current;
		GameObject gameObject = current.currentSelectedGameObject;
		GameObject gameObject2 = gameObject;
		bool flag = false;
		int num = gameObject.transform.GetSiblingIndex() + direction;
		Transform parent = gameObject.transform.parent;
		while (!flag && num >= 0 && num < parent.childCount)
		{
			gameObject = parent.GetChild(num).gameObject;
			flag = gameObject.activeSelf && gameObject.GetComponent<IEventSystemHandler>() != null;
			num += direction;
		}
		if (flag && gameObject != gameObject2)
		{
			if (this.scrollToSelected != null)
			{
				this.scrollToSelected.Scroll(gameObject.transform.position.y - gameObject2.transform.position.y);
			}
			current.SetSelectedGameObject(gameObject);
		}
	}

	// Token: 0x040017ED RID: 6125
	private const float scrollThreshold = 0.2f;

	// Token: 0x040017EE RID: 6126
	private float scroll;

	// Token: 0x040017EF RID: 6127
	private int scrollFrame = -1;

	// Token: 0x040017F0 RID: 6128
	private const int scrollFrameGap = 3;

	// Token: 0x040017F1 RID: 6129
	private Player rePlayer;

	// Token: 0x040017F2 RID: 6130
	private UIScrollToSelected scrollToSelected;
}
