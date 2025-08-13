using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000373 RID: 883
public class SettingSlider : MonoBehaviour
{
	// Token: 0x060010EB RID: 4331 RVA: 0x0000E8B1 File Offset: 0x0000CAB1
	private void OnValidate()
	{
		if (this.slider == null)
		{
			this.slider = base.GetComponent<Slider>();
		}
	}

	// Token: 0x060010EC RID: 4332 RVA: 0x0000E8CD File Offset: 0x0000CACD
	private void OnEnable()
	{
		this.slider.value = Settings.s.ReadFloat(this.key, this.slider.value);
		this.setInitialSetting = true;
	}

	// Token: 0x060010ED RID: 4333 RVA: 0x0000E8FC File Offset: 0x0000CAFC
	public void OnSliderChange(float value)
	{
		if (!this.setInitialSetting)
		{
			return;
		}
		Settings.s.Write(this.key, value);
		Settings.s.LoadSettings();
	}

	// Token: 0x040015EC RID: 5612
	public string key;

	// Token: 0x040015ED RID: 5613
	public Slider slider;

	// Token: 0x040015EE RID: 5614
	private bool setInitialSetting;
}
