using System;
using UnityEngine;

public class SettingOptions : MonoBehaviour
{
	// Token: 0x0600113F RID: 4415 RVA: 0x0000EBE5 File Offset: 0x0000CDE5
	private void OnValidate()
	{
		if (this.selectOptions != null)
		{
			this.selectOptions = base.GetComponent<SelectOptions>();
		}
	}

	// Token: 0x06001140 RID: 4416 RVA: 0x0000EC01 File Offset: 0x0000CE01
	private void OnEnable()
	{
		this.selectOptions.SetSelection(Settings.s.ReadInt(this.key, 0), true);
		this.setInitialSetting = true;
	}

	// Token: 0x06001141 RID: 4417 RVA: 0x0000EC27 File Offset: 0x0000CE27
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
