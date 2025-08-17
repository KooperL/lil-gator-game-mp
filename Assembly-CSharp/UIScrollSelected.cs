using System;
using Rewired;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIScrollSelected : MonoBehaviour
{
	// Token: 0x060012ED RID: 4845 RVA: 0x0000FF7A File Offset: 0x0000E17A
	private void Awake()
	{
		this.scrollToSelected = base.GetComponentInParent<UIScrollToSelected>();
	}

	// Token: 0x060012EE RID: 4846 RVA: 0x0000FF88 File Offset: 0x0000E188
	public void OnEnable()
	{
		this.rePlayer = ReInput.players.GetPlayer(0);
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.ScrollInput), 0, 38, ReInput.mapping.GetActionId("UIScroll"));
	}

	// Token: 0x060012EF RID: 4847 RVA: 0x0000FFC4 File Offset: 0x0000E1C4
	private void OnDisable()
	{
		this.rePlayer.RemoveInputEventDelegate(new Action<InputActionEventData>(this.ScrollInput));
	}

	// Token: 0x060012F0 RID: 4848 RVA: 0x0005D750 File Offset: 0x0005B950
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

	// Token: 0x060012F1 RID: 4849 RVA: 0x0005D7C4 File Offset: 0x0005B9C4
	private void Scroll(int direction)
	{
		EventSystem current = EventSystem.current;
		GameObject gameObject = current.currentSelectedGameObject;
		GameObject gameObject2 = gameObject;
		bool flag = false;
		int num = gameObject.transform.GetSiblingIndex();
		Transform parent = gameObject.transform.parent;
		if (this.isGrid)
		{
			int num2 = Mathf.FloorToInt(((float)num + 0.001f) / (float)this.gridWidth);
			int num3 = Mathf.CeilToInt(((float)parent.childCount + 0.001f) / (float)this.gridWidth);
			if (direction == -1 && num2 == 0)
			{
				return;
			}
			if (direction == 1 && num2 == num3 - 1)
			{
				return;
			}
			num += direction * this.gridWidth;
			num = Mathf.Clamp(num, 0, parent.childCount - 1);
		}
		else
		{
			num += direction;
		}
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

	private const float scrollThreshold = 0.2f;

	private float scroll;

	private int scrollFrame = -1;

	private const int scrollFrameGap = 3;

	private Player rePlayer;

	private UIScrollToSelected scrollToSelected;

	public bool isGrid;

	public int gridWidth = 3;
}
