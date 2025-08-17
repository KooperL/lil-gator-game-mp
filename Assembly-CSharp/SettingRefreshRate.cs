using System;
using UnityEngine;

public class SettingRefreshRate : MonoBehaviour
{
	// Token: 0x06001143 RID: 4419 RVA: 0x0000EC62 File Offset: 0x0000CE62
	private void OnValidate()
	{
		if (this.selectOptions != null)
		{
			this.selectOptions = base.GetComponent<SelectOptions>();
		}
	}

	// Token: 0x06001144 RID: 4420 RVA: 0x00057D50 File Offset: 0x00055F50
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

	// Token: 0x06001145 RID: 4421 RVA: 0x00057E20 File Offset: 0x00056020
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
