using System;
using UnityEngine;

public class UICheckForSave : MonoBehaviour
{
	// Token: 0x06000168 RID: 360 RVA: 0x00008708 File Offset: 0x00006908
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
