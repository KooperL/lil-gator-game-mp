using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingSlider : MonoBehaviour
{
	// Token: 0x0600114B RID: 4427 RVA: 0x0000EC9A File Offset: 0x0000CE9A
	private void OnValidate()
	{
		if (this.slider == null)
		{
			this.slider = base.GetComponent<Slider>();
		}
	}

	// Token: 0x0600114C RID: 4428 RVA: 0x0000ECB6 File Offset: 0x0000CEB6
	private void OnEnable()
	{
		this.slider.value = Settings.s.ReadFloat(this.key, this.slider.value);
		this.setInitialSetting = true;
	}

	// Token: 0x0600114D RID: 4429 RVA: 0x0000ECE5 File Offset: 0x0000CEE5
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
