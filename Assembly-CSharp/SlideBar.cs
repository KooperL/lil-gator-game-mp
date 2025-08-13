using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020002EC RID: 748
public class SlideBar : Selectable, ISubmitHandler, IEventSystemHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	// Token: 0x06000FDD RID: 4061 RVA: 0x0004BB20 File Offset: 0x00049D20
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
				this.elements.Add(Object.Instantiate<GameObject>(this.elementPrefab, this.elementAnchor).GetComponent<UIItemDisplay>());
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

	// Token: 0x06000FDE RID: 4062 RVA: 0x0004BC08 File Offset: 0x00049E08
	public void RefreshElements()
	{
		for (int i = 0; i < this.elementData.Length; i++)
		{
			this.elements[i].LoadItem(this.elementData[i]);
		}
	}

	// Token: 0x06000FDF RID: 4063 RVA: 0x0004BC44 File Offset: 0x00049E44
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

	// Token: 0x06000FE0 RID: 4064 RVA: 0x0004BC84 File Offset: 0x00049E84
	private void SetSelectedIndex(int newIndex)
	{
		newIndex = Mathf.Clamp(newIndex, 0, this.elementData.Length);
		this.lastSelectedIndex = this.selectedIndex;
		this.selectedIndex = newIndex;
		this.selectionChangeTime = Time.time;
		this.selectionChanging = true;
		this.onSelect.Invoke(this.elementData[this.selectedIndex]);
	}

	// Token: 0x06000FE1 RID: 4065 RVA: 0x0004BCDF File Offset: 0x00049EDF
	public override void OnSelect(BaseEventData eventData)
	{
		this.isSelected = true;
		this.onSelect.Invoke(this.elementData[this.selectedIndex]);
		base.OnSelect(eventData);
	}

	// Token: 0x06000FE2 RID: 4066 RVA: 0x0004BD07 File Offset: 0x00049F07
	public override void OnDeselect(BaseEventData eventData)
	{
		this.isSelected = false;
		base.OnDeselect(eventData);
	}

	// Token: 0x06000FE3 RID: 4067 RVA: 0x0004BD17 File Offset: 0x00049F17
	public void OnBeginDrag(PointerEventData eventData)
	{
		this.Select();
		this.isDragging = true;
		this.selectionChanging = true;
	}

	// Token: 0x06000FE4 RID: 4068 RVA: 0x0004BD30 File Offset: 0x00049F30
	public void OnDrag(PointerEventData eventData)
	{
		this.selectedPosition -= 40f * eventData.delta.x / (float)Screen.currentResolution.width;
		int num = Mathf.RoundToInt(Mathf.Clamp(this.selectedPosition, 0f, (float)(this.elementData.Length - 1)));
		if (num != this.selectedIndex)
		{
			this.SetSelectedIndex(num);
		}
	}

	// Token: 0x06000FE5 RID: 4069 RVA: 0x0004BD9B File Offset: 0x00049F9B
	public void OnEndDrag(PointerEventData eventData)
	{
		this.isDragging = false;
	}

	// Token: 0x06000FE6 RID: 4070 RVA: 0x0004BDA4 File Offset: 0x00049FA4
	public void OnSubmit(BaseEventData eventData)
	{
		this.onSubmit.Invoke(this.elementData[this.selectedIndex]);
	}

	// Token: 0x06000FE7 RID: 4071 RVA: 0x0004BDC0 File Offset: 0x00049FC0
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

	// Token: 0x06000FE8 RID: 4072 RVA: 0x0004BE12 File Offset: 0x0004A012
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

	// Token: 0x06000FE9 RID: 4073 RVA: 0x0004BE50 File Offset: 0x0004A050
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

	// Token: 0x06000FEA RID: 4074 RVA: 0x0004BEF0 File Offset: 0x0004A0F0
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

	// Token: 0x06000FEB RID: 4075 RVA: 0x0004BFD0 File Offset: 0x0004A1D0
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

	// Token: 0x06000FEC RID: 4076 RVA: 0x0004C08C File Offset: 0x0004A28C
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

	// Token: 0x040014C2 RID: 5314
	[Header("Slide Bar")]
	public GameObject elementPrefab;

	// Token: 0x040014C3 RID: 5315
	public Transform elementAnchor;

	// Token: 0x040014C4 RID: 5316
	public RectTransform leftAnchor;

	// Token: 0x040014C5 RID: 5317
	public RectTransform centerAnchor;

	// Token: 0x040014C6 RID: 5318
	public RectTransform rightAnchor;

	// Token: 0x040014C7 RID: 5319
	public float elementSpacing = 20f;

	// Token: 0x040014C8 RID: 5320
	public GameObject interactivity;

	// Token: 0x040014C9 RID: 5321
	public ItemObject[] elementData;

	// Token: 0x040014CA RID: 5322
	private List<UIItemDisplay> elements = new List<UIItemDisplay>();

	// Token: 0x040014CB RID: 5323
	private int lastSelectedIndex;

	// Token: 0x040014CC RID: 5324
	private float selectionChangeTime;

	// Token: 0x040014CD RID: 5325
	public int selectedIndex;

	// Token: 0x040014CE RID: 5326
	public float selectedPosition;

	// Token: 0x040014CF RID: 5327
	public float selectionChangeDuration;

	// Token: 0x040014D0 RID: 5328
	private bool selectionChanging;

	// Token: 0x040014D1 RID: 5329
	public AnimationCurve selectionChangeCurve;

	// Token: 0x040014D2 RID: 5330
	public SlideBar.ItemUnityEvent onSelect;

	// Token: 0x040014D3 RID: 5331
	public SlideBar.ItemUnityEvent onSubmit;

	// Token: 0x040014D4 RID: 5332
	private bool isSelected;

	// Token: 0x040014D5 RID: 5333
	private bool isDragging;

	// Token: 0x0200044F RID: 1103
	[Serializable]
	public class ItemUnityEvent : UnityEvent<ItemObject>
	{
	}
}
