using System;
using UnityEngine;

// Token: 0x02000060 RID: 96
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

	// Token: 0x0400020F RID: 527
	public bool hide;
}
