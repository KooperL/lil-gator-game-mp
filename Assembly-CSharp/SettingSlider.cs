using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingSlider : MonoBehaviour
{
	// Token: 0x0600114B RID: 4427 RVA: 0x0000ECA4 File Offset: 0x0000CEA4
	private void OnValidate()
	{
		if (this.slider == null)
		{
			this.slider = base.GetComponent<Slider>();
		}
	}

	// Token: 0x0600114C RID: 4428 RVA: 0x0000ECC0 File Offset: 0x0000CEC0
	private void OnEnable()
	{
		this.slider.value = Settings.s.ReadFloat(this.key, this.slider.value);
		this.setInitialSetting = true;
	}

	// Token: 0x0600114D RID: 4429 RVA: 0x0000ECEF File Offset: 0x0000CEEF
	public void OnSliderChange(float value)
	{
		if (!this.setInitialSetting)
		{
			return;
		}
		Settings.s.Write(this.key, value);
		Settings.s.LoadSettings();
	}

	public string key;

	public Slider slider;

	private bool setInitialSetting;
}
