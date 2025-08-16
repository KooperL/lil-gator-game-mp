using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingToggle : MonoBehaviour
{
	// Token: 0x0600114F RID: 4431 RVA: 0x0000ECF6 File Offset: 0x0000CEF6
	private void OnValidate()
	{
		if (this.toggle == null)
		{
			this.toggle = base.GetComponent<Toggle>();
		}
	}

	// Token: 0x06001150 RID: 4432 RVA: 0x0000ED12 File Offset: 0x0000CF12
	private void OnEnable()
	{
		this.toggle.isOn = Settings.s.ReadBool(this.key, this.toggle.isOn);
		this.setInitialSetting = true;
	}

	// Token: 0x06001151 RID: 4433 RVA: 0x0000ED41 File Offset: 0x0000CF41
	public void OnToggle(bool isOn)
	{
		if (!this.setInitialSetting)
		{
			return;
		}
		Settings.s.Write(this.key, this.toggle.isOn);
		Settings.s.LoadSettings();
	}

	public string key;

	public Toggle toggle;

	private bool setInitialSetting;
}
