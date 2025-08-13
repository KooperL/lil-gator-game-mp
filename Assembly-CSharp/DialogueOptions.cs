using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020000F0 RID: 240
public class DialogueOptions : MonoBehaviour
{
	// Token: 0x17000070 RID: 112
	// (get) Token: 0x06000482 RID: 1154 RVA: 0x0000544D File Offset: 0x0000364D
	protected static DialogueOptions Instance
	{
		get
		{
			if (DialogueOptions.instance == null)
			{
				DialogueOptions.instance = Object.FindObjectOfType<DialogueOptions>(true);
			}
			return DialogueOptions.instance;
		}
	}

	// Token: 0x17000071 RID: 113
	// (get) Token: 0x06000483 RID: 1155 RVA: 0x0000546C File Offset: 0x0000366C
	public static int CurrentlySelectedIndex
	{
		get
		{
			return DialogueOptions.Instance.selectedOption;
		}
	}

	// Token: 0x06000484 RID: 1156 RVA: 0x0002ADE8 File Offset: 0x00028FE8
	public WaitUntil SetOptions(string[] options)
	{
		this.isTriggered = false;
		this.optionCount = options.Length;
		this.selectedOption = 0;
		base.gameObject.SetActive(true);
		for (int i = 0; i < this.optionBoxes.Length; i++)
		{
			if (i < this.optionCount)
			{
				this.optionBoxes[i].Load(options[i], null, true, 0f);
				Navigation navigation = this.optionButtons[i].navigation;
				navigation.selectOnUp = ((i == 0) ? this.optionButtons[this.optionCount - 1] : this.optionButtons[i - 1]);
				navigation.selectOnDown = ((i == this.optionCount - 1) ? this.optionButtons[0] : this.optionButtons[i + 1]);
				this.optionButtons[i].navigation = navigation;
			}
			else
			{
				this.optionBoxes[i].Clear(false);
			}
		}
		EventSystem.current.SetSelectedGameObject(this.optionBoxes[this.selectedOption].gameObject);
		this.selectorPosition = this.optionBoxes[this.selectedOption].rectTransform.anchoredPosition;
		if (this.waitUntilTriggered == null)
		{
			this.waitUntilTriggered = new WaitUntil(() => this.isTriggered);
		}
		return this.waitUntilTriggered;
	}

	// Token: 0x06000485 RID: 1157 RVA: 0x00005478 File Offset: 0x00003678
	public void Clear()
	{
		this.selectorPosition = this.optionBoxes[0].rectTransform.anchoredPosition;
		base.gameObject.SetActive(false);
		this.selectedOption = -1;
		this.isTriggered = false;
	}

	// Token: 0x06000486 RID: 1158 RVA: 0x0002AF2C File Offset: 0x0002912C
	private void Update()
	{
		this.selectorPosition = Vector2.Lerp(this.selectorPosition, this.optionBoxes[this.selectedOption].rectTransform.anchoredPosition, 10f * Time.deltaTime);
		this.selector.anchoredPosition = new Vector2(Mathf.Round(this.selectorPosition.x), Mathf.Round(this.selectorPosition.y));
	}

	// Token: 0x06000487 RID: 1159 RVA: 0x0002AF9C File Offset: 0x0002919C
	public void UpdateSelectedOption(float axis)
	{
		if (axis > 0f)
		{
			this.selectedOption--;
			if (this.selectedOption < 0)
			{
				this.selectedOption = this.optionCount - 1;
				return;
			}
		}
		else if (axis < 0f)
		{
			this.selectedOption++;
			if (this.selectedOption == this.optionCount)
			{
				this.selectedOption = 0;
			}
		}
	}

	// Token: 0x06000488 RID: 1160 RVA: 0x000054AC File Offset: 0x000036AC
	public void Submit()
	{
		this.isTriggered = true;
	}

	// Token: 0x06000489 RID: 1161 RVA: 0x000054B5 File Offset: 0x000036B5
	public void SetSelected(int index)
	{
		this.selectedOption = index;
	}

	// Token: 0x0400065F RID: 1631
	private static DialogueOptions instance;

	// Token: 0x04000660 RID: 1632
	public int selectedOption = -1;

	// Token: 0x04000661 RID: 1633
	private int optionCount;

	// Token: 0x04000662 RID: 1634
	public DialogueBox[] optionBoxes;

	// Token: 0x04000663 RID: 1635
	public Button[] optionButtons;

	// Token: 0x04000664 RID: 1636
	public RectTransform selector;

	// Token: 0x04000665 RID: 1637
	public Color selectedColor;

	// Token: 0x04000666 RID: 1638
	public Color unselectedColor;

	// Token: 0x04000667 RID: 1639
	private Vector2 selectorPosition;

	// Token: 0x04000668 RID: 1640
	public UIButtonPrompt buttonPrompt;

	// Token: 0x04000669 RID: 1641
	private bool isTriggered;

	// Token: 0x0400066A RID: 1642
	private WaitUntil waitUntilTriggered;
}
