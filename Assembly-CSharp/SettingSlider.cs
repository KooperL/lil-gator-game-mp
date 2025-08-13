using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000299 RID: 665
public class SettingSlider : MonoBehaviour
{
	// Token: 0x06000E27 RID: 3623 RVA: 0x000443A6 File Offset: 0x000425A6
	private void OnValidate()
	{
		if (this.slider == null)
		{
			this.slider = base.GetComponent<Slider>();
		}
	}

	// Token: 0x06000E28 RID: 3624 RVA: 0x000443C2 File Offset: 0x000425C2
	private void OnEnable()
	{
		this.slider.value = Settings.s.ReadFloat(this.key, this.slider.value);
		this.setInitialSetting = true;
	}

	// Token: 0x06000E29 RID: 3625 RVA: 0x000443F1 File Offset: 0x000425F1
	public void OnSliderChange(float value)
	{
		if (!this.setInitialSetting)
		{
			return;
		}
		Settings.s.Write(this.key, value);
		Settings.s.LoadSettings();
	}

	// Token: 0x0400128F RID: 4751
	public string key;

	// Token: 0x04001290 RID: 4752
	public Slider slider;

	// Token: 0x04001291 RID: 4753
	private bool setInitialSetting;
}
