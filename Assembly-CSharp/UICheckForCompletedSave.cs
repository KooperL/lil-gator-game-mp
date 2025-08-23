using System;
using UnityEngine;

public class UICheckForCompletedSave : MonoBehaviour
{
	// Token: 0x0600019F RID: 415 RVA: 0x0001D44C File Offset: 0x0001B64C
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
