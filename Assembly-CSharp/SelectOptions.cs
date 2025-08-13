using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200036E RID: 878
public class SelectOptions : Selectable
{
	// Token: 0x060010D6 RID: 4310 RVA: 0x0000E742 File Offset: 0x0000C942
	protected override void OnEnable()
	{
		base.OnEnable();
		this.UpdateDisplay();
	}

	// Token: 0x060010D7 RID: 4311 RVA: 0x00055C6C File Offset: 0x00053E6C
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

	// Token: 0x060010D8 RID: 4312 RVA: 0x0000E750 File Offset: 0x0000C950
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

	// Token: 0x060010D9 RID: 4313 RVA: 0x0000E78E File Offset: 0x0000C98E
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

	// Token: 0x060010DA RID: 4314 RVA: 0x00055CAC File Offset: 0x00053EAC
	private void UpdateDisplay()
	{
		if (this.selectedOptionDisplay == null || this.options.Length == 0)
		{
			return;
		}
		this.selectedOption = Mathf.Clamp(this.selectedOption, 0, this.options.Length - 1);
		if (this.document == null)
		{
			this.selectedOptionDisplay.text = this.options[this.selectedOption];
			return;
		}
		this.selectedOptionDisplay.text = this.document.FetchString(this.options[this.selectedOption], Language.English);
	}

	// Token: 0x060010DB RID: 4315 RVA: 0x0000E7CE File Offset: 0x0000C9CE
	public void SetSelection(int newSelection, bool doCallback = true)
	{
		this.selectedOption = Mathf.Clamp(newSelection, 0, this.options.Length - 1);
		if (doCallback)
		{
			this.onSelectionChange.Invoke(this.selectedOption);
		}
		this.UpdateDisplay();
	}

	// Token: 0x040015DE RID: 5598
	[Header("Select Options")]
	public MultilingualTextDocument document;

	// Token: 0x040015DF RID: 5599
	[TextLookup("document")]
	public string[] options;

	// Token: 0x040015E0 RID: 5600
	public int selectedOption;

	// Token: 0x040015E1 RID: 5601
	public Text selectedOptionDisplay;

	// Token: 0x040015E2 RID: 5602
	public UnityEvent<int> onSelectionChange;
}
