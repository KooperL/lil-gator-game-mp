using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueOptions : MonoBehaviour
{
	// (get) Token: 0x060004A9 RID: 1193 RVA: 0x00005680 File Offset: 0x00003880
	protected static DialogueOptions Instance
	{
		get
		{
			if (DialogueOptions.instance == null)
			{
				DialogueOptions.instance = global::UnityEngine.Object.FindObjectOfType<DialogueOptions>(true);
			}
			return DialogueOptions.instance;
		}
	}

	// (get) Token: 0x060004AA RID: 1194 RVA: 0x0000569F File Offset: 0x0000389F
	public static int CurrentlySelectedIndex
	{
		get
		{
			return DialogueOptions.Instance.selectedOption;
		}
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x0002BDBC File Offset: 0x00029FBC
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

	// Token: 0x060004AC RID: 1196 RVA: 0x000056AB File Offset: 0x000038AB
	public void Clear()
	{
		this.selectorPosition = this.optionBoxes[0].rectTransform.anchoredPosition;
		base.gameObject.SetActive(false);
		this.selectedOption = -1;
		this.isTriggered = false;
	}

	// Token: 0x060004AD RID: 1197 RVA: 0x0002BF00 File Offset: 0x0002A100
	private void Update()
	{
		this.selectorPosition = Vector2.Lerp(this.selectorPosition, this.optionBoxes[this.selectedOption].rectTransform.anchoredPosition, 10f * Time.deltaTime);
		this.selector.anchoredPosition = new Vector2(Mathf.Round(this.selectorPosition.x), Mathf.Round(this.selectorPosition.y));
	}

	// Token: 0x060004AE RID: 1198 RVA: 0x0002BF70 File Offset: 0x0002A170
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

	// Token: 0x060004AF RID: 1199 RVA: 0x000056DF File Offset: 0x000038DF
	public void Submit()
	{
		this.isTriggered = true;
	}

	// Token: 0x060004B0 RID: 1200 RVA: 0x000056E8 File Offset: 0x000038E8
	public void SetSelected(int index)
	{
		this.selectedOption = index;
	}

	private static DialogueOptions instance;

	public int selectedOption = -1;

	private int optionCount;

	public DialogueBox[] optionBoxes;

	public Button[] optionButtons;

	public RectTransform selector;

	public Color selectedColor;

	public Color unselectedColor;

	private Vector2 selectorPosition;

	public UIButtonPrompt buttonPrompt;

	private bool isTriggered;

	private WaitUntil waitUntilTriggered;
}
