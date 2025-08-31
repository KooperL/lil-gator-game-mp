using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectOptions : Selectable
{
	// Token: 0x06000E0E RID: 3598 RVA: 0x00043DF0 File Offset: 0x00041FF0
	public static void ForceRefresh()
	{
		foreach (SelectOptions selectOptions in SelectOptions.active)
		{
			selectOptions.Refresh();
		}
	}

	// Token: 0x06000E0F RID: 3599 RVA: 0x00043E40 File Offset: 0x00042040
	protected override void OnEnable()
	{
		base.OnEnable();
		this.UpdateDisplay();
		SelectOptions.active.Add(this);
	}

	// Token: 0x06000E10 RID: 3600 RVA: 0x00043E59 File Offset: 0x00042059
	protected override void OnDisable()
	{
		base.OnDisable();
		SelectOptions.active.Remove(this);
	}

	// Token: 0x06000E11 RID: 3601 RVA: 0x00043E70 File Offset: 0x00042070
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

	// Token: 0x06000E12 RID: 3602 RVA: 0x00043EB0 File Offset: 0x000420B0
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

	// Token: 0x06000E13 RID: 3603 RVA: 0x00043EEE File Offset: 0x000420EE
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

	// Token: 0x06000E14 RID: 3604 RVA: 0x00043F2E File Offset: 0x0004212E
	private void UpdateDisplay()
	{
		if (this.selectedOptionDisplay == null || this.options.Length == 0)
		{
			return;
		}
		this.selectedOption = Mathf.Clamp(this.selectedOption, 0, this.options.Length - 1);
		this.Refresh();
	}

	// Token: 0x06000E15 RID: 3605 RVA: 0x00043F6C File Offset: 0x0004216C
	public void Refresh()
	{
		if (this.document == null)
		{
			this.selectedOptionDisplay.text = this.options[this.selectedOption];
			return;
		}
		this.selectedOptionDisplay.text = this.document.FetchString(this.options[this.selectedOption], Language.Auto);
	}

	// Token: 0x06000E16 RID: 3606 RVA: 0x00043FC5 File Offset: 0x000421C5
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
