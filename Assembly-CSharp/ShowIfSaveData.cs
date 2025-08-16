using System;
using UnityEngine;

public class ShowIfSaveData : MonoBehaviour
{
	// Token: 0x06001153 RID: 4435 RVA: 0x0000ED71 File Offset: 0x0000CF71
	private void OnEnable()
	{
		if (!FileUtil.HasInitializedSaveData())
		{
			base.enabled = false;
		}
	}

	public bool invert;
}
