using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020000B2 RID: 178
public class DialogueOptions : MonoBehaviour
{
	// Token: 0x17000037 RID: 55
	// (get) Token: 0x060003D5 RID: 981 RVA: 0x000169A1 File Offset: 0x00014BA1
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

	// Token: 0x17000038 RID: 56
	// (get) Token: 0x060003D6 RID: 982 RVA: 0x000169C0 File Offset: 0x00014BC0
	public static int CurrentlySelectedIndex
	{
		get
		{
			return DialogueOptions.Instance.selectedOption;
		}
	}

	// Token: 0x060003D7 RID: 983 RVA: 0x000169CC File Offset: 0x00014BCC
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

	// Token: 0x060003D8 RID: 984 RVA: 0x00016B0E File Offset: 0x00014D0E
	public void Clear()
	{
		this.selectorPosition = this.optionBoxes[0].rectTransform.anchoredPosition;
		base.gameObject.SetActive(false);
		this.selectedOption = -1;
		this.isTriggered = false;
	}

	// Token: 0x060003D9 RID: 985 RVA: 0x00016B44 File Offset: 0x00014D44
	private void Update()
	{
		this.selectorPosition = Vector2.Lerp(this.selectorPosition, this.optionBoxes[this.selectedOption].rectTransform.anchoredPosition, 10f * Time.deltaTime);
		this.selector.anchoredPosition = new Vector2(Mathf.Round(this.selectorPosition.x), Mathf.Round(this.selectorPosition.y));
	}

	// Token: 0x060003DA RID: 986 RVA: 0x00016BB4 File Offset: 0x00014DB4
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

	// Token: 0x060003DB RID: 987 RVA: 0x00016C1A File Offset: 0x00014E1A
	public void Submit()
	{
		this.isTriggered = true;
	}

	// Token: 0x060003DC RID: 988 RVA: 0x00016C23 File Offset: 0x00014E23
	public void SetSelected(int index)
	{
		this.selectedOption = index;
	}

	// Token: 0x04000550 RID: 1360
	private static DialogueOptions instance;

	// Token: 0x04000551 RID: 1361
	public int selectedOption = -1;

	// Token: 0x04000552 RID: 1362
	private int optionCount;

	// Token: 0x04000553 RID: 1363
	public DialogueBox[] optionBoxes;

	// Token: 0x04000554 RID: 1364
	public Button[] optionButtons;

	// Token: 0x04000555 RID: 1365
	public RectTransform selector;

	// Token: 0x04000556 RID: 1366
	public Color selectedColor;

	// Token: 0x04000557 RID: 1367
	public Color unselectedColor;

	// Token: 0x04000558 RID: 1368
	private Vector2 selectorPosition;

	// Token: 0x04000559 RID: 1369
	public UIButtonPrompt buttonPrompt;

	// Token: 0x0400055A RID: 1370
	private bool isTriggered;

	// Token: 0x0400055B RID: 1371
	private WaitUntil waitUntilTriggered;
}
