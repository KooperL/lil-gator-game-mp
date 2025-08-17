using System;
using UnityEngine;

public class UICheckForSave : MonoBehaviour
{
	// Token: 0x060001A1 RID: 417 RVA: 0x0001D47C File Offset: 0x0001B67C
	private void Awake()
	{
		bool flag = FileUtil.HasInitializedSaveData();
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
