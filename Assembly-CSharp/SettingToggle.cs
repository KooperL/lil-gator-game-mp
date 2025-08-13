using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200029A RID: 666
public class SettingToggle : MonoBehaviour
{
	// Token: 0x06000E2B RID: 3627 RVA: 0x0004441F File Offset: 0x0004261F
	private void OnValidate()
	{
		if (this.toggle == null)
		{
			this.toggle = base.GetComponent<Toggle>();
		}
	}

	// Token: 0x06000E2C RID: 3628 RVA: 0x0004443B File Offset: 0x0004263B
	private void OnEnable()
	{
		this.toggle.isOn = Settings.s.ReadBool(this.key, this.toggle.isOn);
		this.setInitialSetting = true;
	}

	// Token: 0x06000E2D RID: 3629 RVA: 0x0004446A File Offset: 0x0004266A
	public void OnToggle(bool isOn)
	{
		if (!this.setInitialSetting)
		{
			return;
		}
		Settings.s.Write(this.key, this.toggle.isOn);
		Settings.s.LoadSettings();
	}

	// Token: 0x04001292 RID: 4754
	public string key;

	// Token: 0x04001293 RID: 4755
	public Toggle toggle;

	// Token: 0x04001294 RID: 4756
	private bool setInitialSetting;
}
