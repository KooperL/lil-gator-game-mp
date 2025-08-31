using System;
using UnityEngine;

public class SettingRefreshRate : MonoBehaviour
{
	// Token: 0x06000E1F RID: 3615 RVA: 0x000440DA File Offset: 0x000422DA
	private void OnValidate()
	{
		if (this.selectOptions != null)
		{
			this.selectOptions = base.GetComponent<SelectOptions>();
		}
	}

	// Token: 0x06000E20 RID: 3616 RVA: 0x000440F8 File Offset: 0x000422F8
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

	// Token: 0x06000E21 RID: 3617 RVA: 0x000441C8 File Offset: 0x000423C8
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
