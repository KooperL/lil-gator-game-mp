using System;
using Rewired;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIScrollSelected : MonoBehaviour
{
	// Token: 0x06000F79 RID: 3961 RVA: 0x0004A3F5 File Offset: 0x000485F5
	private void Awake()
	{
		this.scrollToSelected = base.GetComponentInParent<UIScrollToSelected>();
	}

	// Token: 0x06000F7A RID: 3962 RVA: 0x0004A403 File Offset: 0x00048603
	public void OnEnable()
	{
		this.rePlayer = ReInput.players.GetPlayer(0);
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.ScrollInput), UpdateLoopType.Update, InputActionEventType.AxisRawActiveOrJustInactive, ReInput.mapping.GetActionId("UIScroll"));
	}

	// Token: 0x06000F7B RID: 3963 RVA: 0x0004A43F File Offset: 0x0004863F
	private void OnDisable()
	{
		this.rePlayer.RemoveInputEventDelegate(new Action<InputActionEventData>(this.ScrollInput));
	}

	// Token: 0x06000F7C RID: 3964 RVA: 0x0004A458 File Offset: 0x00048658
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

	// Token: 0x06000F7D RID: 3965 RVA: 0x0004A4CC File Offset: 0x000486CC
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

	private global::Rewired.Player rePlayer;

	private UIScrollToSelected scrollToSelected;

	public bool isGrid;

	public int gridWidth = 3;
}
