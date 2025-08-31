using System;
using UnityEngine;

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

	public bool invert;
}
