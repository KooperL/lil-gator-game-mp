using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020003DD RID: 989
public class SlideBar : Selectable, ISubmitHandler, IEventSystemHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	// Token: 0x06001309 RID: 4873 RVA: 0x0005CFCC File Offset: 0x0005B1CC
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

	// Token: 0x0600130A RID: 4874 RVA: 0x0005D0B4 File Offset: 0x0005B2B4
	public void RefreshElements()
	{
		for (int i = 0; i < this.elementData.Length; i++)
		{
			this.elements[i].LoadItem(this.elementData[i]);
		}
	}

	// Token: 0x0600130B RID: 4875 RVA: 0x0005D0F0 File Offset: 0x0005B2F0
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

	// Token: 0x0600130C RID: 4876 RVA: 0x0005D130 File Offset: 0x0005B330
	private void SetSelectedIndex(int newIndex)
	{
		newIndex = Mathf.Clamp(newIndex, 0, this.elementData.Length);
		this.lastSelectedIndex = this.selectedIndex;
		this.selectedIndex = newIndex;
		this.selectionChangeTime = Time.time;
		this.selectionChanging = true;
		this.onSelect.Invoke(this.elementData[this.selectedIndex]);
	}

	// Token: 0x0600130D RID: 4877 RVA: 0x000102C4 File Offset: 0x0000E4C4
	public override void OnSelect(BaseEventData eventData)
	{
		this.isSelected = true;
		this.onSelect.Invoke(this.elementData[this.selectedIndex]);
		base.OnSelect(eventData);
	}

	// Token: 0x0600130E RID: 4878 RVA: 0x000102EC File Offset: 0x0000E4EC
	public override void OnDeselect(BaseEventData eventData)
	{
		this.isSelected = false;
		base.OnDeselect(eventData);
	}

	// Token: 0x0600130F RID: 4879 RVA: 0x000102FC File Offset: 0x0000E4FC
	public void OnBeginDrag(PointerEventData eventData)
	{
		this.Select();
		this.isDragging = true;
		this.selectionChanging = true;
	}

	// Token: 0x06001310 RID: 4880 RVA: 0x0005D18C File Offset: 0x0005B38C
	public void OnDrag(PointerEventData eventData)
	{
		this.selectedPosition -= 40f * eventData.delta.x / (float)Screen.currentResolution.width;
		int num = Mathf.RoundToInt(Mathf.Clamp(this.selectedPosition, 0f, (float)(this.elementData.Length - 1)));
		if (num != this.selectedIndex)
		{
			this.SetSelectedIndex(num);
		}
	}

	// Token: 0x06001311 RID: 4881 RVA: 0x00010312 File Offset: 0x0000E512
	public void OnEndDrag(PointerEventData eventData)
	{
		this.isDragging = false;
	}

	// Token: 0x06001312 RID: 4882 RVA: 0x0001031B File Offset: 0x0000E51B
	public void OnSubmit(BaseEventData eventData)
	{
		this.onSubmit.Invoke(this.elementData[this.selectedIndex]);
	}

	// Token: 0x06001313 RID: 4883 RVA: 0x0005D1F8 File Offset: 0x0005B3F8
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

	// Token: 0x06001314 RID: 4884 RVA: 0x00010335 File Offset: 0x0000E535
	public override void OnMove(AxisEventData eventData)
	{
		if (eventData.moveDir == 1 || eventData.moveDir == 3)
		{
			base.OnMove(eventData);
			return;
		}
		if (eventData.moveDir == null)
		{
			this.MoveBar(-1);
			return;
		}
		if (eventData.moveDir == 2)
		{
			this.MoveBar(1);
		}
	}

	// Token: 0x06001315 RID: 4885 RVA: 0x0005D24C File Offset: 0x0005B44C
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

	// Token: 0x06001316 RID: 4886 RVA: 0x0005D2EC File Offset: 0x0005B4EC
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

	// Token: 0x06001317 RID: 4887 RVA: 0x0005D3CC File Offset: 0x0005B5CC
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

	// Token: 0x06001318 RID: 4888 RVA: 0x0005D488 File Offset: 0x0005B688
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

	// Token: 0x04001881 RID: 6273
	[Header("Slide Bar")]
	public GameObject elementPrefab;

	// Token: 0x04001882 RID: 6274
	public Transform elementAnchor;

	// Token: 0x04001883 RID: 6275
	public RectTransform leftAnchor;

	// Token: 0x04001884 RID: 6276
	public RectTransform centerAnchor;

	// Token: 0x04001885 RID: 6277
	public RectTransform rightAnchor;

	// Token: 0x04001886 RID: 6278
	public float elementSpacing = 20f;

	// Token: 0x04001887 RID: 6279
	public GameObject interactivity;

	// Token: 0x04001888 RID: 6280
	public ItemObject[] elementData;

	// Token: 0x04001889 RID: 6281
	private List<UIItemDisplay> elements = new List<UIItemDisplay>();

	// Token: 0x0400188A RID: 6282
	private int lastSelectedIndex;

	// Token: 0x0400188B RID: 6283
	private float selectionChangeTime;

	// Token: 0x0400188C RID: 6284
	public int selectedIndex;

	// Token: 0x0400188D RID: 6285
	public float selectedPosition;

	// Token: 0x0400188E RID: 6286
	public float selectionChangeDuration;

	// Token: 0x0400188F RID: 6287
	private bool selectionChanging;

	// Token: 0x04001890 RID: 6288
	public AnimationCurve selectionChangeCurve;

	// Token: 0x04001891 RID: 6289
	public SlideBar.ItemUnityEvent onSelect;

	// Token: 0x04001892 RID: 6290
	public SlideBar.ItemUnityEvent onSubmit;

	// Token: 0x04001893 RID: 6291
	private bool isSelected;

	// Token: 0x04001894 RID: 6292
	private bool isDragging;

	// Token: 0x020003DE RID: 990
	[Serializable]
	public class ItemUnityEvent : UnityEvent<ItemObject>
	{
	}
}
