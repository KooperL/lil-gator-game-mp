using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlideBar : Selectable, ISubmitHandler, IEventSystemHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	// Token: 0x0600136A RID: 4970 RVA: 0x0005F2BC File Offset: 0x0005D4BC
	public void LoadElements(ItemObject[] elementData, int selectedIndex = 0)
	{
		this.elementData = elementData;
		if (elementData.Length == 0)
		{
			base.gameObject.SetActive(false);
			return;
		}
		base.gameObject.SetActive(true);
		if (selectedIndex != -1)
		{
			this.selectedIndex = selectedIndex;
		}
		if (this.selectedIndex < 0)
		{
			this.selectedIndex = 0;
		}
		this.selectedPosition = (float)this.selectedIndex;
		int i = 0;
		foreach (ItemObject itemObject in elementData)
		{
			if (i >= this.elements.Count)
			{
				this.elements.Add(global::UnityEngine.Object.Instantiate<GameObject>(this.elementPrefab, this.elementAnchor).GetComponent<UIItemDisplay>());
			}
			this.elements[i].LoadItem(itemObject);
			i++;
		}
		while (i < this.elements.Count)
		{
			this.elements[i].gameObject.SetActive(false);
			i++;
		}
		this.SetElementPositions((float)selectedIndex);
	}

	// Token: 0x0600136B RID: 4971 RVA: 0x0005F3A4 File Offset: 0x0005D5A4
	public void RefreshElements()
	{
		for (int i = 0; i < this.elementData.Length; i++)
		{
			this.elements[i].LoadItem(this.elementData[i]);
		}
	}

	// Token: 0x0600136C RID: 4972 RVA: 0x0005F3E0 File Offset: 0x0005D5E0
	public void MoveBar(int direction)
	{
		this.Select();
		int num = this.selectedIndex + direction;
		if (num == -1)
		{
			num = this.elementData.Length - 1;
		}
		if (num == this.elementData.Length)
		{
			num = 0;
		}
		this.SetSelectedIndex(num);
	}

	// Token: 0x0600136D RID: 4973 RVA: 0x0005F420 File Offset: 0x0005D620
	private void SetSelectedIndex(int newIndex)
	{
		newIndex = Mathf.Clamp(newIndex, 0, this.elementData.Length);
		this.lastSelectedIndex = this.selectedIndex;
		this.selectedIndex = newIndex;
		this.selectionChangeTime = Time.time;
		this.selectionChanging = true;
		this.onSelect.Invoke(this.elementData[this.selectedIndex]);
	}

	// Token: 0x0600136E RID: 4974 RVA: 0x000106CB File Offset: 0x0000E8CB
	public override void OnSelect(BaseEventData eventData)
	{
		this.isSelected = true;
		this.onSelect.Invoke(this.elementData[this.selectedIndex]);
		base.OnSelect(eventData);
	}

	// Token: 0x0600136F RID: 4975 RVA: 0x000106F3 File Offset: 0x0000E8F3
	public override void OnDeselect(BaseEventData eventData)
	{
		this.isSelected = false;
		base.OnDeselect(eventData);
	}

	// Token: 0x06001370 RID: 4976 RVA: 0x00010703 File Offset: 0x0000E903
	public void OnBeginDrag(PointerEventData eventData)
	{
		this.Select();
		this.isDragging = true;
		this.selectionChanging = true;
	}

	// Token: 0x06001371 RID: 4977 RVA: 0x0005F47C File Offset: 0x0005D67C
	public void OnDrag(PointerEventData eventData)
	{
		this.selectedPosition -= 40f * eventData.delta.x / (float)Screen.currentResolution.width;
		int num = Mathf.RoundToInt(Mathf.Clamp(this.selectedPosition, 0f, (float)(this.elementData.Length - 1)));
		if (num != this.selectedIndex)
		{
			this.SetSelectedIndex(num);
		}
	}

	// Token: 0x06001372 RID: 4978 RVA: 0x00010719 File Offset: 0x0000E919
	public void OnEndDrag(PointerEventData eventData)
	{
		this.isDragging = false;
	}

	// Token: 0x06001373 RID: 4979 RVA: 0x00010722 File Offset: 0x0000E922
	public void OnSubmit(BaseEventData eventData)
	{
		this.onSubmit.Invoke(this.elementData[this.selectedIndex]);
	}

	// Token: 0x06001374 RID: 4980 RVA: 0x0005F4E8 File Offset: 0x0005D6E8
	public void OnClickDisplay(UIItemDisplay clickedDisplay)
	{
		int num = this.elements.IndexOf(clickedDisplay);
		if (num == this.selectedIndex && this.isSelected)
		{
			this.onSubmit.Invoke(this.elementData[this.selectedIndex]);
		}
		else
		{
			this.SetSelectedIndex(num);
		}
		this.Select();
	}

	// Token: 0x06001375 RID: 4981 RVA: 0x0001073C File Offset: 0x0000E93C
	public override void OnMove(AxisEventData eventData)
	{
		if (eventData.moveDir == MoveDirection.Up || eventData.moveDir == MoveDirection.Down)
		{
			base.OnMove(eventData);
			return;
		}
		if (eventData.moveDir == MoveDirection.Left)
		{
			this.MoveBar(-1);
			return;
		}
		if (eventData.moveDir == MoveDirection.Right)
		{
			this.MoveBar(1);
		}
	}

	// Token: 0x06001376 RID: 4982 RVA: 0x0005F53C File Offset: 0x0005D73C
	private void Update()
	{
		if (this.selectionChanging)
		{
			float num = this.selectedPosition;
			int num2 = this.selectedIndex;
			if (!this.isDragging)
			{
				if (Time.time - this.selectionChangeTime < this.selectionChangeDuration)
				{
					float num3 = Mathf.InverseLerp(this.selectionChangeTime, this.selectionChangeTime + this.selectionChangeDuration, Time.time);
					num3 = this.selectionChangeCurve.Evaluate(num3);
					num = Mathf.LerpUnclamped(num, (float)num2, num3);
				}
				else
				{
					num = (this.selectedPosition = (float)num2);
					this.selectionChanging = false;
				}
			}
			else
			{
				this.selectionChangeTime = Time.time;
			}
			this.SetElementPositions(num);
		}
	}

	// Token: 0x06001377 RID: 4983 RVA: 0x0005F5DC File Offset: 0x0005D7DC
	private void SetElementPositions(float position)
	{
		if (Mathf.Approximately(position, Mathf.Round(position)))
		{
			int num = Mathf.RoundToInt(position);
			for (int i = 0; i < this.elementData.Length; i++)
			{
				RectTransform rectTransform = this.elements[i].rectTransform;
				Vector3 vector;
				Vector2 vector2;
				this.GetElementTransform(i, out vector, out vector2, num);
				rectTransform.localPosition = vector;
				rectTransform.sizeDelta = vector2;
			}
			return;
		}
		int num2 = Mathf.FloorToInt(position);
		int num3 = Mathf.CeilToInt(position);
		for (int j = 0; j < this.elementData.Length; j++)
		{
			RectTransform rectTransform2 = this.elements[j].rectTransform;
			float num4 = position - Mathf.Floor(position);
			Vector3 vector3;
			Vector2 vector4;
			this.GetElementTransform(j, out vector3, out vector4, num2);
			Vector3 vector5;
			Vector2 vector6;
			this.GetElementTransform(j, out vector5, out vector6, num3);
			rectTransform2.localPosition = Vector3.Lerp(vector3, vector5, num4);
			rectTransform2.sizeDelta = Vector2.Lerp(vector4, vector6, num4);
		}
	}

	// Token: 0x06001378 RID: 4984 RVA: 0x0005F6BC File Offset: 0x0005D8BC
	private void GetElementTransform(int i, out Vector3 localPosition, out Vector3 localScale, int selectedIndex)
	{
		if (i < selectedIndex)
		{
			localPosition = this.leftAnchor.localPosition + (float)(selectedIndex - i - 1) * this.elementSpacing * Vector3.left;
			localScale = this.leftAnchor.localScale;
			return;
		}
		if (i == selectedIndex)
		{
			localPosition = this.centerAnchor.localPosition;
			localScale = this.centerAnchor.localScale;
			return;
		}
		localPosition = this.rightAnchor.localPosition + (float)(i - selectedIndex - 1) * this.elementSpacing * Vector3.right;
		localScale = this.rightAnchor.localScale;
	}

	// Token: 0x06001379 RID: 4985 RVA: 0x0005F778 File Offset: 0x0005D978
	private void GetElementTransform(int i, out Vector3 localPosition, out Vector2 size, int selectedIndex)
	{
		if (i < selectedIndex)
		{
			localPosition = this.leftAnchor.localPosition + (float)(selectedIndex - i - 1) * this.elementSpacing * Vector3.left;
			size = this.leftAnchor.sizeDelta;
			return;
		}
		if (i == selectedIndex)
		{
			localPosition = this.centerAnchor.localPosition;
			size = this.centerAnchor.sizeDelta;
			return;
		}
		localPosition = this.rightAnchor.localPosition + (float)(i - selectedIndex - 1) * this.elementSpacing * Vector3.right;
		size = this.rightAnchor.sizeDelta;
	}

	[Header("Slide Bar")]
	public GameObject elementPrefab;

	public Transform elementAnchor;

	public RectTransform leftAnchor;

	public RectTransform centerAnchor;

	public RectTransform rightAnchor;

	public float elementSpacing = 20f;

	public GameObject interactivity;

	public ItemObject[] elementData;

	private List<UIItemDisplay> elements = new List<UIItemDisplay>();

	private int lastSelectedIndex;

	private float selectionChangeTime;

	public int selectedIndex;

	public float selectedPosition;

	public float selectionChangeDuration;

	private bool selectionChanging;

	public AnimationCurve selectionChangeCurve;

	public SlideBar.ItemUnityEvent onSelect;

	public SlideBar.ItemUnityEvent onSubmit;

	private bool isSelected;

	private bool isDragging;

	[Serializable]
	public class ItemUnityEvent : UnityEvent<ItemObject>
	{
	}
}
