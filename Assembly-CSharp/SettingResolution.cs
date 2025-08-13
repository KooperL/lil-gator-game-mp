using System;
using UnityEngine;

// Token: 0x02000372 RID: 882
public class SettingResolution : MonoBehaviour
{
	// Token: 0x060010E7 RID: 4327 RVA: 0x0000E895 File Offset: 0x0000CA95
	private void OnValidate()
	{
		if (this.selectOptions != null)
		{
			this.selectOptions = base.GetComponent<SelectOptions>();
		}
	}

	// Token: 0x060010E8 RID: 4328 RVA: 0x00055EC0 File Offset: 0x000540C0
	private void OnEnable()
	{
		this.selectOptions.options = new string[Settings.pixelResolutions.Length];
		for (int i = 0; i < this.selectOptions.options.Length; i++)
		{
			this.selectOptions.options[i] = string.Format("{0}x{1}", Settings.pixelResolutions[i].x, Settings.pixelResolutions[i].y);
		}
		Vector2Int vector2Int;
		vector2Int..ctor(Settings.s.ReadInt("ResolutionX", Screen.currentResolution.width), Settings.s.ReadInt("ResolutionY", Screen.currentResolution.height));
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

	// Token: 0x060010E9 RID: 4329 RVA: 0x00055FB4 File Offset: 0x000541B4
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

	// Token: 0x040015EA RID: 5610
	public SelectOptions selectOptions;

	// Token: 0x040015EB RID: 5611
	private bool setInitialSetting;
}
