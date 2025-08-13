using System;
using UnityEngine;

// Token: 0x02000370 RID: 880
public class SettingOptions : MonoBehaviour
{
	// Token: 0x060010DF RID: 4319 RVA: 0x0000E811 File Offset: 0x0000CA11
	private void OnValidate()
	{
		if (this.selectOptions != null)
		{
			this.selectOptions = base.GetComponent<SelectOptions>();
		}
	}

	// Token: 0x060010E0 RID: 4320 RVA: 0x0000E82D File Offset: 0x0000CA2D
	private void OnEnable()
	{
		this.selectOptions.SetSelection(Settings.s.ReadInt(this.key, 0), true);
		this.setInitialSetting = true;
	}

	// Token: 0x060010E1 RID: 4321 RVA: 0x0000E853 File Offset: 0x0000CA53
	public void OnSelectionChange(int selectedOption)
	{
		if (!this.setInitialSetting)
		{
			return;
		}
		Settings.s.Write(this.key, selectedOption);
		Settings.s.LoadSettings();
	}

	// Token: 0x040015E5 RID: 5605
	public string key;

	// Token: 0x040015E6 RID: 5606
	public SelectOptions selectOptions;

	// Token: 0x040015E7 RID: 5607
	private bool setInitialSetting;
}
