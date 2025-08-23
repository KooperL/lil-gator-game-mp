using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingToggle : MonoBehaviour
{
	// Token: 0x06001150 RID: 4432 RVA: 0x0000ED15 File Offset: 0x0000CF15
	private void OnValidate()
	{
		if (this.toggle == null)
		{
			this.toggle = base.GetComponent<Toggle>();
		}
	}

	// Token: 0x06001151 RID: 4433 RVA: 0x0000ED31 File Offset: 0x0000CF31
	private void OnEnable()
	{
		this.toggle.isOn = Settings.s.ReadBool(this.key, this.toggle.isOn);
		this.setInitialSetting = true;
	}

	// Token: 0x06001152 RID: 4434 RVA: 0x0000ED60 File Offset: 0x0000CF60
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
