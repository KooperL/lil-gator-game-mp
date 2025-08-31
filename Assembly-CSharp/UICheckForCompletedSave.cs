using System;
using UnityEngine;

public class UICheckForCompletedSave : MonoBehaviour
{
	// Token: 0x06000166 RID: 358 RVA: 0x000086BC File Offset: 0x000068BC
	private void OnEnable()
	{
		bool flag = FileUtil.HasCompletedSaveData();
		if (this.hide && flag)
		{
			base.gameObject.SetActive(false);
		}
		if (!this.hide && !flag)
		{
			base.gameObject.SetActive(false);
		}
	}

	public bool hide;
}
