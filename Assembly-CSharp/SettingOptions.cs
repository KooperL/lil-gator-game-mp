using System;
using UnityEngine;

public class SettingOptions : MonoBehaviour
{
	// Token: 0x06000E1B RID: 3611 RVA: 0x0004406A File Offset: 0x0004226A
	private void OnValidate()
	{
		if (this.selectOptions != null)
		{
			this.selectOptions = base.GetComponent<SelectOptions>();
		}
	}

	// Token: 0x06000E1C RID: 3612 RVA: 0x00044086 File Offset: 0x00042286
	private void OnEnable()
	{
		this.selectOptions.SetSelection(Settings.s.ReadInt(this.key, 0), true);
		this.setInitialSetting = true;
	}

	// Token: 0x06000E1D RID: 3613 RVA: 0x000440AC File Offset: 0x000422AC
	public void OnSelectionChange(int selectedOption)
	{
		if (!this.setInitialSetting)
		{
			return;
		}
		Settings.s.Write(this.key, selectedOption);
		Settings.s.LoadSettings();
	}

	public string key;

	public SelectOptions selectOptions;

	private bool setInitialSetting;
}
