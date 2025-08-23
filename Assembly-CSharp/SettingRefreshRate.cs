using System;
using UnityEngine;

public class SettingRefreshRate : MonoBehaviour
{
	// Token: 0x06001144 RID: 4420 RVA: 0x0000EC6C File Offset: 0x0000CE6C
	private void OnValidate()
	{
		if (this.selectOptions != null)
		{
			this.selectOptions = base.GetComponent<SelectOptions>();
		}
	}

	// Token: 0x06001145 RID: 4421 RVA: 0x00058018 File Offset: 0x00056218
	private void OnEnable()
	{
		this.selectOptions.options = new string[Settings.refreshRates.Length];
		for (int i = 0; i < this.selectOptions.options.Length; i++)
		{
			this.selectOptions.options[i] = string.Format("{0}fps", Settings.refreshRates[i]);
		}
		int currentResolutionIndex = Settings.CurrentResolutionIndex;
		Vector2Int vector2Int = new Vector2Int(Settings.s.ReadInt("ResolutionX", Screen.currentResolution.width), Settings.s.ReadInt("ResolutionY", Screen.currentResolution.height));
		int currentResolutionIndex2;
		if (!Settings.pixelResolutions.TryFindIndex(vector2Int, out currentResolutionIndex2))
		{
			currentResolutionIndex2 = Settings.CurrentResolutionIndex;
		}
		this.selectOptions.SetSelection(currentResolutionIndex2, true);
		this.setInitialSetting = true;
	}

	// Token: 0x06001146 RID: 4422 RVA: 0x000580E8 File Offset: 0x000562E8
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
