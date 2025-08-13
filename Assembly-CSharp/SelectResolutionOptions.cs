using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200036F RID: 879
public class SelectResolutionOptions : SelectOptions
{
	// Token: 0x060010DD RID: 4317 RVA: 0x00055D38 File Offset: 0x00053F38
	protected override void OnEnable()
	{
		this.options = new string[Screen.resolutions.Length];
		for (int i = 0; i < this.options.Length; i++)
		{
			this.options[i] = Screen.resolutions[i].ToString();
		}
		base.OnEnable();
	}

	// Token: 0x040015E3 RID: 5603
	private Resolution[] resolutions;

	// Token: 0x040015E4 RID: 5604
	public UnityEvent<int, int> onChangeResolution;
}
