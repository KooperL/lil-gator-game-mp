using System;
using UnityEngine;

// Token: 0x0200029B RID: 667
public class ShowIfSaveData : MonoBehaviour
{
	// Token: 0x06000E2F RID: 3631 RVA: 0x000444A2 File Offset: 0x000426A2
	private void OnEnable()
	{
		if (!FileUtil.HasInitializedSaveData())
		{
			base.enabled = false;
		}
	}

	// Token: 0x04001295 RID: 4757
	public bool invert;
}
