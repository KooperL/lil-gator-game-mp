using System;
using UnityEngine;

public class ShowIfSaveData : MonoBehaviour
{
	// Token: 0x06001153 RID: 4435 RVA: 0x0000ED86 File Offset: 0x0000CF86
	private void OnEnable()
	{
		if (!FileUtil.HasInitializedSaveData())
		{
			base.enabled = false;
		}
	}

	public bool invert;
}
