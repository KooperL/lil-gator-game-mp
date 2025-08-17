using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectOptions : Selectable
{
	// Token: 0x06001132 RID: 4402 RVA: 0x00057C0C File Offset: 0x00055E0C
	public static void ForceRefresh()
	{
		foreach (SelectOptions selectOptions in SelectOptions.active)
		{
			selectOptions.Refresh();
		}
	}

	// Token: 0x06001133 RID: 4403 RVA: 0x0000EAC4 File Offset: 0x0000CCC4
	protected override void OnEnable()
	{
		base.OnEnable();
		this.UpdateDisplay();
		SelectOptions.active.Add(this);
	}

	// Token: 0x06001134 RID: 4404 RVA: 0x0000EADD File Offset: 0x0000CCDD
	protected override void OnDisable()
	{
		base.OnDisable();
		SelectOptions.active.Remove(this);
	}

	// Token: 0x06001135 RID: 4405 RVA: 0x00057C5C File Offset: 0x00055E5C
	public override void OnMove(AxisEventData eventData)
	{
		switch (eventData.moveDir)
		{
		case 0:
			this.MoveLeft();
			return;
		case 1:
		case 3:
			base.OnMove(eventData);
			return;
		case 2:
			this.MoveRight();
			return;
		default:
			return;
		}
	}

	// Token: 0x06001136 RID: 4406 RVA: 0x0000EAF1 File Offset: 0x0000CCF1
	public void MoveRight()
	{
		this.selectedOption++;
		if (this.selectedOption >= this.options.Length)
		{
			this.selectedOption = 0;
		}
		this.onSelectionChange.Invoke(this.selectedOption);
		this.UpdateDisplay();
	}

	// Token: 0x06001137 RID: 4407 RVA: 0x0000EB2F File Offset: 0x0000CD2F
	public void MoveLeft()
	{
		this.selectedOption--;
		if (this.selectedOption < 0)
		{
			this.selectedOption = this.options.Length - 1;
		}
		this.onSelectionChange.Invoke(this.selectedOption);
		this.UpdateDisplay();
	}

	// Token: 0x06001138 RID: 4408 RVA: 0x0000EB6F File Offset: 0x0000CD6F
	private void UpdateDisplay()
	{
		if (this.selectedOptionDisplay == null || this.options.Length == 0)
		{
			return;
		}
		this.selectedOption = Mathf.Clamp(this.selectedOption, 0, this.options.Length - 1);
		this.Refresh();
	}

	// Token: 0x06001139 RID: 4409 RVA: 0x00057C9C File Offset: 0x00055E9C
	public void Refresh()
	{
		if (this.document == null)
		{
			this.selectedOptionDisplay.text = this.options[this.selectedOption];
			return;
		}
		this.selectedOptionDisplay.text = this.document.FetchString(this.options[this.selectedOption], Language.Auto);
	}

	// Token: 0x0600113A RID: 4410 RVA: 0x0000EBAB File Offset: 0x0000CDAB
	public void SetSelection(int newSelection, bool doCallback = true)
	{
		this.selectedOption = Mathf.Clamp(newSelection, 0, this.options.Length - 1);
		if (doCallback)
		{
			this.onSelectionChange.Invoke(this.selectedOption);
		}
		this.UpdateDisplay();
	}

	public static List<SelectOptions> active = new List<SelectOptions>();

	[Header("Select Options")]
	public MultilingualTextDocument document;

	[TextLookup("document")]
	public string[] options;

	public int selectedOption;

	public Text selectedOptionDisplay;

	public UnityEvent<int> onSelectionChange;
}
