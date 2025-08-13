using System;
using UnityEngine;

// Token: 0x0200007C RID: 124
public class UICheckForSave : MonoBehaviour
{
	// Token: 0x06000196 RID: 406 RVA: 0x0001CA3C File Offset: 0x0001AC3C
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

	// Token: 0x0400027D RID: 637
	public bool hide;
}
