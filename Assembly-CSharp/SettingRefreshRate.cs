using System;
using UnityEngine;

// Token: 0x02000371 RID: 881
public class SettingRefreshRate : MonoBehaviour
{
	// Token: 0x060010E3 RID: 4323 RVA: 0x0000E879 File Offset: 0x0000CA79
	private void OnValidate()
	{
		if (this.selectOptions != null)
		{
			this.selectOptions = base.GetComponent<SelectOptions>();
		}
	}

	// Token: 0x060010E4 RID: 4324 RVA: 0x00055D90 File Offset: 0x00053F90
	private void OnEnable()
	{
		this.selectOptions.options = new string[Settings.refreshRates.Length];
		for (int i = 0; i < this.selectOptions.options.Length; i++)
		{
			this.selectOptions.options[i] = string.Format("{0}fps", Settings.refreshRates[i]);
		}
		int currentResolutionIndex = Settings.CurrentResolutionIndex;
		Vector2Int vector2Int;
		vector2Int..ctor(Settings.s.ReadInt("ResolutionX", Screen.currentResolution.width), Settings.s.ReadInt("ResolutionY", Screen.currentResolution.height));
		int currentResolutionIndex2;
		if (!Settings.pixelResolutions.TryFindIndex(vector2Int, out currentResolutionIndex2))
		{
			currentResolutionIndex2 = Settings.CurrentResolutionIndex;
		}
		this.selectOptions.SetSelection(currentResolutionIndex2, true);
		this.setInitialSetting = true;
	}

	// Token: 0x060010E5 RID: 4325 RVA: 0x00055E60 File Offset: 0x00054060
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

	// Token: 0x040015E8 RID: 5608
	public SelectOptions selectOptions;

	// Token: 0x040015E9 RID: 5609
	private bool setInitialSetting;
}
