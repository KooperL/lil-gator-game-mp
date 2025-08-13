using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000374 RID: 884
public class SettingToggle : MonoBehaviour
{
	// Token: 0x060010EF RID: 4335 RVA: 0x0000E922 File Offset: 0x0000CB22
	private void OnValidate()
	{
		if (this.toggle == null)
		{
			this.toggle = base.GetComponent<Toggle>();
		}
	}

	// Token: 0x060010F0 RID: 4336 RVA: 0x0000E93E File Offset: 0x0000CB3E
	private void OnEnable()
	{
		this.toggle.isOn = Settings.s.ReadBool(this.key, this.toggle.isOn);
		this.setInitialSetting = true;
	}

	// Token: 0x060010F1 RID: 4337 RVA: 0x0000E96D File Offset: 0x0000CB6D
	public void OnToggle(bool isOn)
	{
		if (!this.setInitialSetting)
		{
			return;
		}
		Settings.s.Write(this.key, this.toggle.isOn);
		Settings.s.LoadSettings();
	}

	// Token: 0x040015EF RID: 5615
	public string key;

	// Token: 0x040015F0 RID: 5616
	public Toggle toggle;

	// Token: 0x040015F1 RID: 5617
	private bool setInitialSetting;
}
