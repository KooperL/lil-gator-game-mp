using System;
using UnityEngine;
using UnityEngine.Events;

public class SelectResolutionOptions : SelectOptions
{
	// Token: 0x0600113D RID: 4413 RVA: 0x00057B64 File Offset: 0x00055D64
	protected override void OnEnable()
	{
		this.options = new string[Screen.resolutions.Length];
		for (int i = 0; i < this.options.Length; i++)
		{
			this.options[i] = Screen.resolutions[i].ToString();
		}
		base.OnEnable();
	}

	private Resolution[] resolutions;

	public UnityEvent<int, int> onChangeResolution;
}
