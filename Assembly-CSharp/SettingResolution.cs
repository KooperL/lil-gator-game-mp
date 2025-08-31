using System;
using UnityEngine;

public class SettingResolution : MonoBehaviour
{
	// Token: 0x06000E23 RID: 3619 RVA: 0x0004422E File Offset: 0x0004242E
	private void OnValidate()
	{
		if (this.selectOptions != null)
		{
			this.selectOptions = base.GetComponent<SelectOptions>();
		}
	}

	// Token: 0x06000E24 RID: 3620 RVA: 0x0004424C File Offset: 0x0004244C
	private void OnEnable()
	{
		this.selectOptions.options = new string[Settings.pixelResolutions.Length];
		for (int i = 0; i < this.selectOptions.options.Length; i++)
		{
			this.selectOptions.options[i] = string.Format("{0}x{1}", Settings.pixelResolutions[i].x, Settings.pixelResolutions[i].y);
		}
		Vector2Int vector2Int = new Vector2Int(Settings.s.ReadInt("ResolutionX", Screen.currentResolution.width), Settings.s.ReadInt("ResolutionY", Screen.currentResolution.height));
		int num;
		if (!Settings.pixelResolutions.TryFindIndex(vector2Int, out num))
		{
			num = Settings.CurrentResolutionIndex;
		}
		if (num == -1)
		{
			num = Settings.pixelResolutions.Length - 1;
		}
		this.selectOptions.SetSelection(num, true);
		this.setInitialSetting = true;
	}

	// Token: 0x06000E25 RID: 3621 RVA: 0x00044340 File Offset: 0x00042540
	public void OnSelectionChange(int selectedOption)
	{
		if (!this.setInitialSetting)
		{
			return;
		}
		Settings.s.Write("ResolutionX", Settings.pixelResolutions[selectedOption].x);
		Settings.s.Write("ResolutionY", Settings.pixelResolutions[selectedOption].y);
		Settings.s.LoadSettings();
	}

	public SelectOptions selectOptions;

	private bool setInitialSetting;
}
