using System;
using UnityEngine;
using UnityEngine.Events;

public class SelectResolutionOptions : SelectOptions
{
	// Token: 0x06000E19 RID: 3609 RVA: 0x0004400C File Offset: 0x0004220C
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
