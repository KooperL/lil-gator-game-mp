using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectOptions : Selectable
{
	// Token: 0x06001132 RID: 4402 RVA: 0x00057A78 File Offset: 0x00055C78
	public static void ForceRefresh()
	{
		foreach (SelectOptions selectOptions in SelectOptions.active)
		{
			selectOptions.Refresh();
		}
	}

	// Token: 0x06001133 RID: 4403 RVA: 0x0000EAAF File Offset: 0x0000CCAF
	protected override void OnEnable()
	{
		base.OnEnable();
		this.UpdateDisplay();
		SelectOptions.active.Add(this);
	}

	// Token: 0x06001134 RID: 4404 RVA: 0x0000EAC8 File Offset: 0x0000CCC8
	protected override void OnDisable()
	{
		base.OnDisable();
		SelectOptions.active.Remove(this);
	}

	// Token: 0x06001135 RID: 4405 RVA: 0x00057AC8 File Offset: 0x00055CC8
	public override void OnMove(AxisEventData eventData)
	{
		switch (eventData.moveDir)
		{
		case MoveDirection.Left:
			this.MoveLeft();
			return;
		case MoveDirection.Up:
		case MoveDirection.Down:
			base.OnMove(eventData);
			return;
		case MoveDirection.Right:
			this.MoveRight();
			return;
		default:
			return;
		}
	}

	// Token: 0x06001136 RID: 4406 RVA: 0x0000EADC File Offset: 0x0000CCDC
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

	// Token: 0x06001137 RID: 4407 RVA: 0x0000EB1A File Offset: 0x0000CD1A
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

	// Token: 0x06001138 RID: 4408 RVA: 0x0000EB5A File Offset: 0x0000CD5A
	private void UpdateDisplay()
	{
		if (this.selectedOptionDisplay == null || this.options.Length == 0)
		{
			return;
		}
		this.selectedOption = Mathf.Clamp(this.selectedOption, 0, this.options.Length - 1);
		this.Refresh();
	}

	// Token: 0x06001139 RID: 4409 RVA: 0x00057B08 File Offset: 0x00055D08
	public void Refresh()
	{
		if (this.document == null)
		{
			this.selectedOptionDisplay.text = this.options[this.selectedOption];
			return;
		}
		this.selectedOptionDisplay.text = this.document.FetchString(this.options[this.selectedOption], Language.Auto);
	}

	// Token: 0x0600113A RID: 4410 RVA: 0x0000EB96 File Offset: 0x0000CD96
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
